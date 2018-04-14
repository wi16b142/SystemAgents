using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentComServices;
using Shared.Delegates;

namespace RamCollectorAgent
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
            prog.worker.StopCollectingRamInfo();
        }

        public Program()
        {
            informer = new StringMessageInformer(UpdateReceived);

            Console.WriteLine("Starting Ram Collector Agent");
            worker = new Worker(5000, informer, new ServiceCommunication());
            //worker = new Worker(5000, informer, new MSMQCommunication());
            worker.StartCollectingRamInfo();
        }

        /// <summary>
        /// Stop the Application
        /// </summary>
        void Stop()
        {
            worker.StartCollectingRamInfo();
        }

        /// <summary>
        /// Worker collected the Information 
        /// </summary>
        /// <param name="message"></param>
        void UpdateReceived(string message)
        {
            Console.Clear();
            Console.WriteLine("Press Enter to stop the Worker!");

            foreach (var item in worker.Ramlist)
            {
                Console.WriteLine("Total Visible Memory: {0}MB", Int32.Parse(item["TotalVisibleMemorySize"].ToString()) / 1000);
                Console.WriteLine("Free Physical Memory: {0}MB", Int32.Parse(item["FreePhysicalMemory"].ToString()) / 1000);
                Console.WriteLine("Total Virtual Memory: {0}MB", Int32.Parse(item["TotalVirtualMemorySize"].ToString()) / 1000);
                Console.WriteLine("Free Virtual Memory: {0}MB", Int32.Parse(item["FreeVirtualMemory"].ToString()) / 1000);
            }
        }
    }
}
