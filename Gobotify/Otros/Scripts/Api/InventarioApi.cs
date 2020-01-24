using System;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Inventario;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Inventario.Enums;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Inventario;
using Bot_Dofus_1._29._1.Otros.Scripts.Manejadores;
using MoonSharp.Interpreter;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Api
{
	// Token: 0x0200001E RID: 30
	[MoonSharpUserData]
	public class InventarioApi : IDisposable
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00006005 File Offset: 0x00004205
		public InventarioApi(Cuenta _cuenta, ManejadorAcciones _manejar_acciones)
		{
			this.cuenta = _cuenta;
			this.manejar_acciones = _manejar_acciones;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000601B File Offset: 0x0000421B
		public int pods()
		{
			return (int)this.cuenta.juego.personaje.inventario.pods_actuales;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006037 File Offset: 0x00004237
		public int podsMaximos()
		{
			return (int)this.cuenta.juego.personaje.inventario.pods_maximos;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006053 File Offset: 0x00004253
		public int podsPorcentaje()
		{
			return this.cuenta.juego.personaje.inventario.porcentaje_pods;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000606F File Offset: 0x0000426F
		public bool tieneObjeto(int modelo_id)
		{
			return this.cuenta.juego.personaje.inventario.get_Objeto_Modelo_Id(modelo_id) != null;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000608F File Offset: 0x0000428F
		public bool utilizar(int modelo_id)
		{
			if (this.cuenta.juego.personaje.inventario.get_Objeto_Modelo_Id(modelo_id) == null)
			{
				return false;
			}
			this.manejar_acciones.enqueue_Accion(new UtilizarObjetoAccion(modelo_id), true);
			return true;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000060C4 File Offset: 0x000042C4
		public bool equipar(int modelo_id)
		{
			ObjetosInventario objetosInventario = this.cuenta.juego.personaje.inventario.get_Objeto_Modelo_Id(modelo_id);
			if (objetosInventario == null || objetosInventario.posicion != InventarioPosiciones.NON_EQUIPEE)
			{
				return false;
			}
			this.manejar_acciones.enqueue_Accion(new EquiparItemAccion(modelo_id), true);
			return true;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006110 File Offset: 0x00004310
		~InventarioApi()
		{
			this.Dispose(false);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006140 File Offset: 0x00004340
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006149 File Offset: 0x00004349
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.cuenta = null;
				this.manejar_acciones = null;
				this.disposed = true;
			}
		}

		// Token: 0x0400006D RID: 109
		private Cuenta cuenta;

		// Token: 0x0400006E RID: 110
		private ManejadorAcciones manejar_acciones;

		// Token: 0x0400006F RID: 111
		private bool disposed;
	}
}
