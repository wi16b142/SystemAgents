using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AgentComServices;
using Shared;

namespace DummyGuiAgent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ServiceCommunication com = new ServiceCommunication();

        private string messageToSend;

        public string MessageToSend
        {
            get { return messageToSend; }
            set { messageToSend = value; }
        }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            com.Start();
        }

        private void SendMessage_Clicked(object sender, RoutedEventArgs e)
        {
            com.Submit(new CoreMessage() { Source = "DummyGuiAgent", Date = DateTime.Now, Data = MessageToSend });
        }
    }
}
