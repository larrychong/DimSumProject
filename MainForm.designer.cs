using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
namespace COMPortTerminal
{
   
    [ global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated() ]
    public partial class MainForm : System.Windows.Forms.Form 
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
            this.components = new System.ComponentModel.Container();
            this.rtbMonitor = new System.Windows.Forms.RichTextBox();
            this.rtbStatus = new System.Windows.Forms.RichTextBox();
            this.btnPort = new System.Windows.Forms.Button();
            this.btnOpenOrClosePort = new System.Windows.Forms.Button();
            this.tmrLookForPortChanges = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // rtbMonitor
            // 
            this.rtbMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbMonitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMonitor.Location = new System.Drawing.Point(24, 44);
            this.rtbMonitor.Name = "rtbMonitor";
            this.rtbMonitor.Size = new System.Drawing.Size(426, 329);
            this.rtbMonitor.TabIndex = 7;
            this.rtbMonitor.Text = "";
            // 
            // rtbStatus
            // 
            this.rtbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbStatus.BackColor = System.Drawing.SystemColors.Control;
            this.rtbStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbStatus.Location = new System.Drawing.Point(14, 422);
            this.rtbStatus.Margin = new System.Windows.Forms.Padding(5);
            this.rtbStatus.Name = "rtbStatus";
            this.rtbStatus.ReadOnly = true;
            this.rtbStatus.Size = new System.Drawing.Size(453, 50);
            this.rtbStatus.TabIndex = 8;
            this.rtbStatus.Text = "";
            // 
            // btnPort
            // 
            this.btnPort.AutoSize = true;
            this.btnPort.Location = new System.Drawing.Point(286, 461);
            this.btnPort.Name = "btnPort";
            this.btnPort.Size = new System.Drawing.Size(77, 27);
            this.btnPort.TabIndex = 10;
            this.btnPort.Text = "Port Settings";
            this.btnPort.UseVisualStyleBackColor = true;
            // 
            // btnOpenOrClosePort
            // 
            this.btnOpenOrClosePort.AutoSize = true;
            this.btnOpenOrClosePort.Location = new System.Drawing.Point(380, 461);
            this.btnOpenOrClosePort.Name = "btnOpenOrClosePort";
            this.btnOpenOrClosePort.Size = new System.Drawing.Size(95, 27);
            this.btnOpenOrClosePort.TabIndex = 11;
            this.btnOpenOrClosePort.Text = "Open COM Port";
            this.btnOpenOrClosePort.UseVisualStyleBackColor = true;
            // 
            // tmrLookForPortChanges
            // 
            this.tmrLookForPortChanges.Interval = 1000;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 523);
            this.Controls.Add(this.btnOpenOrClosePort);
            this.Controls.Add(this.btnPort);
            this.Controls.Add(this.rtbStatus);
            this.Controls.Add(this.rtbMonitor);
            this.MaximumSize = new System.Drawing.Size(2000, 2000);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Dim Sum Tracker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        } 
        
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.RichTextBox rtbMonitor;
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.RichTextBox rtbStatus; 
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.Button btnPort; 
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.Button btnOpenOrClosePort; 
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.Timer tmrLookForPortChanges; 
        
        
    } 
    
    
} 
