using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentComServices.SR_AgentAdapter;
using Shared.Interfaces;

namespace AgentComServices
{
    public class ServiceCommunication : ISubmitable
    {
        MessageTransmissionServiceClient client; 

        public void Start()
        {
            client = new MessageTransmissionServiceClient();
        }

        public void Start(string serviceName)
        {
            client =  new MessageTransmissionServiceClient(serviceName);
        }

        public void Submit(Shared.CoreMessage message)
        {
            client.TransmitMessageCompleted += client_TransmitMessageCompleted;
            client.TransmitMessageAsync(message);
        }

        void client_TransmitMessageCompleted(object sender, TransmitMessageCompletedEventArgs e)
        {
            Console.WriteLine("Received" + e.Result);
            
        }


     
    }
}
