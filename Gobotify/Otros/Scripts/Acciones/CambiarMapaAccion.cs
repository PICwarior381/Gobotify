using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Movimientos;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Utilidades.Criptografia;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones
{
	// Token: 0x02000026 RID: 38
	public class CambiarMapaAccion : AccionesScript
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00006652 File Offset: 0x00004852
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000665A File Offset: 0x0000485A
		public MapaTeleportCeldas direccion { get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00006663 File Offset: 0x00004863
		// (set) Token: 0x06000176 RID: 374 RVA: 0x0000666B File Offset: 0x0000486B
		public short celda_id { get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00006674 File Offset: 0x00004874
		public bool celda_especifica
		{
			get
			{
				return this.direccion == MapaTeleportCeldas.NULL && this.celda_id != -1;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000668C File Offset: 0x0000488C
		public bool direccion_especifica
		{
			get
			{
				return this.direccion != MapaTeleportCeldas.NULL && this.celda_id == -1;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000066A1 File Offset: 0x000048A1
		public CambiarMapaAccion(MapaTeleportCeldas _direccion, short _celda_id)
		{
			this.direccion = _direccion;
			this.celda_id = _celda_id;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000066B8 File Offset: 0x000048B8
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			if (this.celda_especifica)
			{
				Celda celda = cuenta.juego.mapa.get_Celda_Id(this.celda_id);
				if (!cuenta.juego.manejador.movimientos.get_Cambiar_Mapa(this.direccion, celda, false))
				{
					return AccionesScript.resultado_fallado;
				}
				if (!cuenta.es_lider_grupo || !cuenta.tiene_grupo)
				{
					goto IL_144;
				}
				using (IEnumerator<Cuenta> enumerator = cuenta.grupo.miembros.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Cuenta cuenta2 = enumerator.Current;
						celda = cuenta2.juego.mapa.get_Celda_Id(this.celda_id);
						bool flag = cuenta2.juego.manejador.movimientos.get_Cambiar_Mapa(this.direccion, celda, false);
					}
					goto IL_144;
				}
			}
			if (this.direccion_especifica)
			{
				if (!cuenta.juego.manejador.movimientos.get_Cambiar_Mapa(this.direccion))
				{
					return AccionesScript.resultado_fallado;
				}
				if (cuenta.es_lider_grupo && cuenta.tiene_grupo)
				{
					foreach (Cuenta cuenta3 in cuenta.grupo.miembros)
					{
						bool flag2 = cuenta3.juego.manejador.movimientos.get_Cambiar_Mapa(this.direccion);
					}
				}
			}
			IL_144:
			return AccionesScript.resultado_procesado;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000682C File Offset: 0x00004A2C
		public static bool TryParse(string texto, out CambiarMapaAccion accion)
		{
			string[] array = texto.Split(new char[]
			{
				'|'
			});
			string input = array[Randomize.get_Random(0, array.Length)];
			Match match = Regex.Match(input, "(?<direction>TOP|RIGHT|BOTTOM|LEFT)\\((?<celda>\\d{1,3})\\)");
			if (match.Success)
			{
				accion = new CambiarMapaAccion((MapaTeleportCeldas)Enum.Parse(typeof(MapaTeleportCeldas), match.Groups["direction"].Value, true), short.Parse(match.Groups["celda"].Value));
				return true;
			}
			match = Regex.Match(input, "(?<direction>TOP|RIGHT|BOTTOM|LEFT)");
			if (match.Success)
			{
				accion = new CambiarMapaAccion((MapaTeleportCeldas)Enum.Parse(typeof(MapaTeleportCeldas), match.Groups["direction"].Value, true), -1);
				return true;
			}
			match = Regex.Match(input, "(?<celda>\\d{1,3})");
			if (match.Success)
			{
				accion = new CambiarMapaAccion(MapaTeleportCeldas.NULL, short.Parse(match.Groups["celda"].Value));
				return true;
			}
			accion = null;
			return false;
		}
	}
}
