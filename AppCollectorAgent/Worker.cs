using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Timers;
using Shared;
using Shared.Delegates;
using Shared.Interfaces;

namespace AppCollectorAgent
{
    class Worker
    {
        private ManagementObjectCollection applist;
        private Timer timer;
        private StringMessageInformer informer;
        private ISubmitable submit;

        internal ManagementObjectCollection Applist
        {
            get { return applist; }
            set { applist = value; }
        }

        /// <summary>
        /// updateInterval in ms!
        /// </summary>
        /// <param name="updateInterval"></param>
        public Worker(int updateInterval, StringMessageInformer informer, ISubmitable submissionType)
        {
            this.informer = informer;
            timer = new Timer(updateInterval);
            timer.Elapsed += timer_Elapsed;

            //setup message transmission
            submit = submissionType;
            submit.Start();
        }

        /// <summary>
        /// Timer elapsed => do something
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            applist = GetApps();
            //Inform Console
            informer("");

            //Inform Adapter via MSMQ
            submit.Submit(new CoreMessage() { Source = "AppCollectorAgent", Date = DateTime.Now, Data = applist.ToString() });
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void StartCollectingAppInfo()
        {
            timer.Start();
        }

        /// <summary>
        /// Stops the Timer
        /// </summary>
        public void StopCollectingAppInfo()
        {
            timer.Stop();
        }

        private ManagementObjectCollection GetApps()
        {
            return new ManagementObjectSearcher("Select * from Win32_Product").Get();
        }
    }
}
