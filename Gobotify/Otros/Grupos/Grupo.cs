using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones;

namespace Bot_Dofus_1._29._1.Otros.Grupos
{
	// Token: 0x02000045 RID: 69
	public class Grupo : IDisposable
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00009844 File Offset: 0x00007A44
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000984C File Offset: 0x00007A4C
		public Cuenta lider { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00009855 File Offset: 0x00007A55
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000985D File Offset: 0x00007A5D
		public ObservableCollection<Cuenta> miembros { get; private set; }

		// Token: 0x06000254 RID: 596 RVA: 0x00009866 File Offset: 0x00007A66
		public Grupo(Cuenta _lider)
		{
			this.agrupamiento = new Agrupamiento(this);
			this.cuentas_acabadas = new Dictionary<Cuenta, ManualResetEvent>();
			this.lider = _lider;
			this.miembros = new ObservableCollection<Cuenta>();
			this.lider.grupo = this;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000098A3 File Offset: 0x00007AA3
		public void agregar_Miembro(Cuenta miembro)
		{
			if (this.miembros.Count >= 7)
			{
				return;
			}
			miembro.grupo = this;
			this.miembros.Add(miembro);
			this.cuentas_acabadas.Add(miembro, new ManualResetEvent(false));
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000098D9 File Offset: 0x00007AD9
		public void eliminar_Miembro(Cuenta miembro)
		{
			this.miembros.Remove(miembro);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000098E8 File Offset: 0x00007AE8
		public void conectar_Cuentas()
		{
			this.lider.conectar();
			foreach (Cuenta cuenta in this.miembros)
			{
				cuenta.conectar();
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009940 File Offset: 0x00007B40
		public void desconectar_Cuentas()
		{
			foreach (Cuenta cuenta in this.miembros)
			{
				cuenta.desconectar();
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000998C File Offset: 0x00007B8C
		public void enqueue_Acciones_Miembros(AccionesScript accion, bool iniciar_dequeue = false)
		{
			if (accion is PeleasAccion)
			{
				foreach (Cuenta key in this.miembros)
				{
					this.cuentas_acabadas[key].Set();
				}
				return;
			}
			foreach (Cuenta cuenta in this.miembros)
			{
				Task.Delay(1500);
				cuenta.logger.log_Peligro("GROUPE", "Je suis mon chef !");
				cuenta.logger.log_Peligro("GROUPE", accion.ToString());
				cuenta.script.manejar_acciones.enqueue_Accion(accion, iniciar_dequeue);
				accion.proceso(cuenta);
			}
			if (iniciar_dequeue)
			{
				for (int i = 0; i < this.miembros.Count; i++)
				{
					this.cuentas_acabadas[this.miembros[i]].Reset();
				}
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009AA8 File Offset: 0x00007CA8
		public void esperar_Acciones_Terminadas()
		{
			WaitHandle[] waitHandles = this.cuentas_acabadas.Values.ToArray<ManualResetEvent>();
			WaitHandle.WaitAll(waitHandles);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009ACD File Offset: 0x00007CCD
		private void miembro_Acciones_Acabadas(Cuenta cuenta)
		{
			cuenta.logger.log_informacion("GROUPE", "Actions terminées");
			this.cuentas_acabadas[cuenta].Set();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009AF6 File Offset: 0x00007CF6
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009B00 File Offset: 0x00007D00
		~Grupo()
		{
			this.Dispose(false);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009B30 File Offset: 0x00007D30
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.agrupamiento.Dispose();
					this.lider.Dispose();
					for (int i = 0; i < this.miembros.Count; i++)
					{
						this.miembros[i].Dispose();
					}
				}
				this.agrupamiento = null;
				this.cuentas_acabadas.Clear();
				this.cuentas_acabadas = null;
				this.miembros.Clear();
				this.miembros = null;
				this.lider = null;
				this.disposed = true;
			}
		}

		// Token: 0x040000EE RID: 238
		private Agrupamiento agrupamiento;

		// Token: 0x040000EF RID: 239
		private Dictionary<Cuenta, ManualResetEvent> cuentas_acabadas;

		// Token: 0x040000F2 RID: 242
		private bool disposed;
	}
}
