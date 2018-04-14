using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shared;
using Shared.Delegates;

namespace AgentAdapterMqService
{
    public class ServiceManager : IServiceManager
    {

        private const string messageQueueName = @".\private$\SyCo";
        private MessageQueue messageQueue;
        private bool run = true;
        private MessageInformer informer;

        public ServiceManager(MessageInformer informer)
        {
            this.informer = informer;
        }

        public void Start()
        {
          
            CreateMSMQ();

            //connect to the messeageQueue
            messageQueue = new MessageQueue(@"FormatName:direct=os:" + messageQueueName);
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(CoreMessage) });
            
            run = true;
            
       

            ThreadPool.QueueUserWorkItem(new WaitCallback(CheckForNewMessages), null);
        }

        /// <summary>
        /// stops the service form getting data from the queue
        /// </summary>
        public void Stop()
        {
            run = false;
            Thread.Sleep(500);
        }

        /// <summary>
        /// Create msmq if it does not exist
        /// </summary>
        private void CreateMSMQ()
        {
            if(!MessageQueue.Exists(messageQueueName)){
                MessageQueue.Create(messageQueueName);
            }
        }

        /// <summary>
        /// check for new message
        /// </summary>
        /// <param name="obj"></param>
        private void CheckForNewMessages(object obj)
        {
            while (run)
            {
                Message message = messageQueue.Receive(); //get message from queue
                informer((CoreMessage)message.Body); //inform adapter logic
                
            }
        }


    }
}
