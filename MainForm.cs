using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using COMPortTerminal.Properties;
using System.Diagnostics;
using System.Collections.Generic;
using System.Timers;

namespace COMPortTerminal
{   

    public partial class MainForm  
    {
        private string str_rfid = "";
        private int quantity=1;
        private enum eMode { eOperational, eCancel };
        private eMode mode = eMode.eOperational;
        //stack use to delete the most previous one? Or do we not need it
        private OrderList order_list = new OrderList();
        private Stack<Order> stack = new Stack<Order>();
        private Stack<Order> temp_stack = new Stack<Order>();
        private double bill_total=0;
        private double sub_total = 0;
        private int item_total=0;
        private System.Timers.Timer t = null;

        public MainForm() 
        { 
            InitializeComponent();if (transDefaultFormMainForm == null)    transDefaultFormMainForm = this;
           
            btnOpenOrClosePort.Click += new System.EventHandler( btnOpenOrClosePort_Click );             
            btnPort.Click += new System.EventHandler( btnPort_Click );                            
            Load += new System.EventHandler(Form1_Load);                                                         
            tmrLookForPortChanges.Tick += new System.EventHandler( tmrLookForPortChanges_Tick ); 
        }             
       
        private const string ButtonTextOpenPort = "Open COM Port"; 
        private const string ButtonTextClosePort = "Close COM Port"; 
        private const string ModuleName = "COM Port Terminal"; 
        
        internal MainForm MyMainForm; 
        internal PortSettingsDialog MyPortSettingsDialog; 
        internal ComPorts UserPort1;
       
        private delegate void AccessFormMarshalDelegate( string action, string textToAdd, Color textColor );
            
        private Color colorReceive = Color.Green;
        private Color colorTransmit = Color.Black;
        //private int maximumTextBoxLength;
        private string receiveBuffer;
        private bool savedOpenPortOnStartup;
        private int userInputIndex;
        private int orderID = 0;

