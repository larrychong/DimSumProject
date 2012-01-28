using System;
using System.IO.Ports; 
using System.Runtime.Remoting.Messaging; 
using System.Drawing;
using System.Windows.Forms;

namespace COMPortTerminal
{    
    /// <summary>
    /// Routines for finding and accessing COM ports.
    /// </summary>

    public class ComPorts  
    {  
        private const string ModuleName = "ComPorts"; 
        
        //  Shared members - do not belong to a specific instance of the class.
        
        internal static bool comPortExists; 
        internal static string[] myPortNames; 
        internal static string noComPortsMessage = "No COM ports found. Please attach a COM-port device."; 
        
        internal delegate void UserInterfaceDataEventHandler( string action, string formText, Color textColor ); 
        internal static event UserInterfaceDataEventHandler UserInterfaceData; 
        
        //  Non-shared members - belong to a specific instance of the class.
        
        //internal delegate void SerialDataReceivedEventHandlerDelegate( object sender, SerialDataReceivedEventArgs e );
        //internal delegate void SerialErrorReceivedEventHandlerDelegate( object sender, SerialErrorReceivedEventArgs e );
        internal delegate bool WriteToComPortDelegate( string textToWrite );
              
        internal WriteToComPortDelegate WriteToComPortDelegate1;      
        
        //  Local variables available as Properties.

        private bool m_ParameterChanged;
        private bool m_PortChanged;
        private bool m_PortOpen;
        private SerialPort m_PreviousPort = new SerialPort();
        private int m_ReceivedDataLength;
        private int m_SavedBitRate = 9600;
        private Handshake m_SavedHandshake = Handshake.None ;
        private string m_SavedPortName = "";
        private SerialPort m_SelectedPort = new SerialPort();       
               
        internal bool ParameterChanged 
        { 
            get 
            { 
                return m_ParameterChanged; 
            } 
            set 
            { 
                m_ParameterChanged = value; 
            } 
        } 
        
        internal bool PortChanged 
        { 
            get 
            { 
                return m_PortChanged; 
            } 
            set 
            { 
                m_PortChanged = value; 
            } 
        }         
      
        internal bool PortOpen 
        { 
            get 
            { 
                return m_PortOpen; 
            } 
            set 
            { 
                m_PortOpen = value; 
            } 
        }         
       
        internal SerialPort PreviousPort 
        { 
            get 
            { 
                return m_PreviousPort; 
            } 
            set 
            { 
                m_PreviousPort = value; 
            } 
        }         
       
        internal int ReceivedDataLength 
        { 
            get 
            { 
                return m_ReceivedDataLength; 
            } 
            set 
            { 
                m_ReceivedDataLength = value; 
            } 
        }         
       
        internal int SavedBitRate 
        { 
            get 
            { 
                return m_SavedBitRate; 
            } 
            set 
            { 
                m_SavedBitRate = value; 
            } 
        }         
        
        internal Handshake SavedHandshake 
        { 
            get 
            { 
                return m_SavedHandshake; 
            } 
            set 
            { 
                m_SavedHandshake = value; 
            } 
        }         
       
        internal string SavedPortName 
        { 
            get 
            { 
                return m_SavedPortName; 
            } 
            set 
            { 
                m_SavedPortName = value; 
            } 
        }         
       
        internal SerialPort SelectedPort 
        { 
            get 
            { 
                return m_SelectedPort; 
            } 
            set 
            { 
                m_SelectedPort = value; 
            } 
        } 
        
        private SerialDataReceivedEventHandler SerialDataReceivedEventHandler1;        
        private SerialErrorReceivedEventHandler SerialErrorReceivedEventHandler1; 
        
        /// <summary>
        /// If the COM port is open, close it.
        /// </summary>
        /// 
        /// <param name="portToClose"> the SerialPort object to close </param>  

        internal void CloseComPort( SerialPort portToClose ) 
        { 
            try 
            { 
                if ( null != UserInterfaceData ) UserInterfaceData( "DisplayStatus", "", Color.Black ); 
                
                object transTemp0 = portToClose; 
                if (  !(  transTemp0 == null ) ) 
                {                     
                    if ( portToClose.IsOpen ) 
                    {                         
                        portToClose.Close(); 
                        if ( null != UserInterfaceData ) UserInterfaceData( "DisplayCurrentSettings", "", Color.Black );                         
                    } 
                }                
           }
                                      
            catch ( InvalidOperationException ex ) 
            {                 
                ParameterChanged = true; 
                PortChanged = true; 
                DisplayException( ModuleName, ex );                 
            } 
            catch ( UnauthorizedAccessException ex ) 
            {                 
                ParameterChanged = true; 
                PortChanged = true; 
                DisplayException( ModuleName, ex );                 
            } 
            catch ( System.IO.IOException ex ) 
            {                 
                ParameterChanged = true; 
                PortChanged = true; 
                DisplayException( ModuleName, ex ); 
            }             
        } 

