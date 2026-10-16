using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ELTE.DocuStat.Model;
using ELTE.DocuStat.Persistence;

namespace ELTE.DocuStat.View
{
    public partial class DocuStatDialog : Form
    {
        

        public DocuStatDialog()
        {
            InitializeComponent();

            openFileDialogMenuItem.Click += OpenDialog;
            resetFilterMenuItem.Click += ResetFilter;

            
        }

        private async void OpenDialog(object? sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog.Filter = "Text files (*.txt)|*.txt|PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    resetFilterMenuItem.Enabled = false;
                    openFileDialogMenuItem.Enabled = false;

                    tabControl.TabPages.Clear();
                    List<Task> tasks = new List<Task>();

                    foreach (string fname in openFileDialog.FileNames)
                    {
                        tasks.Add(AddTabPageAsync(fname));
                    }

                    await Task.WhenAll(tasks);
                    resetFilterMenuItem.Enabled = true;
                    openFileDialogMenuItem.Enabled = true;
                }
            }
        }

        private async Task AddTabPageAsync(string fname)
        {
            IFileManager? fileManager = FileManagerFactory.CreateForPath(fname);

            if (fileManager == null)
            {
                MessageBox.Show(
                    "Unsupported file format",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                DocuStatControl control = new DocuStatControl();
                Task loadTask = control.LoadFileAsync(fileManager);
                TabPage tabPage = new TabPage("Loading...");
                tabPage.Controls.Add(control);
                tabControl.TabPages.Add(tabPage);
                await loadTask;
                tabPage.Text = Path.GetFileName(fname);
            }
            catch (FileManagerException ex)
            {
                Invoke( () => MessageBox.Show(
                    $"File read error:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error) );
                return;
            }
        }

        /// <summary>
        /// Passes the filter set on the user interface to the model.
        /// </summary>
        /// <remarks>
        /// All three properties are assigned, but the model raises an event only for the
        /// values that actually changed, so the list is not rebuilt needlessly.
        /// </remarks>


        private void ResetFilter(object? sender, EventArgs e)
        {
            if (tabControl.SelectedTab == null)
            {
                MessageBox.Show(
                    "No tab page selected!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            (tabControl.SelectedTab.Controls[0] as DocuStatControl)!.ResetFilter();
        }
    }
}