        private void AccessForm( string action, string formText, Color textColor ) 
        {
            switch ( action ) 
            {
                case "AppendToMonitorTextBox":
                    
                    //  Append text to the rtbMonitor textbox using the color for received data.

                    //rtbMonitor.AppendText(formText);
                    str_rfid += formText;
                    
                    if (str_rfid.Length > 13)
                    {
                        switch (mode)
                        {
                            //operational mode
                            case eMode.eOperational:
                                if (str_rfid.Contains("4B00DA17F573"))
                                {
                                   DisplayStatus("", textColor);
                                   Order new_item = new Order(Order.eSize.eSmall, quantity, orderID);
                                   order_list.Add(new_item);  
                                  // OrderListBox.SelectedIndex = OrderListBox.Items.Count - 1;
                                   // rtbMonitor.AppendText(DSSmall.getQuantity() + "\t" + DSSmall.getSizeString()
                                                          //  + "\t\t" + new_item.getPrice() + "\n");
                                   // stack.Push(DSSmall);
                                }
                                else if (str_rfid.Contains("4800E50372DC"))
                                {
                                    DisplayStatus("", textColor);
                                    Order new_item = new Order(Order.eSize.eMedium, quantity, orderID);
                                    order_list.Add(new_item);
                                    //OrderListBox.SelectedIndex = OrderListBox.Items.Count -1;
                                    //rtbMonitor.AppendText(new_item.getQuantity() + "\t" + new_item.getSizeString()
                                   //                         + "\t" + new_item.getPrice() + "\n");
                                   // stack.Push(new_item);
                                }
                                else if (str_rfid.Contains("4B00DA60DB2A"))
                                {
                                    DisplayStatus("", textColor);
                                    Order new_item = new Order(Order.eSize.eLarge, quantity, orderID);
                                    order_list.Add(new_item);
                                   // OrderListBox.SelectedIndex = OrderListBox.Items.Count - 1;
                                    //probably don't need this line
                                    //rtbMonitor.AppendText(new_item.getQuantity() + "\t" + new_item.getSizeString() 
                                    //                        + "\t\t" + new_item.getPrice() + "\n");
                                    //stack.Push(new_item);
                                }
                                else
                                {
                                    DisplayStatus("Invalid Card. Please scan again.\n" + str_rfid, textColor);
                                  
                                }

                                orderID++;
                                OrderListBox.DataSource = order_list.toStringList();
                                if (order_list.Count != 0)
                                {
                                    OrderListBox.SetSelected(0, false);
                                }
                                OrderListBox.Refresh();
                                break;

                            //cancel mode
                            case eMode.eCancel:
                                //rtbMonitor.Clear();
                                if (str_rfid.Contains("4B00DA47EA3C"))
                                {
                                    CancelButton.BackColor = Color.LightGray;
                                    mode = eMode.eOperational;
                                    int i = 0;
                                    int del = order_list.Count-1;
                                    foreach (Order n in order_list)
                                    {
                                        if (OrderListBox.GetSelected(i))
                                        {
                                            if (quantity < n.getQuantity())
                                            {
                                                n.setQuantity(n.getQuantity() - quantity);
                                            }
                                            else
                                            {
                                                order_list.RemoveAt(del);
                                                break;
                                            }
                                            
                                        }
                                        i++;
                                        del--;
                                    }
                                    OrderListBox.DataSource = order_list.toStringList();
                                    OrderListBox.Refresh();
                                    if (order_list.Count != 0)
                                    {
                                        OrderListBox.SetSelected(0, false);
                                    }
                                }
                                else
                                {
                                    DisplayStatus("Invalid Card. Please scan again.\n", textColor);
                                    return;
                                }
                                //display new thing
                                //hopefully this doesn't reverse the order hahah...
                                Order[] list_of_items = stack.ToArray();
                                foreach (Order size in list_of_items)
                                {
                                  //  rtbMonitor.AppendText(size.getQuantity() + "\t" + size.getSizeString()
                                    //                + "\t" + size.getPrice() + "\n");
                                }
                                break;
                            default:
                                Console.WriteLine("INVALID MODE");
                                break;
                        }
                        //reset text
                        str_rfid = "";
                        sub_total = order_list.getTotalPrice();
                        bill_total = sub_total*1.13;
                        item_total = order_list.getTotalQuantity();
                        Console.WriteLine("TOTAL: " + bill_total);
                        Console.WriteLine("SUBTOTAL: " + sub_total);
                        Console.WriteLine("Total Item: " + item_total);
                        totalSum.Text = "Subtotal: $" + sub_total + "\n" + "Total:$" + bill_total.ToString("0.00") + "\n" + "Total # of Items: " + item_total;
                    }
                    // Return to the default color.
                    //rtbMonitor.SelectionColor = colorTransmit; 
                    
                    //  Trim the textbox's contents if needed.
                    
                   /* if ( rtbMonitor.TextLength > maximumTextBoxLength ) 
                    {                         
                        TrimTextBoxContents();                         
                    }                    
                    */
                    break;

                case "DisplayStatus":
                    
                    //  Add text to the rtbStatus textbox using the specified color.
                    
                    DisplayStatus( formText, textColor );
                    break;

                case "DisplayCurrentSettings":
                    
                    //  Display the current port settings in the ToolStripStatusLabel.
                    
                    DisplayCurrentSettings();
                    
                    break;

                default:
                    
                    break;
            }                      
        }

        void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            CancelButton.BackColor = Color.LightGray;
            mode = eMode.eOperational;
            if (t != null)
            {
                t.Dispose();
            }
        } 
     
        /// <summary>
        /// Enables accessing the form from another thread.
        /// The parameters match those of AccessForm() 
        /// </summary>
        /// 
        /// <param name="action"> a string that names the action to perform on the form </param>  
        /// <param name="formText"> text that the form displays </param> 
        /// <param name="textColor"> a system color for displaying text </param>

        private void AccessFormMarshal( string action, string textToDisplay, Color textColor ) 
        {          
            AccessFormMarshalDelegate AccessFormMarshalDelegate1; 

            AccessFormMarshalDelegate1 = new AccessFormMarshalDelegate( AccessForm );

            object[] args = { action, textToDisplay, textColor };

            //  Call AccessForm, passing the parameters in args.

            base.Invoke(AccessFormMarshalDelegate1, args);
        }   
               
