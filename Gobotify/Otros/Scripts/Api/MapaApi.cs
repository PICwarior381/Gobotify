using System;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Mapas;
using Bot_Dofus_1._29._1.Otros.Scripts.Manejadores;
using MoonSharp.Interpreter;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Api
{
	// Token: 0x0200001F RID: 31
	[MoonSharpUserData]
	public class MapaApi : IDisposable
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00006168 File Offset: 0x00004368
		public MapaApi(Cuenta _cuenta, ManejadorAcciones _manejador_acciones)
		{
			this.cuenta = _cuenta;
			this.manejador_acciones = _manejador_acciones;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006180 File Offset: 0x00004380
		public bool cambiarMapa(string posicion)
		{
			if (this.cuenta.esta_ocupado())
			{
				return false;
			}
			CambiarMapaAccion accion;
			if (!CambiarMapaAccion.TryParse(posicion, out accion))
			{
				this.cuenta.logger.log_Error("MapaApi", "Verifica el cambio de mapa " + posicion);
				return false;
			}
			this.manejador_acciones.enqueue_Accion(accion, true);
			return true;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000061D8 File Offset: 0x000043D8
		public bool moverCelda(short celda_id)
		{
			if (celda_id < 0 || (int)celda_id > this.cuenta.juego.mapa.celdas.Length)
			{
				return false;
			}
			if (!this.cuenta.juego.mapa.get_Celda_Id(celda_id).es_Caminable() || this.cuenta.juego.mapa.get_Celda_Id(celda_id).es_linea_vision)
			{
				return false;
			}
			this.manejador_acciones.enqueue_Accion(new MoverCeldaAccion(celda_id), true);
			return true;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006254 File Offset: 0x00004454
		public bool enCelda(short celda_id)
		{
			return this.cuenta.juego.personaje.celda.id == celda_id;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006273 File Offset: 0x00004473
		public bool enMapa(string coordenadas)
		{
			return this.cuenta.juego.mapa.esta_En_Mapa(coordenadas);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000628C File Offset: 0x0000448C
		public string actualMapa()
		{
			return this.cuenta.juego.mapa.id.ToString();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000062B6 File Offset: 0x000044B6
		public string actualPosicion()
		{
			return this.cuenta.juego.mapa.coordenadas;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000062D0 File Offset: 0x000044D0
		~MapaApi()
		{
			this.Dispose(false);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006300 File Offset: 0x00004500
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006309 File Offset: 0x00004509
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.cuenta = null;
				this.manejador_acciones = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000070 RID: 112
		private Cuenta cuenta;

		// Token: 0x04000071 RID: 113
		private ManejadorAcciones manejador_acciones;

		// Token: 0x04000072 RID: 114
		private bool disposed;
	}
}
