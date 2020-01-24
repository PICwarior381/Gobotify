using System;
using System.Collections.Generic;

namespace Bot_Dofus_1._29._1.Otros.Mapas.Entidades
{
	// Token: 0x02000055 RID: 85
	public class Npcs : Entidad, IDisposable
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000B90F File Offset: 0x00009B0F
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000B917 File Offset: 0x00009B17
		public int id { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000B920 File Offset: 0x00009B20
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000B928 File Offset: 0x00009B28
		public int npc_modelo_id { get; private set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000B931 File Offset: 0x00009B31
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000B939 File Offset: 0x00009B39
		public Celda celda { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000B942 File Offset: 0x00009B42
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000B94A File Offset: 0x00009B4A
		public short pregunta { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000B953 File Offset: 0x00009B53
		// (set) Token: 0x0600031A RID: 794 RVA: 0x0000B95B File Offset: 0x00009B5B
		public List<short> respuestas { get; set; }

		// Token: 0x0600031B RID: 795 RVA: 0x0000B964 File Offset: 0x00009B64
		public Npcs(int _id, int _npc_modelo_id, Celda _celda)
		{
			this.id = _id;
			this.npc_modelo_id = _npc_modelo_id;
			this.celda = _celda;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000B984 File Offset: 0x00009B84
		~Npcs()
		{
			this.Dispose(false);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000B9BD File Offset: 0x00009BBD
		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				List<short> respuestas = this.respuestas;
				if (respuestas != null)
				{
					respuestas.Clear();
				}
				this.respuestas = null;
				this.celda = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000145 RID: 325
		private bool disposed;
	}
}
