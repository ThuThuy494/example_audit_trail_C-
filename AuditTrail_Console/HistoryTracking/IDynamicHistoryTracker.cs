using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditTrail_Console.HistoryTracking
{
    public interface IDynamicHistoryTracker
    {
        string ReferenceNo { get; set; }
        string SubReferenceNo { get; set; }
        string PropertyName { get; set; }
    }
}
