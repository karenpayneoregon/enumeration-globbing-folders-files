using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace HeadFootSearchingStyles.Classes
{
    public static class Dialogs
    {
        /// <summary>
        /// Simple message box wrapper to ask a question where the default button is No.
        /// </summary>
        /// <param name="text">Question text</param>
        /// <returns>true or false</returns>
        public static bool Question(string text)
        {

            return (MessageBox.Show(
                text,
                Application.ProductName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes);

        }

        /// <summary>
        /// Present a dialog of type Information with option to set the single button text
        /// </summary>
        /// <param name="text">text to display</param>
        public static void InformationalMessage(string text)
        {
            
            var page = new TaskDialogPage()
            {
                Caption = "Information",
                Heading = text,
                Buttons = { new TaskDialogButton("Got it!!!") }
            };

            TaskDialog.ShowDialog(page);

        }
        /// <summary>
        /// Present a dialog of type Error with option to set the single button text
        /// </summary>
        /// <param name="owner">calling form</param>
        /// <param name="text">text to display</param>
        /// <param name="buttonText">button text</param>
        public static void Alert(Form owner, string text, string buttonText = "Got it!!!")
        {

            var page = new TaskDialogPage()
            {
                Caption = "Information",
                Heading = text,
                Buttons = { new TaskDialogButton(buttonText) }, 
                Icon = TaskDialogIcon.Error
            };

            TaskDialog.ShowDialog(owner, page);

        }

        public static void OpenAlert(Form owner, string text, string buttonText, string fileName)
        {

            var openButton = new TaskDialogButton("Open");

            openButton.Click += delegate
            {
                if (File.Exists(fileName))
                {
                    Process.Start("notepad.exe", fileName);
                }
            };

            var page = new TaskDialogPage()
            {
                Caption = "Information",
                Heading = text,
                Buttons = { new TaskDialogButton(buttonText), openButton},
                Icon = TaskDialogIcon.None
            };

            TaskDialog.ShowDialog(owner, page);
        }
    }
}