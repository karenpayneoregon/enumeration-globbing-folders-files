using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace HeadFootSearchingStyles.Classes
{
    public static class ControlExtensions
    {
        /// <summary>
        /// Proactively prevent cross threading violations
        /// </summary>
        /// <typeparam name="T">Control type</typeparam>
        /// <param name="control">Control</param>
        /// <param name="action">Action to invoke on control</param>
        /// <example>
        /// <code title="Update ListBox from a secondary thread" >
        ///    private void DataOperationsOnAfterConnectMonitor(string message)
        ///    {
        ///        listBox1.InvokeIfRequired(lb => { lb.Items.Add(message); });
        ///    }
        /// </code>
        /// </example>        
        public static void InvokeIfRequired<T>(this T control, Action<T> action) where T : ISynchronizeInvoke
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => action(control)), null);
            }
            else
            {
                action(control);
            }
        }

        /// <summary>
        /// Get all checked items
        /// </summary>
        /// <param name="source">CheckedListBox</param>
        /// <returns>List of items checked</returns>
        public static List<string> CheckedList(this CheckedListBox source)
            => source.Items.Cast<string>()
                .Where((item, index) => source.GetItemChecked(index))
                .Select(item => item)
                .ToList();


        /// <summary>
        /// Provides the ability to remove selected rows in detail view.
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static List<ListViewItem> SelectedRows(this ListView.ListViewItemCollection sender)
        {
            return sender.Cast<ListViewItem>()
                .Where(listViewItem => listViewItem.Selected)
                .Select(listViewItem => listViewItem)
                .ToList();
        }

    }
}