        /// <summary>
        /// Display the current port parameters on the form.
        /// </summary>

        private void DisplayCurrentSettings() 
        {               
            string selectedPortState = ""; 
            
            if ( ComPorts.comPortExists ) 
            {                 
                if ( ( !( ( UserPort1.SelectedPort == null ) ) ) ) 
                {                     
                    if ( UserPort1.SelectedPort.IsOpen ) 
                    { 
                        selectedPortState = "OPEN"; 
                        btnOpenOrClosePort.Text = ButtonTextClosePort; 
                    } 
                    else 
                    { 
                        selectedPortState = "CLOSED"; 
                        btnOpenOrClosePort.Text = ButtonTextOpenPort; 
                    } 
                } 
                
                //UpdateStatusLabel( System.Convert.ToString( MyPortSettingsDialog.cmbPort.SelectedItem ) + "   " + System.Convert.ToString( MyPortSettingsDialog.cmbBitRate.SelectedItem ) + "   N 8 1   Handshake: " + MyPortSettingsDialog.cmbHandshaking.SelectedItem.ToString() + "   " + selectedPortState );                 
            } 
            else 
            { 
                DisplayStatus( ComPorts.noComPortsMessage, Color.Red ); 
              //  UpdateStatusLabel( "" );                 
            } 
        }        
                
        /// <summary>
        /// Provide a central mechanism for displaying exception information.
        /// Display a message that describes the exception.
        /// </summary>
        /// 
        /// <param name="moduleName"> the module where the exception occurred.</param>
        /// <param name="ex"> the exception </param>

        private void DisplayException( string moduleName, Exception ex ) 
        {    
            string errorMessage = null; 
            
            errorMessage = "Exception: " + ex.Message + " Module: " + moduleName + ". Method: " + ex.TargetSite.Name; 
            
            DisplayStatus( errorMessage, Color.Red ); 
                     
        }        
 
       
        private void DisplayStatus( string status, Color textColor ) 
        {             
            rtbStatus.ForeColor = textColor; 
            rtbStatus.Text = status;
            System.Console.Out.WriteLine(status);
        } 

        /// <summary>
        /// Get user preferences for the COM port and parameters.
        /// See SetPreferences for more information.
        /// </summary>
       
        private void GetPreferences() 
        {      
            UserPort1.SavedPortName = Settings.Default.ComPort;
            UserPort1.SavedBitRate = Settings.Default.BitRate;
            UserPort1.SavedHandshake = Settings.Default.Handshaking;
            savedOpenPortOnStartup = Settings.Default.OpenComPortOnStartup;         
        } 
         

        private void InitializeDisplayElements() 
        {
            totalSum.Text = "Subtotal: $" + sub_total + "\n" + "Total:$" + bill_total.ToString("0.00") + "\n" + "Total # of Items: " + item_total;
            //maximumTextBoxLength = 10000; 
            //rtbMonitor.SelectionColor = colorTransmit;             
        }
           
        /// <summary> 
        /// Save user preferences for the COM port and parameters.
        /// </summary>

        private void SavePreferences() 
        {  
            // To define additional settings, in the Visual Studio IDE go to
            // Solution Explorer > right click on project name > Properties > Settings.

            if (MyPortSettingsDialog.cmbPort.SelectedIndex > -1) 
            {
                // The system has at least one COM port.

                Settings.Default.ComPort = MyPortSettingsDialog.cmbPort.SelectedItem.ToString();
                Settings.Default.BitRate = (int)MyPortSettingsDialog.cmbBitRate.SelectedItem;
                Settings.Default.Handshaking = (Handshake) MyPortSettingsDialog.cmbHandshaking.SelectedItem;
                Settings.Default.OpenComPortOnStartup = MyPortSettingsDialog.chkOpenComPortOnStartup.Checked;

                Settings.Default.Save();    
            }
        }
        

