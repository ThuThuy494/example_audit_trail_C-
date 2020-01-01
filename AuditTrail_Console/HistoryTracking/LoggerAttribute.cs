using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditTrail_Console.HistoryTracking
{
    public class LoggerAttribute : Attribute
    {
        private readonly string _fieldName;
        private readonly bool _isLogged;
        private readonly string _format;


        public string FieldName
        {
            get { return _fieldName; }
        }

        public bool IsLogged
        {
            get { return _isLogged; }
        }

        public string Format
        {
            get { return _format; }
        }


        public LoggerAttribute(bool isLogged = true)
        {
            _isLogged = isLogged;
        }

        public LoggerAttribute(string fieldName, string format)
        {
            _fieldName = fieldName;
            _isLogged = true;
            _format = format;
        }

        public LoggerAttribute(string fieldName, bool isLogged = true, string format = "", bool isUserName = false)
        {
            _fieldName = fieldName;
            _isLogged = isLogged;
            _format = format;
        }

    }
}
