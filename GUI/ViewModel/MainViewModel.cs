using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GUI.Model;
using CoreLogic;
using Shared.Delegates;
using Shared;

namespace GUI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<AgentVM> agents;
        private ObservableCollection<string> messages;

        public ObservableCollection<string> Messages
        {
            get { return messages; }
            set { messages = value; RaisePropertyChanged(); }
        }


        public ObservableCollection<AgentVM> Agents
        {
            get { return agents; }
            set { agents = value; }
        }
        public MainViewModel()
        {
        
           

            if (IsInDesignMode)
            {
                //design mode
            }
            else
            {
                LogicHandler lh = new LogicHandler(new MessageInformer(NewMessageReceived));
                Messages = new ObservableCollection<string>();
            }
        }

        private void NewMessageReceived(CoreMessage message)
        {
            App.Current.Dispatcher.Invoke(()=> {
                Messages.Add(String.Format("Received message from \"{0}\" at \"{1}\"\r\n\t{2}", message.Source, message.Date.ToShortTimeString(), message.Data)); 
            });
        }

    }
}