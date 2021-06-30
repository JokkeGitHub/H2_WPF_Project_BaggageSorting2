using System;
using System.Windows;
using System.Windows.Media;
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

        // When the start button is clicked, we call the initialization methods
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.Visibility = Visibility.Hidden;

            ReceptionInitialization();
            SplitterInitialization();
            GateInitialization();
        }

        #region Initialization Methods
        // This thread initializes the receptionController and events from the reception
        private void ReceptionInitialization()
        {
            ReceptionController receptionController = new ReceptionController();

            receptionController.BaggageCreated1 += OnBaggageCreated1;
            receptionController.BaggageCreated2 += OnBaggageCreated2;
            receptionController.BaggageCreated3 += OnBaggageCreated3;
            receptionController.BaggageCreated4 += OnBaggageCreated4;

            receptionController.OpenOrClosedCounter1 += OnOpenOrClosedCounter1;
            receptionController.OpenOrClosedCounter2 += OnOpenOrClosedCounter2;
            receptionController.OpenOrClosedCounter3 += OnOpenOrClosedCounter3;
            receptionController.OpenOrClosedCounter4 += OnOpenOrClosedCounter4;
        }

        // This thread initializes the splitterController and events from the splitter
        private void SplitterInitialization()
        {
            SplitterController splitterController = new SplitterController();

            splitterController.BaggageArrivedInSplitter1 += OnBaggageArrivedInSplitter1;
            splitterController.BaggageArrivedInSplitter2 += OnBaggageArrivedInSplitter2;

            splitterController.BaggageLeavesSplitter1 += OnBaggageLeavesSplitter1;
            splitterController.BaggageLeavesSplitter2 += OnBaggageLeavesSplitter2;
        }

        // This thread initializes the gateController and events from the gate
        private void GateInitialization()
        {
            GateController gateController = new GateController();

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
        #endregion

        #region OnBaggageCreated Events
        // The events in this region, displays the corresponding receptions last created bag, and displays it on a label
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
        // The events in this region, displays whether or not the corresponding reception is open(green) or closed(red), and displays it on a label
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

        #region OnBaggageArrivedInSplitter Events
        // The events in this region displays the baggage id, in the corresponding splitter's label, when the baggage arrives in the splitter
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

        #region OnBaggageLeavesSplitter
        // The events in this region, resets the label of the corresponding splitter, when baggage leaves the splitter
        private void OnBaggageLeavesSplitter1(object sender, EventArgs e)
        {
            if (e is BaggageEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Splitter1.Content = "";
                }));
            }
        }
        private void OnBaggageLeavesSplitter2(object sender, EventArgs e)
        {
            if (e is BaggageEvent)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Splitter2.Content = "";
                }));
            }
        }
        #endregion

        #region OnOpenOrClosedGate Events
        // The events in this region, displays whether or not the corresponding gate is open(green) or closed(red), and displays it on a label
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

        // Maybe new events for flight numbers etc

        #region OnBaggageArrivedGate Events
        // The events in this region displays the baggage id, in the corresponding gate's label, when the baggage arrives in the gate
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
