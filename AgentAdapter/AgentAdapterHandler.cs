using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentAdapter.SR_CoreComponent;
using Shared;
using Shared.Delegates;

namespace AgentAdapter
{
    public class AgentAdapterHandler
    {
        private MessageTransmissionServiceClient client;


        public AgentAdapterHandler()
        {
            //service for connection to core components
            client = new MessageTransmissionServiceClient("TcpMessageTransmissionService"); //select TCP Service connection
            client.TransmitMessageCompleted += client_TransmitMessageCompleted; //async call
       
            //Provide Services for Agents
            MessageInformer inf = new MessageInformer(NewMessageReceived);
            ServiceManager sm = new ServiceManager(inf);
            sm.Start();
        }

      

        private void NewMessageReceived(CoreMessage message)
        {
            Console.WriteLine(String.Format("Received message from \"{0}\" at \"{1}\"\r\n\t{2}",
                 message.Source, message.Date.ToShortTimeString(), message.Data));
            client.TransmitMessageAsync(message);
         
        }

        void client_TransmitMessageCompleted(object sender, TransmitMessageCompletedEventArgs e)
        {
            Console.WriteLine("\t Message transmitted to Core Component\r\n \t Server Response: " + e.Result);

        }
    }
}