        private void SetInitialPortParameters() 
        {         
            GetPreferences(); 
            
            if ( ComPorts.comPortExists ) 
            {                 
                //  Select a COM port and bit rate using stored preferences if available.
                
                UsePreferencesToSelectParameters(); 
                
                //  Save the selected indexes of the combo boxes.
                
                MyPortSettingsDialog.SavePortParameters();                 
            } 
            else 
            {                 
                //  No COM ports have been detected. Watch for one to be attached.
                
                tmrLookForPortChanges.Start(); 
                DisplayStatus( ComPorts.noComPortsMessage, Color.Red );                 
            }             
            UserPort1.ParameterChanged = false;             
        }         
       

        private void SetPortParameters( string userPort, int userBitRate, Handshake userHandshake ) 
        {          
            try 
            {                 
                //  Don't do anything if the system has no COM ports.
                
                if ( ComPorts.comPortExists ) 
                {                     
                    if ( MyPortSettingsDialog.ParameterChanged() ) 
                    {                         
                        //  One or more port parameters has changed.
                        
                        if ( ( string.Compare( MyPortSettingsDialog.oldPortName, userPort, true ) != 0 ) ) 
                        {                             
                            //  The port has changed.
                            //  Close the previously selected port.
                            
                            UserPort1.PreviousPort = UserPort1.SelectedPort; 
                            UserPort1.CloseComPort( UserPort1.SelectedPort ); 
                            
                            //  Set SelectedPort to the current port.
                            
                            UserPort1.SelectedPort.PortName = userPort; 
                            UserPort1.PortChanged = true;                            
                        } 
                        
                        //  Set other port parameters.
                        
                        UserPort1.SelectedPort.BaudRate = userBitRate; 
                        UserPort1.SelectedPort.Handshake = userHandshake; 
                        
                        MyPortSettingsDialog.SavePortParameters(); 
                        
                        UserPort1.ParameterChanged = true;                         
                    } 
                    else 
                    { 
                        UserPort1.ParameterChanged = false;                         
                    } 
                }
            } 
            catch ( InvalidOperationException ex ) 
            {                 
                UserPort1.ParameterChanged = true; 
                DisplayException( ModuleName, ex );                 
            } 
            catch ( UnauthorizedAccessException ex ) 
            {                 
                UserPort1.ParameterChanged = true; 
                DisplayException( ModuleName, ex ); 
                
                //  This exception can occur if the port was removed. 
                //  If the port was open, close it.
                
                UserPort1.CloseComPort( UserPort1.SelectedPort );                 
            } 
            catch ( System.IO.IOException ex ) 
            {                 
                UserPort1.ParameterChanged = true; 
                DisplayException( ModuleName, ex );                 
            } 
        }        
    
        /// <summary>
        /// Trim a richtextbox by removing the oldest contents.
        /// </summary>
        /// 
        /// <remarks >
        /// To trim the box while retaining any formatting applied to the retained contents,
        /// create a temporary richtextbox, copy the contents to be preserved to the 
        /// temporary richtextbox,and copy the temporary richtextbox back to the original richtextbox.
        /// </remarks>
        /*
        private void TrimTextBoxContents() 
        {        
            RichTextBox rtbTemp = new RichTextBox(); 
            int textboxTrimSize = 0;           
                        
            //  When the contents are too large, remove half.
            
            textboxTrimSize = maximumTextBoxLength / 2; 
            
            rtbMonitor.Select( rtbMonitor.TextLength - textboxTrimSize + 1, textboxTrimSize ); 
            rtbTemp.Rtf = rtbMonitor.SelectedRtf; 
            rtbMonitor.Clear(); 
            rtbMonitor.Rtf = rtbTemp.Rtf; 
            rtbTemp = null; 
            rtbMonitor.SelectionStart = rtbMonitor.TextLength;             
        } 
        */
        /// <summary>
        /// Set the text in the ToolStripStatusLabel.
        /// </summary>
        /// 
        /// <param name="status"> the text to display </param>



        /// <summary>
        /// Set the user preferences or default values in the combo boxes and ports array
        /// using stored preferences or default values.
        /// </summary>
      
