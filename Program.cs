
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace COMPortTerminal
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }          
    }
}

