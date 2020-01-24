using System;

namespace Bot_Dofus_1._29._1.Otros.Game.Personaje.Hechizos
{
	// Token: 0x02000062 RID: 98
	public class HechizoEfecto
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000E134 File Offset: 0x0000C334
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000E13C File Offset: 0x0000C33C
		public int id { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000E145 File Offset: 0x0000C345
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000E14D File Offset: 0x0000C34D
		public Zonas zona_efecto { get; set; }

		// Token: 0x0600042A RID: 1066 RVA: 0x0000E156 File Offset: 0x0000C356
		public HechizoEfecto(int _id, Zonas zona)
		{
			this.id = _id;
			this.zona_efecto = zona;
		}
	}
}
