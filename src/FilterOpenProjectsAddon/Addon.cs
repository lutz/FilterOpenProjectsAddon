using FilterOpenProjectsAddon.Properties;
using SwissAcademic.Citavi.Shell;
using SwissAcademic.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FilterOpenProjectsAddon
{
    public class Addon : CitaviAddOnEx<OpenProjectDialog>
    {
        public override void OnHostingFormLoaded(OpenProjectDialog openProjectDialog)
        {
            if (openProjectDialog.GetTabControl() is SwissAcademic.Controls.TabControl tabControl)
            {
                openProjectDialog.FormClosed += OpenProjectDialog_FormClosed;
                tabControl.Size = new Size(tabControl.Size.Width, tabControl.Size.Height - Control2.ScaleY(30));
                tabControl.SelectedTabChanged += TabControl_SelectedTabChanged;

                var textBox = new TextBox();
                textBox.Size = new Size(tabControl.Size.Width, textBox.Size.Height);
                textBox.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                textBox.Left = tabControl.Left;
                textBox.Top = tabControl.Top;
                textBox.Parent = openProjectDialog;
                textBox.SetPlaceholder(Resources.TextBox_Placeholder);
                textBox.TextChanged += TextBox_TextChanged;
                openProjectDialog.Controls.Add(textBox);
                tabControl.Location = new Point(tabControl.Left, tabControl.Top + Control2.ScaleY(30));

                openProjectDialog.PerformLayout();
                textBox.Focus();


            }
        }

        private void TabControl_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            if (sender is SwissAcademic.Controls.TabControl tabControl && tabControl.Parent is OpenProjectDialog openProjectDialog)
            {
                TextBox_TextChanged(openProjectDialog.GetTextBox(), EventArgs.Empty);
            }
        }

        private void OpenProjectDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is OpenProjectDialog openProjectDialog)
            {
                openProjectDialog.FormClosed -= OpenProjectDialog_FormClosed;
                if (openProjectDialog.GetTabControl() is SwissAcademic.Controls.TabControl tabControl)
                {
                    tabControl.SelectedTabChanged -= TabControl_SelectedTabChanged;
                }
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox && textBox.Parent is OpenProjectDialog openProjectDialog)
            {
                foreach (var control in openProjectDialog.GetKnownProjectsControls())
                {
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        control.Show();
                    }
                    else if (control.ToString().Contains(textBox.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        control.Show();
                    }
                    else
                    {
                        control.Hide();
                    }
                }
            }
        }
    }
}