using System;
using System.Collections.Generic;

namespace Bot_Dofus_1._29._1.Otros.Grupos
{
	// Token: 0x02000044 RID: 68
	public class Agrupamiento : IDisposable
	{
		// Token: 0x0600024C RID: 588 RVA: 0x000097CA File Offset: 0x000079CA
		public Agrupamiento(Grupo _grupo)
		{
			this.grupo = _grupo;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000097D9 File Offset: 0x000079D9
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000097E4 File Offset: 0x000079E4
		~Agrupamiento()
		{
			this.Dispose(false);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00009814 File Offset: 0x00007A14
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.grupo = null;
				List<Cuenta> list = this.miembros_perdidos;
				if (list != null)
				{
					list.Clear();
				}
				this.miembros_perdidos = null;
				this.disposed = true;
			}
		}

		// Token: 0x040000EB RID: 235
		private Grupo grupo;

		// Token: 0x040000EC RID: 236
		private List<Cuenta> miembros_perdidos;

		// Token: 0x040000ED RID: 237
		private bool disposed;
	}
}
