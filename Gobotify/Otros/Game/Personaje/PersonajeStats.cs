using System;

namespace Bot_Dofus_1._29._1.Otros.Game.Personaje
{
	// Token: 0x02000059 RID: 89
	public class PersonajeStats : IEliminable
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000CB26 File Offset: 0x0000AD26
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0000CB2E File Offset: 0x0000AD2E
		public int base_personaje { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000CB37 File Offset: 0x0000AD37
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000CB3F File Offset: 0x0000AD3F
		public int equipamiento { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000CB48 File Offset: 0x0000AD48
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0000CB50 File Offset: 0x0000AD50
		public int dones { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000CB59 File Offset: 0x0000AD59
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0000CB61 File Offset: 0x0000AD61
		public int boost { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000CB6A File Offset: 0x0000AD6A
		public int total_stats
		{
			get
			{
				return this.base_personaje + this.equipamiento + this.dones + this.boost;
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000CB87 File Offset: 0x0000AD87
		public PersonajeStats(int _base_personaje)
		{
			this.base_personaje = _base_personaje;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000CB96 File Offset: 0x0000AD96
		public PersonajeStats(int _base_personaje, int _equipamiento, int _dones, int _boost)
		{
			this.actualizar_Stats(_base_personaje, _equipamiento, _dones, _boost);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000CBA9 File Offset: 0x0000ADA9
		public void actualizar_Stats(int _base_personaje, int _equipamiento, int _dones, int _boost)
		{
			this.base_personaje = _base_personaje;
			this.equipamiento = _equipamiento;
			this.dones = _dones;
			this.boost = _boost;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000CBC8 File Offset: 0x0000ADC8
		public void limpiar()
		{
			this.base_personaje = 0;
			this.equipamiento = 0;
			this.dones = 0;
			this.boost = 0;
		}
	}
}
