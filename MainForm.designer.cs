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
            this.totalSum = new System.Windows.Forms.Label();
            this.clientAppLabel = new System.Windows.Forms.Label();
            this.OrderListBox = new System.Windows.Forms.ListBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.Numpad0 = new System.Windows.Forms.Button();
            this.NumpadCLR = new System.Windows.Forms.Button();
            this.Numpad9 = new System.Windows.Forms.Button();
            this.Numpad8 = new System.Windows.Forms.Button();
            this.Numpad7 = new System.Windows.Forms.Button();
            this.Numpad6 = new System.Windows.Forms.Button();
            this.Numpad3 = new System.Windows.Forms.Button();
            this.Numpad5 = new System.Windows.Forms.Button();
            this.Numpad4 = new System.Windows.Forms.Button();
            this.Numpad2 = new System.Windows.Forms.Button();
            this.Numpad1 = new System.Windows.Forms.Button();
            this.but_Bill = new System.Windows.Forms.Button();
            this.teaButton = new System.Windows.Forms.Button();
            this.waiterButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbStatus
            // 
            this.rtbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbStatus.BackColor = System.Drawing.SystemColors.Control;
            this.rtbStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbStatus.Location = new System.Drawing.Point(14, 620);
            this.rtbStatus.Margin = new System.Windows.Forms.Padding(5);
            this.rtbStatus.Name = "rtbStatus";
            this.rtbStatus.ReadOnly = true;
            this.rtbStatus.Size = new System.Drawing.Size(466, 50);
            this.rtbStatus.TabIndex = 8;
            this.rtbStatus.Text = "error";
            // 
            // btnPort
            // 
            this.btnPort.AutoSize = true;
            this.btnPort.Location = new System.Drawing.Point(511, 621);
            this.btnPort.Name = "btnPort";
            this.btnPort.Size = new System.Drawing.Size(77, 27);
            this.btnPort.TabIndex = 10;
            this.btnPort.Text = "Port Settings";
            this.btnPort.UseVisualStyleBackColor = true;
            // 
            // btnOpenOrClosePort
            // 
            this.btnOpenOrClosePort.AutoSize = true;
            this.btnOpenOrClosePort.Location = new System.Drawing.Point(493, 654);
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
            // totalSum
            // 
            this.totalSum.AutoSize = true;
            this.totalSum.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalSum.Location = new System.Drawing.Point(110, 525);
            this.totalSum.Name = "totalSum";
            this.totalSum.Size = new System.Drawing.Size(84, 46);
            this.totalSum.TabIndex = 21;
            this.totalSum.Text = "test";
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
            this.OrderListBox.Location = new System.Drawing.Point(14, 73);
            this.OrderListBox.Name = "OrderListBox";
            this.OrderListBox.Size = new System.Drawing.Size(307, 439);
            this.OrderListBox.TabIndex = 23;
            this.OrderListBox.SelectedIndexChanged += new System.EventHandler(this.OrderListBox_SelectedIndexChanged);
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.Color.LightGray;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CancelButton.Location = new System.Drawing.Point(350, 93);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(211, 89);
            this.CancelButton.TabIndex = 24;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButtont_Click);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Enabled = false;
            this.txtQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtQuantity.Location = new System.Drawing.Point(350, 246);
            this.txtQuantity.MaxLength = 2;
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(191, 53);
            this.txtQuantity.TabIndex = 48;
            // 
            // Numpad0
            // 
            this.Numpad0.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Numpad0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Numpad0.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.Numpad0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Numpad0.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Numpad0.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.Numpad0.Location = new System.Drawing.Point(412, 439);
            this.Numpad0.Name = "Numpad0";
            this.Numpad0.Size = new System.Drawing.Size(65, 48);
            this.Numpad0.TabIndex = 47;
            this.Numpad0.Text = "0";
            this.Numpad0.UseVisualStyleBackColor = false;
            this.Numpad0.Click += new System.EventHandler(this.Numpad0_Click);
            // 
            // NumpadCLR
            // 
            this.NumpadCLR.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.NumpadCLR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.NumpadCLR.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.NumpadCLR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NumpadCLR.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NumpadCLR.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.NumpadCLR.Location = new System.Drawing.Point(350, 439);
            this.NumpadCLR.Name = "NumpadCLR";
            this.NumpadCLR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.NumpadCLR.Size = new System.Drawing.Size(65, 48);
            this.NumpadCLR.TabIndex = 46;
            this.NumpadCLR.Text = "CLR";
            this.NumpadCLR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.NumpadCLR.UseVisualStyleBackColor = false;
            this.NumpadCLR.Click += new System.EventHandler(this.NumpadCLR_Click);
            // 
            // Numpad9
            // 
            this.Numpad9.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Numpad9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Numpad9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.Numpad9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Numpad9.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Numpad9.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.Numpad9.Location = new System.Drawing.Point(476, 392);
            this.Numpad9.Name = "Numpad9";
            this.Numpad9.Size = new System.Drawing.Size(65, 48);
            this.Numpad9.TabIndex = 45;
            this.Numpad9.Text = "9";
            this.Numpad9.UseVisualStyleBackColor = false;
            this.Numpad9.Click += new System.EventHandler(this.Numpad9_Click);
            // 
            // Numpad8
            // 
            this.Numpad8.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Numpad8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Numpad8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.Numpad8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Numpad8.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Numpad8.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.Numpad8.Location = new System.Drawing.Point(412, 392);
            this.Numpad8.Name = "Numpad8";
            this.Numpad8.Size = new System.Drawing.Size(65, 48);
            this.Numpad8.TabIndex = 44;
            this.Numpad8.Text = "8";
            this.Numpad8.UseVisualStyleBackColor = false;
            this.Numpad8.Click += new System.EventHandler(this.Numpad8_Click);
            // 
            // Numpad7
            // 
            this.Numpad7.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Numpad7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Numpad7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.Numpad7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Numpad7.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Numpad7.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.Numpad7.Location = new System.Drawing.Point(350, 392);
            this.Numpad7.Name = "Numpad7";
            this.Numpad7.Size = new System.Drawing.Size(65, 48);
            this.Numpad7.TabIndex = 43;
            this.Numpad7.Text = "7";
            this.Numpad7.UseVisualStyleBackColor = false;
            this.Numpad7.Click += new System.EventHandler(this.Numpad7_Click);
            // 
            // Numpad6
            // 
            this.Numpad6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Numpad6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Numpad6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.Numpad6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Numpad6.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Numpad6.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.Numpad6.Location = new System.Drawing.Point(476, 345);
            this.Numpad6.Name = "Numpad6";
            this.Numpad6.Size = new System.Drawing.Size(65, 48);
            this.Numpad6.TabIndex = 42;
            this.Numpad6.Text = "6";
            this.Numpad6.UseVisualStyleBackColor = false;
            this.Numpad6.Click += new System.EventHandler(this.Numpad6_Click);
            // 
            // Numpad3
            // 
            this.Numpad3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Numpad3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Numpad3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.Numpad3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Numpad3.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Numpad3.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.Numpad3.Location = new System.Drawing.Point(476, 299);
            this.Numpad3.Name = "Numpad3";
            this.Numpad3.Size = new System.Drawing.Size(65, 48);
            this.Numpad3.TabIndex = 41;
            this.Numpad3.Text = "3";
            this.Numpad3.UseVisualStyleBackColor = false;
            this.Numpad3.Click += new System.EventHandler(this.Numpad3_Click);
            // 
            // Numpad5
            // 
            this.Numpad5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Numpad5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Numpad5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.Numpad5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Numpad5.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Numpad5.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.Numpad5.Location = new System.Drawing.Point(412, 345);
            this.Numpad5.Name = "Numpad5";
            this.Numpad5.Size = new System.Drawing.Size(65, 48);
            this.Numpad5.TabIndex = 40;
            this.Numpad5.Text = "5";
            this.Numpad5.UseVisualStyleBackColor = false;
            this.Numpad5.Click += new System.EventHandler(this.Numpad5_Click);
            // 
            // Numpad4
            // 
            this.Numpad4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Numpad4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Numpad4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.Numpad4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Numpad4.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Numpad4.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.Numpad4.Location = new System.Drawing.Point(350, 345);
            this.Numpad4.Name = "Numpad4";
            this.Numpad4.Size = new System.Drawing.Size(65, 48);
            this.Numpad4.TabIndex = 39;
            this.Numpad4.Text = "4";
            this.Numpad4.UseVisualStyleBackColor = false;
            this.Numpad4.Click += new System.EventHandler(this.Numpad4_Click);
            // 
            // Numpad2
            // 
            this.Numpad2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Numpad2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Numpad2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.Numpad2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Numpad2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Numpad2.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.Numpad2.Location = new System.Drawing.Point(412, 299);
            this.Numpad2.Name = "Numpad2";
            this.Numpad2.Size = new System.Drawing.Size(65, 48);
            this.Numpad2.TabIndex = 38;
            this.Numpad2.Text = "2";
            this.Numpad2.UseVisualStyleBackColor = false;
            this.Numpad2.Click += new System.EventHandler(this.Numpad2_Click);
            // 
            // Numpad1
            // 
            this.Numpad1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Numpad1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Numpad1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.Numpad1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Numpad1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Numpad1.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.Numpad1.Location = new System.Drawing.Point(350, 299);
            this.Numpad1.Name = "Numpad1";
            this.Numpad1.Size = new System.Drawing.Size(65, 48);
            this.Numpad1.TabIndex = 37;
            this.Numpad1.Text = "1";
            this.Numpad1.UseVisualStyleBackColor = false;
            this.Numpad1.Click += new System.EventHandler(this.Numpad1_Click);
            // 
            // but_Bill
            // 
            this.but_Bill.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.but_Bill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.but_Bill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.but_Bill.Font = new System.Drawing.Font("Microsoft Sans Serif", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.but_Bill.Location = new System.Drawing.Point(423, 525);
            this.but_Bill.Name = "but_Bill";
            this.but_Bill.Size = new System.Drawing.Size(138, 75);
            this.but_Bill.TabIndex = 49;
            this.but_Bill.Text = "Bill";
            this.but_Bill.UseVisualStyleBackColor = false;
            this.but_Bill.Click += new System.EventHandler(this.but_Bill_Click);
            // 
            // teaButton
            // 
            this.teaButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.teaButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.teaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.teaButton.Location = new System.Drawing.Point(402, 12);
            this.teaButton.Name = "teaButton";
            this.teaButton.Size = new System.Drawing.Size(75, 66);
            this.teaButton.TabIndex = 51;
            this.teaButton.Text = "Request for Tea";
            this.teaButton.UseVisualStyleBackColor = false;
            this.teaButton.Click += new System.EventHandler(this.teaButton_Click);
            // 
            // waiterButton
            // 
            this.waiterButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.waiterButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise;
            this.waiterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.waiterButton.Location = new System.Drawing.Point(486, 12);
            this.waiterButton.Name = "waiterButton";
            this.waiterButton.Size = new System.Drawing.Size(75, 66);
            this.waiterButton.TabIndex = 52;
            this.waiterButton.Text = "Request for Waiter";
            this.waiterButton.UseVisualStyleBackColor = false;
            this.waiterButton.Click += new System.EventHandler(this.waiterButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(595, 689);
            this.Controls.Add(this.waiterButton);
            this.Controls.Add(this.teaButton);
            this.Controls.Add(this.but_Bill);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.Numpad0);
            this.Controls.Add(this.NumpadCLR);
            this.Controls.Add(this.Numpad9);
            this.Controls.Add(this.Numpad8);
            this.Controls.Add(this.Numpad7);
            this.Controls.Add(this.Numpad6);
            this.Controls.Add(this.Numpad3);
            this.Controls.Add(this.Numpad5);
            this.Controls.Add(this.Numpad4);
            this.Controls.Add(this.Numpad2);
            this.Controls.Add(this.Numpad1);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OrderListBox);
            this.Controls.Add(this.clientAppLabel);
            this.Controls.Add(this.totalSum);
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
        private Label totalSum;
        private Label clientAppLabel;
        internal ListBox OrderListBox;
        private Button CancelButton;
        private TextBox txtQuantity;
        private Button Numpad0;
        private Button NumpadCLR;
        private Button Numpad9;
        private Button Numpad8;
        private Button Numpad7;
        private Button Numpad6;
        private Button Numpad3;
        private Button Numpad5;
        private Button Numpad4;
        private Button Numpad2;
        private Button Numpad1;
        private Button but_Bill;
        private Button teaButton;
        private Button waiterButton; 
        
        
    } 
    
    
} 