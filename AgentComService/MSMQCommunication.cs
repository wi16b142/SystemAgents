using System;
using System.Messaging;
using Shared;
using Shared.Interfaces;

namespace AgentComServices
{
    public class MSMQCommunication : ISubmitable
    {
        private const string messageQueueName = @".\private$\SyCo";
        private MessageQueue messageQueue;

        public void Start()
        {
            messageQueue = new MessageQueue(@"FormatName:direct=os:" + messageQueueName);
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(CoreMessage) });
        }

        public void Start(string messageQueueName)
        {
          messageQueue = new MessageQueue(@"FormatName:direct=os:" + messageQueueName);
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(CoreMessage) });
    }
        

        public void Submit(Shared.CoreMessage message)
        {
            messageQueue.Send(message);
        }

    }
}
