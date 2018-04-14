using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using Shared;
using Shared.Delegates;
using Shared.Interfaces;

namespace ProcessCollectorAgent
{
    class Worker
    {
        private Process[] processlist;
        private Timer timer;
        private StringMessageInformer informer;
        private ISubmitable submit;

        internal Process[] Processlist
        {
            get { return processlist; }
            set { processlist = value; }
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
            processlist = Process.GetProcesses();
            //Inform Console
            informer(""); 

            //Inform Adapter via MSMQ
            submit.Submit(new CoreMessage() { Source = "ProcessCollectorAgent", Date = DateTime.Now, Data = processlist.ToString() });
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void StartCollectingProccessInfo()
        {
            timer.Start();
        }

        /// <summary>
        /// Stops the Timer
        /// </summary>
        public void StopCollectingProccessInfo()
        {
            timer.Stop();
        }

        /// <summary>
        /// returns a Processlist by name
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        internal List<Process> GetProcessInformationByName(string processName)
        {
            return processlist.Where<Process>((x) => x.ProcessName.Equals(processName)).ToList<Process>();
        }

    }
}