        /// <summary>
        /// Called when data is received on the COM port.
        /// Reads and displays the data.
        /// See FindPorts for the AddHandler statement for this routine.
        /// </summary>
                
        internal void DataReceived( object sender, SerialDataReceivedEventArgs e ) 
        {  
            string newReceivedData = null; 
            
            try 
            { 
                //  Get data from the COM port.
                
                newReceivedData = SelectedPort.ReadExisting(); 
                
                //  Save the number of characters received.
                
                ReceivedDataLength += newReceivedData.Length; 
                
                if ( null != UserInterfaceData ) UserInterfaceData( "AppendToMonitorTextBox", newReceivedData, Color.Black ); 
                
            } 
            catch ( Exception ex ) 
            { 
                DisplayException( ModuleName, ex ); 
            } 
        }        

        /// <summary>
        /// Provide a central mechanism for displaying exception information.
        /// Display a message that describes the exception.
        /// </summary>
        /// 
        /// <param name="ex"> The exception </param> 
        /// <param name="moduleName" > the module where the exception was raised. </param>
        
        private void DisplayException( string moduleName, Exception ex ) 
        {
            string errorMessage = null; 
            
            errorMessage = "Exception: " + ex.Message + " Module: " + moduleName + ". Method: " + ex.TargetSite.Name; 
            
            if ( null != UserInterfaceData ) UserInterfaceData( "DisplayStatus", errorMessage, Color.Red ); 
            
            //  To display errors in a message box, uncomment this line:
            //  MessageBox.Show(errorMessage)
        }        

        /// <summary>
        /// Respond to error events.
        /// </summary>
        
        private void ErrorReceived( object sender, SerialErrorReceivedEventArgs e ) 
        { 
            SerialError SerialErrorReceived1 = 0; 
            
            SerialErrorReceived1 = e.EventType; 
            
            switch ( SerialErrorReceived1 ) 
            {
                case SerialError.Frame:
                    Console.WriteLine( "Framing error." ); 
                    
                    break;
                case SerialError.Overrun:
                    Console.WriteLine( "Character buffer overrun." ); 
                    
                    break;
                case SerialError.RXOver:
                    Console.WriteLine( "Input buffer overflow." ); 
                    
                    break;
                case SerialError.RXParity:
                    Console.WriteLine( "Parity error." ); 
                    
                    break;
                case SerialError.TXFull:
                    Console.WriteLine( "Output buffer full." ); 
                    break;
            }            
        }         

        /// <summary>
        /// Find the PC's COM ports and store parameters for each port.
        /// Use saved parameters if possible, otherwise use default values.  
        /// </summary>
        /// 
        /// <remarks> 
        /// The ports can change if a USB/COM-port converter is attached or removed,
        /// so this routine may need to run multiple times.
        /// </remarks>
        
       internal static void FindComPorts() 
        { 
            myPortNames = SerialPort.GetPortNames(); 
            
            //  Is there at least one COM port?
            
            if ( myPortNames.Length > 0 ) 
            {                 
                comPortExists = true; 
                Array.Sort( myPortNames );                 
            } 
            else 
            { 
                //  No COM ports found.
                
                comPortExists = false; 
            }             
        } 

        /// <summary>
        /// Open the SerialPort object selectedPort.
        /// If open, close the SerialPort object previousPort.
        /// </summary>
                
