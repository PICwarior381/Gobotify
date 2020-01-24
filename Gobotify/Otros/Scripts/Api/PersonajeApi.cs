using System;
using MoonSharp.Interpreter;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Api
{
	// Token: 0x02000022 RID: 34
	[MoonSharpUserData]
	public class PersonajeApi
	{
		// Token: 0x06000164 RID: 356 RVA: 0x0000654C File Offset: 0x0000474C
		public PersonajeApi(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000655B File Offset: 0x0000475B
		public string nombre()
		{
			return this.cuenta.juego.personaje.nombre;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006572 File Offset: 0x00004772
		public byte nivel()
		{
			return this.cuenta.juego.personaje.nivel;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006589 File Offset: 0x00004789
		public int experiencia()
		{
			return this.cuenta.juego.personaje.porcentaje_experiencia;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000065A0 File Offset: 0x000047A0
		public int kamas()
		{
			return this.cuenta.juego.personaje.kamas;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000065B8 File Offset: 0x000047B8
		~PersonajeApi()
		{
			this.Dispose(false);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000065E8 File Offset: 0x000047E8
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000065F1 File Offset: 0x000047F1
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.cuenta = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000079 RID: 121
		private Cuenta cuenta;

		// Token: 0x0400007A RID: 122
		private bool disposed;
	}
}
