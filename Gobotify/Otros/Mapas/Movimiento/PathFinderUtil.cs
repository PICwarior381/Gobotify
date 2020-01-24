using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bot_Dofus_1._29._1.Otros.Mapas.Movimiento.Mapas;
using Bot_Dofus_1._29._1.Utilidades.Criptografia;

namespace Bot_Dofus_1._29._1.Otros.Mapas.Movimiento
{
	// Token: 0x02000049 RID: 73
	internal class PathFinderUtil
	{
		// Token: 0x060002AD RID: 685 RVA: 0x0000A89C File Offset: 0x00008A9C
		public static int get_Tiempo_Desplazamiento_Mapa(Celda celda_actual, List<Celda> celdas_camino, bool con_montura = false)
		{
			int num = 20;
			DuracionAnimacion duracionAnimacion;
			if (con_montura)
			{
				duracionAnimacion = PathFinderUtil.tiempo_tipo_animacion[TipoAnimacion.MONTURA];
			}
			else
			{
				duracionAnimacion = ((celdas_camino.Count > 6) ? PathFinderUtil.tiempo_tipo_animacion[TipoAnimacion.CORRIENDO] : PathFinderUtil.tiempo_tipo_animacion[TipoAnimacion.CAMINANDO]);
			}
			for (int i = 1; i < celdas_camino.Count; i++)
			{
				Celda celda = celdas_camino[i];
				if (celda_actual.y == celda.y)
				{
					num += duracionAnimacion.horizontal;
				}
				else if (celda_actual.x == celda.y)
				{
					num += duracionAnimacion.vertical;
				}
				else
				{
					num += duracionAnimacion.lineal;
				}
				if (celda_actual.layer_ground_nivel < celda.layer_ground_nivel)
				{
					num += 100;
				}
				else if (celda.layer_ground_nivel > celda_actual.layer_ground_nivel)
				{
					num -= 100;
				}
				else if (celda_actual.layer_ground_slope != celda.layer_ground_slope)
				{
					if (celda_actual.layer_ground_slope == 1)
					{
						num += 100;
					}
					else if (celda.layer_ground_slope == 1)
					{
						num -= 100;
					}
				}
				celda_actual = celda;
			}
			return num;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000A994 File Offset: 0x00008B94
		public static string get_Pathfinding_Limpio(List<Celda> camino)
		{
			Celda celda = camino.Last<Celda>();
			if (camino.Count <= 2)
			{
				return celda.get_Direccion_Char(camino.First<Celda>()).ToString() + Hash.get_Celda_Char(celda.id);
			}
			StringBuilder stringBuilder = new StringBuilder();
			char c = camino[1].get_Direccion_Char(camino.First<Celda>());
			for (int i = 2; i < camino.Count; i++)
			{
				Celda celda2 = camino[i];
				Celda celda3 = camino[i - 1];
				char c2 = celda2.get_Direccion_Char(celda3);
				if (c != c2)
				{
					stringBuilder.Append(c);
					stringBuilder.Append(Hash.get_Celda_Char(celda3.id));
					c = c2;
				}
			}
			stringBuilder.Append(c);
			stringBuilder.Append(Hash.get_Celda_Char(celda.id));
			return stringBuilder.ToString();
		}

		// Token: 0x04000118 RID: 280
		private static readonly Dictionary<TipoAnimacion, DuracionAnimacion> tiempo_tipo_animacion = new Dictionary<TipoAnimacion, DuracionAnimacion>
		{
			{
				TipoAnimacion.MONTURA,
				new DuracionAnimacion(135, 200, 120)
			},
			{
				TipoAnimacion.CORRIENDO,
				new DuracionAnimacion(170, 255, 150)
			},
			{
				TipoAnimacion.CAMINANDO,
				new DuracionAnimacion(480, 510, 425)
			},
			{
				TipoAnimacion.FANTASMA,
				new DuracionAnimacion(57, 85, 50)
			}
		};
	}
}
