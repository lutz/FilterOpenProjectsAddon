using SwissAcademic.Citavi.Shell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FilterOpenProjectsAddon
{
    internal static class Extensions
    {
        public static SwissAcademic.Controls.TabControl GetTabControl(this OpenProjectDialog openProjectDialog)
        {
            return openProjectDialog.Controls.OfType<SwissAcademic.Controls.TabControl>().FirstOrDefault();
        }

        public static TextBox GetTextBox(this OpenProjectDialog openProjectDialog)
        {
            return openProjectDialog.Controls.OfType<TextBox>().FirstOrDefault();
        }

        public static IEnumerable<Control> GetKnownProjectsControls(this OpenProjectDialog openProjectDialog)
        {
            var list = openProjectDialog.GetType().GetProperty("SelectedControlCollection", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(openProjectDialog) as IList;
            return list.Cast<Control>().ToList();
        }

        public static void SetPlaceholder(this TextBox textBox, string placeHolder)
        {
            if (textBox.IsHandleCreated && placeHolder != null)
            {
                SendMessage(textBox.Handle, 0x1501, (IntPtr)1, placeHolder);
            }
        }
        // PInvoke
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, string lp);
    }
}
