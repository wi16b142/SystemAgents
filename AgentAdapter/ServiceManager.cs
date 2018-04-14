using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.Delegates;

namespace AgentAdapter
{
    public class ServiceManager : IServiceManager
    {

        private MessageInformer informer;

        public ServiceManager(MessageInformer informer)
        {
            this.informer = informer;
        }

        /// <summary>
        /// Establish all services to enable the agents to send data to the adapter.
        /// We need to provide a: MSMQ, TCP Service, Webservice (read the info.txt file)
        /// </summary>
        public void Start()
        {
            IServiceManager msmqService = new AgentAdapterMqService.ServiceManager(informer);
            
            //here you can see a benefit of components! - we can reuse the service compontent of the Core Unit! the only thing is that we change the urls of the services
            Uri[] urls = new Uri[2];
            urls[0] = new Uri("http://localhost:9080/AgentService/"); //url for HTTP service (web service)
            urls[1] = new Uri("net.tcp://localhost:9090/CoreService/"); //url for tcp service
            IServiceManager tcpService = new CoreService.ServiceManager(informer, urls);

            msmqService.Start();
            tcpService.Start();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

       
    }
}
