using System;
using System.Collections.Generic;
using System.Linq;

namespace Bot_Dofus_1._29._1.Otros.Mapas.Movimiento.Mapas
{
	// Token: 0x0200004F RID: 79
	public class Pathfinder : IDisposable
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000B05F File Offset: 0x0000925F
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000B067 File Offset: 0x00009267
		private Celda[] celdas { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000B070 File Offset: 0x00009270
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000B078 File Offset: 0x00009278
		private Mapa mapa { get; set; }

		// Token: 0x060002D9 RID: 729 RVA: 0x0000B081 File Offset: 0x00009281
		public void set_Mapa(Mapa _mapa)
		{
			this.mapa = _mapa;
			this.celdas = this.mapa.celdas;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000B09C File Offset: 0x0000929C
		public List<Celda> get_Path(Celda celda_inicio, Celda celda_final, List<Celda> celdas_no_permitidas, bool detener_delante, byte distancia_detener)
		{
			if (celda_inicio == null || celda_final == null)
			{
				return null;
			}
			List<Celda> list = new List<Celda>
			{
				celda_inicio
			};
			if (celdas_no_permitidas.Contains(celda_final))
			{
				celdas_no_permitidas.Remove(celda_final);
			}
			while (list.Count > 0)
			{
				int index = 0;
				for (int i = 1; i < list.Count; i++)
				{
					if (list[i].coste_f < list[index].coste_f)
					{
						index = i;
					}
					if (list[i].coste_f == list[index].coste_f)
					{
						if (list[i].coste_g > list[index].coste_g)
						{
							index = i;
						}
						if (list[i].coste_g == list[index].coste_g)
						{
							index = i;
						}
						if (list[i].coste_g == list[index].coste_g)
						{
							index = i;
						}
					}
				}
				Celda celda = list[index];
				if (detener_delante && this.get_Distancia_Nodos(celda, celda_final) <= (int)distancia_detener && !celda_final.es_Caminable())
				{
					return this.get_Camino_Retroceso(celda_inicio, celda);
				}
				if (celda == celda_final)
				{
					return this.get_Camino_Retroceso(celda_inicio, celda_final);
				}
				list.Remove(celda);
				celdas_no_permitidas.Add(celda);
				foreach (Celda celda2 in this.get_Celdas_Adyecentes(celda))
				{
					if (!celdas_no_permitidas.Contains(celda2) && celda2.es_Caminable() && (!celda2.es_Teleport() || celda2 == celda_final))
					{
						int num = celda.coste_g + this.get_Distancia_Nodos(celda2, celda);
						if (!list.Contains(celda2))
						{
							list.Add(celda2);
						}
						else if (num >= celda2.coste_g)
						{
							continue;
						}
						celda2.coste_g = num;
						celda2.coste_h = this.get_Distancia_Nodos(celda2, celda_final);
						celda2.coste_f = celda2.coste_g + celda2.coste_h;
						celda2.nodo_padre = celda;
					}
				}
			}
			return null;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000B2A0 File Offset: 0x000094A0
		private List<Celda> get_Camino_Retroceso(Celda nodo_inicial, Celda nodo_final)
		{
			Celda celda = nodo_final;
			List<Celda> list = new List<Celda>();
			while (celda != nodo_inicial)
			{
				list.Add(celda);
				celda = celda.nodo_padre;
			}
			list.Add(nodo_inicial);
			list.Reverse();
			return list;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000B2D8 File Offset: 0x000094D8
		public List<Celda> get_Celdas_Adyecentes(Celda nodo)
		{
			List<Celda> list = new List<Celda>();
			Celda celda = this.celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x + 1 && nodec.y == nodo.y);
			Celda celda2 = this.celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x - 1 && nodec.y == nodo.y);
			Celda celda3 = this.celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x && nodec.y == nodo.y + 1);
			Celda celda4 = this.celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x && nodec.y == nodo.y - 1);
			if (celda != null)
			{
				list.Add(celda);
			}
			if (celda2 != null)
			{
				list.Add(celda2);
			}
			if (celda3 != null)
			{
				list.Add(celda3);
			}
			if (celda4 != null)
			{
				list.Add(celda4);
			}
			Celda celda5 = this.celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x - 1 && nodec.y == nodo.y - 1);
			Celda celda6 = this.celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x + 1 && nodec.y == nodo.y + 1);
			Celda celda7 = this.celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x - 1 && nodec.y == nodo.y + 1);
			Celda celda8 = this.celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x + 1 && nodec.y == nodo.y - 1);
			if (celda5 != null)
			{
				list.Add(celda5);
			}
			if (celda8 != null)
			{
				list.Add(celda8);
			}
			if (celda6 != null)
			{
				list.Add(celda6);
			}
			if (celda7 != null)
			{
				list.Add(celda7);
			}
			return list;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000B41B File Offset: 0x0000961B
		private int get_Distancia_Nodos(Celda a, Celda b)
		{
			return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000B454 File Offset: 0x00009654
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000B460 File Offset: 0x00009660
		~Pathfinder()
		{
			this.Dispose(false);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000B490 File Offset: 0x00009690
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.celdas = null;
				this.mapa = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000129 RID: 297
		private bool disposed;
	}
}
