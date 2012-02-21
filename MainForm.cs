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
        private enum eMode { eOperational, eCancel, eCheckout };
        private enum eTea { eRequest,eClear };
        private enum eWaiter { eRequest, eClear };
        private eTea tea_status= eTea.eClear;
        private eWaiter waiter_status= eWaiter.eClear;
        private eMode mode = eMode.eOperational;
        //stack use to delete the most previous one? Or do we not need it
        private OrderList order_list = new OrderList();
        private Stack<Order> stack = new Stack<Order>();
        private Stack<Order> temp_stack = new Stack<Order>();
        private double bill_total=0;
        private double sub_total = 0;
        private int item_total=0;
        private System.Timers.Timer t = null;
        private System.Timers.Timer t_num = null;
        private delegate void SetTextCallback(string text);

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
                                string card_of_interest = "";
                                int i = 0;
                                int del = order_list.Count-1;
                                foreach (Order n in order_list)
                                {
                                    if (OrderListBox.GetSelected(i))
                                    {
                                        switch (n.getSize())
                                        {
                                            case Order.eSize.eSmall:
                                                card_of_interest = "4B00DA17F573";
                                                break;
                                            case Order.eSize.eMedium:
                                                card_of_interest = "4800E50372DC";
                                                break;
                                            case Order.eSize.eLarge:
                                                card_of_interest = "4B00DA60DB2A";
                                                break;
                                            default:
                                                Console.WriteLine("ERROR: INVALID CARD");
                                                break;
                                        }

                                        if (str_rfid.Contains("4B00DA47EA3C")|| str_rfid.Contains(card_of_interest))
                                        {
                                            CancelButton.BackColor = Color.LightGray;
                                            mode = eMode.eOperational;
                                            if (quantity < n.getQuantity())
                                            {
                                                n.setQuantity(n.getQuantity() - quantity);
                                            }
                                            else
                                            {
                                                order_list.RemoveAt(del);
                                            }
                                            
                                            OrderListBox.DataSource = order_list.toStringList();
                                            OrderListBox.Refresh();
                                            if (order_list.Count != 0)
                                            {
                                                OrderListBox.SetSelected(0, false);
                                            }
                                            break;
                                        }
                                        else
                                        {
                                            DisplayStatus("Invalid Card. Please scan again.\n", textColor);
                                            return;
                                        }
                                    }
                                    i++;
                                    del--;
                                }
                                break;
                                
                                case eMode.eCheckout:
                                    //disable all use                                   
                                break;

                            default:
                                Console.WriteLine("INVALID MODE");
                                break;
                        }
                        //reset text
                        str_rfid = "";
                        txtQuantity.Clear();
                        quantity = 1;
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

        private void CancelButtont_Click(object sender, EventArgs e)
        {
            CancelButton.BackColor = Color.DarkGray;
            t = new System.Timers.Timer(5000);
            t.Elapsed += new ElapsedEventHandler(t_Elapsed);
            t.AutoReset = false;
            t.Enabled = true;
            mode = eMode.eCancel;

        }

        private void OrderListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Numpad1_Click(object sender, EventArgs e)
        {
            txtQuantity.AppendText("1");
            checkQuantitySize();
        }

        private void NumpadCLR_Click(object sender, EventArgs e)
        {
            txtQuantity.Clear();
        }

        private void Numpad9_Click(object sender, EventArgs e)
        {
            txtQuantity.AppendText("9");
            checkQuantitySize();
        }

        private void Numpad8_Click(object sender, EventArgs e)
        {
            txtQuantity.AppendText("8");
            checkQuantitySize();
        }

        private void Numpad7_Click(object sender, EventArgs e)
        {
            txtQuantity.AppendText("7");
            checkQuantitySize();
        }

        private void Numpad6_Click(object sender, EventArgs e)
        {
            txtQuantity.AppendText("6");
            checkQuantitySize();
        }

        private void Numpad3_Click(object sender, EventArgs e)
        {
            txtQuantity.AppendText("3");
            checkQuantitySize();
        }

        private void Numpad5_Click(object sender, EventArgs e)
        {
            txtQuantity.AppendText("5");
            checkQuantitySize();
        }

        private void Numpad4_Click(object sender, EventArgs e)
        {
            txtQuantity.AppendText("4");
            checkQuantitySize();
        }

        private void Numpad2_Click(object sender, EventArgs e)
        {
            txtQuantity.AppendText("2");
            checkQuantitySize();
        }

        private void Numpad0_Click(object sender, EventArgs e)
        {
            //to prevent case starting at 0
            if (txtQuantity.Text != "")
            {
                txtQuantity.AppendText("0");
                checkQuantitySize();
            }
        }
        private void checkQuantitySize()
        {
            if (Convert.ToInt16(txtQuantity.Text) > 99)
            {
                txtQuantity.Text = "99";
            }
            quantity = Convert.ToInt16(txtQuantity.Text);
            if (t_num != null)
            {
                t_num.Dispose();
            }
            t_num = new System.Timers.Timer(5000);
            t_num.Elapsed += new ElapsedEventHandler(t_Elapsed_Numpad);
            t_num.AutoReset = false;
            t_num.Enabled = true;
        }
        private void t_Elapsed_Numpad(object sender, ElapsedEventArgs e)
        {
            this.SetText("") ;
        }
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtQuantity.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtQuantity.Text = text;
            }
        }

        private void but_Bill_Click(object sender, EventArgs e)
        {
            //checkout mode
            mode = eMode.eCheckout;
            
            //clear tea and waiter
            tea_status = eTea.eClear;
            waiter_status = eWaiter.eClear;
            teaWaiterStatusCheck();

            DialogResult result= MessageBox.Show(this,"The system is now in checkout mode. To exist checkout mode, press OK.", "Checkout Mode");
            if (result == DialogResult.OK)
            {
                mode = eMode.eOperational;
            }
        }

        private void teaButton_Click(object sender, EventArgs e)
        {
            //set a request and clear status
            //set request will make light highlight, clear will change back to normal
            tea_status = eTea.eRequest;
            teaButton.BackColor = Color.Lime;
            //send request

        }

        private void waiterButton_Click(object sender, EventArgs e)
        {
            waiter_status = eWaiter.eRequest;
            waiterButton.BackColor = Color.DarkTurquoise;
            //send request to manager saying he needs tea
        }
        private void teaWaiterStatusCheck()
        {
            switch (waiter_status)
            {
                case eWaiter.eClear:
                    waiterButton.BackColor = SystemColors.ButtonHighlight;
                    break;
                case eWaiter.eRequest:
                    waiterButton.BackColor = Color.DarkTurquoise;
                    //send request
                    break;
            }
            switch (tea_status)
            {
                case eTea.eClear:
                    teaButton.BackColor = SystemColors.ButtonHighlight;
                    break;
                case eTea.eRequest:
                    teaButton.BackColor = Color.Lime;
                    //send request
                    break;
            }
        }
        //need a function that constantly checks for xml parsing of status and pte form accordingly...
    }   
} 