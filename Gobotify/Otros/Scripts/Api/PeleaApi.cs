using System;
using System.Collections.Generic;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones;
using Bot_Dofus_1._29._1.Otros.Scripts.Manejadores;
using MoonSharp.Interpreter;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Api
{
	// Token: 0x02000021 RID: 33
	[MoonSharpUserData]
	public class PeleaApi : IDisposable
	{
		// Token: 0x0600015E RID: 350 RVA: 0x0000648C File Offset: 0x0000468C
		public PeleaApi(Cuenta _cuenta, ManejadorAcciones _manejador_acciones)
		{
			this.cuenta = _cuenta;
			this.manejador_acciones = _manejador_acciones;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000064A2 File Offset: 0x000046A2
		public bool puedePelear(int monstruos_minimos = 1, int monstruos_maximos = 8, int nivel_minimo = 1, int nivel_maximo = 1000, List<int> monstruos_prohibidos = null, List<int> monstruos_obligatorios = null)
		{
			return this.cuenta.juego.mapa.get_Puede_Luchar_Contra_Grupo_Monstruos(monstruos_minimos, monstruos_maximos, nivel_minimo, nivel_maximo, monstruos_prohibidos, monstruos_obligatorios);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000064C2 File Offset: 0x000046C2
		public bool pelear(int monstruos_minimos = 1, int monstruos_maximos = 8, int nivel_minimo = 1, int nivel_maximo = 1000, List<int> monstruos_prohibidos = null, List<int> monstruos_obligatorios = null)
		{
			if (this.puedePelear(monstruos_minimos, monstruos_maximos, nivel_minimo, nivel_maximo, monstruos_prohibidos, monstruos_obligatorios))
			{
				this.manejador_acciones.enqueue_Accion(new PeleasAccion(monstruos_minimos, monstruos_maximos, nivel_minimo, nivel_maximo, monstruos_prohibidos, monstruos_obligatorios), true);
				return true;
			}
			return false;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000064F4 File Offset: 0x000046F4
		~PeleaApi()
		{
			this.Dispose(false);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006524 File Offset: 0x00004724
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000652D File Offset: 0x0000472D
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.cuenta = null;
				this.manejador_acciones = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000076 RID: 118
		private Cuenta cuenta;

		// Token: 0x04000077 RID: 119
		private ManejadorAcciones manejador_acciones;

		// Token: 0x04000078 RID: 120
		private bool disposed;
	}
}
