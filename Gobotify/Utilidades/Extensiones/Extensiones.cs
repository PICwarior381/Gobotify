using System;
using System.Collections.Generic;
using System.Linq;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Movimientos;
using MoonSharp.Interpreter;

namespace Bot_Dofus_1._29._1.Utilidades.Extensiones
{
	// Token: 0x02000007 RID: 7
	public static class Extensiones
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002460 File Offset: 0x00000660
		public static string cadena_Amigable(this EstadoCuenta estado)
		{
			switch (estado)
			{
			case EstadoCuenta.CONNECTE:
				return "Connecté";
			case EstadoCuenta.CONNECTE_INATIF:
				return "Inactif";
			case EstadoCuenta.DECONNECTE:
				return "Deconnecté";
			case EstadoCuenta.MOUVEMENT:
				return "Deplacement";
			case EstadoCuenta.COMBAT:
				return "Combat";
			case EstadoCuenta.RECOLTE:
				return "Recolte";
			case EstadoCuenta.DIALOGUE:
				return "Dialogue";
			case EstadoCuenta.STOCKAGE:
				return "Stockage";
			case EstadoCuenta.ECHANGE:
				return "Echange";
			case EstadoCuenta.ACHETER:
				return "Achat";
			case EstadoCuenta.VENDRE:
				return "Vente";
			case EstadoCuenta.REGENERATION:
				return "Regeneration";
			default:
				return "-";
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024F4 File Offset: 0x000006F4
		public static T get_Or<T>(this Table table, string key, DataType type, T orValue)
		{
			DynValue dynValue = table.Get(key);
			if (dynValue.IsNil() || dynValue.Type != type)
			{
				return orValue;
			}
			T result;
			try
			{
				result = (T)((object)dynValue.ToObject(typeof(T)));
			}
			catch
			{
				result = orValue;
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000254C File Offset: 0x0000074C
		public static Dictionary<MapaTeleportCeldas, List<short>> Add(this Dictionary<MapaTeleportCeldas, List<short>> cells, short cellId)
		{
			short[] source = new short[]
			{
				16,
				17,
				18,
				19,
				20,
				21,
				22,
				23,
				24,
				25,
				26,
				27,
				36
			};
			short[] source2 = new short[]
			{
				28,
				57,
				86,
				115,
				144,
				173,
				231,
				202,
				260,
				289,
				318,
				347,
				376,
				405,
				434
			};
			short[] source3 = new short[]
			{
				451,
				452,
				453,
				454,
				455,
				456,
				457,
				458,
				459,
				460,
				461,
				462,
				463
			};
			IEnumerable<short> source4 = new short[]
			{
				15,
				44,
				73,
				102,
				131,
				160,
				189,
				218,
				247,
				276,
				305,
				334,
				363,
				392,
				421,
				450
			};
			if (source.Contains(cellId))
			{
				if (cells.ContainsKey(MapaTeleportCeldas.TOP))
				{
					cells[MapaTeleportCeldas.TOP].Add(cellId);
				}
				else
				{
					cells.Add(MapaTeleportCeldas.TOP, new List<short>());
					cells[MapaTeleportCeldas.TOP].Add(cellId);
				}
			}
			if (source2.Contains(cellId))
			{
				if (cells.ContainsKey(MapaTeleportCeldas.RIGHT))
				{
					cells[MapaTeleportCeldas.RIGHT].Add(cellId);
				}
				else
				{
					cells.Add(MapaTeleportCeldas.RIGHT, new List<short>());
					cells[MapaTeleportCeldas.RIGHT].Add(cellId);
				}
			}
			if (source3.Contains(cellId))
			{
				if (cells.ContainsKey(MapaTeleportCeldas.BOTTOM))
				{
					cells[MapaTeleportCeldas.BOTTOM].Add(cellId);
				}
				else
				{
					cells.Add(MapaTeleportCeldas.BOTTOM, new List<short>());
					cells[MapaTeleportCeldas.BOTTOM].Add(cellId);
				}
			}
			if (source4.Contains(cellId))
			{
				if (cells.ContainsKey(MapaTeleportCeldas.LEFT))
				{
					cells[MapaTeleportCeldas.LEFT].Add(cellId);
				}
				else
				{
					cells.Add(MapaTeleportCeldas.LEFT, new List<short>());
					cells[MapaTeleportCeldas.LEFT].Add(cellId);
				}
			}
			return cells;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000268C File Offset: 0x0000088C
		public static void Shuffle<T>(this IList<T> list)
		{
			int i = list.Count;
			while (i > 1)
			{
				i--;
				int index = Extensiones.rng.Next(i + 1);
				T value = list[index];
				list[index] = list[i];
				list[i] = value;
			}
		}

		// Token: 0x0400000F RID: 15
		private static Random rng = new Random();
	}
}
