using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;
using MiniHttpd;

namespace MiniHttpdApp
{
	/// <summary>
	/// Summary description for TransferMonitor.
	/// </summary>
	public class TransferMonitorForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView transferView;
		private System.Windows.Forms.ColumnHeader clientColumn;
		private System.Windows.Forms.ColumnHeader lastRequestColumn;
		private System.Windows.Forms.ColumnHeader progressColumn;
		private System.Windows.Forms.ColumnHeader speedColumn;
		private System.Windows.Forms.Timer updateTimer;
		private System.Windows.Forms.ColumnHeader responseColumn;
		private System.ComponentModel.IContainer components;

		public TransferMonitorForm(HttpWebServer server)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.server = server;
			server.ClientConnected += new MiniHttpd.HttpServer.ClientEventHandler(server_ClientConnected);
			server.ClientDisconnected += new MiniHttpd.HttpServer.ClientEventHandler(server_ClientDisconnected);
			server.RequestReceived += new MiniHttpd.HttpServer.RequestEventHandler(server_RequestReceived);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.transferView = new ListView();
			this.clientColumn = new System.Windows.Forms.ColumnHeader();
			this.lastRequestColumn = new System.Windows.Forms.ColumnHeader();
			this.progressColumn = new System.Windows.Forms.ColumnHeader();
			this.speedColumn = new System.Windows.Forms.ColumnHeader();
			this.updateTimer = new System.Windows.Forms.Timer(this.components);
			this.responseColumn = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// transferView
			// 
			this.transferView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						   this.clientColumn,
																						   this.lastRequestColumn,
																						   this.progressColumn,
																						   this.speedColumn,
																						   this.responseColumn});
			this.transferView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.transferView.Location = new System.Drawing.Point(0, 0);
			this.transferView.Name = "transferView";
			this.transferView.Size = new System.Drawing.Size(480, 260);
			this.transferView.TabIndex = 0;
			this.transferView.View = System.Windows.Forms.View.Details;
			// 
			// clientColumn
			// 
			this.clientColumn.Text = "Client";
			this.clientColumn.Width = 80;
			// 
			// lastRequestColumn
			// 
			this.lastRequestColumn.Text = "Last Request";
			this.lastRequestColumn.Width = 180;
			// 
			// progressColumn
			// 
			this.progressColumn.Text = "Progress";
			this.progressColumn.Width = 92;
			// 
			// speedColumn
			// 
			this.speedColumn.Text = "Speed";
			// 
			// updateTimer
			// 
			this.updateTimer.Enabled = true;
			this.updateTimer.Interval = 1000;
			this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
			// 
			// responseColumn
			// 
			this.responseColumn.Text = "Response";
			this.responseColumn.Width = 44;
			// 
			// TransferMonitorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(480, 260);
			this.Controls.Add(this.transferView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "TransferMonitorForm";
			this.Text = "Transfer Monitor";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.TransferMonitorForm_Closing);
			this.ResumeLayout(false);

		}
		#endregion

		HttpWebServer server;
		Hashtable clients = new Hashtable();

		static string CalculateReadableByteLength(long bytes)
		{
			if(bytes < 1024*0.96)
				return bytes.ToString(CultureInfo.InvariantCulture) + " bytes";
			if(bytes < 1024*1024*0.96)
				return (bytes/(decimal)1024).ToString("0.00", CultureInfo.InvariantCulture) + " KB";
			if(bytes < 1024*1024*1024*0.96)
				return (bytes/(decimal)(1024*1024)).ToString("0.00", CultureInfo.InvariantCulture) + " MB";
			if(bytes < 1024*1024*1024*(decimal)1024*(decimal)0.5)
				return (bytes/(decimal)(1024*1024*1024)).ToString("0.00", CultureInfo.InvariantCulture) + " GB";

			return (bytes/(decimal)(1024*1024*1024*(decimal)1024)).ToString("0.00", CultureInfo.InvariantCulture) + " TB";
		}

		static string[] EmptyStrings(int count)
		{
			string[] strings = new string[count];
			for(int i = 0; i < strings.Length; i++)
				strings[i] = "";
			return strings;
		}

		private void server_ClientConnected(object sender, ClientEventArgs e)
		{
			if(InvokeRequired)
			{
				Invoke(new MiniHttpd.HttpServer.ClientEventHandler(server_ClientConnected), new object[] {sender, e});
				return;
			}

			//Why isn't there a ListViewItem(int columns) constructor? -_-
			ListViewItem item = new ListViewItem(EmptyStrings(transferView.Columns.Count));
			item.SubItems[clientColumn.Index].Text = e.HttpClient.RemoteAddress;

			clients.Add(e.HttpClient, item);

			transferView.Items.Add(item);
		}

		private void server_ClientDisconnected(object sender, ClientEventArgs e)
		{
			if(InvokeRequired)
			{
				Invoke(new HttpServer.ClientEventHandler(server_ClientDisconnected), new object[] {sender, e});
				return;
			}

			transferView.Items.Remove(clients[e.HttpClient] as ListViewItem);
			clients.Remove(e.HttpClient);
		}

		private void server_RequestReceived(object sender, RequestEventArgs e)
		{
			if(InvokeRequired)
			{
				Invoke(new HttpServer.RequestEventHandler(server_RequestReceived), new object[] {sender, e});
				return;
			}

			e.Request.Response.SendingResponse += new MiniHttpd.HttpResponse.ResponseEventHandler(Response_SendingResponse);
			e.Request.Response.SentResponse += new MiniHttpd.HttpResponse.ResponseEventHandler(Response_SentResponse);
		}

		private void Response_SendingResponse(object sender, ResponseEventArgs e)
		{
			if(InvokeRequired)
			{
				Invoke(new HttpResponse.ResponseEventHandler(Response_SendingResponse), new object[] {sender, e});
				return;
			}

			ListViewItem item = clients[e.HttpClient] as ListViewItem;
			
			if(item == null)
				return;

			if(e.Response.Request.Uri != null)
			{
				item.SubItems[lastRequestColumn.Index].Text = UrlEncoding.Decode(e.Response.Request.Uri.PathAndQuery);
				item.SubItems[responseColumn.Index].Text = StatusCodes.GetDescription(e.Response.ResponseCode);
			}

			item.Tag = new TransferTag(e.Response);
		}

		private void updateTimer_Tick(object sender, System.EventArgs e)
		{
			transferView.BeginUpdate();

			foreach(DictionaryEntry de in clients)
			{
				HttpClient client = de.Key as HttpClient;
				ListViewItem item = de.Value as ListViewItem;

				if(item.Tag == null)
					continue;

				TransferTag tag = item.Tag as TransferTag;

				HttpResponse response = tag.response;
				long lastBytesSent = tag.lastBytesSent;

				if(response.ContentLength >= 0)
				{
					item.SubItems[progressColumn.Index].Text = string.Format("{0} of {1} ({2}%)",
						CalculateReadableByteLength(response.BytesSent),
						CalculateReadableByteLength(response.ContentLength),
						(response.BytesSent / (double)response.ContentLength * 100d).ToString("0.00"));
				}
				else
					item.SubItems[progressColumn.Index].Text = CalculateReadableByteLength(response.BytesSent);

				item.SubItems[speedColumn.Index].Text = string.Format("{0}/s",
					CalculateReadableByteLength(response.BytesSent - tag.lastBytesSent));
				
				tag.lastBytesSent = response.BytesSent;
			}

			transferView.EndUpdate();
		}

		private void Response_SentResponse(object sender, ResponseEventArgs e)
		{
			if(InvokeRequired)
			{
				Invoke(new HttpResponse.ResponseEventHandler(Response_SentResponse), new object[] {sender, e});
				return;
			}
			ListViewItem item = clients[e.HttpClient] as ListViewItem;
			if(item == null)
				return;

			item.Tag = null;

			item.SubItems[progressColumn.Index].Text = string.Format("Done ({0})",
				CalculateReadableByteLength(e.Response.BytesSent));
		}

		private void TransferMonitorForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
		}

		class TransferTag
		{
			public TransferTag(HttpResponse response)
			{
				this.response = response;
			}

			public HttpResponse response;
			public long lastBytesSent;
		}
	}
}