        private void UsePreferencesToSelectParameters() 
        {         
            int myPortIndex = 0;

            myPortIndex = MyPortSettingsDialog.SelectComPort(UserPort1.SavedPortName);     
            MyPortSettingsDialog.SelectBitRate(UserPort1.SavedBitRate);
            UserPort1.SelectedPort.BaudRate = (int)MyPortSettingsDialog.cmbBitRate.SelectedItem;     
            MyPortSettingsDialog.SelectHandshaking(UserPort1.SavedHandshake);
            UserPort1.SelectedPort.Handshake = (Handshake)MyPortSettingsDialog.cmbHandshaking.SelectedItem;
            MyPortSettingsDialog.chkOpenComPortOnStartup.Checked = savedOpenPortOnStartup;           
        }
        
        /// <summary>
        /// Depending on the text displayed on the button, open or close the selected port
        /// and change the button text to the opposite action.
        /// </summary>

        private void btnOpenOrClosePort_Click( object sender, System.EventArgs e ) 
        {         
            if ( ( btnOpenOrClosePort.Text == ButtonTextOpenPort ) ) 
            { 
                UserPort1.OpenComPort(); 
                if ( UserPort1.SelectedPort.IsOpen ) 
                { 
                    btnOpenOrClosePort.Text = ButtonTextClosePort; 
                }                 
            } 
            else 
            { 
                UserPort1.CloseComPort( UserPort1.SelectedPort ); 
                
                if ( !( UserPort1.SelectedPort.IsOpen ) ) 
                { 
                    btnOpenOrClosePort.Text = ButtonTextOpenPort; 
                }                 
            }             
        } 

        /// <summary>
        /// Look for COM ports and display them in the combo box.
        /// </summary>

        private void btnPort_Click( object sender, System.EventArgs e ) 
        {          
            ComPorts.FindComPorts(); 
            MyPortSettingsDialog.DisplayComPorts(); 
            MyPortSettingsDialog.SelectComPort( UserPort1.SelectedPort.PortName ); 
            MyPortSettingsDialog.SelectBitRate( UserPort1.SelectedPort.BaudRate ); 
            MyPortSettingsDialog.SelectHandshaking( UserPort1.SelectedPort.Handshake );  
            UserPort1.ParameterChanged = false; 
            MyPortSettingsDialog.ShowDialog(); 
        } 

        /// <summary>
        /// Create an instance of the ComPorts class.
        /// Initialize port settings and other parameters. 
        /// specify behavior on events.
        /// </summary>
       
        private void Form1_Load( object sender, System.EventArgs e ) 
        {
                Show();                        

                UserPort1 = new ComPorts();

                MyPortSettingsDialog = new PortSettingsDialog();

                tmrLookForPortChanges.Interval = 1000;
                tmrLookForPortChanges.Stop();

                InitializeDisplayElements();

                SetInitialPortParameters();

                if (ComPorts.comPortExists)
                {
                    UserPort1.SelectedPort.PortName = ComPorts.myPortNames[MyPortSettingsDialog.cmbPort.SelectedIndex];

                    //  A check box enables requesting to open the selected COM port on start up.
                    //  Otherwise the application opens the port when the user clicks the Open Port
                    //  button or types text to send. 

                    if (MyPortSettingsDialog.chkOpenComPortOnStartup.Checked)
                    {
                        UserPort1.PortOpen = UserPort1.OpenComPort();
                        AccessForm("DisplayCurrentSettings", "", Color.Black);
                        AccessForm("DisplayStatus", "", Color.Black);
                    }
                    else
                    {
                        DisplayCurrentSettings();
                    }
                }

                //  Specify the routines that execute on events in other modules.
                //  The routines can receive data from other modules. 

                ComPorts.UserInterfaceData += new ComPorts.UserInterfaceDataEventHandler(AccessFormMarshal);
                PortSettingsDialog.UserInterfaceData += new PortSettingsDialog.UserInterfaceDataEventHandler(AccessFormMarshal);
                PortSettingsDialog.UserInterfacePortSettings += new PortSettingsDialog.UserInterfacePortSettingsEventHandler(SetPortParameters);
        }         
        
        /// <summary>
        /// Close the port if needed and save preferences.
        /// </summary>
        
        private void Form1_FormClosing( object sender, System.Windows.Forms.FormClosingEventArgs e ) 
        {             
            UserPort1.CloseComPort( UserPort1.SelectedPort ); 
            SavePreferences(); 
        } 

         
        /// <summary>
        /// Look for ports. If at least one is found, stop the timer and
        /// select the saved port if possible or the first port.
        /// This timer is enabled only when no COM ports are present.
        /// </summary>

