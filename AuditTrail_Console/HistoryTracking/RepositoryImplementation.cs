using AuditTrail_Console.Entity;
using AuditTrail_Console.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AuditTrail_Console.HistoryTracking
{
    public class RepositoryImplementation 
    {
        private static IUnitOfWork _unitOfWork;
        public RepositoryImplementation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region Override Methods

        public static void LogHistoryTracking(DbEntityEntry entity)
        {
            bool isLogged;
            Guid? historyTrackingId;

            if (entity.State == EntityState.Added || (entity.State == EntityState.Modified ))
            {
                isLogged = LogHistoryTrackingForAddAndDelete(entity, out historyTrackingId);
            }
            else
            {
                isLogged = LogHistoryTrackingForUpdate(entity, out historyTrackingId);
            }

            if (isLogged)
            {
                _unitOfWork.SaveChanges();
            }
            else if (historyTrackingId.HasValue)
            {
                _unitOfWork.HistoryTrackingAuditRepository.Delete(ht => ht.Id == historyTrackingId, true);
                _unitOfWork.SaveChanges();
            }
        }

        public static void LogDynamicHistoryTracking(DbEntityEntry entity)
        {
            //if (entity.State == EntityState.Added ||
            //    (entity.State == EntityState.Modified &&
            //     entity.CurrentValues.GetValue<object>(Constant.EntityField.IsDeleted).ToString().ToLower().Equals(Constant.Common.True.ToLower())))
            //{
            //    // Do nothing...
            //}
            //else
            //{
                Guid? historyTrackingId;
                var isLogged = LogDynamicHistoryTrackingForUpdate(entity, out historyTrackingId);
                if (isLogged)
                {
                    _unitOfWork.SaveChanges();
                }
                else if (historyTrackingId.HasValue)
                {
                    _unitOfWork.HistoryTrackingAuditRepository.Delete(ht => ht.Id == historyTrackingId, true);
                    _unitOfWork.SaveChanges();
                }
            //}
        }

        #endregion

        #region Private Methods
        private static void GetAttributes(object entity, out Dictionary<string, LoggerAttribute> logger, out Dictionary<string, ForeignKeyLoggerAttribute> foreignKeyLogger)
        {
            var props = new List<PropertyInfo>(entity.GetType().GetProperties());
            logger = new Dictionary<string, LoggerAttribute>();
            foreignKeyLogger = new Dictionary<string, ForeignKeyLoggerAttribute>();
            foreach (var prop in props)
            {
                var attrs = prop.GetCustomAttributes(true);

                foreach (var attr in attrs.OfType<LoggerAttribute>())
                {
                    logger.Add(prop.Name, attr);
                }

                foreach (var attr in attrs.OfType<ForeignKeyLoggerAttribute>())
                {
                    foreignKeyLogger.Add(prop.Name, attr);
                }
            }
        }

        private static void GetAttributes(object entity, out Dictionary<string, LoggerAttribute> logger)
        {
            var props = new List<PropertyInfo>(entity.GetType().GetProperties());
            logger = new Dictionary<string, LoggerAttribute>();
            foreach (var prop in props)
            {
                var attrs = prop.GetCustomAttributes(true);

                foreach (var attr in attrs.OfType<LoggerAttribute>())
                {
                    logger.Add(prop.Name, attr);
                }
            }
        }

        private static void GetAttributes(string property, Dictionary<string, LoggerAttribute> loggerAttributes, Dictionary<string, ForeignKeyLoggerAttribute> foreignKeyLoggerAttributes, out string fieldName, out bool isIgnored, out string format, out string repositoryName, out string foreignPropertyName)
        {
            LoggerAttribute loggerAttribute;
            ForeignKeyLoggerAttribute foreignKeyLoggerAttribute;

            if (loggerAttributes.TryGetValue(property, out loggerAttribute))
            {
                fieldName = loggerAttribute.FieldName;
                isIgnored = !loggerAttribute.IsLogged;
                format = loggerAttribute.Format;
            }
            else
            {
                fieldName = property;
                isIgnored = false;
                format = string.Empty;
            }

            if (string.IsNullOrEmpty(fieldName)) fieldName = property;

            if (foreignKeyLoggerAttributes.TryGetValue(property, out foreignKeyLoggerAttribute))
            {
                repositoryName = foreignKeyLoggerAttribute.EntityName + "Repository";
                foreignPropertyName = foreignKeyLoggerAttribute.ForeignPropertyName;
            }
            else
            {
                repositoryName = null;
                foreignPropertyName = null;
            }
        }

        private static void GetAttributes(string property, Dictionary<string, LoggerAttribute> loggerAttributes, out string fieldName, out bool isIgnored, out string format)
        {
            LoggerAttribute loggerAttribute;

            if (loggerAttributes.TryGetValue(property, out loggerAttribute))
            {
                fieldName = loggerAttribute.FieldName;
                isIgnored = !loggerAttribute.IsLogged;
                format = loggerAttribute.Format;
            }
            else
            {
                fieldName = property;
                isIgnored = false;
                format = string.Empty;
            }

            if (string.IsNullOrEmpty(fieldName)) fieldName = property;
        }

        private static string GetReferenceValue(string repositoryName, string foreignPropertyName, Guid id, string format)
        {
            if (id == Guid.Empty) return null;
            try
            {
                var propertyInfo = _unitOfWork.GetType().GetProperty(repositoryName);
                if (propertyInfo != null)
                {
                    var repository = propertyInfo.GetValue(_unitOfWork, null);
                    var methodInfo = repository.GetType().GetMethod("GetById");
                    var obj = methodInfo.Invoke(repository, new object[] { id });
                    if (obj != null)
                    {
                        propertyInfo = string.IsNullOrWhiteSpace(foreignPropertyName)
                            ? obj.GetType().GetProperty("Name")
                            : obj.GetType().GetProperty(foreignPropertyName);
                        return propertyInfo != null ? string.Format(format, propertyInfo.GetValue(obj, null)) : null;
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        private static Guid? LogHistoryTracking(IHistoryTracker obj, string subObjectName)
        {
            var recordType = ObjectContext.GetObjectType(obj.GetType()).FullName;
            //var recordId = ((EntityBase)obj).Id;
            var objectName = obj.ObjectName;

            var historyTracking = new HistoryTrackingAudit
            {
                RecordId = Guid.NewGuid(),
                RecordType = recordType,
                ObjectName = objectName ?? string.Empty,
                SubObjectName = subObjectName
            };
            if (!string.IsNullOrEmpty(historyTracking.ObjectName))
            {
                _unitOfWork.HistoryTrackingAuditRepository.Add(historyTracking);
                return historyTracking.Id;
            }
            return null;
        }

        private static bool LogHistoryTrackingValue(Guid historyTrackingId, string fieldName, string oldValue, string newValue, string action)
        {
            var historyTrackingValue = new HistoryTrackingValueAudit
            {
                HistoryTrackingId = historyTrackingId,
                ColumnName = fieldName,
                OldValue = oldValue,
                NewValue = newValue,
                Action = action
            };
            _unitOfWork.HistoryTrackingValueAuditRepository.Add(historyTrackingValue);
            return true;
        }

        private static bool LogHistoryTrackingForAddAndDelete(DbEntityEntry entity, out Guid? historyTrackingId)
        {
            var obj = (IHistoryTracker)entity.Entity;
            if (obj.ParentObject != null)
            {
                historyTrackingId = LogHistoryTracking(obj.ParentObject, obj.ObjectName);
                if (!historyTrackingId.HasValue) return false;

                var type = ObjectContext.GetObjectType(entity.Entity.GetType());
                var fieldName = type.Name;
                var loggers = type.GetCustomAttributes(typeof(LoggerAttribute), false);
                if (loggers.Length > 0) fieldName = ((LoggerAttribute)loggers[0]).FieldName;
                return LogHistoryTrackingValue(historyTrackingId.Value, fieldName, null, null, entity.State == EntityState.Added ? "AddNew" : "Delete");
            }

            historyTrackingId = null;
            return false;
        }

        private static bool LogHistoryTrackingForUpdate(DbEntityEntry entity, out Guid? historyTrackingId)
        {
            var isLogged = false;
            var obj = (IHistoryTracker)entity.Entity;
            historyTrackingId = LogHistoryTracking(obj, null);
            if (!historyTrackingId.HasValue) return false;

            Dictionary<string, LoggerAttribute> loggerAttributes;
            Dictionary<string, ForeignKeyLoggerAttribute> foreignKeyLoggerAttributes;
            GetAttributes(entity.Entity, out loggerAttributes, out foreignKeyLoggerAttributes);
            if (entity == null || entity.GetDatabaseValues() == null) return false; //ignore log history tracking
            var dataInDb = entity.GetDatabaseValues().ToObject();

            foreach (var property in entity.OriginalValues.PropertyNames)
            {
                string fieldName, format, repositoryName, foreignPropertyName;
                bool isIgnored;

                GetAttributes(property, loggerAttributes, foreignKeyLoggerAttributes, out fieldName, out isIgnored, out format, out repositoryName, out foreignPropertyName);
                var original = dataInDb.GetType().GetProperty(property).GetValue(dataInDb, null);
                var current = entity.CurrentValues.GetValue<object>(property);
                format = string.IsNullOrEmpty(format) ? "{0}" : format;
                if ((original != null && !original.Equals(current)) || (original == null && current != null))
                {
                    if (!isIgnored)
                    {
                        var originalValue = original == null ? string.Empty : string.Format(format, original);
                        var currentValue = current == null ? string.Empty : string.Format(format, current);
                        if (original is bool) originalValue = (bool)original ? "Yes" : "No";
                        if (current is bool) currentValue = (bool)current ? "Yes" : "No";
                        if (originalValue != currentValue)
                        {
                            isLogged = LogHistoryTrackingValue(historyTrackingId.Value, fieldName, originalValue, currentValue, "Update");
                        }
                    }
                    if (repositoryName != null)
                    {
                        isLogged = LogHistoryTrackingForForeignKey(historyTrackingId.Value, original, current, fieldName, format, repositoryName, foreignPropertyName);
                    }
                }
            }
            return isLogged;
        }

        private static bool LogHistoryTrackingForForeignKey(Guid historyTrackingId, object original, object current, string fieldName, string format, string repositoryName, string foreignPropertyName)
        {
            try
            {
                var originalId = original == null ? Guid.Empty : new Guid(original.ToString());
                var currentId = current == null ? Guid.Empty : new Guid(current.ToString());
                var originalValue = GetReferenceValue(repositoryName, foreignPropertyName, originalId, format);
                var currentValue = GetReferenceValue(repositoryName, foreignPropertyName, currentId, format);
                return LogHistoryTrackingValue(historyTrackingId, fieldName, originalValue, currentValue, "Update");
            }
            catch
            {
                return false;
            }
        }

        private static Guid? LogDynamicHistoryTracking(IDynamicHistoryTracker obj)
        {
            var recordType = ObjectContext.GetObjectType(obj.GetType()).FullName;
            //var recordId = ((EntityBase)obj).Id;

            var historyTracking = new HistoryTrackingAudit
            {
                RecordId = Guid.NewGuid(),
                RecordType = recordType,
                ObjectName = obj.ReferenceNo,
                SubObjectName = obj.SubReferenceNo
            };

            if (!string.IsNullOrEmpty(historyTracking.ObjectName))
            {
                _unitOfWork.HistoryTrackingAuditRepository.Add(historyTracking);
                return historyTracking.Id;
            }

            return null;
        }

        private static bool LogDynamicHistoryTrackingForUpdate(DbEntityEntry entity, out Guid? historyTrackingId)
        {
            var isLogged = false;
            var obj = (IDynamicHistoryTracker)entity.Entity;
            historyTrackingId = LogDynamicHistoryTracking(obj);
            if (!historyTrackingId.HasValue) return false;

            Dictionary<string, LoggerAttribute> loggerAttributes;
            GetAttributes(entity.Entity, out loggerAttributes);
            var dataInDb = entity.GetDatabaseValues().ToObject();

            foreach (var property in entity.OriginalValues.PropertyNames)
            {
                string fieldName, format;
                bool isIgnored;

                GetAttributes(property, loggerAttributes, out fieldName, out isIgnored, out format);
                var original = dataInDb.GetType().GetProperty(property).GetValue(dataInDb, null);
                var current = entity.CurrentValues.GetValue<object>(property);
                format = string.IsNullOrEmpty(format) ? "{0}" : format;
                if ((original != null && !original.Equals(current)) || (original == null && current != null))
                {
                    if (!isIgnored)
                    {
                        isLogged = LogHistoryTrackingValue(historyTrackingId.Value,
                            obj.PropertyName,
                            original == null ? null : string.Format(format, original),
                            current == null ? null : string.Format(format, current), "Update");
                    }
                }
            }

            return isLogged;
        }

        #endregion
    }
}
