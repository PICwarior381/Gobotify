using System;
using System.Linq;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Npcs;
using Bot_Dofus_1._29._1.Otros.Scripts.Manejadores;
using MoonSharp.Interpreter;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Api
{
	// Token: 0x02000020 RID: 32
	[MoonSharpUserData]
	public class NpcAPI : IDisposable
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00006328 File Offset: 0x00004528
		public NpcAPI(Cuenta _cuenta, ManejadorAcciones _manejador_acciones)
		{
			this.cuenta = _cuenta;
			this.manejador_acciones = _manejador_acciones;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006340 File Offset: 0x00004540
		public bool npcBanco(int npc_id)
		{
			if (npc_id > 0 && this.cuenta.juego.mapa.lista_npcs().FirstOrDefault((Npcs n) => n.npc_modelo_id == npc_id) == null)
			{
				return false;
			}
			this.manejador_acciones.enqueue_Accion(new NpcBancoAccion(npc_id), true);
			return true;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000063A8 File Offset: 0x000045A8
		public bool hablarNpc(int npc_id)
		{
			if (npc_id > 0 && this.cuenta.juego.mapa.lista_npcs().FirstOrDefault((Npcs n) => n.npc_modelo_id == npc_id) == null)
			{
				return false;
			}
			this.manejador_acciones.enqueue_Accion(new NpcAccion(npc_id), true);
			return true;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000640D File Offset: 0x0000460D
		public void responder(short respuesta_id)
		{
			this.manejador_acciones.enqueue_Accion(new RespuestaAccion(respuesta_id), true);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006421 File Offset: 0x00004621
		public void cerrar(short respuesta_id)
		{
			this.manejador_acciones.enqueue_Accion(new CerrarDialogoAccion(), true);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006434 File Offset: 0x00004634
		~NpcAPI()
		{
			this.Dispose(false);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006464 File Offset: 0x00004664
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000646D File Offset: 0x0000466D
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.cuenta = null;
				this.manejador_acciones = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000073 RID: 115
		private Cuenta cuenta;

		// Token: 0x04000074 RID: 116
		private ManejadorAcciones manejador_acciones;

		// Token: 0x04000075 RID: 117
		private bool disposed;
	}
}
