using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditTrail_Console.HistoryTracking
{
    public interface IHistoryTracker
    {
        string ObjectName { get; }
        IHistoryTracker ParentObject { get; }
    }
}
