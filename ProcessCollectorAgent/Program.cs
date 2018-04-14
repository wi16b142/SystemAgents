using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentComServices;
using Shared.Delegates;

namespace ProcessCollectorAgent
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
            prog.worker.StopCollectingProccessInfo();
        }

        public Program()
        {
            informer = new StringMessageInformer(UpdateReceived);

            Console.WriteLine("Starting Process Collector Agent");
            worker = new Worker(5000, informer, new MSMQCommunication());
            worker.StartCollectingProccessInfo();
        }

        /// <summary>
        /// Stop the Application
        /// </summary>
        void Stop()
        {
            worker.StartCollectingProccessInfo();
        }

        /// <summary>
        /// Worker collected the Information 
        /// </summary>
        /// <param name="message"></param>
        void UpdateReceived(string message)
        {
            Console.Clear();
            Console.WriteLine("Press Enter to stop the Worker!");

            foreach (var item in worker.Processlist)
            {
                Console.WriteLine("\t" + item.ProcessName + "         \t " + item.VirtualMemorySize64);
            }

        }
    }
}
