namespace Bot_Dofus_1._29._1.Forms
{
	// Token: 0x0200007B RID: 123
	public partial class Auth : global::System.Windows.Forms.Form
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x0001CDFB File Offset: 0x0001AFFB
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001CE1C File Offset: 0x0001B01C
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Bot_Dofus_1._29._1.Forms.Auth));
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			this.textBox2 = new global::System.Windows.Forms.TextBox();
			this.textBox3 = new global::System.Windows.Forms.TextBox();
			this.button1 = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(16, 30);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(25, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Key";
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(21, 119);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(53, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Password";
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(20, 73);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(33, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Login";
			this.textBox1.BackColor = global::System.Drawing.Color.FromArgb(224, 224, 224);
			this.textBox1.Location = new global::System.Drawing.Point(87, 69);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new global::System.Drawing.Size(275, 20);
			this.textBox1.TabIndex = 3;
			this.textBox2.BackColor = global::System.Drawing.Color.FromArgb(224, 224, 224);
			this.textBox2.Location = new global::System.Drawing.Point(87, 25);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new global::System.Drawing.Size(275, 20);
			this.textBox2.TabIndex = 4;
			this.textBox3.BackColor = global::System.Drawing.Color.FromArgb(224, 224, 224);
			this.textBox3.Location = new global::System.Drawing.Point(87, 116);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new global::System.Drawing.Size(275, 20);
			this.textBox3.TabIndex = 5;
			this.button1.Location = new global::System.Drawing.Point(132, 171);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(75, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "Valider";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new global::System.EventHandler(this.button1_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.Gray;
			base.ClientSize = new global::System.Drawing.Size(384, 242);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.textBox3);
			base.Controls.Add(this.textBox2);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "Auth";
			this.Text = "Auth";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400030D RID: 781
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400030E RID: 782
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400030F RID: 783
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000310 RID: 784
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000311 RID: 785
		private global::System.Windows.Forms.TextBox textBox1;

		// Token: 0x04000312 RID: 786
		private global::System.Windows.Forms.TextBox textBox2;

		// Token: 0x04000313 RID: 787
		private global::System.Windows.Forms.TextBox textBox3;

		// Token: 0x04000314 RID: 788
		private global::System.Windows.Forms.Button button1;
	}
}
