using System;
using System.Collections.Generic;
using System.Linq;
using Bot_Dofus_1._29._1.Otros.Peleas;
using Bot_Dofus_1._29._1.Otros.Peleas.Peleadores;

namespace Bot_Dofus_1._29._1.Otros.Mapas.Movimiento.Peleas
{
	// Token: 0x0200004D RID: 77
	internal class PeleasPathfinder
	{
		// Token: 0x060002CA RID: 714 RVA: 0x0000ABD4 File Offset: 0x00008DD4
		public static PeleaCamino get_Path_Pelea(short celda_actual, short celda_objetivo, Dictionary<short, MovimientoNodo> celdas)
		{
			if (!celdas.ContainsKey(celda_objetivo))
			{
				return null;
			}
			short num = celda_objetivo;
			List<short> list = new List<short>();
			List<short> list2 = new List<short>();
			Dictionary<short, int> dictionary = new Dictionary<short, int>();
			Dictionary<short, int> dictionary2 = new Dictionary<short, int>();
			byte b = 0;
			while (num != celda_actual)
			{
				MovimientoNodo movimientoNodo = celdas[num];
				if (movimientoNodo.alcanzable)
				{
					list.Insert(0, num);
					dictionary.Add(num, (int)b);
				}
				else
				{
					list2.Insert(0, num);
					dictionary2.Add(num, (int)b);
				}
				num = movimientoNodo.celda_inicial;
				b += 1;
			}
			return new PeleaCamino
			{
				celdas_accesibles = list,
				celdas_inalcanzables = list2,
				mapa_celdas_alcanzables = dictionary,
				mapa_celdas_inalcanzable = dictionary2
			};
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000AC74 File Offset: 0x00008E74
		public static Dictionary<short, MovimientoNodo> get_Celdas_Accesibles(Pelea pelea, Mapa mapa, Celda celda_actual)
		{
			Dictionary<short, MovimientoNodo> dictionary = new Dictionary<short, MovimientoNodo>();
			if (pelea.jugador_luchador.pm <= 0)
			{
				return dictionary;
			}
			short pm = (short)pelea.jugador_luchador.pm;
			List<NodoPelea> list = new List<NodoPelea>();
			Dictionary<short, NodoPelea> dictionary2 = new Dictionary<short, NodoPelea>();
			NodoPelea nodoPelea = new NodoPelea(celda_actual, (int)pm, (int)pelea.jugador_luchador.pa, 1);
			list.Add(nodoPelea);
			dictionary2[celda_actual.id] = nodoPelea;
			while (list.Count > 0)
			{
				NodoPelea nodoPelea2 = list.Last<NodoPelea>();
				list.Remove(nodoPelea2);
				Celda celda = nodoPelea2.celda;
				List<Celda> adyecentes = PeleasPathfinder.get_Celdas_Adyecentes(celda, mapa.celdas);
				int i = 0;
				Func<Luchadores, bool> <>9__0;
				while (i < adyecentes.Count)
				{
					IEnumerable<Luchadores> get_Luchadores = pelea.get_Luchadores;
					Func<Luchadores, bool> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = delegate(Luchadores f)
						{
							int id = (int)f.celda.id;
							Celda celda2 = adyecentes[i];
							short? num3 = (celda2 != null) ? new short?(celda2.id) : null;
							int? num4 = (num3 != null) ? new int?((int)num3.GetValueOrDefault()) : null;
							return id == num4.GetValueOrDefault() & num4 != null;
						});
					}
					Luchadores luchadores = get_Luchadores.FirstOrDefault(predicate);
					if (adyecentes[i] != null && luchadores == null)
					{
						int j = i;
						i = j + 1;
					}
					else
					{
						adyecentes.RemoveAt(i);
					}
				}
				int num = nodoPelea2.pm_disponible - 1;
				int pa_disponible = nodoPelea2.pa_disponible;
				int distancia = nodoPelea2.distancia + 1;
				bool alcanzable = num >= 0;
				i = 0;
				while (i < adyecentes.Count)
				{
					if (!dictionary2.ContainsKey(adyecentes[i].id))
					{
						goto IL_1C5;
					}
					NodoPelea nodoPelea3 = dictionary2[adyecentes[i].id];
					if (nodoPelea3.pm_disponible <= num && (nodoPelea3.pm_disponible != num || nodoPelea3.pm_disponible < pa_disponible))
					{
						goto IL_1C5;
					}
					IL_25D:
					int j = i;
					i = j + 1;
					continue;
					IL_1C5:
					if (!adyecentes[i].es_Caminable())
					{
						goto IL_25D;
					}
					dictionary[adyecentes[i].id] = new MovimientoNodo(celda.id, alcanzable);
					nodoPelea = new NodoPelea(adyecentes[i], num, pa_disponible, distancia);
					dictionary2[adyecentes[i].id] = nodoPelea;
					if (nodoPelea2.distancia < (int)pm)
					{
						list.Add(nodoPelea);
						goto IL_25D;
					}
					goto IL_25D;
				}
			}
			foreach (short num2 in dictionary.Keys)
			{
				dictionary[num2].camino = PeleasPathfinder.get_Path_Pelea(celda_actual.id, num2, dictionary);
			}
			return dictionary;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000AF74 File Offset: 0x00009174
		public static List<Celda> get_Celdas_Adyecentes(Celda nodo, Celda[] mapa_celdas)
		{
			List<Celda> list = new List<Celda>();
			Celda celda = mapa_celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x + 1 && nodec.y == nodo.y);
			Celda celda2 = mapa_celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x - 1 && nodec.y == nodo.y);
			Celda celda3 = mapa_celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x && nodec.y == nodo.y + 1);
			Celda celda4 = mapa_celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x && nodec.y == nodo.y - 1);
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
			return list;
		}
	}
}
