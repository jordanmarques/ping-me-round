using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using PingMyHeadRightRoundRightRound.Annotations;

namespace PingMyHeadRightRoundRightRound
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PingViewModel MyPingViewModel { get; set; }
       

        public MainWindow()
        {
            InitializeComponent();

            MyPingViewModel = new PingViewModel();
            DataContext = this;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MyPingViewModel.StartPing();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MyPingViewModel.StopPing();
        }
    }
}