        private void tmrLookForPortChanges_Tick( object sender, System.EventArgs e ) 
        {         
            ComPorts.FindComPorts(); 
            
            if ( ComPorts.comPortExists ) 
            {                 
                tmrLookForPortChanges.Stop(); 
                DisplayStatus( "COM port(s) found.", Color.Black ); 
                
                MyPortSettingsDialog.DisplayComPorts(); 
                MyPortSettingsDialog.SelectComPort( UserPort1.SavedPortName ); 
                MyPortSettingsDialog.SelectBitRate(UserPort1.SavedBitRate); 
                MyPortSettingsDialog.SelectHandshaking( ( ( Handshake )( UserPort1.SavedHandshake ) ) ); 
                
                //  Set selectedPort.
                
                SetPortParameters( UserPort1.SavedPortName, UserPort1.SavedBitRate, ( ( Handshake )( UserPort1.SavedHandshake ) ) ); 
                
                DisplayCurrentSettings(); 
                UserPort1.ParameterChanged = true;
            } 
        } 
                
        // Default instance for Form

        private static MainForm transDefaultFormMainForm = null;
        public static MainForm TransDefaultFormMainForm
        { 
        	get
        	{ 
        		if (transDefaultFormMainForm == null)
        		{
        			transDefaultFormMainForm = new MainForm();
        		}
        		return transDefaultFormMainForm;
        	} 
        }

        private void NumPad1_Click(object sender, EventArgs e)
        {
            if (NumPad1.BackColor == Color.White)
            {
                NumPad1.ForeColor = Color.White;
                NumPad1.BackColor = Color.CadetBlue;
                NumPad2.ForeColor = Color.Black;
                NumPad2.BackColor = Color.White;
                NumPad3.ForeColor = Color.Black;
                NumPad3.BackColor = Color.White;
                NumPad4.ForeColor = Color.Black;
                NumPad4.BackColor = Color.White;
                NumPad5.ForeColor = Color.Black;
                NumPad5.BackColor = Color.White;
                NumPad6.ForeColor = Color.Black;
                NumPad6.BackColor = Color.White;
                NumPad7.ForeColor = Color.Black;
                NumPad7.BackColor = Color.White;
                NumPad8.ForeColor = Color.Black;
                NumPad8.BackColor = Color.White;
                NumPad9.ForeColor = Color.Black;
                NumPad9.BackColor = Color.White;
            }
            else
            {
                NumPad1.ForeColor = Color.Black;
                NumPad1.BackColor = Color.White;                
            }
            quantity = 1;
        }

        private void NumPad2_Click(object sender, EventArgs e)
        {
            if (NumPad2.BackColor == Color.White)
            {
                NumPad1.ForeColor = Color.Black;
                NumPad1.BackColor = Color.White;
                NumPad2.ForeColor = Color.White;
                NumPad2.BackColor = Color.CadetBlue;
                NumPad3.ForeColor = Color.Black;
                NumPad3.BackColor = Color.White;
                NumPad4.ForeColor = Color.Black;
                NumPad4.BackColor = Color.White;
                NumPad5.ForeColor = Color.Black;
                NumPad5.BackColor = Color.White;
                NumPad6.ForeColor = Color.Black;
                NumPad6.BackColor = Color.White;
                NumPad7.ForeColor = Color.Black;
                NumPad7.BackColor = Color.White;
                NumPad8.ForeColor = Color.Black;
                NumPad8.BackColor = Color.White;
                NumPad9.ForeColor = Color.Black;
                NumPad9.BackColor = Color.White; 
                quantity = 2;
            }
            else
            {
                NumPad2.ForeColor = Color.Black;
                NumPad2.BackColor = Color.White;
                quantity = 1;
            }
        }

