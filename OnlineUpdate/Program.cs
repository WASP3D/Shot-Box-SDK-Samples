using BeeSys.Wasp3D.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OnlineUpdate
{
    static class Program
    {
       

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WAssemblyManager.AddDefaultPath();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmOnline());
        }
    }
}
