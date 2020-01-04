using AuditTrail_Console.Active;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditTrail_Console.Run
{
    public class RunEntityFrameworkPlus
    {
        public void ActionRunEntityFramworkPlus()
        {
            Console.WriteLine("Start Example Insert Data use Entity Framework Plus");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Time Start: " + DateTime.Now);
            //To do
            //EntityFrameworkPlus.Demo_EntityFrameworkPlus();
            //To do
            stopwatch.Stop();
            TimeSpan timeSpan = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
            Console.WriteLine("Run Time EF Plus: " + elapsedTime);
        }
    }
}