        private void NumPad3_Click(object sender, EventArgs e)
        {
            if (NumPad3.BackColor == Color.White)
            {
                NumPad1.ForeColor = Color.Black;
                NumPad1.BackColor = Color.White;
                NumPad2.ForeColor = Color.Black;
                NumPad2.BackColor = Color.White;
                NumPad3.ForeColor = Color.White;
                NumPad3.BackColor = Color.CadetBlue;
                NumPad4.ForeColor = Color.Black;
                NumPad4.BackColor = Color.White;
                NumPad5.ForeColor = Color.Black;
                NumPad5.BackColor = Color.White;
                NumPad6.ForeColor = Color.Black;
                NumPad6.BackColor = Color.White;
                NumPad7.ForeColor = Color.Black;
                NumPad7.BackColor = Color.White;
                NumPad8.ForeColor = Color.Black;
                NumPad8.BackColor = Color.White;
                NumPad9.ForeColor = Color.Black;
                NumPad9.BackColor = Color.White;
                quantity = 3;
            }
            else
            {
                NumPad3.ForeColor = Color.Black;
                NumPad3.BackColor = Color.White;
                quantity = 1;
            }
        }

        private void NumPad4_Click(object sender, EventArgs e)
        {
            if (NumPad4.BackColor == Color.White)
            {
                NumPad1.ForeColor = Color.Black;
                NumPad1.BackColor = Color.White;
                NumPad2.ForeColor = Color.Black;
                NumPad2.BackColor = Color.White;
                NumPad3.ForeColor = Color.Black;
                NumPad3.BackColor = Color.White;
                NumPad4.ForeColor = Color.White;
                NumPad4.BackColor = Color.CadetBlue;
                NumPad5.ForeColor = Color.Black;
                NumPad5.BackColor = Color.White;
                NumPad6.ForeColor = Color.Black;
                NumPad6.BackColor = Color.White;
                NumPad7.ForeColor = Color.Black;
                NumPad7.BackColor = Color.White;
                NumPad8.ForeColor = Color.Black;
                NumPad8.BackColor = Color.White;
                NumPad9.ForeColor = Color.Black;
                NumPad9.BackColor = Color.White;
                quantity = 4;
            }
            else
            {
                NumPad4.ForeColor = Color.Black;
                NumPad4.BackColor = Color.White;
                quantity = 1;
            }
        }

        private void NumPad5_Click(object sender, EventArgs e)
        {
            if (NumPad5.BackColor == Color.White)
            {
                NumPad1.ForeColor = Color.Black;
                NumPad1.BackColor = Color.White;
                NumPad2.ForeColor = Color.Black;
                NumPad2.BackColor = Color.White;
                NumPad3.ForeColor = Color.Black;
                NumPad3.BackColor = Color.White;
                NumPad4.ForeColor = Color.Black;
                NumPad4.BackColor = Color.White;
                NumPad5.ForeColor = Color.White;
                NumPad5.BackColor = Color.CadetBlue;
                NumPad6.ForeColor = Color.Black;
                NumPad6.BackColor = Color.White;
                NumPad7.ForeColor = Color.Black;
                NumPad7.BackColor = Color.White;
                NumPad8.ForeColor = Color.Black;
                NumPad8.BackColor = Color.White;
                NumPad9.ForeColor = Color.Black;
                NumPad9.BackColor = Color.White;
                quantity = 5;
            }
            else
            {
                NumPad5.ForeColor = Color.Black;
                NumPad5.BackColor = Color.White;
                quantity = 1;
            }
        }

        private void NumPad6_Click(object sender, EventArgs e)
        {
            if (NumPad6.BackColor == Color.White)
            {
                NumPad1.ForeColor = Color.Black;
                NumPad1.BackColor = Color.White;
                NumPad2.ForeColor = Color.Black;
                NumPad2.BackColor = Color.White;
                NumPad3.ForeColor = Color.Black;
                NumPad3.BackColor = Color.White;
                NumPad4.ForeColor = Color.Black;
                NumPad4.BackColor = Color.White;
                NumPad5.ForeColor = Color.Black;
                NumPad5.BackColor = Color.White;
                NumPad6.ForeColor = Color.White;
                NumPad6.BackColor = Color.CadetBlue;
                NumPad7.ForeColor = Color.Black;
                NumPad7.BackColor = Color.White;
                NumPad8.ForeColor = Color.Black;
                NumPad8.BackColor = Color.White;
                NumPad9.ForeColor = Color.Black;
                NumPad9.BackColor = Color.White;
                quantity = 6;
            }
            else
            {
                NumPad6.ForeColor = Color.Black;
                NumPad6.BackColor = Color.White;
                quantity = 1;
            }
        }

