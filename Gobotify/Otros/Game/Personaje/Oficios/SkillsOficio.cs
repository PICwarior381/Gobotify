using System;
using Bot_Dofus_1._29._1.Otros.Mapas.Interactivo;

namespace Bot_Dofus_1._29._1.Otros.Game.Personaje.Oficios
{
	// Token: 0x0200005C RID: 92
	public class SkillsOficio
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000D081 File Offset: 0x0000B281
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0000D089 File Offset: 0x0000B289
		public short id { get; private set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000D092 File Offset: 0x0000B292
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000D09A File Offset: 0x0000B29A
		public byte cantidad_minima { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000D0A3 File Offset: 0x0000B2A3
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0000D0AB File Offset: 0x0000B2AB
		public byte cantidad_maxima { get; private set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000D0B4 File Offset: 0x0000B2B4
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0000D0BC File Offset: 0x0000B2BC
		public ObjetoInteractivoModelo interactivo_modelo { get; private set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000D0C5 File Offset: 0x0000B2C5
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x0000D0CD File Offset: 0x0000B2CD
		public bool es_craft { get; private set; } = true;

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000D0D6 File Offset: 0x0000B2D6
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x0000D0DE File Offset: 0x0000B2DE
		public float tiempo { get; private set; }

		// Token: 0x060003EA RID: 1002 RVA: 0x0000D0E8 File Offset: 0x0000B2E8
		public SkillsOficio(short _id, byte _cantidad_minima, byte _cantidad_maxima, float _tiempo)
		{
			this.id = _id;
			this.cantidad_minima = _cantidad_minima;
			this.cantidad_maxima = _cantidad_maxima;
			ObjetoInteractivoModelo objetoInteractivoModelo = ObjetoInteractivoModelo.get_Modelo_Por_Habilidad(this.id);
			if (objetoInteractivoModelo != null)
			{
				this.interactivo_modelo = objetoInteractivoModelo;
				if (this.interactivo_modelo.recolectable)
				{
					this.es_craft = false;
				}
			}
			this.tiempo = _tiempo;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000D149 File Offset: 0x0000B349
		public void set_Actualizar(short _id, byte _cantidad_minima, byte _cantidad_maxima, float _tiempo)
		{
			this.id = _id;
			this.cantidad_minima = _cantidad_minima;
			this.cantidad_maxima = _cantidad_maxima;
			this.tiempo = _tiempo;
		}
	}
}
