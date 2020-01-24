using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game.Personaje;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Inventario;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Almacenamiento;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Global;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Npcs;
using Bot_Dofus_1._29._1.Otros.Scripts.Api;
using Bot_Dofus_1._29._1.Otros.Scripts.Banderas;
using Bot_Dofus_1._29._1.Otros.Scripts.Manejadores;
using Bot_Dofus_1._29._1.Properties;
using Bot_Dofus_1._29._1.Utilidades.Extensiones;
using MoonSharp.Interpreter;

namespace Bot_Dofus_1._29._1.Otros.Scripts
{
	// Token: 0x02000013 RID: 19
	public class ManejadorScript : IDisposable
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003C86 File Offset: 0x00001E86
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003C8E File Offset: 0x00001E8E
		public ManejadorAcciones manejar_acciones { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003C97 File Offset: 0x00001E97
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00003CA0 File Offset: 0x00001EA0
		public bool activado
		{
			get
			{
				return this._activado;
			}
			set
			{
				this._activado = value;
				if (this.cuenta.tiene_grupo && this.cuenta.es_lider_grupo)
				{
					foreach (Cuenta cuenta in this.cuenta.grupo.miembros)
					{
						cuenta.script._activado = value;
					}
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003D1C File Offset: 0x00001F1C
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00003D24 File Offset: 0x00001F24
		public bool pausado
		{
			get
			{
				return this._pausado;
			}
			set
			{
				this._pausado = value;
				if (this.cuenta.tiene_grupo && this.cuenta.es_lider_grupo)
				{
					foreach (Cuenta cuenta in this.cuenta.grupo.miembros)
					{
						cuenta.script._pausado = value;
					}
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003DA0 File Offset: 0x00001FA0
		public bool corriendo
		{
			get
			{
				return this.activado && !this.pausado;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060000D1 RID: 209 RVA: 0x00003DB8 File Offset: 0x00001FB8
		// (remove) Token: 0x060000D2 RID: 210 RVA: 0x00003DF0 File Offset: 0x00001FF0
		public event Action<string> evento_script_cargado;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060000D3 RID: 211 RVA: 0x00003E28 File Offset: 0x00002028
		// (remove) Token: 0x060000D4 RID: 212 RVA: 0x00003E60 File Offset: 0x00002060
		public event Action evento_script_iniciado;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060000D5 RID: 213 RVA: 0x00003E98 File Offset: 0x00002098
		// (remove) Token: 0x060000D6 RID: 214 RVA: 0x00003ED0 File Offset: 0x000020D0
		public event Action<string> evento_script_detenido;

		// Token: 0x060000D7 RID: 215 RVA: 0x00003F08 File Offset: 0x00002108
		public ManejadorScript(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this.manejador_script = new LuaManejadorScript();
			this.manejar_acciones = new ManejadorAcciones(this.cuenta, this.manejador_script);
			this.banderas = new List<Bandera>();
			this.api = new API(this.cuenta, this.manejar_acciones);
			this.manejar_acciones.evento_accion_normal += this.get_Accion_Finalizada;
			this.manejar_acciones.evento_accion_personalizada += this.get_Accion_Personalizada_Finalizada;
			this.cuenta.juego.pelea.pelea_creada += this.get_Pelea_Creada;
			this.cuenta.juego.pelea.pelea_acabada += this.get_Pelea_Acabada;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003FD8 File Offset: 0x000021D8
		public void get_Desde_Archivo(string ruta_archivo)
		{
			if (this.activado)
			{
				throw new Exception("Un script est déjà en cours d'exécution.");
			}
			if (!File.Exists(ruta_archivo) || !ruta_archivo.EndsWith(".lua"))
			{
				throw new Exception("Fichier non trouvé ou non valide.");
			}
			this.manejador_script.cargar_Desde_Archivo(ruta_archivo, new Action(this.funciones_Personalizadas));
			Action<string> action = this.evento_script_cargado;
			if (action == null)
			{
				return;
			}
			action(Path.GetFileNameWithoutExtension(ruta_archivo));
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004048 File Offset: 0x00002248
		private void funciones_Personalizadas()
		{
			this.manejador_script.Set_Global("api", this.api);
			this.manejador_script.Set_Global("personaje", this.api.personaje);
			this.manejador_script.Set_Global("message", new Action<string>(delegate(string mensaje)
			{
				this.cuenta.logger.log_informacion("SCRIPT", mensaje);
			}));
			this.manejador_script.Set_Global("messageErreur", new Action<string>(delegate(string mensaje)
			{
				this.cuenta.logger.log_Error("SCRIPT", mensaje);
			}));
			this.manejador_script.Set_Global("stopScript", new Action(delegate
			{
				this.detener_Script("", false);
			}));
			this.manejador_script.Set_Global("delayFuncion", new Action<int>(delegate(int ms)
			{
				this.manejar_acciones.enqueue_Accion(new DelayAccion(ms), true);
			}));
			this.manejador_script.Set_Global("estaRecolectando", new Func<bool>(this.cuenta.esta_recolectando));
			this.manejador_script.Set_Global("estaDialogando", new Func<bool>(this.cuenta.esta_dialogando));
			this.manejador_script.script.DoString(Resources.api_ayuda, null, null);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004150 File Offset: 0x00002350
		public void activar_Script()
		{
			if (this.activado || this.cuenta.esta_ocupado())
			{
				return;
			}
			this.activado = true;
			Action action = this.evento_script_iniciado;
			if (action != null)
			{
				action();
			}
			this.estado_script = EstadoScript.MOVIMIENTO;
			this.iniciar_Script();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004190 File Offset: 0x00002390
		public void detener_Script(string mensaje = "", bool reactive = false)
		{
			if (!this.activado)
			{
				return;
			}
			int contador_recoleccion = this.manejar_acciones.contador_recoleccion;
			int contador_pelea = this.manejar_acciones.contador_pelea;
			this.activado = false;
			this.pausado = false;
			this.banderas.Clear();
			this.bandera_id = 0;
			this.manejar_acciones.get_Borrar_Todo();
			Action<string> action = this.evento_script_detenido;
			if (action != null)
			{
				action(mensaje);
			}
			if (this.cuenta.relance > 4)
			{
				Task.Delay(5000).Wait();
				this.cuenta.desconectar();
				Task.Delay(3000).Wait();
				this.cuenta.conectar();
				Task.Delay(1500).Wait();
				this.cuenta.script.activar_Script();
				this.manejar_acciones.contador_pelea = contador_pelea;
				this.manejar_acciones.contador_recoleccion = contador_recoleccion;
				this.cuenta.relance = 0;
				return;
			}
			if (reactive)
			{
				Task.Delay(10000).Wait();
				this.cuenta.script.activar_Script();
				this.manejar_acciones.contador_pelea = contador_pelea;
				this.manejar_acciones.contador_recoleccion = contador_recoleccion;
				this.cuenta.relance++;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000042CE File Offset: 0x000024CE
		private void iniciar_Script()
		{
			Task.Run(async delegate()
			{
				if (this.corriendo)
				{
					try
					{
						Table table = this.manejador_script.get_Global_Or<Table>("MAPS_DONJON", DataType.Table, null);
						if (table != null)
						{
							if ((from m in table.Values
							where m.Type == DataType.Number
							select m into n
							select (int)n.Number).Contains(this.cuenta.juego.mapa.id))
							{
								this.es_dung = true;
							}
						}
						await this.aplicar_Comprobaciones();
						if (this.corriendo)
						{
							IEnumerable<Table> enumerable = this.manejador_script.get_Entradas_Funciones(this.estado_script.ToString().ToLower());
							if (enumerable == null)
							{
								this.detener_Script("La fonction " + this.estado_script.ToString().ToLower() + " n'existe pas", true);
							}
							else
							{
								foreach (Table table2 in enumerable)
								{
									if (table2["mapa"] != null && this.cuenta.juego.mapa.esta_En_Mapa(table2["mapa"].ToString()))
									{
										this.procesar_Entradas(table2);
										this.procesar_Actual_Entrada(null);
										return;
									}
								}
								this.detener_Script("Aucune action sur cette map dans le script", false);
							}
						}
					}
					catch (Exception ex)
					{
						this.cuenta.logger.log_Error("SCRIPT", ex.ToString());
						this.detener_Script("", false);
					}
				}
			});
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000042E2 File Offset: 0x000024E2
		public bool isOnMap()
		{
			return true;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000042E8 File Offset: 0x000024E8
		private async Task aplicar_Comprobaciones()
		{
			await this.verificar_Muerte();
			if (this.corriendo)
			{
				await this.get_Verificar_Script_Regeneracion();
				if (this.corriendo)
				{
					await this.get_Verificar_Regeneracion();
					if (this.corriendo)
					{
						await this.get_Verificar_Sacos();
						if (this.corriendo)
						{
							this.verificar_Maximos_Pods();
							if (this.cuenta.tiene_grupo && this.cuenta.grupo != null)
							{
								await this.check_Followers_Inactiv();
							}
						}
					}
				}
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004330 File Offset: 0x00002530
		private async Task check_Followers_Inactiv()
		{
			bool followerInactiv = false;
			while (!followerInactiv)
			{
				bool flag = true;
				foreach (Cuenta cuenta in this.cuenta.grupo.miembros)
				{
					if (cuenta.Estado_Cuenta != EstadoCuenta.CONNECTE_INATIF || cuenta.juego.mapa.id != this.cuenta.juego.mapa.id)
					{
						flag = false;
					}
				}
				if (flag)
				{
					followerInactiv = true;
				}
				await Task.Delay(200);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004378 File Offset: 0x00002578
		private async Task verificar_Muerte()
		{
			if (this.cuenta.juego.personaje.caracteristicas.energia_actual == 0)
			{
				this.cuenta.logger.log_informacion("SCRIPT", "Le personnage est mort, passage en mode fenix.");
				this.estado_script = EstadoScript.FENIX;
			}
			await Task.Delay(50);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000043C0 File Offset: 0x000025C0
		private void verificar_Maximos_Pods()
		{
			if (!this.get_Maximos_Pods())
			{
				return;
			}
			bool flag = false;
			Table table = this.manejador_script.get_Global_Or<Table>("AUTO_DELETE", DataType.Table, null);
			if (table != null && this.estado_script != EstadoScript.BANCO)
			{
				this.cuenta.logger.log_informacion("SCRIPT", "Suppression des objets automatique");
				foreach (DynValue dynValue in table.Values)
				{
					if (dynValue.Type == DataType.Number)
					{
						foreach (ObjetosInventario objetosInventario in this.cuenta.juego.personaje.inventario.objetos)
						{
							if ((double)objetosInventario.id_modelo == dynValue.Number)
							{
								this.cuenta.juego.personaje.inventario.eliminar_Objeto(objetosInventario, objetosInventario.cantidad, true);
								short pods_actuales = this.cuenta.juego.personaje.inventario.pods_actuales;
								Task.Delay(500).Wait();
								flag = true;
							}
						}
					}
				}
			}
			if (this.get_Maximos_Pods())
			{
				this.cuenta.juego.personaje.evento_Pods_Actualizados();
				int num = this.manejador_script.get_Global_Or<int>("MAXIMOS_PODS", DataType.Number, 90);
				if (this.cuenta.juego.personaje.inventario.porcentaje_pods < num && flag)
				{
					this.estado_script = EstadoScript.MOVIMIENTO;
					return;
				}
			}
			if (!this.es_dung && this.estado_script != EstadoScript.BANCO && !flag)
			{
				if (!this.corriendo)
				{
					return;
				}
				this.cuenta.logger.log_informacion("SCRIPT", "Inventaire plein, le script passe en mode banque");
				this.estado_script = EstadoScript.BANCO;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000045AC File Offset: 0x000027AC
		private bool get_Maximos_Pods()
		{
			int num = this.manejador_script.get_Global_Or<int>("MAXIMOS_PODS", DataType.Number, 90);
			bool flag = this.cuenta.juego.personaje.inventario.porcentaje_pods >= num;
			if (this.cuenta.es_lider_grupo && this.cuenta.tiene_grupo)
			{
				foreach (Cuenta cuenta in this.cuenta.grupo.miembros)
				{
					flag = (flag || cuenta.juego.personaje.inventario.porcentaje_pods >= num);
				}
			}
			return flag;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004670 File Offset: 0x00002870
		private void check_disconnect_time()
		{
			DateTime now = DateTime.Now;
			if (this.cuenta.juego.personaje.enableDisconnect && now > this.cuenta.juego.personaje.dateDisconnect)
			{
				this.cuenta.logger.log_informacion("SCRIPT", "Deconnexion du compte");
				this.detener_Script("Arrêt du script avant deconnexion", false);
				this.cuenta.juego.personaje.enableDisconnect = false;
				this.cuenta.desconectar();
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004700 File Offset: 0x00002900
		private void procesar_Entradas(Table valor)
		{
			this.banderas.Clear();
			this.bandera_id = 0;
			this.check_disconnect_time();
			DynValue dynValue = valor.Get("personalizado");
			if (!dynValue.IsNil() && dynValue.Type == DataType.Function)
			{
				this.banderas.Add(new FuncionPersonalizada(dynValue));
			}
			if (this.estado_script == EstadoScript.MOVIMIENTO)
			{
				dynValue = valor.Get("recolectar");
				if (!dynValue.IsNil() && dynValue.Type == DataType.Boolean && dynValue.Boolean)
				{
					this.banderas.Add(new RecoleccionBandera());
				}
				dynValue = valor.Get("pelea");
				if (!dynValue.IsNil() && dynValue.Type == DataType.Boolean && dynValue.Boolean)
				{
					this.banderas.Add(new PeleaBandera());
				}
			}
			if (this.estado_script == EstadoScript.BANCO)
			{
				dynValue = valor.Get("npc_banco");
				if (!dynValue.IsNil() && dynValue.Type == DataType.Boolean && dynValue.Boolean)
				{
					this.banderas.Add(new NPCBancoBandera());
				}
			}
			if (this.estado_script == EstadoScript.BANCO)
			{
				dynValue = valor.Get("exchange");
				if (!dynValue.IsNil())
				{
					this.banderas.Add(new ExchangeBandera());
				}
			}
			dynValue = valor.Get("celda");
			if (!dynValue.IsNil() && dynValue.Type == DataType.String)
			{
				this.banderas.Add(new CambiarMapa(dynValue.String));
			}
			if (this.banderas.Count == 0)
			{
				this.detener_Script("aucune action trouvée sur cette carte", false);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000487C File Offset: 0x00002A7C
		private void procesar_Actual_Entrada(AccionesScript tiene_accion_disponible = null)
		{
			if (!this.corriendo)
			{
				return;
			}
			Bandera bandera = this.banderas[this.bandera_id];
			if (bandera is RecoleccionBandera)
			{
				this.manejar_Recoleccion_Bandera(tiene_accion_disponible as RecoleccionAccion);
				return;
			}
			if (bandera is PeleaBandera)
			{
				this.manejar_Pelea_mapa(tiene_accion_disponible as PeleasAccion);
				return;
			}
			if (bandera is NPCBancoBandera)
			{
				this.manejar_Npc_Banco_Bandera();
				return;
			}
			if (bandera is ExchangeBandera)
			{
				this.process_exchange_bandera();
				return;
			}
			FuncionPersonalizada funcionPersonalizada = bandera as FuncionPersonalizada;
			if (funcionPersonalizada != null)
			{
				this.manejar_acciones.get_Funcion_Personalizada(funcionPersonalizada.funcion);
				return;
			}
			CambiarMapa cambiarMapa = bandera as CambiarMapa;
			if (cambiarMapa == null)
			{
				return;
			}
			this.manejar_Cambio_Mapa(cambiarMapa);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000491C File Offset: 0x00002B1C
		private void process_exchange_bandera()
		{
			string text = this.manejador_script.get_Global_Or<string>("EXCHANGE_ID", DataType.String, "false");
			if (text != "false")
			{
				this.manejar_acciones.enqueue_Accion(new LaunchExchange(text), true);
				List<int> list = new List<int>();
				Table table = this.cuenta.script.manejador_script.get_Global_Or<Table>("KEEP_ITEM_EXCHANGE", DataType.Table, null);
				if (table == null)
				{
					return;
				}
				using (IEnumerator<DynValue> enumerator = table.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DynValue dynValue = enumerator.Current;
						if (dynValue.Type == DataType.Number)
						{
							list.Add((int)dynValue.Number);
						}
					}
					return;
				}
			}
			this.cuenta.script.detener_Script("Exchange_ID manquant dans le script", false);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000049F0 File Offset: 0x00002BF0
		private void manejar_Recoleccion_Bandera(RecoleccionAccion accion_recoleccion)
		{
			RecoleccionAccion recoleccionAccion = accion_recoleccion ?? this.crear_Accion_Recoleccion();
			if (recoleccionAccion == null)
			{
				return;
			}
			if (this.cuenta.juego.manejador.recoleccion.get_Puede_Recolectar(recoleccionAccion.elementos))
			{
				this.manejar_acciones.enqueue_Accion(recoleccionAccion, true);
				return;
			}
			this.procesar_Actual_Bandera();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004A44 File Offset: 0x00002C44
		private RecoleccionAccion crear_Accion_Recoleccion()
		{
			Table table = this.manejador_script.get_Global_Or<Table>("ELEMENTOS_RECOLECTABLES", DataType.Table, null);
			List<short> list = new List<short>();
			if (table != null)
			{
				foreach (DynValue dynValue in table.Values)
				{
					if (dynValue.Type == DataType.Number && this.cuenta.juego.personaje.get_Tiene_Skill_Id((int)dynValue.Number))
					{
						list.Add((short)dynValue.Number);
					}
				}
			}
			if (list.Count == 0)
			{
				list.AddRange(this.cuenta.juego.personaje.get_Skills_Recoleccion_Disponibles());
			}
			if (list.Count == 0)
			{
				this.cuenta.script.detener_Script("Liste des ressources vides, ou tu a peut-être oublier ton outil", false);
				return null;
			}
			return new RecoleccionAccion(list);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004B24 File Offset: 0x00002D24
		private void manejar_Npc_Banco_Bandera()
		{
			this.manejar_acciones.enqueue_Accion(new NpcBancoAccion(-1), false);
			this.manejar_acciones.enqueue_Accion(new AlmacenarTodosLosObjetosAccion(null), false);
			Table table = this.manejador_script.get_Global_Or<Table>("BANCO_RECUPERAR_OBJETOS", DataType.Table, null);
			if (table != null)
			{
				foreach (DynValue dynValue in table.Values)
				{
					if (dynValue.Type == DataType.Table)
					{
						DynValue dynValue2 = dynValue.Table.Get("objeto");
						DynValue dynValue3 = dynValue.Table.Get("cantidad");
						if (!dynValue2.IsNil() && dynValue2.Type == DataType.Number && !dynValue3.IsNil())
						{
							DataType type = dynValue3.Type;
						}
					}
				}
			}
			this.manejar_acciones.enqueue_Accion(new CerrarVentanaAccion(), true);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004C08 File Offset: 0x00002E08
		private void manejar_Cambio_Mapa(CambiarMapa mapa)
		{
			CambiarMapaAccion accion;
			if (CambiarMapaAccion.TryParse(mapa.celda_id, out accion))
			{
				this.manejar_acciones.enqueue_Accion(accion, true);
				return;
			}
			this.detener_Script("La cellule n'est pas valide pour changer de map", false);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004C40 File Offset: 0x00002E40
		private async Task get_Verificar_Sacos()
		{
			if (this.manejador_script.get_Global_Or<bool>("ABRIR_SACOS", DataType.Boolean, false))
			{
				PersonajeJuego personaje = this.cuenta.juego.personaje;
				List<ObjetosInventario> sacos = (from o in personaje.inventario.objetos
				where o.tipo == 100
				select o).ToList<ObjetosInventario>();
				if (sacos.Count > 0)
				{
					foreach (ObjetosInventario objeto in sacos)
					{
						personaje.inventario.utilizar_Objeto(objeto);
						await Task.Delay(500);
					}
					List<ObjetosInventario>.Enumerator enumerator = default(List<ObjetosInventario>.Enumerator);
					this.cuenta.logger.log_informacion("SCRIPT", string.Format("{0} sac(s) ouvert(s).", sacos.Count));
				}
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004C88 File Offset: 0x00002E88
		private void manejar_Pelea_mapa(PeleasAccion pelea_accion)
		{
			PeleasAccion peleasAccion = pelea_accion ?? this.get_Crear_Pelea_Accion();
			int num = this.manejador_script.get_Global_Or<int>("PELEAS_POR_MAPA", DataType.Number, -1);
			if (num != -1 && this.manejar_acciones.contador_peleas_mapa >= num)
			{
				this.cuenta.logger.log_informacion("SCRIPT", "Limite des combats atteints sur cette map");
				this.procesar_Actual_Bandera();
				return;
			}
			if (!this.es_dung && !this.cuenta.juego.mapa.get_Puede_Luchar_Contra_Grupo_Monstruos(peleasAccion.monstruos_minimos, peleasAccion.monstruos_maximos, peleasAccion.monstruo_nivel_minimo, peleasAccion.monstruo_nivel_maximo, peleasAccion.monstruos_prohibidos, peleasAccion.monstruos_obligatorios))
			{
				this.cuenta.logger.log_informacion("SCRIPT", "Aucun groupe de monstres disponible sur la carte");
				this.procesar_Actual_Bandera();
				return;
			}
			while (this.es_dung && !this.cuenta.juego.mapa.get_Puede_Luchar_Contra_Grupo_Monstruos(peleasAccion.monstruos_minimos, peleasAccion.monstruos_maximos, peleasAccion.monstruo_nivel_minimo, peleasAccion.monstruo_nivel_maximo, peleasAccion.monstruos_prohibidos, peleasAccion.monstruos_obligatorios))
			{
				peleasAccion = this.get_Crear_Pelea_Accion();
			}
			this.manejar_acciones.enqueue_Accion(peleasAccion, true);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004DA4 File Offset: 0x00002FA4
		private async Task get_Verificar_Regeneracion()
		{
			if (this.cuenta.pelea_extension.configuracion.iniciar_regeneracion != 0)
			{
				if (this.cuenta.pelea_extension.configuracion.detener_regeneracion > this.cuenta.pelea_extension.configuracion.iniciar_regeneracion)
				{
					if (this.cuenta.juego.personaje.caracteristicas.porcentaje_vida <= (int)this.cuenta.pelea_extension.configuracion.iniciar_regeneracion)
					{
						int num = (int)this.cuenta.pelea_extension.configuracion.detener_regeneracion * this.cuenta.juego.personaje.caracteristicas.vitalidad_maxima / 100 - this.cuenta.juego.personaje.caracteristicas.vitalidad_actual;
						if (num > 0)
						{
							int tiempo_estimado = num / 2;
							if (this.cuenta.Estado_Cuenta != EstadoCuenta.REGENERATION)
							{
								if (this.cuenta.esta_ocupado())
								{
									return;
								}
								this.cuenta.conexion.enviar_Paquete("eU1", true);
							}
							this.cuenta.logger.log_informacion("SCRIPTS", string.Format("Régénération commencée, points de vie à récupérer: {0}, temps: {1} secondes.", num, tiempo_estimado));
							int i = 0;
							while (i < tiempo_estimado && this.cuenta.juego.personaje.caracteristicas.porcentaje_vida <= (int)this.cuenta.pelea_extension.configuracion.detener_regeneracion && this.corriendo)
							{
								await Task.Delay(1000);
								i++;
							}
							if (this.corriendo)
							{
								if (this.cuenta.Estado_Cuenta == EstadoCuenta.REGENERATION)
								{
									this.cuenta.conexion.enviar_Paquete("eU1", true);
								}
								this.cuenta.logger.log_informacion("SCRIPTS", "Régénération terminée.");
							}
						}
					}
				}
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004DEC File Offset: 0x00002FEC
		private async Task get_Verificar_Script_Regeneracion()
		{
			Table table = this.manejador_script.get_Global_Or<Table>("AUTO_REGENERACION", DataType.Table, null);
			if (table != null)
			{
				PersonajeJuego personaje = this.cuenta.juego.personaje;
				int num = table.get_Or("VIDA_MINIMA", DataType.Number, 0);
				int vida_maxima = table.get_Or("VIDA_MAXIMA", DataType.Number, 100);
				bool regenAll = table.get_Or("REGENERATION_GROUP", DataType.Boolean, false);
				if (num != 0 && personaje.caracteristicas.porcentaje_vida <= num)
				{
					int num2 = vida_maxima * personaje.caracteristicas.vitalidad_maxima / 100;
					int vida_para_regenerar = num2 - personaje.caracteristicas.vitalidad_actual;
					List<int> objetos = table.Get("OBJETOS").ToObject<List<int>>();
					foreach (int gid in objetos)
					{
						if (vida_para_regenerar < 20)
						{
							break;
						}
						ObjetosInventario objeto = personaje.inventario.get_Objeto_Modelo_Id(gid);
						if (objeto != null && objeto.vida_regenerada > 0)
						{
							int val = (int)Math.Floor((double)vida_para_regenerar / (double)objeto.vida_regenerada);
							int cantidad_correcta = Math.Min(val, objeto.cantidad);
							for (int i = 0; i < cantidad_correcta; i++)
							{
								personaje.inventario.utilizar_Objeto(objeto);
								await Task.Delay(800);
							}
							vida_para_regenerar -= (int)objeto.vida_regenerada * cantidad_correcta;
							objeto = null;
						}
					}
					List<int>.Enumerator enumerator = default(List<int>.Enumerator);
					this.cuenta.logger.log_informacion("SCRIPT", "Verification - Régeneration de groupe");
					if (regenAll && this.cuenta.tiene_grupo && this.cuenta.es_lider_grupo)
					{
						foreach (Cuenta member in this.cuenta.grupo.miembros)
						{
							num2 = vida_maxima * member.juego.personaje.caracteristicas.vitalidad_maxima / 100;
							vida_para_regenerar = num2 - member.juego.personaje.caracteristicas.vitalidad_actual;
							foreach (int gid2 in objetos)
							{
								if (vida_para_regenerar < 20)
								{
									break;
								}
								ObjetosInventario objeto = member.juego.personaje.inventario.get_Objeto_Modelo_Id(gid2);
								if (objeto != null && objeto.vida_regenerada > 0)
								{
									int cantidad_correcta = Math.Min((int)Math.Floor((double)vida_para_regenerar / (double)objeto.vida_regenerada), objeto.cantidad);
									for (int i = 0; i < cantidad_correcta; i++)
									{
										member.juego.personaje.inventario.utilizar_Objeto(objeto);
										await Task.Delay(800);
									}
									vida_para_regenerar -= (int)objeto.vida_regenerada * cantidad_correcta;
									objeto = null;
								}
							}
							enumerator = default(List<int>.Enumerator);
							member = null;
						}
						IEnumerator<Cuenta> enumerator2 = null;
					}
				}
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004E34 File Offset: 0x00003034
		private void procesar_Actual_Bandera()
		{
			if (!this.corriendo)
			{
				return;
			}
			if (!this.es_dung && this.get_Maximos_Pods())
			{
				this.iniciar_Script();
				return;
			}
			Bandera bandera = this.banderas[this.bandera_id];
			if (!(bandera is RecoleccionBandera))
			{
				if (bandera is PeleaBandera)
				{
					PeleasAccion crear_Pelea_Accion = this.get_Crear_Pelea_Accion();
					if (this.cuenta.juego.mapa.get_Puede_Luchar_Contra_Grupo_Monstruos(crear_Pelea_Accion.monstruos_minimos, crear_Pelea_Accion.monstruos_maximos, crear_Pelea_Accion.monstruo_nivel_minimo, crear_Pelea_Accion.monstruo_nivel_maximo, crear_Pelea_Accion.monstruos_prohibidos, crear_Pelea_Accion.monstruos_obligatorios))
					{
						this.procesar_Actual_Entrada(crear_Pelea_Accion);
						return;
					}
				}
			}
			else
			{
				RecoleccionAccion recoleccionAccion = this.crear_Accion_Recoleccion();
				if (this.cuenta.juego.manejador.recoleccion.get_Puede_Recolectar(recoleccionAccion.elementos))
				{
					this.procesar_Actual_Entrada(recoleccionAccion);
					return;
				}
			}
			this.bandera_id++;
			if (this.bandera_id == this.banderas.Count)
			{
				this.detener_Script("Aucune action trouvée sur cette map", false);
				return;
			}
			this.procesar_Actual_Entrada(null);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004F38 File Offset: 0x00003138
		private PeleasAccion get_Crear_Pelea_Accion()
		{
			int monstruos_minimos = this.manejador_script.get_Global_Or<int>("MONSTRUOS_MINIMOS", DataType.Number, 1);
			int monstruos_maximos = this.manejador_script.get_Global_Or<int>("MONSTRUOS_MAXIMOS", DataType.Number, 8);
			int monstruo_nivel_minimo = this.manejador_script.get_Global_Or<int>("MINIMO_NIVEL_MONSTRUOS", DataType.Number, 1);
			int monstruo_nivel_maximo = this.manejador_script.get_Global_Or<int>("MAXIMO_NIVEL_MONSTRUOS", DataType.Number, 1000);
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			Table table = this.manejador_script.get_Global_Or<Table>("MONSTRUOS_PROHIBIDOS", DataType.Table, null);
			if (table != null)
			{
				foreach (DynValue dynValue in table.Values)
				{
					if (dynValue.Type == DataType.Number)
					{
						list.Add((int)dynValue.Number);
					}
				}
			}
			table = this.manejador_script.get_Global_Or<Table>("MONSTRUOS_OBLIGATORIOS", DataType.Table, null);
			if (table != null)
			{
				foreach (DynValue dynValue2 in table.Values)
				{
					if (dynValue2.Type == DataType.Number)
					{
						list2.Add((int)dynValue2.Number);
					}
				}
			}
			return new PeleasAccion(monstruos_minimos, monstruos_maximos, monstruo_nivel_minimo, monstruo_nivel_maximo, list, list2);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005090 File Offset: 0x00003290
		private bool verificar_Acciones_Especiales()
		{
			if (this.estado_script == EstadoScript.BANCO && !this.get_Maximos_Pods())
			{
				this.estado_script = EstadoScript.MOVIMIENTO;
				this.iniciar_Script();
				return true;
			}
			return false;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000050B3 File Offset: 0x000032B3
		private void get_Accion_Finalizada(bool mapa_cambiado)
		{
			if (this.verificar_Acciones_Especiales())
			{
				return;
			}
			if (mapa_cambiado)
			{
				this.iniciar_Script();
				return;
			}
			this.procesar_Actual_Bandera();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000050B3 File Offset: 0x000032B3
		private void get_Accion_Personalizada_Finalizada(bool mapa_cambiado)
		{
			if (this.verificar_Acciones_Especiales())
			{
				return;
			}
			if (mapa_cambiado)
			{
				this.iniciar_Script();
				return;
			}
			this.procesar_Actual_Bandera();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000050CE File Offset: 0x000032CE
		private void get_Pelea_Creada()
		{
			if (!this.activado)
			{
				return;
			}
			this.pausado = true;
			this.cuenta.juego.manejador.recoleccion.get_Cancelar_Interactivo();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000050FA File Offset: 0x000032FA
		private void get_Pelea_Acabada()
		{
			if (!this.activado)
			{
				return;
			}
			this.pausado = false;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000510C File Offset: 0x0000330C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005118 File Offset: 0x00003318
		~ManejadorScript()
		{
			this.Dispose(false);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005148 File Offset: 0x00003348
		public virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				if (disposing)
				{
					this.manejador_script.Dispose();
					this.api.Dispose();
					this.manejar_acciones.Dispose();
				}
				this.manejar_acciones = null;
				this.manejador_script = null;
				this.api = null;
				this.activado = false;
				this.pausado = false;
				this.cuenta = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000048 RID: 72
		private Cuenta cuenta;

		// Token: 0x04000049 RID: 73
		private LuaManejadorScript manejador_script;

		// Token: 0x0400004B RID: 75
		private EstadoScript estado_script;

		// Token: 0x0400004C RID: 76
		private List<Bandera> banderas;

		// Token: 0x0400004D RID: 77
		private int bandera_id;

		// Token: 0x0400004E RID: 78
		private API api;

		// Token: 0x0400004F RID: 79
		private bool es_dung;

		// Token: 0x04000050 RID: 80
		private bool disposed;

		// Token: 0x04000051 RID: 81
		private bool _activado;

		// Token: 0x04000052 RID: 82
		public bool _pausado;
	}
}
