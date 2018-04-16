using GalaSoft.MvvmLight;
using GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModel
{
    public class AgentVM : ViewModelBase
    {
        private string source;
        private ObservableCollection<Message> message;

        public ObservableCollection<Message> Message
        {
            get { return message; }
            set { message = value; RaisePropertyChanged(); }
            
        }


        public string Source
        {
            get { return source; }
            set
            {
                source = value;
                RaisePropertyChanged();
            }
        }
    }
}
