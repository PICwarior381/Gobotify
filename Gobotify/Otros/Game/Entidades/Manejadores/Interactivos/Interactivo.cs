using System;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Movimientos;
using Bot_Dofus_1._29._1.Otros.Mapas.Interactivo;

namespace Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Interactivos
{
	// Token: 0x02000071 RID: 113
	public class Interactivo : IDisposable
	{
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000490 RID: 1168 RVA: 0x0000F5C8 File Offset: 0x0000D7C8
		// (remove) Token: 0x06000491 RID: 1169 RVA: 0x0000F600 File Offset: 0x0000D800
		public event Action<bool> fin_interactivo;

		// Token: 0x06000492 RID: 1170 RVA: 0x0000F635 File Offset: 0x0000D835
		public Interactivo(Cuenta _cuenta, Movimiento movimiento)
		{
			this.cuenta = _cuenta;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000F644 File Offset: 0x0000D844
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000F650 File Offset: 0x0000D850
		~Interactivo()
		{
			this.Dispose(false);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000F680 File Offset: 0x0000D880
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.interactivo_utilizado = null;
				this.cuenta = null;
				this.disposed = true;
			}
		}

		// Token: 0x0400020D RID: 525
		private Cuenta cuenta;

		// Token: 0x0400020E RID: 526
		private ObjetoInteractivo interactivo_utilizado;

		// Token: 0x04000210 RID: 528
		private bool disposed;
	}
}
