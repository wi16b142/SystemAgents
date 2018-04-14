using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoreLogic;
using Shared;
using Shared.Delegates;

namespace CoreOutput
{                                                           //HINT: START AS ADMIN!
    class Program
    {
        /// <summary>
        /// Contains a very simple output to the console
        ///
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Program prog = new Program();
        }

        public Program()
        {
            Console.WriteLine("Core Component Started");
            LogicHandler lh = new LogicHandler(new MessageInformer(NewMessageReceived));

            while (true)
            {
                Thread.Sleep(100);
            }

        }
        /// <summary>
        /// Prints an output to the console after a message was received
        /// </summary>
        /// <param name="message"></param>
        private void NewMessageReceived(CoreMessage message)
        {
            Console.WriteLine(String.Format("Received message from \"{0}\" at \"{1}\"\r\n\t{2}",
                                message.Source, message.Date.ToShortTimeString(), message.Data));
        }
    }
}