        private void NumPad7_Click(object sender, EventArgs e)
        {
            if (NumPad7.BackColor == Color.White)
            {
                NumPad1.ForeColor = Color.Black;
                NumPad1.BackColor = Color.White;
                NumPad2.ForeColor = Color.Black;
                NumPad2.BackColor = Color.White;
                NumPad3.ForeColor = Color.Black;
                NumPad3.BackColor = Color.White;
                NumPad4.ForeColor = Color.Black;
                NumPad4.BackColor = Color.White;
                NumPad5.ForeColor = Color.Black;
                NumPad5.BackColor = Color.White;
                NumPad6.ForeColor = Color.Black;
                NumPad6.BackColor = Color.White;
                NumPad7.ForeColor = Color.White;
                NumPad7.BackColor = Color.CadetBlue;
                NumPad8.ForeColor = Color.Black;
                NumPad8.BackColor = Color.White;
                NumPad9.ForeColor = Color.Black;
                NumPad9.BackColor = Color.White;
                quantity = 7;
            }
            else
            {
                NumPad7.ForeColor = Color.Black;
                NumPad7.BackColor = Color.White;
                quantity = 1;
            }
        }

        private void NumPad8_Click(object sender, EventArgs e)
        {
            if (NumPad8.BackColor == Color.White)
            {
                NumPad1.ForeColor = Color.Black;
                NumPad1.BackColor = Color.White;
                NumPad2.ForeColor = Color.Black;
                NumPad2.BackColor = Color.White;
                NumPad3.ForeColor = Color.Black;
                NumPad3.BackColor = Color.White;
                NumPad4.ForeColor = Color.Black;
                NumPad4.BackColor = Color.White;
                NumPad5.ForeColor = Color.Black;
                NumPad5.BackColor = Color.White;
                NumPad6.ForeColor = Color.Black;
                NumPad6.BackColor = Color.White;
                NumPad7.ForeColor = Color.Black;
                NumPad7.BackColor = Color.White;
                NumPad8.ForeColor = Color.White;
                NumPad8.BackColor = Color.CadetBlue;
                NumPad9.ForeColor = Color.Black;
                NumPad9.BackColor = Color.White;
                quantity = 8;
            }
            else
            {
                NumPad8.ForeColor = Color.Black;
                NumPad8.BackColor = Color.White;
                quantity = 1;
            }
        }

        private void NumPad9_Click(object sender, EventArgs e)
        {
            if (NumPad9.BackColor == Color.White)
            {
                NumPad1.ForeColor = Color.Black;
                NumPad1.BackColor = Color.White;
                NumPad2.ForeColor = Color.Black;
                NumPad2.BackColor = Color.White;
                NumPad3.ForeColor = Color.Black;
                NumPad3.BackColor = Color.White;
                NumPad4.ForeColor = Color.Black;
                NumPad4.BackColor = Color.White;
                NumPad5.ForeColor = Color.Black;
                NumPad5.BackColor = Color.White;
                NumPad6.ForeColor = Color.Black;
                NumPad6.BackColor = Color.White;
                NumPad7.ForeColor = Color.Black;
                NumPad7.BackColor = Color.White;
                NumPad8.ForeColor = Color.Black;
                NumPad8.BackColor = Color.White;
                NumPad9.ForeColor = Color.White;
                NumPad9.BackColor = Color.CadetBlue;
                quantity = 9;
            }
            else
            {
                NumPad9.ForeColor = Color.Black;
                NumPad9.BackColor = Color.White;
                quantity = 1;
            }
        }

        private void OrderListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CancelButtont_Click(object sender, EventArgs e)
        {
            CancelButton.BackColor = Color.DarkGray;
            t = new System.Timers.Timer(5000);
            t.Elapsed += new ElapsedEventHandler(t_Elapsed);
            t.Enabled = true;
            mode = eMode.eCancel;

        }



    }   
} 