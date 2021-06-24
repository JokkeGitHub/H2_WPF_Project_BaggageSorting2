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
using System.Windows.Threading;

namespace H2_WPF_Project_BaggageSorting2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            TempClass tempClass = new TempClass();

            tempClass.BaggageCreated1 += OnBaggageCreated1;
            tempClass.BaggageCreated2 += OnBaggageCreated2;
            tempClass.BaggageCreated3 += OnBaggageCreated3;
            tempClass.BaggageCreated4 += OnBaggageCreated4;

            tempClass.OpenOrClosedCounter1 += OnOpenOrClosedCounter1;
            tempClass.OpenOrClosedCounter2 += OnOpenOrClosedCounter2;
            tempClass.OpenOrClosedCounter3 += OnOpenOrClosedCounter3;
            tempClass.OpenOrClosedCounter4 += OnOpenOrClosedCounter4;
        }

        #region OnBaggageCreated Listeners
        private void OnBaggageCreated1(object sender, EventArgs e)
        {
            if (e is BaggageEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Counter1.Content = ((BaggageEvent)e).Baggage.BaggageId;
                }));
            }
        }

        private void OnBaggageCreated2(object sender, EventArgs e)
        {
            if (e is BaggageEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Counter2.Content = ((BaggageEvent)e).Baggage.BaggageId;
                }));
            }
        }

        private void OnBaggageCreated3(object sender, EventArgs e)
        {
            if (e is BaggageEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Counter3.Content = ((BaggageEvent)e).Baggage.BaggageId;
                }));
            }
        }

        private void OnBaggageCreated4(object sender, EventArgs e)
        {
            if (e is BaggageEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Counter4.Content = ((BaggageEvent)e).Baggage.BaggageId;
                }));
            }
        }
        #endregion

        #region Open/ClosedCounter Listeners
        private void OnOpenOrClosedCounter1(object sender, EventArgs e)
        {
            if (e is ReceptionEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    switch (((ReceptionEvent)e).Reception.Open)
                    {
                        case bool when ((ReceptionEvent)e).Reception.Open == true:
                            Counter1OpenOrClosed.Background = new SolidColorBrush(Colors.Green);
                            break;

                        case bool when ((ReceptionEvent)e).Reception.Open == false:
                            Counter1OpenOrClosed.Background = new SolidColorBrush(Colors.Red);
                            Counter1.Content = "";
                            break;

                        default:
                            break;
                    }
                }));
            }
        }

        private void OnOpenOrClosedCounter2(object sender, EventArgs e)
        {
            if (e is ReceptionEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    switch (((ReceptionEvent)e).Reception.Open)
                    {
                        case bool when ((ReceptionEvent)e).Reception.Open == true:
                            Counter2OpenOrClosed.Background = new SolidColorBrush(Colors.Green);
                            break;

                        case bool when ((ReceptionEvent)e).Reception.Open == false:
                            Counter2OpenOrClosed.Background = new SolidColorBrush(Colors.Red);
                            Counter2.Content = "";
                            break;

                        default:
                            break;
                    }
                }));
            }
        }

        private void OnOpenOrClosedCounter3(object sender, EventArgs e)
        {
            if (e is ReceptionEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    switch (((ReceptionEvent)e).Reception.Open)
                    {
                        case bool when ((ReceptionEvent)e).Reception.Open == true:
                            Counter3OpenOrClosed.Background = new SolidColorBrush(Colors.Green);
                            break;

                        case bool when ((ReceptionEvent)e).Reception.Open == false:
                            Counter3OpenOrClosed.Background = new SolidColorBrush(Colors.Red);
                            Counter3.Content = "";
                            break;

                        default:
                            break;
                    }
                }));
            }
        }

        private void OnOpenOrClosedCounter4(object sender, EventArgs e)
        {
            if (e is ReceptionEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    switch (((ReceptionEvent)e).Reception.Open)
                    {
                        case bool when ((ReceptionEvent)e).Reception.Open == true:
                            Counter4OpenOrClosed.Background = new SolidColorBrush(Colors.Green);
                            break;

                        case bool when ((ReceptionEvent)e).Reception.Open == false:
                            Counter4OpenOrClosed.Background = new SolidColorBrush(Colors.Red);
                            Counter4.Content = "";
                            break;

                        default:
                            break;
                    }
                }));
            }
        }
        #endregion
    }
}
