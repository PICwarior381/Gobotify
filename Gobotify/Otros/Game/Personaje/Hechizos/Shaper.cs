using System;
using System.Collections.Generic;
using System.Linq;
using Bot_Dofus_1._29._1.Otros.Mapas;

namespace Bot_Dofus_1._29._1.Otros.Game.Personaje.Hechizos
{
	// Token: 0x02000066 RID: 102
	internal class Shaper
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x0000E2D0 File Offset: 0x0000C4D0
		public static IEnumerable<Celda> Circulo(int x, int y, int radio_minimo, int radio_maximo, Mapa mapa)
		{
			List<Celda> list = new List<Celda>();
			if (radio_minimo == 0)
			{
				list.Add(mapa.get_Celda_Por_Coordenadas(x, y));
			}
			for (int i = (radio_minimo == 0) ? 1 : radio_minimo; i <= radio_maximo; i++)
			{
				for (int j = 0; j < i; j++)
				{
					int num = i - j;
					list.Add(mapa.get_Celda_Por_Coordenadas(x + j, y - num));
					list.Add(mapa.get_Celda_Por_Coordenadas(x + num, y + j));
					list.Add(mapa.get_Celda_Por_Coordenadas(x - j, y + num));
					list.Add(mapa.get_Celda_Por_Coordenadas(x - num, y - j));
				}
			}
			return from c in list
			where c != null
			select c;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000E388 File Offset: 0x0000C588
		public static IEnumerable<Celda> Linea(int x, int y, int radio_minimo, int radio_maximo, Mapa mapa)
		{
			List<Celda> list = new List<Celda>();
			for (int i = radio_minimo; i <= radio_maximo; i++)
			{
				list.Add(mapa.get_Celda_Por_Coordenadas(x * i, y * i));
			}
			return from c in list
			where c != null
			select c;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000E3E0 File Offset: 0x0000C5E0
		public static IEnumerable<Celda> Cruz(int x, int y, int radio_minimo, int radio_maximo, Mapa mapa)
		{
			List<Celda> list = new List<Celda>();
			if (radio_minimo == 0)
			{
				list.Add(mapa.get_Celda_Por_Coordenadas(x, y));
			}
			for (int i = (radio_minimo == 0) ? 1 : radio_minimo; i <= radio_maximo; i++)
			{
				list.Add(mapa.get_Celda_Por_Coordenadas(x - i, y));
				list.Add(mapa.get_Celda_Por_Coordenadas(x + i, y));
				list.Add(mapa.get_Celda_Por_Coordenadas(x, y - i));
				list.Add(mapa.get_Celda_Por_Coordenadas(x, y + i));
			}
			return from c in list
			where c != null
			select c;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000E480 File Offset: 0x0000C680
		public static IEnumerable<Celda> Anillo(int x, int y, int radio_minimo, int radio_maximo, Mapa mapa)
		{
			List<Celda> list = new List<Celda>();
			if (radio_minimo == 0)
			{
				list.Add(mapa.get_Celda_Por_Coordenadas(x, y));
			}
			for (int i = (radio_minimo == 0) ? 1 : radio_minimo; i <= radio_maximo; i++)
			{
				for (int j = 0; j < i; j++)
				{
					int num = i - j;
					list.Add(mapa.get_Celda_Por_Coordenadas(x + j, y - num));
					list.Add(mapa.get_Celda_Por_Coordenadas(x + num, y + j));
					list.Add(mapa.get_Celda_Por_Coordenadas(x - j, y + num));
					list.Add(mapa.get_Celda_Por_Coordenadas(x - num, y - j));
				}
			}
			return from c in list
			where c != null
			select c;
		}
	}
}
