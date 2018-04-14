using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shared;
using Shared.Delegates;


namespace CoreService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MessageReceiverService" in both code and config file together.
    internal class MessageTransmissionService : IMessageTransmissionService
    {
        public static MessageInformer Informer;

        /// <summary>
        /// Service logic 
        /// The service informs the Service Manager via a delegater
        /// this design was choosen to remove the logic from the service implementation
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool TransmitMessage(CoreMessage message)
        {
            Informer(message);
            return true;
        }
    }
}
