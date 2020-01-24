using System;

namespace Bot_Dofus_1._29._1.Otros.Mapas.Interactivo
{
	// Token: 0x02000051 RID: 81
	public class ObjetoInteractivo
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000B4AF File Offset: 0x000096AF
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public short gfx { get; private set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000B4C0 File Offset: 0x000096C0
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000B4C8 File Offset: 0x000096C8
		public Celda celda { get; private set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000B4D1 File Offset: 0x000096D1
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000B4D9 File Offset: 0x000096D9
		public ObjetoInteractivoModelo modelo { get; private set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000B4E2 File Offset: 0x000096E2
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000B4EA File Offset: 0x000096EA
		public bool es_utilizable { get; set; }

		// Token: 0x060002EA RID: 746 RVA: 0x0000B4F4 File Offset: 0x000096F4
		public ObjetoInteractivo(short _gfx, Celda _celda)
		{
			this.gfx = _gfx;
			this.celda = _celda;
			ObjetoInteractivoModelo objetoInteractivoModelo = ObjetoInteractivoModelo.get_Modelo_Por_Gfx(this.gfx);
			if (objetoInteractivoModelo != null)
			{
				this.modelo = objetoInteractivoModelo;
				this.es_utilizable = true;
			}
		}
	}
}