        internal bool OpenComPort() 
        { 
            bool success = false;
            SerialDataReceivedEventHandler1 = new SerialDataReceivedEventHandler(DataReceived);
            SerialErrorReceivedEventHandler1 = new SerialErrorReceivedEventHandler(ErrorReceived); 

            try 
            { 
                if ( comPortExists ) 
                {                     
                    //  The system has at least one COM port.
                    //  If the previously selected port is still open, close it.
                    
                    if ( PreviousPort.IsOpen ) 
                    { 
                        CloseComPort( PreviousPort ); 
                    } 
                    
                    if ( ( !( ( SelectedPort.IsOpen ) ) | PortChanged ) ) 
                    {                         
                        SelectedPort.Open(); 
                        
                        if ( SelectedPort.IsOpen ) 
                        {                             
                            //  The port is open. Set additional parameters.
                            //  Timeouts are in milliseconds.
                            
                            SelectedPort.ReadTimeout = 5000; 
                            SelectedPort.WriteTimeout = 5000; 
                            
                            //  Specify the routines that run when a DataReceived or ErrorReceived event occurs.
                           
                            SelectedPort.DataReceived += SerialDataReceivedEventHandler1; 
                            SelectedPort.ErrorReceived += SerialErrorReceivedEventHandler1; 
                           
                            //  Send data to other modules.
                            
                            if ( null != UserInterfaceData ) UserInterfaceData( "DisplayCurrentSettings", "", Color.Black ); 
                            if ( null != UserInterfaceData ) UserInterfaceData( "DisplayStatus", "", Color.Black ); 
                            
                            success = true; 
                            
                            //  The port is open with the current parameters.
                            
                            PortChanged = false;                             
                        } 
                    }                    
                }                 
            }
            
            catch ( InvalidOperationException ex ) 
            {                 
                ParameterChanged = true; 
                PortChanged = true; 
                DisplayException( ModuleName, ex );                 
            } 
            catch ( UnauthorizedAccessException ex ) 
            {                 
                ParameterChanged = true; 
                PortChanged = true; 
                DisplayException( ModuleName, ex );                 
            } 
            catch ( System.IO.IOException ex ) 
            {                 
                ParameterChanged = true; 
                PortChanged = true; 
                DisplayException( ModuleName, ex ); 
            } 
            
            return success;             
        } 

        /// <summary>
        ///  Executes when WriteToComPortDelegate1 completes.
        /// </summary>
        /// <param name="ar"> the value returned by the delegate's BeginInvoke method </param> 
                
        internal void WriteCompleted( IAsyncResult ar ) 
        {   
            WriteToComPortDelegate deleg = null; 
            string msg = null; 
            bool success = false; 
            
            //  Extract the value returned by BeginInvoke (optional).
            
            msg = System.Convert.ToString( ar.AsyncState ); 
            
            //  Get the value returned by the delegate.
            
            deleg = ( ( WriteToComPortDelegate )( ( ( AsyncResult )( ar ) ).AsyncDelegate ) ); 
            
            success = deleg.EndInvoke( ar ); 
            
            if ( success ) 
            { 
                if ( null != UserInterfaceData ) UserInterfaceData( "UpdateStatusLabel", "", Color.Black ); 
            } 
            
            //  Add any actions that need to be performed after a write to the COM port completes.
            //  This example displays the value passed to the BeginInvoke method
            //  and the value returned by EndInvoke.
            
            Console.WriteLine( "Write operation began: " + msg ); 
            Console.WriteLine( "Write operation succeeded: " + success );             
        } 

        /// <summary>
        /// Write a string to the SerialPort object selectedPort.
        /// </summary>
        /// 
        /// <param name="textToWrite"> A string to write </param>
                
        internal bool WriteToComPort( string textToWrite ) 
        { 
            bool success = false;
           
            try 
            { 
                //  Open the COM port if necessary.
                
                if ( ( !( ( SelectedPort == null ) ) ) ) 
                { 
                    if ( ( ( !( SelectedPort.IsOpen ) ) | PortChanged ) ) 
                    {                         
                        //  Close the port if needed and open the selected port.
                        
                        PortOpen = OpenComPort();                         
                    } 
                } 
                
                if ( SelectedPort.IsOpen ) 
                { 
                    SelectedPort.Write( textToWrite ); 
                    success = true; 
                }                 
            }           
            catch ( TimeoutException ex ) 
            { 
                DisplayException( ModuleName, ex ); 
                
            } 
            catch ( InvalidOperationException ex ) 
            { 
                DisplayException( ModuleName, ex ); 
                ParameterChanged = true; 
                if ( null != UserInterfaceData ) UserInterfaceData( "DisplayCurrentSettings", "", Color.Black );                 
            } 
            catch ( UnauthorizedAccessException ex ) 
            { 
                DisplayException( ModuleName, ex ); 
                
                //  This exception can occur if the port was removed. 
                //  If the port was open, close it.
                
                CloseComPort( SelectedPort ); 
                ParameterChanged = true; 
                if ( null != UserInterfaceData ) UserInterfaceData( "DisplayCurrentSettings", "", Color.Black );                 
            }             
            return success;             
        }                
    }     
} 
