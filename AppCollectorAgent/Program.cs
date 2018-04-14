using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentComServices;
using Shared.Delegates;

namespace AppCollectorAgent
{
    class Program
    {
        StringMessageInformer informer;
        Worker worker;

        static void Main(string[] args)
        {
            Program prog = new Program();
            Console.WriteLine("worker running...");

            Console.WriteLine("Press Enter to stop the Worker!");
            Console.ReadLine();
            prog.worker.StopCollectingAppInfo();
        }

        public Program()
        {
            informer = new StringMessageInformer(UpdateReceived);

            Console.WriteLine("Starting App Collector Agent");
            worker = new Worker(5000, informer, new MSMQCommunication());
            worker.StartCollectingAppInfo();
        }

        /// <summary>
        /// Stop the Application
        /// </summary>
        void Stop()
        {
            worker.StartCollectingAppInfo();
        }

        /// <summary>
        /// Worker collected the Information 
        /// </summary>
        /// <param name="message"></param>
        void UpdateReceived(string message)
        {
            Console.Clear();
            Console.WriteLine("Press Enter to stop the Worker!");

            Console.WriteLine(worker.Applist.ToString() + " is too long to be displayed.");

            /*
            foreach (var item in worker.Applist)
            {
                Console.WriteLine("\t" + item["Name"].ToString() + "         \t " + item["Vendor"].ToString());
            }
            */

        }
    }
}
