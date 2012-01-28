using System;
using System.Drawing;
using System.IO.Ports; 
using System.Windows.Forms;

namespace COMPortTerminal
{  
    /// <summary>
    /// Provides a dialog box for viewing and selecting COM ports and parameters.
    /// </summary>

    public partial class PortSettingsDialog  
    {      
        private int[] bitRates = new int[ 11 ];
        private int oldBitRateIndex;
        private int oldHandshakeIndex; 
        internal string oldPortName;
        private bool settingsInitialized; 
        
        //  These events enable other modules to detect events and receive data.
        
        internal delegate void UserInterfacePortSettingsEventHandler( string selectedPort, int selectedBitRate, Handshake selectedHandshake );       
        internal delegate void UserInterfaceDataEventHandler( string action, string formText, Color textColor );

        internal static event UserInterfacePortSettingsEventHandler UserInterfacePortSettings; 
        internal static event UserInterfaceDataEventHandler UserInterfaceData;        
       
        /// <summary>
        /// Display available COM ports in a combo box.
        /// Assumes ComPorts.FindComPorts has been run to fill the myPorts array.
        /// </summary>

        internal void DisplayComPorts() 
        { 
            //  Clear the combo box and repopulate (in case ports have been added or removed).
            
            cmbPort.DataSource = ComPorts.myPortNames;             
        } 
               
        /// <summary>
        /// Set initial port parameters. 
        /// </summary>

        internal void InitializePortSettings() 
        {   
            if ( !( settingsInitialized ) ) 
            {                 
                // Bit rates to select from.
                
                bitRates[ 0 ] = 300; 
                bitRates[ 1 ] = 600; 
                bitRates[ 2 ] = 1200; 
                bitRates[ 3 ] = 2400; 
                bitRates[ 4 ] = 9600; 
                bitRates[ 5 ] = 14400; 
                bitRates[ 6 ] = 19200; 
                bitRates[ 7 ] = 38400; 
                bitRates[ 8 ] = 57600; 
                bitRates[ 9 ] = 115200; 
                bitRates[ 10 ] = 128000; 
                
                // Place the bit rates and handshaking options in the combo boxes.
                
                cmbBitRate.DataSource = bitRates; 
                cmbBitRate.DropDownStyle = ComboBoxStyle.DropDownList; 
                
                //  Handshaking options.
                
                cmbHandshaking.Items.Add( Handshake.None ); 
                cmbHandshaking.Items.Add( Handshake.XOnXOff ); 
                cmbHandshaking.Items.Add( Handshake.RequestToSend ); 
                cmbHandshaking.Items.Add( Handshake.RequestToSendXOnXOff ); 
                
                cmbHandshaking.DropDownStyle = ComboBoxStyle.DropDownList; 
                
                // Find and display available COM ports.
                
                ComPorts.FindComPorts(); 
                
                cmbPort.DataSource = ComPorts.myPortNames; 
                
                cmbPort.DropDownStyle = ComboBoxStyle.DropDownList; 
                
                settingsInitialized = true; 
            } 
        } 

        /// <summary>
        /// Compares stored parameters with the current parameters.
        /// </summary>
        /// 
        /// <returns>
        /// True if any parameter has changed.
        /// </returns>
       
        internal bool ParameterChanged() 
        {   
            return ( oldBitRateIndex != cmbBitRate.SelectedIndex ) | ( oldHandshakeIndex != cmbHandshaking.SelectedIndex ) | ( ( string.Compare( oldPortName, System.Convert.ToString( cmbPort.SelectedItem ), true ) != 0 ) ); 
        } 
       
        /// <summary>
        /// Save the current port parameters.
        /// Enables learning if a parameter has changed.
        /// </summary>

        internal void SavePortParameters() 
        { 
            oldBitRateIndex = cmbBitRate.SelectedIndex; 
            oldHandshakeIndex = cmbHandshaking.SelectedIndex; 
            oldPortName = System.Convert.ToString( cmbPort.SelectedItem );             
        } 

        /// <summary>
        /// Select a bit rate in the combo box.
        /// Does not set the bit rate for a COM port.
        /// </summary>
        /// 
        /// <param name="bitRate"> The requested bit rate </param>
        
