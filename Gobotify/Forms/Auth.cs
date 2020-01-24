using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Bot_Dofus_1._29._1.Forms
{
	// Token: 0x0200007B RID: 123
	public partial class Auth : Form
	{
		// Token: 0x06000512 RID: 1298 RVA: 0x0001CCD6 File Offset: 0x0001AED6
		public Auth()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001CCE4 File Offset: 0x0001AEE4
		private void button1_Click(object sender, EventArgs e)
		{
			string requestUriString = "http://54.37.164.33/api/";
			string text = this.textBox2.Text;
			string text2 = this.textBox1.Text;
			string text3 = this.textBox3.Text;
			string s = string.Concat(new string[]
			{
				"login=",
				text2,
				"&password=",
				text3,
				"&key=",
				text
			});
			new ASCIIEncoding();
			byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(s);
			WebRequest webRequest = WebRequest.Create(requestUriString);
			webRequest.Method = "POST";
			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.ContentLength = (long)bytes.Length;
			Stream requestStream = webRequest.GetRequestStream();
			requestStream.Write(bytes, 0, bytes.Length);
			requestStream.Close();
			Stream responseStream = webRequest.GetResponse().GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream);
			string text4 = streamReader.ReadToEnd();
			MessageBox.Show(text4);
			streamReader.Close();
			responseStream.Close();
			if (text4 == "true")
			{
				base.DialogResult = DialogResult.OK;
				return;
			}
			Application.Exit();
		}

		private void Auth_Load(object sender, EventArgs e)
		{

		}
	}
}
