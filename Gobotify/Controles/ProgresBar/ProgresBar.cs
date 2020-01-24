using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Bot_Dofus_1._29._1.Controles.ProgresBar
{
	// Token: 0x02000082 RID: 130
	[DefaultEvent("ValueChanged")]
	internal class ProgresBar : Control
	{
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600054C RID: 1356 RVA: 0x00021144 File Offset: 0x0001F344
		// (remove) Token: 0x0600054D RID: 1357 RVA: 0x0002117C File Offset: 0x0001F37C
		public event EventHandler valor_cambiado;

		// Token: 0x0600054E RID: 1358 RVA: 0x000211B4 File Offset: 0x0001F3B4
		public ProgresBar()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.DoubleBuffered = true;
			base.Size = new Size(100, 24);
			this.color = Color.FromArgb(102, 150, 232);
			this.valor_maximo = 100;
			this.valor = 0;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0002120F File Offset: 0x0001F40F
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x00021217 File Offset: 0x0001F417
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00021226 File Offset: 0x0001F426
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x0002122E File Offset: 0x0001F42E
		public Color color_Barra
		{
			get
			{
				return this.color;
			}
			set
			{
				if (this.color == value)
				{
					return;
				}
				this.color = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0002124C File Offset: 0x0001F44C
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x00021254 File Offset: 0x0001F454
		public int valor_Maximo
		{
			get
			{
				return this.valor_maximo;
			}
			set
			{
				if (this.valor_maximo == value)
				{
					return;
				}
				this.valor_maximo = value;
				if (this.valor > this.valor_maximo)
				{
					this.valor = this.valor_maximo;
				}
				base.Invalidate();
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00021287 File Offset: 0x0001F487
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x00021290 File Offset: 0x0001F490
		public int Valor
		{
			get
			{
				return this.valor;
			}
			set
			{
				if (this.valor == value)
				{
					return;
				}
				this.valor = value;
				if (this.valor > this.valor_maximo)
				{
					this.valor = this.valor_maximo;
				}
				else if (this.valor < 0)
				{
					this.valor = 0;
				}
				base.Invalidate();
				EventHandler eventHandler = this.valor_cambiado;
				if (eventHandler == null)
				{
					return;
				}
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x000212F6 File Offset: 0x0001F4F6
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x000212FE File Offset: 0x0001F4FE
		public TipoProgresBar tipos_Barra
		{
			get
			{
				return this.tipo_barra;
			}
			set
			{
				this.tipo_barra = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0002130D File Offset: 0x0001F50D
		public int porcentaje
		{
			get
			{
				return (int)((double)this.Valor / (double)this.valor_Maximo * 100.0);
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0002132C File Offset: 0x0001F52C
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphics.InterpolationMode = InterpolationMode.High;
			graphics.Clear(this.BackColor);
			using (SolidBrush solidBrush = new SolidBrush(this.color_Barra))
			{
				double num = (double)(base.Width * this.porcentaje / 100);
				graphics.FillRectangle(solidBrush, 0, 0, (int)num, base.Height);
			}
			using (Pen pen = new Pen(Color.Black))
			{
				graphics.DrawLines(pen, new Point[]
				{
					new Point(0, 0),
					new Point(0, base.Height),
					new Point(base.Width, base.Height),
					new Point(base.Width, 0),
					new Point(0, 0)
				});
			}
			using (SolidBrush solidBrush2 = new SolidBrush(this.ForeColor))
			{
				SizeF sizeF = graphics.MeasureString(this.get_Texto_Barra(), this.Font);
				graphics.DrawString(this.get_Texto_Barra(), this.Font, solidBrush2, (float)(base.Width / 2) - sizeF.Width / 2f, (float)(base.Height / 2) - sizeF.Height / 2f);
			}
			base.OnPaint(e);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000214C4 File Offset: 0x0001F6C4
		private string get_Texto_Barra()
		{
			switch (this.tipo_barra)
			{
			case TipoProgresBar.VALOR_MAXIMO_PORCENTAJE:
				return string.Format("{0}/{1} ({2}%)", this.valor, this.valor_maximo, this.porcentaje);
			case TipoProgresBar.VALOR_MAXIMO:
				return string.Format("{0}/{1}", this.valor, this.valor_maximo);
			case TipoProgresBar.VALOR_PORCENTAJE:
				return string.Format("{0} ({1}%)", this.valor, this.porcentaje);
			case TipoProgresBar.TEXTO_PORCENTAJE:
				return string.Format("{0} ({1}%)", this.Text, this.porcentaje);
			default:
				return string.Format("{0}%", this.porcentaje);
			}
		}

		// Token: 0x04000361 RID: 865
		private Color color;

		// Token: 0x04000362 RID: 866
		private int valor_maximo;

		// Token: 0x04000363 RID: 867
		private int valor;

		// Token: 0x04000364 RID: 868
		private TipoProgresBar tipo_barra;
	}
}