        internal void SelectBitRate( int bitRate ) 
        {
            cmbBitRate.SelectedItem = bitRate;           
        } 

        /// <summary>
        /// Select a COM port in the combo box.
        /// Does not set the selectedPort variable.
        /// </summary>
        /// 
        /// <param name="comPortName"> A COM port name </param>
        /// 
        /// <returns > The index of the selected port in the combo box </returns>
       
        internal int SelectComPort( string comPortName ) 
        {    
            // If comPortName doesn't exist in the combo box, SelectedItem remains the same.

            cmbPort.SelectedItem = comPortName;

            if (cmbPort.SelectedIndex > -1)
            {
                // At least one COM port exists.

                if (!(String.Compare(comPortName, System.Convert.ToString(cmbPort.SelectedItem), true) == 0))
                {
                    // The requested port isn't available. Select the first port.

                    cmbPort.SelectedIndex = 0;
                }
            }        
            else
            {
                if (null != UserInterfaceData) UserInterfaceData("DisplayStatus", ComPorts.noComPortsMessage, Color.Red);
                ComPorts.comPortExists = false;
            }            
        
        return cmbPort.SelectedIndex;   
        }

        /// <summary>
        /// Sets handshaking in the combo box.
        /// Does not set handshaking for a COM port.
        /// </summary>
        /// 
        /// <param name="requestedHandshake"> the requested handshaking as a System.IO.Ports.Handshake value. </param> 

        internal void SelectHandshaking( Handshake requestedHandshake ) 
        {
            cmbHandshaking.SelectedItem = requestedHandshake;           
        } 
             
        /// <summary>
        /// Initialize port settings.
        /// InitializeComponent is required by the Windows Form Designer.
        /// </summary>

        public PortSettingsDialog() 
        {   
            InitializeComponent(); 
            
            InitializePortSettings(); 
            
            btnOK.Click += new System.EventHandler( btnOK_Click ); 
            Load += new System.EventHandler(PortSettingsDialog_Load);          
            btnCancel.Click += new System.EventHandler( btnCancel_Click ); 
        } 
        
        /// <summary>
        /// The port parameters may have changed.
        /// Make the parameters available to other modules. 
        /// </summary>

        private void btnOK_Click( System.Object sender, System.EventArgs e ) 
        {   
            string statusMessage = null; 
            
            //  Set the port parameters.

            if (ComPorts.comPortExists)
            {            
            if ( null != UserInterfacePortSettings ) UserInterfacePortSettings( cmbPort.SelectedItem.ToString(), System.Convert.ToInt32( cmbBitRate.SelectedItem ), ( ( Handshake )( cmbHandshaking.SelectedItem ) ) ); 
            }

            if ( null != UserInterfaceData ) UserInterfaceData( "DisplayCurrentSettings", "", Color.Black ); 
            
            if ( cmbPort.SelectedIndex == -1 ) 
            { 
                statusMessage = ComPorts.noComPortsMessage; 
            } 
            else 
            { 
                statusMessage = ""; 
            } 
            
            if ( null != UserInterfaceData ) UserInterfaceData( "DisplayStatus", statusMessage, Color.Black );             
        } 
                
        /// <summary>
        /// Configure components in the dialog box.
        /// </summary>

        internal void PortSettingsDialog_Load( object sender, System.EventArgs e ) 
        {             
            btnOK.DialogResult = System.Windows.Forms.DialogResult.OK; 
            this.AcceptButton = btnOK; 
            btnOK.Focus();             
        } 

        /// <summary>
        /// Don't save any changes to the form.
        /// </summary>
        
        private void btnCancel_Click( System.Object sender, System.EventArgs e ) 
        { 
            this.Hide(); 
        }

        // Default instance for Form

        private static PortSettingsDialog transDefaultFormPortSettingsDialog = null;
        public static PortSettingsDialog TransDefaultFormPortSettingsDialog
        {
            get
            {
                if (transDefaultFormPortSettingsDialog == null)
                {
                    transDefaultFormPortSettingsDialog = new PortSettingsDialog();
                }
                return transDefaultFormPortSettingsDialog;
            }
        } 
    }     
} 
