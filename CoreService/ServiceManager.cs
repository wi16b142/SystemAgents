using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.Delegates;

namespace CoreService
{
    public class ServiceManager : IServiceManager
    {
        private MessageInformer informer;
        private  ServiceHost messageReceiverHost;
        private Uri[] serviceUrl;

        public ServiceManager(MessageInformer informer, params Uri[] serviceUrl)
        {
            this.informer = informer;
            this.serviceUrl = serviceUrl;
        }

        /// <summary>
        /// Start the Service
        /// </summary>
        public void Start()
        {
            MessageTransmissionService.Informer = informer;
            //start service here! via the servicehost class from the namespace System.ServiceModel
            messageReceiverHost = new ServiceHost(typeof(MessageTransmissionService), serviceUrl);
            messageReceiverHost.Open();
        }

        /// <summary>
        /// stops the service form listening
        /// </summary>
        public void Stop()
        {
            messageReceiverHost.Close();
        }

    }
}
