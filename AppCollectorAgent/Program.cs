using AgentComServices;
using Shared.Delegates;
using System;

namespace AppCollectorAgent
{
    class Program
    {
        StringMessageInformer informer;
        Worker worker;

        //[STAThread]
        static void Main(string[] args)
        {
            Program prog = new Program();
            Console.WriteLine("worker running...");

            Console.WriteLine("Press Enter to stop the Worker!");
            Console.ReadLine();
            //prog.worker.StopCollectingAppInfo();
            prog.Stop();
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
            worker.StopCollectingAppInfo();
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

            //Doesn't work for some reason
            /*
            foreach (var item in worker.Applist)
            {
                Console.WriteLine("\t" + item["Name"].ToString() + "         \t " + item["Vendor"].ToString());
            }
            */

        }
    }
}
