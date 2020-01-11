using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Command.Category
{
    public class UpdateCategoryCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
    }
    public class CreateCategoryCommand
    {
        public string Name { get; set; }
        public string SubName { get; set; }
    }
}