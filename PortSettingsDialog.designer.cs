using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
namespace COMPortTerminal
{
  
    [ global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated() ]
    public partial class PortSettingsDialog : System.Windows.Forms.Form 
    { 
        
        // Form overrides dispose to clean up the component list.
        [ System.Diagnostics.DebuggerNonUserCode() ]
        protected override void Dispose( bool disposing ) 
        { 
            if ( disposing && components != null ) 
            { 
                components.Dispose(); 
            } 
            base.Dispose( disposing ); 
        } 
        
        
        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components; 
        
        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [ System.Diagnostics.DebuggerStepThrough() ]
        private void InitializeComponent() 
        { 
            this.gbxPort = new System.Windows.Forms.GroupBox(); 
            this.cmbPort = new System.Windows.Forms.ComboBox(); 
            this.gbxBitRate = new System.Windows.Forms.GroupBox(); 
            this.cmbBitRate = new System.Windows.Forms.ComboBox(); 
            this.btnOK = new System.Windows.Forms.Button(); 
            this.gbxHandshaking = new System.Windows.Forms.GroupBox(); 
            this.cmbHandshaking = new System.Windows.Forms.ComboBox(); 
            this.chkOpenComPortOnStartup = new System.Windows.Forms.CheckBox(); 
            this.btnCancel = new System.Windows.Forms.Button(); 
            this.gbxPort.SuspendLayout(); 
            this.gbxBitRate.SuspendLayout(); 
            this.gbxHandshaking.SuspendLayout(); 
            this.SuspendLayout(); 
            // 
            // gbxPort
            // 
            this.gbxPort.Controls.Add( this.cmbPort ); 
            this.gbxPort.Location = new System.Drawing.Point( 15, 25 ); 
            this.gbxPort.Name = "gbxPort"; 
            this.gbxPort.Size = new System.Drawing.Size( 123, 65 ); 
            this.gbxPort.TabIndex = 0; 
            this.gbxPort.TabStop = false; 
            this.gbxPort.Text = "Port"; 
            // 
            // cmbPort
            // 
            this.cmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; 
            this.cmbPort.FormattingEnabled = true; 
            this.cmbPort.Location = new System.Drawing.Point( 10, 23 ); 
            this.cmbPort.Name = "cmbPort"; 
            this.cmbPort.Size = new System.Drawing.Size( 101, 21 ); 
            this.cmbPort.TabIndex = 0; 
            // 
            // gbxBitRate
            // 
            this.gbxBitRate.Controls.Add( this.cmbBitRate ); 
            this.gbxBitRate.Location = new System.Drawing.Point( 160, 25 ); 
            this.gbxBitRate.Name = "gbxBitRate"; 
            this.gbxBitRate.Size = new System.Drawing.Size( 124, 65 ); 
            this.gbxBitRate.TabIndex = 1; 
            this.gbxBitRate.TabStop = false; 
            this.gbxBitRate.Text = "Bit Rate"; 
            // 
            // cmbBitRate
            // 
            this.cmbBitRate.FormattingEnabled = true; 
            this.cmbBitRate.Location = new System.Drawing.Point( 8, 27 ); 
            this.cmbBitRate.Name = "cmbBitRate"; 
            this.cmbBitRate.Size = new System.Drawing.Size( 102, 21 ); 
            this.cmbBitRate.TabIndex = 0; 
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK; 
            this.btnOK.Location = new System.Drawing.Point( 168, 224 ); 
            this.btnOK.Name = "btnOK"; 
            this.btnOK.Size = new System.Drawing.Size( 114, 50 ); 
            this.btnOK.TabIndex = 3; 
            this.btnOK.Text = "OK"; 
            // 
            // gbxHandshaking
            // 
            this.gbxHandshaking.Controls.Add( this.cmbHandshaking ); 
            this.gbxHandshaking.Location = new System.Drawing.Point( 12, 106 ); 
            this.gbxHandshaking.Name = "gbxHandshaking"; 
            this.gbxHandshaking.Size = new System.Drawing.Size( 272, 65 ); 
            this.gbxHandshaking.TabIndex = 5; 
            this.gbxHandshaking.TabStop = false; 
            this.gbxHandshaking.Text = "Handshaking"; 
            // 
            // cmbHandshaking
            // 
            this.cmbHandshaking.FormattingEnabled = true; 
            this.cmbHandshaking.Location = new System.Drawing.Point( 13, 28 ); 
            this.cmbHandshaking.Name = "cmbHandshaking"; 
            this.cmbHandshaking.Size = new System.Drawing.Size( 248, 21 ); 
            this.cmbHandshaking.TabIndex = 0; 
            // 
            // chkOpenComPortOnStartup
            // 
            this.chkOpenComPortOnStartup.AutoSize = true; 
            this.chkOpenComPortOnStartup.Location = new System.Drawing.Point( 15, 187 ); 
            this.chkOpenComPortOnStartup.Name = "chkOpenComPortOnStartup"; 
            this.chkOpenComPortOnStartup.Size = new System.Drawing.Size( 150, 17 ); 
            this.chkOpenComPortOnStartup.TabIndex = 6; 
            this.chkOpenComPortOnStartup.Text = "Open COM port on startup"; 
            this.chkOpenComPortOnStartup.UseVisualStyleBackColor = true; 
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point( 12, 224 ); 
            this.btnCancel.Name = "btnCancel"; 
            this.btnCancel.Size = new System.Drawing.Size( 114, 50 ); 
            this.btnCancel.TabIndex = 7; 
            this.btnCancel.Text = "Cancel"; 
            this.btnCancel.UseVisualStyleBackColor = true; 
            // 
            // PortSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6.0F, 13.0F ); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
            this.ClientSize = new System.Drawing.Size( 306, 286 ); 
            this.ControlBox = false; 
            this.Controls.Add( this.btnCancel ); 
            this.Controls.Add( this.chkOpenComPortOnStartup ); 
            this.Controls.Add( this.gbxHandshaking ); 
            this.Controls.Add( this.btnOK ); 
            this.Controls.Add( this.gbxBitRate ); 
            this.Controls.Add( this.gbxPort ); 
            this.MaximizeBox = false; 
            this.MinimizeBox = false; 
            this.Name = "PortSettingsDialog"; 
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; 
            this.Text = "PortSettings"; 
            this.gbxPort.ResumeLayout( false ); 
            this.gbxBitRate.ResumeLayout( false ); 
            this.gbxHandshaking.ResumeLayout( false ); 
            this.ResumeLayout( false ); 
            this.PerformLayout(); 
            
        } 
        
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.GroupBox gbxPort; 
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.GroupBox gbxBitRate; 
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.ComboBox cmbPort; 
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.ComboBox cmbBitRate; 
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.Button btnOK; 
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.GroupBox gbxHandshaking; 
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.ComboBox cmbHandshaking; 
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.CheckBox chkOpenComPortOnStartup; 
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.Button btnCancel; 
    } 
    
    
} 
