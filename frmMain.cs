using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MozillaPlugins
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			this.bgwLoad.RunWorkerAsync();
		}

		private void frmMain_Resize(object sender, EventArgs e)
		{
			this.lsvList.Width = this.Width - 40;
			this.lsvList.Height = this.Height - 103;

			this.btnDelete.Left = this.Width - 134;
			this.btnDelete.Top = this.Height - 85;

			this.label1.Top = this.Height - 88;
			this.label2.Top = this.Height - 73;
		}

		private string[] WebLoad(WebClient wc, string url)
		{
			try
			{
				string body = wc.DownloadString(url);
				body = body.Replace("\r", "\n");
				while (body.IndexOf("\n\n") >= 0)
					body = body.Replace("\n\n", "\n");

				return body.Split('\n');
			}
			catch
			{
				return new string[0];
			}
		}

		private delegate void dv();
		private void bgwLoad_DoWork(object sender, DoWorkEventArgs e)
		{
			string[] safe, rocommend;

			using (WebClient wc = new WebClient())
			{
				wc.Encoding = Encoding.UTF8;
				rocommend = WebLoad(wc, "https://raw.github.com/RyuaNerin/MozillaPlugins/master/recom.txt");
				safe = WebLoad(wc, "https://raw.github.com/RyuaNerin/MozillaPlugins/master/safe.txt");
			}

			List<RegistryKey> lst = new List<RegistryKey>();

			RegistryKey r;

			r = Registry.CurrentUser
				.CreateSubKey("SOFTWARE")
				.CreateSubKey("MozillaPlugins");

			object o = r.GetValue("@");

			r = Registry.CurrentUser
				.CreateSubKey("SOFTWARE")
				.CreateSubKey("MozillaPlugins");

			foreach (string s in r.GetSubKeyNames())
				lst.Add(r.CreateSubKey(s));

			r = Registry.LocalMachine
				.CreateSubKey("SOFTWARE")
				.CreateSubKey("MozillaPlugins");

			foreach (string s in r.GetSubKeyNames())
				lst.Add(r.CreateSubKey(s));

			try
			{
				r = Registry.LocalMachine
					.CreateSubKey("SOFTWARE")
					.CreateSubKey("Wow6432Node")
					.CreateSubKey("MozillaPlugins");

				foreach (string s in r.GetSubKeyNames())
					lst.Add(r.CreateSubKey(s));
			}
			catch { }

			this.Invoke(new dv(delegate() { this.lsvList.Items.Clear(); }));

			foreach (RegistryKey rk in lst)
			{
				ListViewItem item = new ListViewItem();
				item.Text = rk.Name.Substring(rk.Name.LastIndexOf('\\') + 1);
				item.SubItems.Add((string)rk.GetValue("ProductName"));
				item.SubItems.Add((string)rk.GetValue("Description"));
				item.SubItems.Add((string)rk.GetValue("Path"));
				item.SubItems.Add(rk.Name);
				item.Tag = rk;

				if ((string)rk.GetValue("Path") == "disabled")
				{
					item.ForeColor = Color.Gray;
				}
				else
				{
				if (!File.Exists((string)rk.GetValue("Path")))
					item.ForeColor = Color.Red;

				foreach (string s in rocommend)
				{
					if (item.Text.StartsWith(s) || item.Text.StartsWith("@" + s))
					{
						item.ForeColor = Color.Red;
						break;
					}
				}

				foreach (string s in safe)
				{
					if (item.Text.StartsWith(s) || item.Text.StartsWith("@" + s))
					{
						item.ForeColor = Color.Blue;
						break;
					}
				}
				}
				

				this.Invoke(new dv(delegate() { this.lsvList.Items.Add(item); }));
			}
		}

		private void bgwLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.btnDelete.Enabled = true;
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "삭제하시겠습니까?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
				!= DialogResult.Yes)
				return;

			this.btnDelete.Enabled = false;
			this.bgwDelete.RunWorkerAsync();
		}

		private delegate int d_i();
		private delegate RegistryKey d_r();
		private void bgwDelete_DoWork(object sender, DoWorkEventArgs e)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Windows Registry Editor Version 5.00\r\n\r\n");

			int CheckedCount = (int)this.Invoke(new d_i(delegate() { return this.lsvList.CheckedIndices.Count; }));

			for (int i = 0; i < CheckedCount; i++)
			{
				RegistryKey rk = (RegistryKey)this.Invoke(new d_r(delegate() { return (RegistryKey)this.lsvList.CheckedItems[i].Tag; }));
				RegBackup(rk, sb);
				rk.DeleteSubKeyTree("");
				rk.Close();
			}

			File.WriteAllText(DateTime.Now.ToString("백업 yyyy-MM-dd HH-mm-ss") + ".reg", sb.ToString(), Encoding.ASCII);
		}
		private void RegBackup(RegistryKey key, StringBuilder sb)
		{
			sb.Append("\r\n[");
			sb.Append(key.Name);
			sb.Append("]\r\n");

			foreach (string sKey in key.GetValueNames())
			{
				RegistryValueKind rvk = key.GetValueKind(sKey);

				switch (rvk)
				{
					case RegistryValueKind.String:
						sb.AppendFormat("\"{0}\"=\"{1}\"\r\n", sKey, (string)key.GetValue(sKey));
						break;

					case RegistryValueKind.DWord:
						sb.AppendFormat("\"{0}\"=dword:{1:X}\r\n", sKey, (int)key.GetValue(sKey));
						break;

					case RegistryValueKind.QWord:
						sb.AppendFormat("\"{0}\"=dword:{1}\r\n", sKey, StringFromQWord((long)key.GetValue(sKey)));
						break;

					case RegistryValueKind.Binary:
						sb.AppendFormat("\"{0}\"=hex:{1}\r\n", sKey, BinarytoString((byte[])key.GetValue(sKey)));
						break;

					case RegistryValueKind.MultiString:
						sb.AppendFormat("\"{0}\"=hex:{1}\r\n", sKey, stringFormMultiSz((string[])key.GetValue(sKey)));
						break;

					case RegistryValueKind.ExpandString:
						sb.AppendFormat("\"{0}\"=hex(2):{1}\r\n", sKey, stringFromExpandSz((string)key.GetValue(sKey)));
						break;
				}
			}

			foreach (string sKey in key.GetSubKeyNames())
				this.RegBackup(key.OpenSubKey(sKey), sb);
		}
		private string BinarytoString(byte[] arr)
		{
			StringBuilder sb = new StringBuilder(arr.Length * 3);
			foreach (byte b in arr)
				if (b < 16)
					sb.AppendFormat("0{0:X},", b);
				else
					sb.AppendFormat("{0:X},", b);

			sb.Remove(sb.Length - 1, 1);
			return sb.ToString();
		}
		private string StringFromQWord(long dat)
		{
			// 0x0123456789ABCDEF
			// FE DC BA 98 76 54 32 01
			//  0  1  2  3  4  5  6  7

			StringBuilder sb = new StringBuilder(24);

			for (int i = 0; i < 8; i++)
			{
				byte b = (byte)((dat >> (i * 8)) & 0x100);
				if (( i == 7 | 0 < b) && b < 16)
					sb.AppendFormat("0{0:X},", b);
				else if (16 < b)
					sb.AppendFormat("{0:X},", b);
			}

			sb.Remove(sb.Length - 1, 1);
			return sb.ToString();
		}
		private string stringFromExpandSz(string str)
		{
			return BinarytoString(Encoding.Unicode.GetBytes(str + "\x0"));
		}
		private string stringFormMultiSz(string[] str)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string s in str)
				sb.Append(stringFromExpandSz(s) + ",");

			sb.Append("00,00");
			return sb.ToString();
		}

		private void bgwDelete_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			MessageBox.Show(this, "삭제되었습니다!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

			this.btnDelete.Enabled = false;
			this.bgwLoad.RunWorkerAsync();
		}
	}
}
