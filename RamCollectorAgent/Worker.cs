using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Timers;
using Shared;
using Shared.Delegates;
using Shared.Interfaces;

namespace RamCollectorAgent
{
    class Worker
    {
        private ManagementObjectCollection ramlist;
        private Timer timer;
        private StringMessageInformer informer;
        private ISubmitable submit;

        internal ManagementObjectCollection Ramlist
        {
            get { return ramlist; }
            set { ramlist = value; }
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
            ramlist = GetRam();
            //Inform Console
            informer("");

            //Inform Adapter via MSMQ
            submit.Submit(new CoreMessage() { Source = "RamCollectorAgent", Date = DateTime.Now, Data = ramlist.ToString() + ": " + ramlist.Count + " Elements" });
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void StartCollectingRamInfo()
        {
            timer.Start();
        }

        /// <summary>
        /// Stops the Timer
        /// </summary>
        public void StopCollectingRamInfo()
        {
            timer.Stop();
        }

        private ManagementObjectCollection GetRam()
        {
            return new ManagementObjectSearcher("Select * from Win32_OperatingSystem").Get();
        }
    }
}
