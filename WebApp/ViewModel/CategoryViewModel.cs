using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.ViewModel
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime TimeStart { get; set; }
        public string TimeRun { get; set; }
    }
}