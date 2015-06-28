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
using System.Threading;
using System.Diagnostics;

namespace Hexy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>    

    public partial class MainWindow : Window
    {
        int currentPlayerInt = 1;
        string firstPlayer, secondPlayer, currentPlayer;

        Process player1;
        Process player2;

        Thread pl1;
        Thread pl2;

        PipeServer server1;
        PipeServer server2;

        public MainWindow()
        {
            InitializeComponent();

            this.Title = "Manager";
            start();
            //StartListening();
            server1 = new PipeServer("player1");
            server1.CallbackMethod += new MessageSender(StartListening);
            
            server2 = new PipeServer("player2");
            server2.CallbackMethod += new MessageSender(StartListening1);

            server1.Listener("player1");
            server2.Listener("player2");

            PipeClient client1 = new PipeClient("player1");
            PipeClient client2 = new PipeClient("player2");
        }

        private void start()
        {
            
            int choice = new Random().Next(2);
                        

            if (choice == 0)
            {
                firstPlayer = "#FF2A2A"; //Red
                secondPlayer = "#0066FF";//Blue
            }
            else
            {
                firstPlayer = "#0066FF"; //Blue
                secondPlayer = "#FF2A2A";//Red
            }            

            player1 = new Process();
            player1.StartInfo.FileName = "..\\..\\..\\HexPlayer\\obj\\Debug\\HexPlayer.exe";            
            player1.StartInfo.Arguments = firstPlayer + " 1";

            player2 = new Process();
            player2.StartInfo.FileName = "..\\..\\..\\HexPlayer\\obj\\Debug\\HexPlayer.exe";            
            player2.StartInfo.Arguments = secondPlayer + " 2";

            pl1 = new Thread(() => player1.Start());
           pl2 = new Thread(() => player2.Start());
            
            pl1.Start();
            pl2.Start();
            
            currentPlayer = firstPlayer;
        }

        private void CreatePipe()
        {
            PipeServer server = new PipeServer("player" + currentPlayerInt.ToString());
        }

        private void StartListening(string position)
        {            
            try
            {
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(new MessageSender(StartListening), position);
                }
                else
                {
                    string actualPos = position.Substring(0, 10);
                    ((System.Windows.Shapes.Ellipse)this.FindName(actualPos)).Fill = colorPicker(Int16.Parse(position.Substring(11, 1)));
                    
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private void StartListening1(string position)
        {
            try
            {
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(new MessageSender(StartListening1), position);
                }
                else
                {
                    string actualPos = position.Substring(0, 10);
                    ((System.Windows.Shapes.Ellipse)this.FindName(actualPos)).Fill = colorPicker(Int16.Parse(position.Substring(11, 1)));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private Brush colorPicker(int current)
        {
            if (current == 1)
            {
                
                return (Brush)new BrushConverter().ConvertFromString(firstPlayer);
            }
            else
            {
                
                return (Brush)new BrushConverter().ConvertFromString(secondPlayer);
            }            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            pl1.Abort();
            pl2.Abort();
            player1.Kill();
            player2.Kill();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
