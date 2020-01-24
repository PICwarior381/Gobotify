using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Bot_Dofus_1._29._1.Controles.ColorCheckBox
{
	// Token: 0x0200008B RID: 139
	public class ColorCheckBox : CheckBox
	{
		// Token: 0x060005B8 RID: 1464 RVA: 0x0002302C File Offset: 0x0002122C
		public ColorCheckBox()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00023040 File Offset: 0x00021240
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphics.InterpolationMode = InterpolationMode.High;
			base.OnPaint(e);
			using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
			{
				graphics.FillRectangle(solidBrush, new Rectangle(0, 0, base.Width - 2, base.Height));
			}
			if (base.Checked)
			{
				using (GraphicsPath graphicsPath = new GraphicsPath())
				{
					graphicsPath.AddLines(new Point[]
					{
						new Point(2, base.Height / 2),
						new Point(base.Width / 3, base.Height - 3),
						new Point(base.Width - 2, base.Height / 3)
					});
					using (Pen pen = new Pen(Color.White, 2f))
					{
						graphics.DrawPath(pen, graphicsPath);
					}
				}
			}
			if (!base.Enabled)
			{
				using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(120, Color.Gray)))
				{
					graphics.FillRectangle(solidBrush2, 0, 0, base.Width, base.Height);
				}
			}
		}
	}
}
