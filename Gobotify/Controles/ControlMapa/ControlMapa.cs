using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using Bot_Dofus_1._29._1.Controles.ControlMapa.Animaciones;
using Bot_Dofus_1._29._1.Controles.ControlMapa.Celdas;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;

namespace Bot_Dofus_1._29._1.Controles.ControlMapa
{
	// Token: 0x02000086 RID: 134
	[Serializable]
	public class ControlMapa : UserControl
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x000215AD File Offset: 0x0001F7AD
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x000215B5 File Offset: 0x0001F7B5
		public byte mapa_altura { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x000215BE File Offset: 0x0001F7BE
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x000215C6 File Offset: 0x0001F7C6
		public byte mapa_anchura { get; set; }

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000561 RID: 1377 RVA: 0x000215D0 File Offset: 0x0001F7D0
		// (remove) Token: 0x06000562 RID: 1378 RVA: 0x00021608 File Offset: 0x0001F808
		public event ControlMapa.CellClickedHandler clic_celda;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000563 RID: 1379 RVA: 0x00021640 File Offset: 0x0001F840
		// (remove) Token: 0x06000564 RID: 1380 RVA: 0x00021678 File Offset: 0x0001F878
		public event Action<CeldaMapa, CeldaMapa> clic_celda_terminado;

		// Token: 0x06000565 RID: 1381 RVA: 0x000216B0 File Offset: 0x0001F8B0
		public ControlMapa()
		{
			this.DoubleBuffered = true;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.tipo_calidad = CalidadMapa.MEDIA;
			this.mapa_altura = 17;
			this.mapa_anchura = 15;
			this.TraceOnOver = false;
			this.ColorCeldaInactiva = Color.DarkGray;
			this.ColorCeldaActiva = Color.Gray;
			this.mostrar_animaciones = false;
			this.animaciones = new ConcurrentDictionary<int, MovimientoAnimacion>();
			this.animaciones_timer = new System.Timers.Timer(80.0);
			this.animaciones_timer.Elapsed += this.animacion_Finalizada;
			this.set_Celda_Numero();
			this.dibujar_Cuadricula();
			this.InitializeComponent();
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00021759 File Offset: 0x0001F959
		protected void OnCellClicked(CeldaMapa cell, MouseButtons buttons, bool abajo)
		{
			ControlMapa.CellClickedHandler cellClickedHandler = this.clic_celda;
			if (cellClickedHandler == null)
			{
				return;
			}
			cellClickedHandler(cell, buttons, abajo);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0002176E File Offset: 0x0001F96E
		protected void OnCellOver(CeldaMapa cell, CeldaMapa last)
		{
			Action<CeldaMapa, CeldaMapa> action = this.clic_celda_terminado;
			if (action == null)
			{
				return;
			}
			action(cell, last);
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00021782 File Offset: 0x0001F982
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x0002178A File Offset: 0x0001F98A
		public bool Mostrar_Animaciones
		{
			get
			{
				return this.mostrar_animaciones;
			}
			set
			{
				this.mostrar_animaciones = value;
				if (this.mostrar_animaciones)
				{
					this.animaciones_timer.Start();
				}
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x000217A6 File Offset: 0x0001F9A6
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x000217AE File Offset: 0x0001F9AE
		public bool Mostrar_Celdas_Id
		{
			get
			{
				return this.mostrar_celdas;
			}
			set
			{
				this.mostrar_celdas = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x000217BD File Offset: 0x0001F9BD
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x000217C5 File Offset: 0x0001F9C5
		[Browsable(false)]
		public int RealCellHeight { get; private set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x000217CE File Offset: 0x0001F9CE
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x000217D6 File Offset: 0x0001F9D6
		[Browsable(false)]
		public int RealCellWidth { get; private set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x000217DF File Offset: 0x0001F9DF
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x000217E7 File Offset: 0x0001F9E7
		public Color ColorCeldaInactiva { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x000217F0 File Offset: 0x0001F9F0
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x000217F8 File Offset: 0x0001F9F8
		public Color ColorCeldaActiva { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00021801 File Offset: 0x0001FA01
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x00021809 File Offset: 0x0001FA09
		public bool TraceOnOver { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00021812 File Offset: 0x0001FA12
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x0002181A File Offset: 0x0001FA1A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public CeldaMapa CurrentCellOver { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00021823 File Offset: 0x0001FA23
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x0002182B File Offset: 0x0001FA2B
		public Color BorderColorOnOver { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00021834 File Offset: 0x0001FA34
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x0002183C File Offset: 0x0001FA3C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public CeldaMapa[] celdas { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00021845 File Offset: 0x0001FA45
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x0002184D File Offset: 0x0001FA4D
		public CalidadMapa TipoCalidad
		{
			get
			{
				return this.tipo_calidad;
			}
			set
			{
				this.tipo_calidad = value;
				base.Invalidate();
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0002185C File Offset: 0x0001FA5C
		public void set_Cuenta(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00021868 File Offset: 0x0001FA68
		private void aplicar_Calidad_Mapa(Graphics g)
		{
			switch (this.tipo_calidad)
			{
			case CalidadMapa.BAJA:
				g.CompositingMode = CompositingMode.SourceOver;
				g.CompositingQuality = CompositingQuality.HighSpeed;
				g.InterpolationMode = InterpolationMode.Low;
				g.SmoothingMode = SmoothingMode.HighSpeed;
				return;
			case CalidadMapa.MEDIA:
				g.CompositingMode = CompositingMode.SourceOver;
				g.CompositingQuality = CompositingQuality.GammaCorrected;
				g.InterpolationMode = InterpolationMode.High;
				g.SmoothingMode = SmoothingMode.AntiAlias;
				return;
			case CalidadMapa.ALTA:
				g.CompositingMode = CompositingMode.SourceOver;
				g.CompositingQuality = CompositingQuality.HighQuality;
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.SmoothingMode = SmoothingMode.HighQuality;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x000218E8 File Offset: 0x0001FAE8
		public void set_Celda_Numero()
		{
			this.celdas = new CeldaMapa[(int)(2 * this.mapa_altura * this.mapa_anchura)];
			short num = 0;
			for (int i = 0; i < (int)this.mapa_altura; i++)
			{
				for (int j = 0; j < (int)(this.mapa_anchura * 2); j++)
				{
					short num2 = num;
					num = num2 + 1;
					CeldaMapa celdaMapa = new CeldaMapa(num2);
					this.celdas[(int)celdaMapa.id] = celdaMapa;
				}
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00021950 File Offset: 0x0001FB50
		private double get_Maximo_Escalado()
		{
			double val = (double)base.Width / (double)(this.mapa_anchura + 1);
			return Math.Min((double)base.Height / (double)(this.mapa_altura + 1) * 2.0, val);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00021994 File Offset: 0x0001FB94
		public void dibujar_Cuadricula()
		{
			int num = 0;
			double maximo_Escalado = this.get_Maximo_Escalado();
			double num2 = Math.Ceiling(maximo_Escalado / 2.0);
			int num3 = Convert.ToInt32(((double)base.Width - ((double)this.mapa_anchura + 0.5) * maximo_Escalado) / 2.0);
			int num4 = Convert.ToInt32(((double)base.Height - ((double)this.mapa_altura + 0.5) * num2) / 2.0);
			double num5 = num2 / 2.0;
			double num6 = maximo_Escalado / 2.0;
			for (int i = 0; i <= (int)(2 * this.mapa_altura - 1); i++)
			{
				if (i % 2 == 0)
				{
					for (int j = 0; j <= (int)(this.mapa_anchura - 1); j++)
					{
						Point point = new Point(Convert.ToInt32((double)num3 + (double)j * maximo_Escalado), Convert.ToInt32((double)num4 + (double)i * num5 + num5));
						Point point2 = new Point(Convert.ToInt32((double)num3 + (double)j * maximo_Escalado + num6), Convert.ToInt32((double)num4 + (double)i * num5));
						Point point3 = new Point(Convert.ToInt32((double)num3 + (double)j * maximo_Escalado + maximo_Escalado), Convert.ToInt32((double)num4 + (double)i * num5 + num5));
						Point point4 = new Point(Convert.ToInt32((double)num3 + (double)j * maximo_Escalado + num6), Convert.ToInt32((double)num4 + (double)i * num5 + num2));
						this.celdas[num++].Puntos = new Point[]
						{
							point,
							point2,
							point3,
							point4
						};
					}
				}
				else
				{
					for (int k = 0; k <= (int)(this.mapa_anchura - 2); k++)
					{
						Point point5 = new Point(Convert.ToInt32((double)num3 + (double)k * maximo_Escalado + num6), Convert.ToInt32((double)num4 + (double)i * num5 + num5));
						Point point6 = new Point(Convert.ToInt32((double)num3 + (double)k * maximo_Escalado + maximo_Escalado), Convert.ToInt32((double)num4 + (double)i * num5));
						Point point7 = new Point(Convert.ToInt32((double)num3 + (double)k * maximo_Escalado + maximo_Escalado + num6), Convert.ToInt32((double)num4 + (double)i * num5 + num5));
						Point point8 = new Point(Convert.ToInt32((double)num3 + (double)k * maximo_Escalado + maximo_Escalado), Convert.ToInt32((double)num4 + (double)i * num5 + num2));
						this.celdas[num++].Puntos = new Point[]
						{
							point5,
							point6,
							point7,
							point8
						};
					}
				}
			}
			this.RealCellHeight = (int)num2;
			this.RealCellWidth = (int)maximo_Escalado;
			base.Invalidate();
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00021C5C File Offset: 0x0001FE5C
		public void dibujar_Celdas(Graphics g)
		{
			this.aplicar_Calidad_Mapa(g);
			g.Clear(this.BackColor);
			CeldaMapa[] celdas = this.celdas;
			for (int i = 0; i < celdas.Length; i++)
			{
				CeldaMapa celda = celdas[i];
				if (celda.esta_En_Rectangulo(g.ClipBounds))
				{
					switch (celda.estado)
					{
					case CeldaEstado.CAMINABLE:
						celda.dibujar_Color(g, Color.Gray, new Color?(Color.White));
						if (this.mostrar_celdas)
						{
							celda.dibujar_Celda_Id(this, g);
						}
						break;
					case CeldaEstado.NO_CAMINABLE:
					case CeldaEstado.PELEA_EQUIPO_AZUL:
					case CeldaEstado.PELEA_EQUIPO_ROJO:
						goto IL_144;
					case CeldaEstado.CELDA_TELEPORT:
						celda.dibujar_Color(g, Color.Gray, new Color?(Color.Orange));
						celda.dibujar_Celda_Id(this, g);
						break;
					case CeldaEstado.OBJETO_INTERACTIVO:
						celda.dibujar_Color(g, Color.LightGoldenrodYellow, new Color?(Color.LightGoldenrodYellow));
						celda.dibujar_Celda_Id(this, g);
						break;
					case CeldaEstado.OBSTACULO:
						if (this.mostrar_celdas)
						{
							celda.dibujar_Celda_Id(this, g);
						}
						else
						{
							celda.dibujar_Obstaculo(g, Color.Gray, Color.FromArgb(60, 60, 60));
						}
						break;
					default:
						goto IL_144;
					}
					IL_15F:
					if (this.cuenta == null)
					{
						goto IL_346;
					}
					if (this.cuenta.juego.personaje.celda != null && celda.id == this.cuenta.juego.personaje.celda.id && !this.animaciones.ContainsKey(this.cuenta.juego.personaje.id))
					{
						celda.dibujar_FillPie(g, Color.Blue, (float)(this.RealCellHeight / 2));
						goto IL_346;
					}
					if ((from m in this.cuenta.juego.mapa.entidades.Values
					where m is Monstruos
					select m).FirstOrDefault((Entidad m) => m.celda.id == celda.id && !this.animaciones.ContainsKey(m.id)) != null)
					{
						celda.dibujar_FillPie(g, Color.DarkRed, (float)(this.RealCellHeight / 2));
						goto IL_346;
					}
					if ((from n in this.cuenta.juego.mapa.entidades.Values
					where n is Npcs
					select n).FirstOrDefault((Entidad n) => n.celda.id == celda.id && !this.animaciones.ContainsKey(n.id)) != null)
					{
						celda.dibujar_FillPie(g, Color.FromArgb(179, 120, 211), (float)(this.RealCellHeight / 2));
						goto IL_346;
					}
					if ((from p in this.cuenta.juego.mapa.entidades.Values
					where p is Personajes
					select p).FirstOrDefault((Entidad p) => p.celda.id == celda.id && !this.animaciones.ContainsKey(p.id)) != null)
					{
						celda.dibujar_FillPie(g, Color.FromArgb(81, 113, 202), (float)(this.RealCellHeight / 2));
						goto IL_346;
					}
					goto IL_346;
					IL_144:
					celda.dibujar_Color(g, Color.Gray, new Color?(Color.DarkGray));
					goto IL_15F;
				}
				IL_346:
				this.dibujar_Animaciones(g);
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00021FC4 File Offset: 0x000201C4
		public void agregar_Animacion(int id, List<Celda> path, int duracion, TipoAnimaciones actor)
		{
			if (path.Count < 2 || !this.mostrar_animaciones)
			{
				return;
			}
			if (this.animaciones.ContainsKey(id))
			{
				this.animacion_Finalizada(this.animaciones[id]);
			}
			MovimientoAnimacion movimientoAnimacion = new MovimientoAnimacion(id, from f in path
			select this.celdas[(int)f.id], duracion, actor);
			movimientoAnimacion.finalizado += this.animacion_Finalizada;
			this.animaciones.TryAdd(id, movimientoAnimacion);
			movimientoAnimacion.iniciar();
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00022048 File Offset: 0x00020248
		private void animacion_Finalizada(MovimientoAnimacion animacion)
		{
			animacion.finalizado -= this.animacion_Finalizada;
			MovimientoAnimacion movimientoAnimacion;
			this.animaciones.TryRemove(animacion.entidad_id, out movimientoAnimacion);
			animacion.Dispose();
			base.Invalidate();
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00022088 File Offset: 0x00020288
		private void dibujar_Animaciones(Graphics g)
		{
			foreach (MovimientoAnimacion movimientoAnimacion in this.animaciones.Values)
			{
				if (movimientoAnimacion.path != null)
				{
					using (SolidBrush solidBrush = new SolidBrush(this.get_Animacion_Color(movimientoAnimacion)))
					{
						g.FillPie(solidBrush, movimientoAnimacion.actual_punto.X - (float)(this.RealCellHeight / 2 / 2), movimientoAnimacion.actual_punto.Y - (float)(this.RealCellHeight / 2 / 2), (float)(this.RealCellHeight / 2), (float)(this.RealCellHeight / 2), 0f, 360f);
					}
				}
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0002215C File Offset: 0x0002035C
		private void animacion_Finalizada(object sender, ElapsedEventArgs e)
		{
			if (this.animaciones.Count > 0)
			{
				base.Invalidate();
				return;
			}
			if (!this.mostrar_animaciones)
			{
				this.animaciones_timer.Stop();
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00022188 File Offset: 0x00020388
		private Color get_Animacion_Color(MovimientoAnimacion animacion)
		{
			TipoAnimaciones tipo_animacion = animacion.tipo_animacion;
			if (tipo_animacion == TipoAnimaciones.PERSONAJE)
			{
				return Color.Blue;
			}
			if (tipo_animacion != TipoAnimaciones.GRUPO_MONSTRUOS)
			{
				return Color.FromArgb(81, 113, 202);
			}
			return Color.DarkRed;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000221C0 File Offset: 0x000203C0
		public void refrescar_Mapa()
		{
			if (this.cuenta.juego.mapa == null)
			{
				return;
			}
			this.animaciones.Clear();
			this.animaciones_timer.Stop();
			foreach (Celda celda in this.cuenta.juego.mapa.celdas)
			{
				this.celdas[(int)celda.id].estado = CeldaEstado.NO_CAMINABLE;
				if (celda.es_Caminable())
				{
					this.celdas[(int)celda.id].estado = CeldaEstado.CAMINABLE;
				}
				if (celda.es_linea_vision)
				{
					this.celdas[(int)celda.id].estado = CeldaEstado.OBSTACULO;
				}
				if (celda.es_Teleport())
				{
					this.celdas[(int)celda.id].estado = CeldaEstado.CELDA_TELEPORT;
				}
				if (celda.es_Interactivo())
				{
					this.celdas[(int)celda.id].estado = CeldaEstado.OBJETO_INTERACTIVO;
				}
			}
			this.animaciones_timer.Start();
			base.Invalidate();
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000222B4 File Offset: 0x000204B4
		protected override void OnPaint(PaintEventArgs e)
		{
			this.dibujar_Celdas(e.Graphics);
			base.OnPaint(e);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000222C9 File Offset: 0x000204C9
		protected override void OnResize(EventArgs e)
		{
			this.dibujar_Cuadricula();
			base.OnResize(e);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x000222D8 File Offset: 0x000204D8
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (this.mapa_raton_abajo)
			{
				CeldaMapa celdaMapa = this.get_Celda(e.Location);
				if (this.celda_retenida != null && this.celda_retenida != celdaMapa)
				{
					this.OnCellClicked(this.celda_retenida, e.Button, true);
					this.celda_retenida = celdaMapa;
				}
				if (celdaMapa != null)
				{
					this.OnCellClicked(celdaMapa, e.Button, true);
				}
			}
			if (this.TraceOnOver)
			{
				CeldaMapa celdaMapa2 = this.get_Celda(e.Location);
				Rectangle rectangle = Rectangle.Empty;
				CeldaMapa last = null;
				if (this.CurrentCellOver != null && this.CurrentCellOver != celdaMapa2)
				{
					this.CurrentCellOver.MouseOverPen = null;
					rectangle = this.CurrentCellOver.Rectangulo;
					last = this.CurrentCellOver;
				}
				if (celdaMapa2 != null)
				{
					celdaMapa2.MouseOverPen = new Pen(this.BorderColorOnOver, 1f);
					rectangle = ((rectangle != Rectangle.Empty) ? Rectangle.Union(rectangle, celdaMapa2.Rectangulo) : celdaMapa2.Rectangulo);
					this.CurrentCellOver = celdaMapa2;
				}
				this.OnCellOver(celdaMapa2, last);
				if (rectangle != Rectangle.Empty)
				{
					base.Invalidate(rectangle);
				}
			}
			base.OnMouseMove(e);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x000223EC File Offset: 0x000205EC
		protected override void OnMouseDown(MouseEventArgs e)
		{
			CeldaMapa celdaMapa = this.get_Celda(e.Location);
			if (celdaMapa != null)
			{
				this.celda_retenida = (this.celda_abajo = celdaMapa);
			}
			this.mapa_raton_abajo = true;
			base.OnMouseDown(e);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00022428 File Offset: 0x00020628
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.mapa_raton_abajo = false;
			CeldaMapa celdaMapa = this.get_Celda(e.Location);
			if (this.celda_retenida != null)
			{
				this.OnCellClicked(this.celda_retenida, e.Button, celdaMapa != this.celda_abajo);
				this.celda_retenida = null;
			}
			base.OnMouseUp(e);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00022480 File Offset: 0x00020680
		public CeldaMapa get_Celda(Point p)
		{
			return this.celdas.FirstOrDefault((CeldaMapa cell) => cell.esta_En_Rectangulo(new Rectangle(p.X - this.RealCellWidth, p.Y - this.RealCellHeight, this.RealCellWidth, this.RealCellHeight)) && ControlMapa.PointInPoly(cell.Puntos, p));
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000224B8 File Offset: 0x000206B8
		public static bool PointInPoly(Point[] poly, Point p)
		{
			bool flag = false;
			if (poly.Length < 3)
			{
				return false;
			}
			int num = poly[poly.Length - 1].X;
			int num2 = poly[poly.Length - 1].Y;
			foreach (Point point in poly)
			{
				int x = point.X;
				int y = point.Y;
				int num3;
				int num4;
				int num5;
				int num6;
				if (x > num)
				{
					num3 = num;
					num4 = x;
					num5 = num2;
					num6 = y;
				}
				else
				{
					num3 = x;
					num4 = num;
					num5 = y;
					num6 = num2;
				}
				if (x < p.X == p.X <= num && ((long)p.Y - (long)num5) * (long)(num4 - num3) < ((long)num6 - (long)num5) * (long)(p.X - num3))
				{
					flag = !flag;
				}
				num = x;
				num2 = y;
			}
			return flag;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00022595 File Offset: 0x00020795
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000225B4 File Offset: 0x000207B4
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "Map";
			base.Size = new Size(523, 347);
			base.ResumeLayout(false);
		}

		// Token: 0x04000372 RID: 882
		private CalidadMapa tipo_calidad;

		// Token: 0x04000373 RID: 883
		private bool mapa_raton_abajo;

		// Token: 0x04000374 RID: 884
		private CeldaMapa celda_retenida;

		// Token: 0x04000375 RID: 885
		private CeldaMapa celda_abajo;

		// Token: 0x04000376 RID: 886
		private Cuenta cuenta;

		// Token: 0x04000377 RID: 887
		private ConcurrentDictionary<int, MovimientoAnimacion> animaciones;

		// Token: 0x04000378 RID: 888
		private System.Timers.Timer animaciones_timer;

		// Token: 0x04000379 RID: 889
		private bool mostrar_animaciones;

		// Token: 0x0400037A RID: 890
		private bool mostrar_celdas;

		// Token: 0x04000385 RID: 901
		private IContainer components;

		// Token: 0x020000EE RID: 238
		// (Invoke) Token: 0x06000715 RID: 1813
		public delegate void CellClickedHandler(CeldaMapa celda, MouseButtons botones, bool abajo);
	}
}
