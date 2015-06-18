﻿using System;
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
        int currentPlayer = 1;
        

        Process player1;
        Process player2;

        Thread pl1;
        Thread pl2;

        public MainWindow()
        {
            InitializeComponent();

            this.Title = "Manager";
            start();
            StartListening();
            PipeServer server1 = new PipeServer("player1");
            PipeServer server2 = new PipeServer("player2");

            PipeClient client1 = new PipeClient("player1");
            PipeClient client2 = new PipeClient("player2");
        }

        private void start()
        {
            string firstPlayer, secondPlayer;
            int choice = new Random().Next(2);
                        

            if (choice == 0)
            {
                firstPlayer = "red";
                secondPlayer = "blue";
            }
            else
            {
                firstPlayer = "blue";
                secondPlayer = "red";
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
        }

        private void CreatePipe()
        {
            PipeServer server = new PipeServer("player" + currentPlayer.ToString());
        }

        private void StartListening()
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            pl1.Abort();
            pl2.Abort();
            player1.Kill();
            player2.Kill();
        }
    }
}
