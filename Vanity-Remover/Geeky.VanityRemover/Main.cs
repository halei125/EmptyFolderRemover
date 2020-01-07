using Geeky.VanityRemover.Core;
using Geeky.VanityRemover.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Geeky.VanityRemover
{
	internal class Main : Form
	{
		private IContainer components;

		private TextBox path;

		private Button browse;

		private FolderBrowserDialog pathDialog;

		private Label dropZoneText;

		private Button start;

		private ProgressBar progressBar;

		private ErrorProvider errorProvider;

		private Button cancel;

		private readonly ICleaner cleaner;

		private readonly IPathValidator pathValidator;

		private bool Running
		{
			set
			{
				progressBar.Style = (value ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks);
				path.Enabled = !value;
				browse.Enabled = !value;
				start.Enabled = (!value && pathValidator.IsValid(path.Text));
				cancel.Enabled = value;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Geeky.VanityRemover.Main));
			path = new System.Windows.Forms.TextBox();
			pathDialog = new System.Windows.Forms.FolderBrowserDialog();
			dropZoneText = new System.Windows.Forms.Label();
			start = new System.Windows.Forms.Button();
			browse = new System.Windows.Forms.Button();
			progressBar = new System.Windows.Forms.ProgressBar();
			errorProvider = new System.Windows.Forms.ErrorProvider(components);
			cancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
			SuspendLayout();
			path.AccessibleDescription = "Type in the path to clean here.";
			path.AccessibleName = "Path";
			path.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			path.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			path.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			errorProvider.SetIconAlignment(path, System.Windows.Forms.ErrorIconAlignment.BottomRight);
			path.Location = new System.Drawing.Point(39, 6);
			path.Name = "path";
			path.Size = new System.Drawing.Size(175, 20);
			path.TabIndex = 1;
			path.TextChanged += new System.EventHandler(PathChanged);
			pathDialog.Description = "Please select the folder to clean:";
			pathDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
			pathDialog.ShowNewFolderButton = false;
			dropZoneText.Anchor = System.Windows.Forms.AnchorStyles.None;
			dropZoneText.AutoSize = true;
			dropZoneText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dropZoneText.ForeColor = System.Drawing.Color.Gray;
			dropZoneText.Location = new System.Drawing.Point(22, 54);
			dropZoneText.Name = "dropZoneText";
			dropZoneText.Size = new System.Drawing.Size(174, 32);
			dropZoneText.TabIndex = 3;
			dropZoneText.Text = "Give me a folder and\r\nthe vanity within will go away";
			dropZoneText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			start.AccessibleDescription = "Starts the cleaning process.";
			start.AccessibleName = "Start";
			start.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			start.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			start.Enabled = false;
			start.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
			start.Image = Geeky.VanityRemover.Properties.Resources.go;
			start.Location = new System.Drawing.Point(187, 111);
			start.Name = "start";
			start.Size = new System.Drawing.Size(27, 24);
			start.TabIndex = 2;
			start.Tag = "";
			start.UseVisualStyleBackColor = true;
			start.Click += new System.EventHandler(StartClicked);
			browse.AccessibleDescription = "Opens up a folder browse dialog for selecting the path to clean.";
			browse.AccessibleName = "Browse";
			browse.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
			browse.Image = Geeky.VanityRemover.Properties.Resources.browse;
			browse.Location = new System.Drawing.Point(6, 3);
			browse.Name = "browse";
			browse.Size = new System.Drawing.Size(27, 24);
			browse.TabIndex = 0;
			browse.UseVisualStyleBackColor = true;
			browse.Click += new System.EventHandler(BrowseClicked);
			progressBar.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			progressBar.Location = new System.Drawing.Point(6, 113);
			progressBar.MarqueeAnimationSpeed = 20;
			progressBar.Name = "progressBar";
			progressBar.Size = new System.Drawing.Size(142, 20);
			progressBar.TabIndex = 8;
			errorProvider.ContainerControl = this;
			cancel.AccessibleDescription = "Cancels the cleaning process.";
			cancel.AccessibleName = "Cancel";
			cancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cancel.Enabled = false;
			cancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
			cancel.Image = Geeky.VanityRemover.Properties.Resources.stop;
			cancel.Location = new System.Drawing.Point(154, 111);
			cancel.Name = "cancel";
			cancel.Size = new System.Drawing.Size(27, 24);
			cancel.TabIndex = 3;
			cancel.Tag = "";
			cancel.UseVisualStyleBackColor = true;
			cancel.Click += new System.EventHandler(CancelClicked);
			base.AcceptButton = start;
			AllowDrop = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = cancel;
			base.ClientSize = new System.Drawing.Size(219, 141);
			base.Controls.Add(cancel);
			base.Controls.Add(progressBar);
			base.Controls.Add(start);
			base.Controls.Add(dropZoneText);
			base.Controls.Add(browse);
			base.Controls.Add(path);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(230, 150);
			base.Name = "Main";
			Text = "Vanity remover";
			base.TopMost = true;
			base.DragDrop += new System.Windows.Forms.DragEventHandler(SomethingDropped);
			base.DragEnter += new System.Windows.Forms.DragEventHandler(SomethingEntered);
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormIsClosing);
			((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		public Main(string initialDirectory)
		{
			InitializeComponent();
			pathValidator = new PathValidator();
			cleaner = new Cleaner
			{
				Context = SynchronizationContext.Current
			};
			cleaner.CleaningDone += CleaningDone;
			path.Text = (initialDirectory ?? "");
			path.SelectionStart = 0;
			base.ActiveControl = ((path.Text == "") ? ((Control)path) : ((Control)start));
			Running = false;
		}

		private void BrowseClicked(object sender, EventArgs e)
		{
			pathDialog.SelectedPath = path.Text;
			if (pathDialog.ShowDialog() == DialogResult.OK)
			{
				path.Text = pathDialog.SelectedPath;
			}
		}

		private void CancelClicked(object sender, EventArgs e)
		{
			cancel.Enabled = false;
			cleaner.Cancel();
		}

		private void StartClicked(object sender, EventArgs e)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path.Text);
			string caption = "Please confirm";
			string text = "All empty folders will be deleted from" + Environment.NewLine + Environment.NewLine + directoryInfo.FullName;
			DialogResult dialogResult = MessageBox.Show(this, text, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
			if (dialogResult == DialogResult.OK)
			{
				Running = true;
				cleaner.Clean(directoryInfo);
			}
		}

		private void CleaningDone(object sender, CleaningDoneEventArgs e)
		{
			Running = false;
			string caption = "Done c'',)";
			string text = string.Format(CultureInfo.InvariantCulture, "{0} folders scanned.{1}{2} folders removed.", e.Total, Environment.NewLine, e.Deleted);
			MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private void SomethingDropped(object sender, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				return;
			}
			string[] array = e.Data.GetData(DataFormats.FileDrop) as string[];
			if (array != null && array.Length >= 1)
			{
				string text = array[0];
				if (pathValidator.IsValid(text))
				{
					path.Text = text;
				}
			}
		}

		private void SomethingEntered(object sender, DragEventArgs e)
		{
			e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None);
		}

		private void PathChanged(object sender, EventArgs e)
		{
			bool flag = pathValidator.IsValid(path.Text);
			start.Enabled = flag;
			path.BackColor = (flag ? Color.AliceBlue : Color.LightCoral);
		}

		private void FormIsClosing(object sender, FormClosingEventArgs e)
		{
			cleaner.Cancel();
		}
	}
}
