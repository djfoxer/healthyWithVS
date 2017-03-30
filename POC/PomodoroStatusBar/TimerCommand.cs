//------------------------------------------------------------------------------
// <copyright file="TimerCommand.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.Drawing;
using PomodoroStatusBar.Resources;

using Extensibility;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.CommandBars;
using System.Windows.Controls;
using System.Windows;

namespace PomodoroStatusBar
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class TimerCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("9543be81-1e8e-484a-a203-4e19b96300b5");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private TimerCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static TimerCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new TimerCommand(package);
        }

        private DTE2 _applicationObject;

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            string title = "TimerCommand";

            IVsStatusbar statusBar = (IVsStatusbar)ServiceProvider.GetService(typeof(SVsStatusbar));



            // Use the standard Visual Studio icon for building.  
            object icon = (short)Microsoft.VisualStudio.Shell.Interop.Constants.SBAI_Print;

            // Display the icon in the Animation region.  
            statusBar.Animation(1, ref icon);

            // The message box pauses execution for you to look at the animation.  
            // System.Windows.Forms.MessageBox.Show("showing?");

            // Stop the animation.   
            statusBar.Animation(0, ref icon);


            //   IVsStatusbar statusBar = (IVsStatusbar)GetService(typeof(SVsStatusbar));

            Bitmap b = new Bitmap(Resource.tomMini);

            IntPtr hdc = IntPtr.Zero;
            hdc = b.GetHbitmap();

            object hdcObject = (object)hdc;

            statusBar.Animation(1, ref hdcObject);

            //System.Windows.Forms.MessageBox.Show(
            //   "Click OK to end status bar animation.");

            //statusBar.Animation(0, ref hdcObject);
            //DeleteObject(hdc);


            //DeleteObject(hdc);


            var x = ServiceProvider.GetService(typeof(Microsoft.VisualStudio.Shell.Interop.SDTE)) as EnvDTE80.DTE2;

            var statusBarObj = UIHelper.FindChildControl<DockPanel>(Application.Current.MainWindow,
                "StatusBarPanel");
            var button = new Button();
            button.Content = "Klikamy!";
            statusBarObj.Children.Insert(0, button);


        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
    }
}
