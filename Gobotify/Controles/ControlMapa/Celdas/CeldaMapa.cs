using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Bot_Dofus_1._29._1.Controles.ControlMapa.Celdas
{
	// Token: 0x02000088 RID: 136
	public class CeldaMapa
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00022619 File Offset: 0x00020819
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x00022621 File Offset: 0x00020821
		public CeldaEstado estado { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0002262A File Offset: 0x0002082A
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x00022632 File Offset: 0x00020832
		public Brush CustomBrush { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0002263B File Offset: 0x0002083B
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x00022643 File Offset: 0x00020843
		public Pen CustomBorderPen { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0002264C File Offset: 0x0002084C
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x00022654 File Offset: 0x00020854
		public Pen MouseOverPen { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0002265D File Offset: 0x0002085D
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x00022665 File Offset: 0x00020865
		public Rectangle Rectangulo { get; private set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00022670 File Offset: 0x00020870
		public Point Centro
		{
			get
			{
				return new Point((this.Puntos[0].X + this.Puntos[2].X) / 2, (this.Puntos[1].Y + this.Puntos[3].Y) / 2);
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000226CC File Offset: 0x000208CC
		public CeldaMapa(short _id)
		{
			this.id = _id;
			this.estado = CeldaEstado.NO_CAMINABLE;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x000226E2 File Offset: 0x000208E2
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x000226EA File Offset: 0x000208EA
		public Point[] Puntos
		{
			get
			{
				return this.mapa_puntos;
			}
			set
			{
				this.mapa_puntos = value;
				this.RefreshBounds();
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000226FC File Offset: 0x000208FC
		public void RefreshBounds()
		{
			int num = this.Puntos.Min((Point entry) => entry.X);
			int num2 = this.Puntos.Min((Point entry) => entry.Y);
			int width = this.Puntos.Max((Point entry) => entry.X) - num;
			int height = this.Puntos.Max((Point entry) => entry.Y) - num2;
			this.Rectangulo = new Rectangle(num, num2, width, height);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000227C8 File Offset: 0x000209C8
		public void dibujar_Color(Graphics g, Color borderColor, Color? fillingColor)
		{
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				graphicsPath.AddLines(this.Puntos);
				if (fillingColor != null)
				{
					using (SolidBrush solidBrush = new SolidBrush(fillingColor.Value))
					{
						g.FillPath(solidBrush, graphicsPath);
					}
				}
				using (Pen pen = new Pen(borderColor))
				{
					g.DrawPath(pen, graphicsPath);
				}
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00022860 File Offset: 0x00020A60
		public virtual void dibujar_Celda_Id(ControlMapa parent, Graphics g)
		{
			StringFormat format = new StringFormat
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};
			g.DrawString(this.id.ToString(), parent.Font, Brushes.Black, new RectangleF((float)this.Rectangulo.X, (float)this.Rectangulo.Y, (float)this.Rectangulo.Width, (float)this.Rectangulo.Height), format);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000228E0 File Offset: 0x00020AE0
		public void dibujar_FillPie(Graphics g, Color color, float size)
		{
			using (SolidBrush solidBrush = new SolidBrush(color))
			{
				g.FillPie(solidBrush, (float)this.Puntos[1].X - size / 2f, (float)this.Puntos[1].Y + 4.2f, size, size, 0f, 360f);
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00022958 File Offset: 0x00020B58
		public void dibujar_Obstaculo(Graphics g, Color borderColor, Color fillingColor)
		{
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				graphicsPath.AddLines(new PointF[]
				{
					new PointF((float)this.Puntos[0].X, (float)(this.Puntos[0].Y - 10)),
					new PointF((float)this.Puntos[1].X, (float)(this.Puntos[1].Y - 10)),
					new PointF((float)this.Puntos[2].X, (float)(this.Puntos[2].Y - 10)),
					new PointF((float)this.Puntos[3].X, (float)(this.Puntos[3].Y - 10)),
					new PointF((float)this.Puntos[0].X, (float)(this.Puntos[0].Y - 10))
				});
				graphicsPath.AddLines(new PointF[]
				{
					new PointF((float)this.Puntos[0].X, (float)(this.Puntos[0].Y - 10)),
					new PointF((float)this.Puntos[3].X, (float)(this.Puntos[3].Y - 10)),
					this.Puntos[3],
					this.Puntos[0],
					new PointF((float)this.Puntos[0].X, (float)(this.Puntos[0].Y - 10))
				});
				graphicsPath.AddLines(new PointF[]
				{
					new PointF((float)this.Puntos[3].X, (float)(this.Puntos[3].Y - 10)),
					new PointF((float)this.Puntos[2].X, (float)(this.Puntos[2].Y - 10)),
					this.Puntos[2],
					this.Puntos[3],
					new PointF((float)this.Puntos[3].X, (float)(this.Puntos[3].Y - 10))
				});
				using (SolidBrush solidBrush = new SolidBrush(fillingColor))
				{
					g.FillPath(solidBrush, graphicsPath);
				}
				using (Pen pen = new Pen(borderColor))
				{
					g.DrawPath(pen, graphicsPath);
				}
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00022CAC File Offset: 0x00020EAC
		public bool esta_En_Rectangulo(RectangleF rectangulo)
		{
			return this.Rectangulo.IntersectsWith(Rectangle.Ceiling(rectangulo));
		}

		// Token: 0x0400038F RID: 911
		public short id;

		// Token: 0x04000390 RID: 912
		private Point[] mapa_puntos;
	}
}
