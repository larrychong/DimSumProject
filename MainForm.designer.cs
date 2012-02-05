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
            this.rtbStatus = new System.Windows.Forms.RichTextBox();
            this.btnPort = new System.Windows.Forms.Button();
            this.btnOpenOrClosePort = new System.Windows.Forms.Button();
            this.tmrLookForPortChanges = new System.Windows.Forms.Timer(this.components);
            this.NumPad1 = new System.Windows.Forms.Label();
            this.NumPad2 = new System.Windows.Forms.Label();
            this.NumPad3 = new System.Windows.Forms.Label();
            this.NumPad4 = new System.Windows.Forms.Label();
            this.NumPad6 = new System.Windows.Forms.Label();
            this.NumPad5 = new System.Windows.Forms.Label();
            this.NumPad7 = new System.Windows.Forms.Label();
            this.NumPad8 = new System.Windows.Forms.Label();
            this.NumPad9 = new System.Windows.Forms.Label();
            this.totalSum = new System.Windows.Forms.Label();
            this.clientAppLabel = new System.Windows.Forms.Label();
            this.OrderListBox = new System.Windows.Forms.ListBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            this.rtbStatus.Size = new System.Drawing.Size(627, 50);
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
            // NumPad1
            // 
            this.NumPad1.AutoSize = true;
            this.NumPad1.BackColor = System.Drawing.Color.White;
            this.NumPad1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumPad1.Location = new System.Drawing.Point(446, 139);
            this.NumPad1.Name = "NumPad1";
            this.NumPad1.Size = new System.Drawing.Size(43, 46);
            this.NumPad1.TabIndex = 13;
            this.NumPad1.Text = "1";
            this.NumPad1.Click += new System.EventHandler(this.NumPad1_Click);
            // 
            // NumPad2
            // 
            this.NumPad2.AutoSize = true;
            this.NumPad2.BackColor = System.Drawing.Color.White;
            this.NumPad2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumPad2.Location = new System.Drawing.Point(489, 139);
            this.NumPad2.Name = "NumPad2";
            this.NumPad2.Size = new System.Drawing.Size(43, 46);
            this.NumPad2.TabIndex = 14;
            this.NumPad2.Text = "2";
            this.NumPad2.Click += new System.EventHandler(this.NumPad2_Click);
            // 
            // NumPad3
            // 
            this.NumPad3.AutoSize = true;
            this.NumPad3.BackColor = System.Drawing.Color.White;
            this.NumPad3.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumPad3.Location = new System.Drawing.Point(532, 139);
            this.NumPad3.Name = "NumPad3";
            this.NumPad3.Size = new System.Drawing.Size(43, 46);
            this.NumPad3.TabIndex = 15;
            this.NumPad3.Text = "3";
            this.NumPad3.Click += new System.EventHandler(this.NumPad3_Click);
            // 
            // NumPad4
            // 
            this.NumPad4.AutoSize = true;
            this.NumPad4.BackColor = System.Drawing.Color.White;
            this.NumPad4.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumPad4.Location = new System.Drawing.Point(446, 185);
            this.NumPad4.Name = "NumPad4";
            this.NumPad4.Size = new System.Drawing.Size(43, 46);
            this.NumPad4.TabIndex = 16;
            this.NumPad4.Text = "4";
            this.NumPad4.Click += new System.EventHandler(this.NumPad4_Click);
            // 
            // NumPad6
            // 
            this.NumPad6.AutoSize = true;
            this.NumPad6.BackColor = System.Drawing.Color.White;
            this.NumPad6.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumPad6.Location = new System.Drawing.Point(532, 185);
            this.NumPad6.Name = "NumPad6";
            this.NumPad6.Size = new System.Drawing.Size(43, 46);
            this.NumPad6.TabIndex = 17;
            this.NumPad6.Text = "6";
            this.NumPad6.Click += new System.EventHandler(this.NumPad6_Click);
            // 
            // NumPad5
            // 
            this.NumPad5.AutoSize = true;
            this.NumPad5.BackColor = System.Drawing.Color.White;
            this.NumPad5.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumPad5.Location = new System.Drawing.Point(489, 185);
            this.NumPad5.Name = "NumPad5";
            this.NumPad5.Size = new System.Drawing.Size(43, 46);
            this.NumPad5.TabIndex = 17;
            this.NumPad5.Text = "5";
            this.NumPad5.Click += new System.EventHandler(this.NumPad5_Click);
            // 
            // NumPad7
            // 
            this.NumPad7.AutoSize = true;
            this.NumPad7.BackColor = System.Drawing.Color.White;
            this.NumPad7.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumPad7.Location = new System.Drawing.Point(446, 231);
            this.NumPad7.Name = "NumPad7";
            this.NumPad7.Size = new System.Drawing.Size(43, 46);
            this.NumPad7.TabIndex = 18;
            this.NumPad7.Text = "7";
            this.NumPad7.Click += new System.EventHandler(this.NumPad7_Click);
            // 
            // NumPad8
            // 
            this.NumPad8.AutoSize = true;
            this.NumPad8.BackColor = System.Drawing.Color.White;
            this.NumPad8.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumPad8.Location = new System.Drawing.Point(489, 231);
            this.NumPad8.Name = "NumPad8";
            this.NumPad8.Size = new System.Drawing.Size(43, 46);
            this.NumPad8.TabIndex = 19;
            this.NumPad8.Text = "8";
            this.NumPad8.Click += new System.EventHandler(this.NumPad8_Click);
            // 
            // NumPad9
            // 
            this.NumPad9.AutoSize = true;
            this.NumPad9.BackColor = System.Drawing.Color.White;
            this.NumPad9.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumPad9.Location = new System.Drawing.Point(532, 229);
            this.NumPad9.Name = "NumPad9";
            this.NumPad9.Size = new System.Drawing.Size(43, 46);
            this.NumPad9.TabIndex = 20;
            this.NumPad9.Text = "9";
            this.NumPad9.Click += new System.EventHandler(this.NumPad9_Click);
            // 
            // totalSum
            // 
            this.totalSum.AutoSize = true;
            this.totalSum.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalSum.Location = new System.Drawing.Point(390, 319);
            this.totalSum.Name = "totalSum";
            this.totalSum.Size = new System.Drawing.Size(0, 46);
            this.totalSum.TabIndex = 21;
            // 
            // clientAppLabel
            // 
            this.clientAppLabel.AutoSize = true;
            this.clientAppLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientAppLabel.Location = new System.Drawing.Point(12, 9);
            this.clientAppLabel.Name = "clientAppLabel";
            this.clientAppLabel.Size = new System.Drawing.Size(276, 31);
            this.clientAppLabel.TabIndex = 22;
            this.clientAppLabel.Text = "CLIENT APP DEMO";
            // 
            // OrderListBox
            // 
            this.OrderListBox.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OrderListBox.FormattingEnabled = true;
            this.OrderListBox.ItemHeight = 29;
            this.OrderListBox.Location = new System.Drawing.Point(14, 92);
            this.OrderListBox.Name = "OrderListBox";
            this.OrderListBox.Size = new System.Drawing.Size(298, 236);
            this.OrderListBox.TabIndex = 23;
            this.OrderListBox.SelectedIndexChanged += new System.EventHandler(this.OrderListBox_SelectedIndexChanged);
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.Color.LightGray;
            this.CancelButton.Location = new System.Drawing.Point(418, 52);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(180, 70);
            this.CancelButton.TabIndex = 24;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButtont_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 523);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OrderListBox);
            this.Controls.Add(this.clientAppLabel);
            this.Controls.Add(this.totalSum);
            this.Controls.Add(this.NumPad9);
            this.Controls.Add(this.NumPad8);
            this.Controls.Add(this.NumPad7);
            this.Controls.Add(this.NumPad5);
            this.Controls.Add(this.NumPad6);
            this.Controls.Add(this.NumPad4);
            this.Controls.Add(this.NumPad3);
            this.Controls.Add(this.NumPad2);
            this.Controls.Add(this.NumPad1);
            this.Controls.Add(this.btnOpenOrClosePort);
            this.Controls.Add(this.btnPort);
            this.Controls.Add(this.rtbStatus);
            this.MaximumSize = new System.Drawing.Size(2000, 2000);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Dim Sum Tracker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.RichTextBox rtbStatus; 
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.Button btnPort; 
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.Button btnOpenOrClosePort;
        internal /* TRANSINFO: WithEvents */ System.Windows.Forms.Timer tmrLookForPortChanges;
        private Label NumPad1;
        private Label NumPad2;
        private Label NumPad3;
        private Label NumPad4;
        private Label NumPad6;
        private Label NumPad5;
        private Label NumPad7;
        private Label NumPad8;
        private Label NumPad9;
        private Label totalSum;
        private Label clientAppLabel;
        internal ListBox OrderListBox;
        private Button CancelButton; 
        
        
    } 
    
    
} 