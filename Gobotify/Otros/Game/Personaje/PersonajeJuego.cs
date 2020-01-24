using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Hechizos;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Inventario;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Oficios;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;

namespace Bot_Dofus_1._29._1.Otros.Game.Personaje
{
	// Token: 0x02000058 RID: 88
	public class PersonajeJuego : Entidad, IDisposable, IEliminable
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000BC49 File Offset: 0x00009E49
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0000BC51 File Offset: 0x00009E51
		public int id { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000BC5A File Offset: 0x00009E5A
		// (set) Token: 0x0600033D RID: 829 RVA: 0x0000BC62 File Offset: 0x00009E62
		public string nombre { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000BC6B File Offset: 0x00009E6B
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0000BC73 File Offset: 0x00009E73
		public byte nivel { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000BC7C File Offset: 0x00009E7C
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0000BC84 File Offset: 0x00009E84
		public byte sexo { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000BC8D File Offset: 0x00009E8D
		// (set) Token: 0x06000343 RID: 835 RVA: 0x0000BC95 File Offset: 0x00009E95
		public byte raza_id { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000BC9E File Offset: 0x00009E9E
		// (set) Token: 0x06000345 RID: 837 RVA: 0x0000BCA6 File Offset: 0x00009EA6
		public InventarioGeneral inventario { get; private set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000BCAF File Offset: 0x00009EAF
		// (set) Token: 0x06000347 RID: 839 RVA: 0x0000BCB7 File Offset: 0x00009EB7
		public int puntos_caracteristicas { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000BCC0 File Offset: 0x00009EC0
		// (set) Token: 0x06000349 RID: 841 RVA: 0x0000BCC8 File Offset: 0x00009EC8
		public int kamas { get; private set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000BCD1 File Offset: 0x00009ED1
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0000BCD9 File Offset: 0x00009ED9
		public Caracteristicas caracteristicas { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000BCE2 File Offset: 0x00009EE2
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0000BCEA File Offset: 0x00009EEA
		public Dictionary<short, Hechizo> hechizos { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000BCF3 File Offset: 0x00009EF3
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000BCFB File Offset: 0x00009EFB
		public List<Oficio> oficios { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000BD04 File Offset: 0x00009F04
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0000BD0C File Offset: 0x00009F0C
		public System.Threading.Timer timer_regeneracion { get; private set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000BD15 File Offset: 0x00009F15
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000BD1D File Offset: 0x00009F1D
		public System.Threading.Timer timer_afk { get; private set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000BD26 File Offset: 0x00009F26
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0000BD2E File Offset: 0x00009F2E
		public string canales { get; set; } = string.Empty;

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000BD37 File Offset: 0x00009F37
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000BD3F File Offset: 0x00009F3F
		public Celda celda { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000BD48 File Offset: 0x00009F48
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0000BD50 File Offset: 0x00009F50
		public System.Windows.Forms.Timer timerAfk { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000BD59 File Offset: 0x00009F59
		// (set) Token: 0x0600035B RID: 859 RVA: 0x0000BD61 File Offset: 0x00009F61
		public bool en_grupo { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000BD6A File Offset: 0x00009F6A
		// (set) Token: 0x0600035D RID: 861 RVA: 0x0000BD72 File Offset: 0x00009F72
		internal bool isBank { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000BD7B File Offset: 0x00009F7B
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0000BD83 File Offset: 0x00009F83
		public bool esta_utilizando_dragopavo { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000BD8C File Offset: 0x00009F8C
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000BD94 File Offset: 0x00009F94
		public sbyte hablando_npc_id { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000BD9D File Offset: 0x00009F9D
		public int porcentaje_experiencia
		{
			get
			{
				return (int)((this.caracteristicas.experiencia_actual - this.caracteristicas.experiencia_minima_nivel) / (this.caracteristicas.experiencia_siguiente_nivel - this.caracteristicas.experiencia_minima_nivel) * 100.0);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000BDD9 File Offset: 0x00009FD9
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000BDE1 File Offset: 0x00009FE1
		public DateTime dateDisconnect { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000BDEA File Offset: 0x00009FEA
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000BDF2 File Offset: 0x00009FF2
		public bool enableDisconnect { get; set; }

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000367 RID: 871 RVA: 0x0000BDFC File Offset: 0x00009FFC
		// (remove) Token: 0x06000368 RID: 872 RVA: 0x0000BE34 File Offset: 0x0000A034
		public event Action servidor_seleccionado;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000369 RID: 873 RVA: 0x0000BE6C File Offset: 0x0000A06C
		// (remove) Token: 0x0600036A RID: 874 RVA: 0x0000BEA4 File Offset: 0x0000A0A4
		public event Action personaje_seleccionado;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600036B RID: 875 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		// (remove) Token: 0x0600036C RID: 876 RVA: 0x0000BF14 File Offset: 0x0000A114
		public event Action caracteristicas_actualizadas;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600036D RID: 877 RVA: 0x0000BF4C File Offset: 0x0000A14C
		// (remove) Token: 0x0600036E RID: 878 RVA: 0x0000BF84 File Offset: 0x0000A184
		public event Action pods_actualizados;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600036F RID: 879 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		// (remove) Token: 0x06000370 RID: 880 RVA: 0x0000BFF4 File Offset: 0x0000A1F4
		public event Action hechizos_actualizados;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000371 RID: 881 RVA: 0x0000C02C File Offset: 0x0000A22C
		// (remove) Token: 0x06000372 RID: 882 RVA: 0x0000C064 File Offset: 0x0000A264
		public event Action oficios_actualizados;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000373 RID: 883 RVA: 0x0000C09C File Offset: 0x0000A29C
		// (remove) Token: 0x06000374 RID: 884 RVA: 0x0000C0D4 File Offset: 0x0000A2D4
		public event Action dialogo_npc_recibido;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000375 RID: 885 RVA: 0x0000C10C File Offset: 0x0000A30C
		// (remove) Token: 0x06000376 RID: 886 RVA: 0x0000C144 File Offset: 0x0000A344
		public event Action dialogo_npc_acabado;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000377 RID: 887 RVA: 0x0000C17C File Offset: 0x0000A37C
		// (remove) Token: 0x06000378 RID: 888 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
		public event Action exchange_with_player;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000379 RID: 889 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		// (remove) Token: 0x0600037A RID: 890 RVA: 0x0000C224 File Offset: 0x0000A424
		public event Action exchange_accept;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600037B RID: 891 RVA: 0x0000C25C File Offset: 0x0000A45C
		// (remove) Token: 0x0600037C RID: 892 RVA: 0x0000C294 File Offset: 0x0000A494
		public event Action<List<Celda>> movimiento_pathfinding_minimapa;

		// Token: 0x0600037D RID: 893 RVA: 0x0000C2CC File Offset: 0x0000A4CC
		public PersonajeJuego(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this.timer_regeneracion = new System.Threading.Timer(new TimerCallback(this.regeneracion_TimerCallback), null, -1, -1);
			this.timer_afk = new System.Threading.Timer(new TimerCallback(this.anti_Afk), null, -1, -1);
			this.inventario = new InventarioGeneral(this.cuenta);
			this.caracteristicas = new Caracteristicas();
			this.hechizos = new Dictionary<short, Hechizo>();
			this.oficios = new List<Oficio>();
			this.timerAfk = new System.Windows.Forms.Timer();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000C362 File Offset: 0x0000A562
		public void set_Datos_Personaje(int _id, string _nombre_personaje, byte _nivel, byte _sexo, byte _raza_id)
		{
			this.id = _id;
			this.nombre = _nombre_personaje;
			this.nivel = _nivel;
			this.sexo = _sexo;
			this.raza_id = _raza_id;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000C389 File Offset: 0x0000A589
		public void agregar_Canal_Personaje(string cadena_canales)
		{
			if (cadena_canales.Length <= 1)
			{
				this.canales += cadena_canales;
				return;
			}
			this.canales = cadena_canales;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000C3AE File Offset: 0x0000A5AE
		public void eliminar_Canal_Personaje(string simbolo_canal)
		{
			this.canales = this.canales.Replace(simbolo_canal, string.Empty);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000C3C7 File Offset: 0x0000A5C7
		public void evento_Pods_Actualizados()
		{
			Action action = this.pods_actualizados;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000C3D9 File Offset: 0x0000A5D9
		public void evento_Servidor_Seleccionado()
		{
			Action action = this.servidor_seleccionado;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000C3EB File Offset: 0x0000A5EB
		public void evento_Personaje_Seleccionado()
		{
			Action action = this.personaje_seleccionado;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000C3FD File Offset: 0x0000A5FD
		public void evento_Personaje_Pathfinding_Minimapa(List<Celda> lista)
		{
			Action<List<Celda>> action = this.movimiento_pathfinding_minimapa;
			if (action == null)
			{
				return;
			}
			action(lista);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000C410 File Offset: 0x0000A610
		public void evento_Oficios_Actualizados()
		{
			Action action = this.oficios_actualizados;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000C422 File Offset: 0x0000A622
		public void evento_Dialogo_Recibido()
		{
			Action action = this.dialogo_npc_recibido;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000C434 File Offset: 0x0000A634
		public void evento_Dialogo_Acabado()
		{
			Action action = this.dialogo_npc_acabado;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000C446 File Offset: 0x0000A646
		public void event_exchange_start()
		{
			Action action = this.exchange_with_player;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000C458 File Offset: 0x0000A658
		public void event_accept_exchange()
		{
			Action action = this.exchange_accept;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000C46C File Offset: 0x0000A66C
		public void actualizar_Caracteristicas(string paquete)
		{
			string[] array = paquete.Substring(2).Split(new char[]
			{
				'|'
			});
			string[] array2 = array[0].Split(new char[]
			{
				','
			});
			this.caracteristicas.experiencia_actual = double.Parse(array2[0]);
			this.caracteristicas.experiencia_minima_nivel = double.Parse(array2[1]);
			this.caracteristicas.experiencia_siguiente_nivel = double.Parse(array2[2]);
			this.kamas = int.Parse(array[1]);
			this.puntos_caracteristicas = int.Parse(array[2]);
			array2 = array[5].Split(new char[]
			{
				','
			});
			this.caracteristicas.vitalidad_actual = int.Parse(array2[0]);
			this.caracteristicas.vitalidad_maxima = int.Parse(array2[1]);
			array2 = array[6].Split(new char[]
			{
				','
			});
			this.caracteristicas.energia_actual = int.Parse(array2[0]);
			this.caracteristicas.maxima_energia = int.Parse(array2[1]);
			if (this.caracteristicas.iniciativa != null)
			{
				this.caracteristicas.iniciativa.base_personaje = int.Parse(array[7]);
			}
			else
			{
				this.caracteristicas.iniciativa = new PersonajeStats(int.Parse(array[7]));
			}
			if (this.caracteristicas.prospeccion != null)
			{
				this.caracteristicas.prospeccion.base_personaje = int.Parse(array[8]);
			}
			else
			{
				this.caracteristicas.prospeccion = new PersonajeStats(int.Parse(array[8]));
			}
			for (int i = 9; i <= 18; i++)
			{
				array2 = array[i].Split(new char[]
				{
					','
				});
				int base_personaje = int.Parse(array2[0]);
				int equipamiento = int.Parse(array2[1]);
				int dones = int.Parse(array2[2]);
				int boost = int.Parse(array2[3]);
				switch (i)
				{
				case 9:
					this.caracteristicas.puntos_accion.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 10:
					this.caracteristicas.puntos_movimiento.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 11:
					this.caracteristicas.fuerza.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 12:
					this.caracteristicas.vitalidad.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 13:
					this.caracteristicas.sabiduria.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 14:
					this.caracteristicas.suerte.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 15:
					this.caracteristicas.agilidad.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 16:
					this.caracteristicas.inteligencia.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 17:
					this.caracteristicas.alcanze.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 18:
					this.caracteristicas.criaturas_invocables.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				}
			}
			Action action = this.caracteristicas_actualizadas;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000C788 File Offset: 0x0000A988
		public void actualizar_Hechizos(string paquete)
		{
			this.hechizos.Clear();
			string[] array = paquete.Split(new char[]
			{
				';'
			});
			for (int i = 0; i < array.Length - 1; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'~'
				});
				short num = short.Parse(array2[0]);
				Hechizo hechizo = Hechizo.get_Hechizo(num);
				hechizo.nivel = byte.Parse(array2[1]);
				this.hechizos.Add(num, hechizo);
			}
			this.hechizos_actualizados();
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000C814 File Offset: 0x0000AA14
		private void regeneracion_TimerCallback(object state)
		{
			try
			{
				Caracteristicas caracteristicas = this.caracteristicas;
				int? num = (caracteristicas != null) ? new int?(caracteristicas.vitalidad_actual) : null;
				Caracteristicas caracteristicas2 = this.caracteristicas;
				int? num2 = (caracteristicas2 != null) ? new int?(caracteristicas2.vitalidad_maxima) : null;
				if (num.GetValueOrDefault() >= num2.GetValueOrDefault() & (num != null & num2 != null))
				{
					this.timer_regeneracion.Change(-1, -1);
				}
				else
				{
					Caracteristicas caracteristicas3 = this.caracteristicas;
					int vitalidad_actual = caracteristicas3.vitalidad_actual;
					caracteristicas3.vitalidad_actual = vitalidad_actual + 1;
					Action action = this.caracteristicas_actualizadas;
					if (action != null)
					{
						action();
					}
				}
			}
			catch (Exception arg)
			{
				this.cuenta.logger.log_Error("TIMER-REGENERANDO", string.Format("ERROR: {0}", arg));
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		private void anti_Afk(object state)
		{
			try
			{
				if (this.cuenta.Estado_Cuenta != EstadoCuenta.DECONNECTE)
				{
					this.cuenta.conexion.enviar_Paquete("ping", false);
				}
			}
			catch (Exception arg)
			{
				this.cuenta.logger.log_Error("TIMER-ANTIAFK", string.Format("ERROR: {0}", arg));
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000C95C File Offset: 0x0000AB5C
		public Hechizo get_Hechizo(short id)
		{
			return this.hechizos.FirstOrDefault((KeyValuePair<short, Hechizo> x) => x.Key == id).Value;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000C998 File Offset: 0x0000AB98
		public bool get_Tiene_Skill_Id(int id)
		{
			Func<SkillsOficio, bool> <>9__1;
			return this.oficios.FirstOrDefault(delegate(Oficio j)
			{
				IEnumerable<SkillsOficio> skills = j.skills;
				Func<SkillsOficio, bool> predicate;
				if ((predicate = <>9__1) == null)
				{
					predicate = (<>9__1 = ((SkillsOficio s) => (int)s.id == id));
				}
				return skills.FirstOrDefault(predicate) != null;
			}) != null;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000C9CC File Offset: 0x0000ABCC
		public IEnumerable<SkillsOficio> get_Skills_Disponibles()
		{
			return this.oficios.SelectMany((Oficio oficio) => from skill in oficio.skills
			select skill);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		public IEnumerable<short> get_Skills_Recoleccion_Disponibles()
		{
			return this.oficios.SelectMany((Oficio oficio) => from skill in oficio.skills
			where !skill.es_craft
			select skill.id);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000CA24 File Offset: 0x0000AC24
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000CA30 File Offset: 0x0000AC30
		~PersonajeJuego()
		{
			this.Dispose(false);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000CA60 File Offset: 0x0000AC60
		public void limpiar()
		{
			this.id = 0;
			this.hechizos.Clear();
			this.oficios.Clear();
			this.inventario.limpiar();
			this.caracteristicas.limpiar();
			this.timer_regeneracion.Change(-1, -1);
			this.timer_afk.Change(-1, -1);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000CABC File Offset: 0x0000ACBC
		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.inventario.Dispose();
					this.timer_regeneracion.Dispose();
					this.timer_afk.Dispose();
				}
				this.hechizos = null;
				this.caracteristicas = null;
				this.nombre = null;
				this.inventario = null;
				this.timer_regeneracion = null;
				this.timer_afk = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000156 RID: 342
		private Cuenta cuenta;

		// Token: 0x04000163 RID: 355
		private bool disposed;
	}
}
