using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Bot_Dofus_1._29._1.Controles.ControlMapa.Celdas;

namespace Bot_Dofus_1._29._1.Controles.ControlMapa.Animaciones
{
	// Token: 0x02000089 RID: 137
	internal class MovimientoAnimacion : IDisposable
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00022CCD File Offset: 0x00020ECD
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x00022CD5 File Offset: 0x00020ED5
		public int entidad_id { get; private set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00022CDE File Offset: 0x00020EDE
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x00022CE6 File Offset: 0x00020EE6
		public List<CeldaMapa> path { get; private set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00022CEF File Offset: 0x00020EEF
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x00022CF7 File Offset: 0x00020EF7
		public PointF actual_punto { get; private set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00022D00 File Offset: 0x00020F00
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x00022D08 File Offset: 0x00020F08
		public TipoAnimaciones tipo_animacion { get; private set; }

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060005B0 RID: 1456 RVA: 0x00022D14 File Offset: 0x00020F14
		// (remove) Token: 0x060005B1 RID: 1457 RVA: 0x00022D4C File Offset: 0x00020F4C
		public event Action<MovimientoAnimacion> finalizado;

		// Token: 0x060005B2 RID: 1458 RVA: 0x00022D84 File Offset: 0x00020F84
		public MovimientoAnimacion(int _entidad_id, IEnumerable<CeldaMapa> _path, int duration, TipoAnimaciones _tipo_animacion)
		{
			this.entidad_id = _entidad_id;
			this.path = new List<CeldaMapa>(_path);
			this.tipo_animacion = _tipo_animacion;
			this.timer = new Timer(new TimerCallback(this.realizar_Animacion), null, -1, -1);
			this.iniciar_Frames();
			this.tiempo_por_frame = duration / this.frames.Count;
			this.index_frame = 0;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00022DEC File Offset: 0x00020FEC
		private void iniciar_Frames()
		{
			this.frames = new List<PointF>();
			for (int i = 0; i < this.path.Count - 1; i++)
			{
				this.frames.AddRange(this.get_Punto_Entre_Dos(this.path[i].Centro, this.path[i + 1].Centro, 3));
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00022E5C File Offset: 0x0002105C
		public void iniciar()
		{
			try
			{
				this.actual_punto = this.frames[this.index_frame];
				this.timer.Change(this.tiempo_por_frame, this.tiempo_por_frame);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00022EB0 File Offset: 0x000210B0
		private PointF[] get_Punto_Entre_Dos(PointF p1, PointF p2, int cantidad)
		{
			PointF[] array = new PointF[cantidad];
			float num = p2.Y - p1.Y;
			float num2 = p2.X - p1.X;
			double num3 = (double)(p2.Y - p1.Y) / (double)(p2.X - p1.X);
			cantidad--;
			for (double num4 = 0.0; num4 < (double)cantidad; num4 += 1.0)
			{
				double num5 = (num3 == 0.0) ? 0.0 : ((double)num * (num4 / (double)cantidad));
				double a = (num3 == 0.0) ? ((double)num2 * (num4 / (double)cantidad)) : (num5 / num3);
				array[(int)num4] = new PointF((float)Math.Round(a) + p1.X, (float)Math.Round(num5) + p1.Y);
			}
			array[cantidad] = p2;
			return array;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00022FA0 File Offset: 0x000211A0
		private void realizar_Animacion(object state)
		{
			this.index_frame++;
			this.actual_punto = this.frames[this.index_frame];
			if (this.index_frame == this.frames.Count - 1)
			{
				this.timer.Change(-1, -1);
				Action<MovimientoAnimacion> action = this.finalizado;
				if (action == null)
				{
					return;
				}
				action(this);
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00023006 File Offset: 0x00021206
		public void Dispose()
		{
			this.path.Clear();
			this.timer.Dispose();
			this.path = null;
			this.timer = null;
		}

		// Token: 0x0400039A RID: 922
		private int index_frame;

		// Token: 0x0400039B RID: 923
		private int tiempo_por_frame;

		// Token: 0x0400039C RID: 924
		private Timer timer;

		// Token: 0x0400039D RID: 925
		private List<PointF> frames;
	}
}
