using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        //Label[] label = new Label[12];

        public MainWindow()
        {
            InitializeComponent();
            //InitializeLabelList();
        }

        /*
        void InitializeLabelList()
        {
            label[0] = ConveyorBeltLabel0;
            label[1] = ConveyorBeltLabel1;
            label[2] = ConveyorBeltLabel2;
            label[3] = ConveyorBeltLabel3;
            label[4] = ConveyorBeltLabel4;
            label[5] = ConveyorBeltLabel5;
            label[6] = ConveyorBeltLabel6;
            label[7] = ConveyorBeltLabel7;
            label[8] = ConveyorBeltLabel8;
            label[9] = ConveyorBeltLabel9;
            label[10] = ConveyorBeltLabel10;
            label[11] = ConveyorBeltLabel11;
        }*/

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.Visibility = Visibility.Hidden;

            ReceptionController receptionController = new ReceptionController();
            ConveyorBeltController conveyorBeltController = new ConveyorBeltController();
            SplitterController splitterController = new SplitterController();
            GateController gateController = new GateController();

            receptionController.BaggageCreated1 += OnBaggageCreated1;
            receptionController.BaggageCreated2 += OnBaggageCreated2;
            receptionController.BaggageCreated3 += OnBaggageCreated3;
            receptionController.BaggageCreated4 += OnBaggageCreated4;

            receptionController.OpenOrClosedCounter1 += OnOpenOrClosedCounter1;
            receptionController.OpenOrClosedCounter2 += OnOpenOrClosedCounter2;
            receptionController.OpenOrClosedCounter3 += OnOpenOrClosedCounter3;
            receptionController.OpenOrClosedCounter4 += OnOpenOrClosedCounter4;

            //conveyorBeltController.BaggageInConveyorBelt += OnBaggageInConveyorBelt;

            splitterController.BaggageArrivedInSplitter1 += OnBaggageArrivedInSplitter1;
            splitterController.BaggageArrivedInSplitter2 += OnBaggageArrivedInSplitter2;

            gateController.OpenOrClosedGate1 += OnOpenOrClosedGate1;
            gateController.OpenOrClosedGate2 += OnOpenOrClosedGate2;
            gateController.OpenOrClosedGate3 += OnOpenOrClosedGate3;

            gateController.BaggageArrivedGate1 += OnBaggageArrivedGate1;
            gateController.BaggageArrivedGate2 += OnBaggageArrivedGate2;
            gateController.BaggageArrivedGate3 += OnBaggageArrivedGate3;
            /*
            gateController.FlightPlanGate1 += OnFlightPlanGate1;
            gateController.FlightPlanGate2 += OnFlightPlanGate2;
            gateController.FlightPlanGate3 += OnFlightPlanGate3;*/
        }

        #region OnBaggageCreated Events
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

        #region OnOpenOrClosedCounter Events
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

        #region CONVEYOR BELT EVENTS ??????? NOT WORKING FIX THIS MOFUGGAH

        public void ConveyorGUITest()
        {
            ConveyorBeltController conveyorBeltController = new ConveyorBeltController();

            Baggage[] conveyorBelt = conveyorBeltController.GetConveyorBelt();

            ConveyorBeltListBox.ItemsSource = conveyorBelt;
        }

        /*
        private void OnBaggageInConveyorBelt(object sender, EventArgs e)
        {
            if (e is BaggageEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    ConveyorBeltLabel0.Content = ((BaggageEvent)e).Baggage.BaggageId;
                }));
            }
        }

        //Maybe Baggage event instead
        /*
        public void ConveyorTest()
        {
            ConveyorBeltController conveyorBeltController = new ConveyorBeltController();
            Baggage[] conveyorBelt = conveyorBeltController.GetConveyorBelt();            

            for (int i = 0; i < conveyorBelt.Length -1; i++)
            {
                if (conveyorBelt[i] == null)
                {
                    label[i].Content = "";
                }
                else
                {
                    label[i].Content = conveyorBelt[i].BaggageId;
                }
            }
        }*/
        #endregion

        #region OnBaggageArrivedInSplitter Events
        private void OnBaggageArrivedInSplitter1(object sender, EventArgs e)
        {
            if (e is BaggageEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Splitter1.Content = ((BaggageEvent)e).Baggage.BaggageId;
                }));
            }
        }
        private void OnBaggageArrivedInSplitter2(object sender, EventArgs e)
        {
            if (e is BaggageEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Splitter2.Content = ((BaggageEvent)e).Baggage.BaggageId;
                }));
            }
        }
        #endregion

        #region OnOpenOrClosedGate Events
        private void OnOpenOrClosedGate1(object sender, EventArgs e)
        {
            if (e is GateEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    switch (((GateEvent)e).Gate.Open)
                    {
                        case bool when ((GateEvent)e).Gate.Open == true:
                            Gate1OpenOrClosed.Background = new SolidColorBrush(Colors.Green);
                            DestinationGate1.Background = new SolidColorBrush(Colors.Green);
                            DestinationGate1.Content = ((GateEvent)e).Gate.Destination;
                            FligtNumber1.Content = $"Flight {((GateEvent)e).Gate.FlightNumber}";
                            break;

                        case bool when ((GateEvent)e).Gate.Open == false:
                            Gate1OpenOrClosed.Background = new SolidColorBrush(Colors.Red);
                            DestinationGate1.Background = new SolidColorBrush(Colors.Red);
                            DestinationGate1.Content = "";
                            FligtNumber1.Content = "NO PLANE";
                            Gate1.Content = "";
                            break;

                        default:
                            break;
                    }
                }));
            }
        }

        private void OnOpenOrClosedGate2(object sender, EventArgs e)
        {
            if (e is GateEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    switch (((GateEvent)e).Gate.Open)
                    {
                        case bool when ((GateEvent)e).Gate.Open == true:
                            Gate2OpenOrClosed.Background = new SolidColorBrush(Colors.Green);
                            DestinationGate2.Background = new SolidColorBrush(Colors.Green);
                            DestinationGate2.Content = ((GateEvent)e).Gate.Destination;
                            FligtNumber2.Content = $"Flight {((GateEvent)e).Gate.FlightNumber}";
                            break;

                        case bool when ((GateEvent)e).Gate.Open == false:
                            Gate2OpenOrClosed.Background = new SolidColorBrush(Colors.Red);
                            DestinationGate2.Background = new SolidColorBrush(Colors.Red);
                            DestinationGate2.Content = "";
                            FligtNumber2.Content = "NO PLANE";
                            Gate2.Content = "";
                            break;

                        default:
                            break;
                    }
                }));
            }
        }

        private void OnOpenOrClosedGate3(object sender, EventArgs e)
        {
            if (e is GateEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    switch (((GateEvent)e).Gate.Open)
                    {
                        case bool when ((GateEvent)e).Gate.Open == true:
                            Gate3OpenOrClosed.Background = new SolidColorBrush(Colors.Green);
                            DestinationGate3.Background = new SolidColorBrush(Colors.Green);
                            DestinationGate3.Content = ((GateEvent)e).Gate.Destination;
                            FligtNumber3.Content = $"Flight {((GateEvent)e).Gate.FlightNumber}";
                            break;

                        case bool when ((GateEvent)e).Gate.Open == false:
                            Gate3OpenOrClosed.Background = new SolidColorBrush(Colors.Red);
                            DestinationGate3.Background = new SolidColorBrush(Colors.Red);
                            DestinationGate3.Content = "";
                            FligtNumber3.Content = "NO PLANE";
                            Gate3.Content = "";
                            break;

                        default:
                            break;
                    }
                }));
            }
        }
        #endregion

        #region OnBaggageArrivedGate Events
        private void OnBaggageArrivedGate1(object sender, EventArgs e)
        {
            if (e is BaggageEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Gate1.Content = ((BaggageEvent)e).Baggage.BaggageId;
                }));
            }
        }
        private void OnBaggageArrivedGate2(object sender, EventArgs e)
        {
            if (e is BaggageEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Gate2.Content = ((BaggageEvent)e).Baggage.BaggageId;
                }));
            }
        }
        private void OnBaggageArrivedGate3(object sender, EventArgs e)
        {
            if (e is BaggageEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Gate3.Content = ((BaggageEvent)e).Baggage.BaggageId;
                }));
            }
        }
        #endregion
    }
}
