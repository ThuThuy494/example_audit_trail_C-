using AuditTrail_Console.Active;
using AuditTrail_Console.Run;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Z.EntityFramework.Plus;

namespace AuditTrail_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Start Example Insert Data use Entity Framework");
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            ////to do
            ////using (var context = new AuditTrailDbContext())
            ////{
            //Console.WriteLine("Time Start: " + DateTime.Now);
            //EntityFrameworkPlus.Demo_EntityFrameworkPlus();
            ////var entity = new Person()
            ////{
            ////    Id = Guid.NewGuid(),
            ////    FirstName = "Yuki",
            ////    LastName = "Cross"
            ////};
            ////var json = new JavaScriptSerializer().Serialize(entity);
            //////string json = JsonConvert.SerializeObject(entity);
            //////context.Students.Add(std);
            //////context.SaveChanges();
            ////Console.WriteLine(json);
            ////}

            ////to do
            //stopwatch.Stop();
            //TimeSpan timeSpan = stopwatch.Elapsed;
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            //    timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
            //Console.WriteLine("Run Time: " + elapsedTime);


            //using (var context = new AuditTrailDbContext())
            //{
            //    context.People.Add(new Person() { Id = Guid.NewGuid(), FirstName = "Hello", LastName = "World" }); // add

            //    var audit = new Audit();
            //    context.SaveChanges(audit);

            //    Access to all auditing information
            //   var entries = audit.Entries;

            //    Console.WriteLine("All Entry" + entries);

            //    foreach (var entry in entries)
            //    {
            //        Console.WriteLine("Properties for One Entry" + entry.Properties);
            //    }
            //}


            RunEntityFramework.ActionRunEntityFramework();
            //Console.WriteLine("===================================");
            //RunEntityFrameworkPlus.ActionRunEntityFramworkPlus();

            Console.ReadLine();
        }
    }
}
