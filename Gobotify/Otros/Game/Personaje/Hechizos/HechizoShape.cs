using System;
using System.Collections.Generic;
using Bot_Dofus_1._29._1.Otros.Mapas;

namespace Bot_Dofus_1._29._1.Otros.Game.Personaje.Hechizos
{
	// Token: 0x02000063 RID: 99
	public class HechizoShape
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x0000E16C File Offset: 0x0000C36C
		public static IEnumerable<Celda> Get_Lista_Celdas_Rango_Hechizo(Celda celda, HechizoStats spellLevel, Mapa mapa, int rango_adicional = 0)
		{
			int radio_maximo = (int)spellLevel.alcanze_maximo + (spellLevel.es_alcanze_modificable ? rango_adicional : 0);
			if (spellLevel.es_lanzado_linea)
			{
				return Shaper.Cruz(celda.x, celda.y, (int)spellLevel.alcanze_minimo, radio_maximo, mapa);
			}
			return Shaper.Anillo(celda.x, celda.y, (int)spellLevel.alcanze_minimo, radio_maximo, mapa);
		}
	}
}
