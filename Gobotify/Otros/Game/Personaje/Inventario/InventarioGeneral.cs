using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Inventario.Enums;

namespace Bot_Dofus_1._29._1.Otros.Game.Personaje.Inventario
{
	// Token: 0x0200005D RID: 93
	public class InventarioGeneral : IDisposable, IEliminable
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000D168 File Offset: 0x0000B368
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x0000D170 File Offset: 0x0000B370
		public int kamas { get; private set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000D179 File Offset: 0x0000B379
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x0000D181 File Offset: 0x0000B381
		public short pods_actuales { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000D18A File Offset: 0x0000B38A
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0000D192 File Offset: 0x0000B392
		public short pods_maximos { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000D19B File Offset: 0x0000B39B
		public IEnumerable<ObjetosInventario> objetos
		{
			get
			{
				return this._objetos.Values;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		public IEnumerable<ObjetosInventario> equipamiento
		{
			get
			{
				return from o in this.objetos
				where o.tipo_inventario == TipoObjetosInventario.EQUIPEMENT
				select o;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000D1D4 File Offset: 0x0000B3D4
		public IEnumerable<ObjetosInventario> varios
		{
			get
			{
				return from o in this.objetos
				where o.tipo_inventario == TipoObjetosInventario.DIVERS
				select o;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000D200 File Offset: 0x0000B400
		public IEnumerable<ObjetosInventario> recursos
		{
			get
			{
				return from o in this.objetos
				where o.tipo_inventario == TipoObjetosInventario.RESSOURCES
				select o;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000D22C File Offset: 0x0000B42C
		public IEnumerable<ObjetosInventario> mision
		{
			get
			{
				return from o in this.objetos
				where o.tipo_inventario == TipoObjetosInventario.OBJETS_QUETES
				select o;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000D258 File Offset: 0x0000B458
		public int porcentaje_pods
		{
			get
			{
				return (int)((double)this.pods_actuales / (double)this.pods_maximos * 100.0);
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060003F8 RID: 1016 RVA: 0x0000D274 File Offset: 0x0000B474
		// (remove) Token: 0x060003F9 RID: 1017 RVA: 0x0000D2AC File Offset: 0x0000B4AC
		public event Action<bool> inventario_actualizado;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060003FA RID: 1018 RVA: 0x0000D2E4 File Offset: 0x0000B4E4
		// (remove) Token: 0x060003FB RID: 1019 RVA: 0x0000D31C File Offset: 0x0000B51C
		public event Action almacenamiento_abierto;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060003FC RID: 1020 RVA: 0x0000D354 File Offset: 0x0000B554
		// (remove) Token: 0x060003FD RID: 1021 RVA: 0x0000D38C File Offset: 0x0000B58C
		public event Action almacenamiento_cerrado;

		// Token: 0x060003FE RID: 1022 RVA: 0x0000D3C1 File Offset: 0x0000B5C1
		internal InventarioGeneral(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this._objetos = new ConcurrentDictionary<uint, ObjetosInventario>();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000D3DC File Offset: 0x0000B5DC
		public ObjetosInventario get_Objeto_Modelo_Id(int gid)
		{
			return this.objetos.FirstOrDefault((ObjetosInventario f) => f.id_modelo == gid);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000D410 File Offset: 0x0000B610
		public ObjetosInventario get_Objeto_en_Posicion(InventarioPosiciones posicion)
		{
			return this.objetos.FirstOrDefault((ObjetosInventario o) => o.posicion == posicion);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000D441 File Offset: 0x0000B641
		public void agregar_Objetos(string paquete)
		{
			Task.Run(delegate()
			{
				foreach (string text in paquete.Split(new char[]
				{
					';'
				}))
				{
					if (!string.IsNullOrEmpty(text))
					{
						uint key = Convert.ToUInt32(text.Split(new char[]
						{
							'~'
						})[0], 16);
						ObjetosInventario value = new ObjetosInventario(text);
						this._objetos.TryAdd(key, value);
					}
				}
			}).Wait();
			Action<bool> action = this.inventario_actualizado;
			if (action == null)
			{
				return;
			}
			action(true);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000D47C File Offset: 0x0000B67C
		public void modificar_Objetos(string paquete)
		{
			if (!string.IsNullOrEmpty(paquete))
			{
				string[] separador = paquete.Split(new char[]
				{
					'|'
				});
				ObjetosInventario objetosInventario = this.objetos.FirstOrDefault((ObjetosInventario f) => f.id_inventario == uint.Parse(separador[0]));
				if (objetosInventario != null)
				{
					int cantidad = int.Parse(separador[1]);
					ObjetosInventario objetosInventario2 = objetosInventario;
					objetosInventario2.cantidad = cantidad;
					if (this._objetos.TryUpdate(objetosInventario.id_inventario, objetosInventario2, objetosInventario))
					{
						Action<bool> action = this.inventario_actualizado;
						if (action == null)
						{
							return;
						}
						action(true);
					}
				}
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000D508 File Offset: 0x0000B708
		public void eliminar_Objeto(ObjetosInventario obj, int cantidad, bool paquete_eliminar)
		{
			if (obj == null)
			{
				return;
			}
			cantidad = ((cantidad == 0) ? obj.cantidad : ((cantidad > obj.cantidad) ? obj.cantidad : cantidad));
			if (obj.cantidad > cantidad)
			{
				obj.cantidad -= cantidad;
				this._objetos.TryUpdate(obj.id_inventario, obj, obj);
			}
			else
			{
				ObjetosInventario objetosInventario;
				this._objetos.TryRemove(obj.id_inventario, out objetosInventario);
			}
			if (paquete_eliminar)
			{
				this.cuenta.conexion.enviar_Paquete(string.Format("Od{0}|{1}", obj.id_inventario, cantidad), false);
				this.cuenta.logger.log_informacion("INVENTAIRE", string.Format("{0} {1} éliminée(s).", cantidad, obj.nombre));
			}
			Action<bool> action = this.inventario_actualizado;
			if (action == null)
			{
				return;
			}
			action(true);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		public void eliminar_Objeto(uint id_inventario, int cantidad, bool paquete_eliminar)
		{
			ObjetosInventario obj;
			if (!this._objetos.TryGetValue(id_inventario, out obj))
			{
				return;
			}
			this.eliminar_Objeto(obj, cantidad, paquete_eliminar);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000D610 File Offset: 0x0000B810
		public bool equipar_Objeto(ObjetosInventario objeto)
		{
			if (objeto == null || objeto.cantidad == 0 || this.cuenta.esta_ocupado())
			{
				this.cuenta.logger.log_Error("INVENTAIRE", "El objeto " + objeto.nombre + " ne peux être équipé");
				return false;
			}
			if (objeto.nivel > (short)this.cuenta.juego.personaje.nivel)
			{
				this.cuenta.logger.log_Error("INVENTAIRE", "Le niveau de l'objet " + objeto.nombre + " est supérieur à ton niveau");
				return false;
			}
			if (objeto.posicion != InventarioPosiciones.NON_EQUIPEE)
			{
				this.cuenta.logger.log_Error("INVENTAIRE", "l'objet " + objeto.nombre + " est équipé");
				return false;
			}
			List<InventarioPosiciones> list = InventarioUtiles.get_Posibles_Posiciones((int)objeto.tipo);
			if (list == null || list.Count == 0)
			{
				this.cuenta.logger.log_Error("INVENTARIO", "L'objet " + objeto.nombre + " n'est pas équipable");
				return false;
			}
			foreach (InventarioPosiciones inventarioPosiciones in list)
			{
				if (this.get_Objeto_en_Posicion(inventarioPosiciones) == null)
				{
					this.cuenta.conexion.enviar_Paquete("OM" + objeto.id_inventario.ToString() + "|" + ((sbyte)inventarioPosiciones).ToString(), true);
					this.cuenta.logger.log_informacion("INVENTAIRE", objeto.nombre + " équipé.");
					objeto.posicion = inventarioPosiciones;
					Action<bool> action = this.inventario_actualizado;
					if (action != null)
					{
						action(true);
					}
					return true;
				}
			}
			ObjetosInventario objetosInventario;
			if (this._objetos.TryGetValue(this.get_Objeto_en_Posicion(list[0]).id_inventario, out objetosInventario))
			{
				objetosInventario.posicion = InventarioPosiciones.NON_EQUIPEE;
				this.cuenta.conexion.enviar_Paquete("OM" + objetosInventario.id_inventario.ToString() + "|" + -1.ToString(), false);
			}
			this.cuenta.conexion.enviar_Paquete("OM" + objeto.id_inventario.ToString() + "|" + ((sbyte)list[0]).ToString(), false);
			if (objeto.cantidad == 1)
			{
				objeto.posicion = list[0];
			}
			this.cuenta.logger.log_informacion("INVENTAIRE", objeto.nombre + " équipé.");
			Action<bool> action2 = this.inventario_actualizado;
			if (action2 != null)
			{
				action2(true);
			}
			return true;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000D8E0 File Offset: 0x0000BAE0
		public bool desequipar_Objeto(ObjetosInventario objeto)
		{
			if (objeto == null)
			{
				return false;
			}
			if (objeto.posicion == InventarioPosiciones.NON_EQUIPEE)
			{
				return false;
			}
			this.cuenta.conexion.enviar_Paquete("OM" + objeto.id_inventario.ToString() + "|" + -1.ToString(), false);
			objeto.posicion = InventarioPosiciones.NON_EQUIPEE;
			this.cuenta.logger.log_informacion("INVENTAIRE", objeto.nombre + " déséquipé.");
			Action<bool> action = this.inventario_actualizado;
			if (action != null)
			{
				action(true);
			}
			return true;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000D974 File Offset: 0x0000BB74
		public void utilizar_Objeto(ObjetosInventario objeto)
		{
			if (objeto == null)
			{
				return;
			}
			if (objeto.cantidad == 0)
			{
				this.cuenta.logger.log_Error("INVENTAIRE", "L'objet " + objeto.nombre + " ne peut être mis à cause de tes caractéristiques");
				return;
			}
			this.cuenta.conexion.enviar_Paquete("OU" + objeto.id_inventario.ToString() + "|", false);
			this.eliminar_Objeto(objeto, 1, false);
			this.cuenta.logger.log_informacion("INVENTAIRE", objeto.nombre + " utilisée.");
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000DA14 File Offset: 0x0000BC14
		public void evento_Almacenamiento_Abierto()
		{
			Action action = this.almacenamiento_abierto;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000DA26 File Offset: 0x0000BC26
		public void evento_Almacenamiento_Cerrado()
		{
			Action action = this.almacenamiento_cerrado;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000DA38 File Offset: 0x0000BC38
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000DA44 File Offset: 0x0000BC44
		~InventarioGeneral()
		{
			this.Dispose(false);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000DA74 File Offset: 0x0000BC74
		public void limpiar()
		{
			this.kamas = 0;
			this.pods_actuales = 0;
			this.pods_maximos = 0;
			this._objetos.Clear();
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000DA96 File Offset: 0x0000BC96
		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this._objetos.Clear();
				this._objetos = null;
				this.cuenta = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000198 RID: 408
		private Cuenta cuenta;

		// Token: 0x04000199 RID: 409
		private ConcurrentDictionary<uint, ObjetosInventario> _objetos;

		// Token: 0x0400019A RID: 410
		private bool disposed;
	}
}
