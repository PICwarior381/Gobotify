using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Bot_Dofus_1._29._1.Controles.TabControl
{
	// Token: 0x0200007F RID: 127
	public class Cabezera : Control
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0002077D File Offset: 0x0001E97D
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x00020785 File Offset: 0x0001E985
		public string propiedad_Cuenta
		{
			get
			{
				return this.cuenta;
			}
			set
			{
				this.cuenta = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00020794 File Offset: 0x0001E994
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x0002079C File Offset: 0x0001E99C
		public string propiedad_Estado
		{
			get
			{
				return this.estado;
			}
			set
			{
				this.estado = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x000207AB File Offset: 0x0001E9AB
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x000207B3 File Offset: 0x0001E9B3
		public string propiedad_Grupo
		{
			get
			{
				return this.grupo;
			}
			set
			{
				this.grupo = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x000207C2 File Offset: 0x0001E9C2
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x000207CA File Offset: 0x0001E9CA
		public Image propiedad_Imagen
		{
			get
			{
				return this.imagen;
			}
			set
			{
				this.imagen = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x000207D9 File Offset: 0x0001E9D9
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x000207E1 File Offset: 0x0001E9E1
		public bool propiedad_Esta_Seleccionada
		{
			get
			{
				return this.esta_seleccionada;
			}
			set
			{
				this.esta_seleccionada = value;
				base.Invalidate();
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000207F0 File Offset: 0x0001E9F0
		public Cabezera()
		{
			this.DoubleBuffered = true;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.FixedHeight | ControlStyles.OptimizedDoubleBuffer, true);
			this.Cursor = Cursors.Hand;
			base.Size = new Size(150, 40);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00020828 File Offset: 0x0001EA28
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphics.InterpolationMode = InterpolationMode.High;
			base.OnPaint(e);
			Rectangle rect = new Rectangle(0, 0, base.Width, base.Height);
			using (SolidBrush solidBrush = new SolidBrush(this.esta_seleccionada ? Color.FromArgb(217, 228, 244) : Control.DefaultBackColor))
			{
				graphics.FillRectangle(solidBrush, rect);
			}
			graphics.DrawRectangle(Pens.Black, rect);
			if (this.imagen != null)
			{
				graphics.DrawImage(this.imagen, new Rectangle(4, 8, 28, 28));
				rect.X += 30;
			}
			Font font = new Font(this.Font.FontFamily, this.Font.Size - 1.6f);
			if (!string.IsNullOrEmpty(this.cuenta) && !string.IsNullOrEmpty(this.estado) && !string.IsNullOrEmpty(this.grupo))
			{
				SizeF sizeF = graphics.MeasureString(this.cuenta, font);
				SizeF sizeF2 = graphics.MeasureString(this.estado, font);
				SizeF sizeF3 = graphics.MeasureString(this.grupo, font);
				graphics.DrawString(char.ToUpper(this.cuenta[0]).ToString() + this.cuenta.Substring(1), font, Brushes.Black, (float)rect.X, 25f - (sizeF.Height + sizeF2.Height + sizeF3.Height) / 2f);
				graphics.DrawString("Statut: " + this.estado, font, Brushes.Black, (float)rect.X, 20f - (sizeF.Height + sizeF2.Height + sizeF3.Height) / 2f + sizeF.Height);
				graphics.DrawString("Groupe: " + this.grupo, font, Brushes.Black, (float)rect.X, 15f - (sizeF.Height + sizeF2.Height + sizeF3.Height) / 2f + sizeF.Height + sizeF2.Height);
				return;
			}
			if (!string.IsNullOrEmpty(this.cuenta))
			{
				SizeF sizeF4 = graphics.MeasureString(this.cuenta, font);
				graphics.DrawString(this.cuenta, font, Brushes.Black, (float)rect.X, 25f - sizeF4.Height / 2f);
			}
		}

		// Token: 0x04000353 RID: 851
		public string cuenta;

		// Token: 0x04000354 RID: 852
		public string estado;

		// Token: 0x04000355 RID: 853
		public string grupo;

		// Token: 0x04000356 RID: 854
		public Image imagen;

		// Token: 0x04000357 RID: 855
		public bool esta_seleccionada;
	}
}
