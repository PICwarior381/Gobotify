using System;
using System.Net;
using Bot_Dofus_1._29._1.Comun.Network;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game;
using Bot_Dofus_1._29._1.Otros.Grupos;
using Bot_Dofus_1._29._1.Otros.Peleas;
using Bot_Dofus_1._29._1.Otros.Scripts;
using Bot_Dofus_1._29._1.Utilidades.Configuracion;
using Bot_Dofus_1._29._1.Utilidades.Logs;

namespace Bot_Dofus_1._29._1.Otros
{
	// Token: 0x0200000E RID: 14
	public class Cuenta : IDisposable
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000381F File Offset: 0x00001A1F
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003827 File Offset: 0x00001A27
		public string apodo { get; set; } = string.Empty;

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003830 File Offset: 0x00001A30
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003838 File Offset: 0x00001A38
		public string key_bienvenida { get; set; } = string.Empty;

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003841 File Offset: 0x00001A41
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003849 File Offset: 0x00001A49
		public string tiquet_game { get; set; } = string.Empty;

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003852 File Offset: 0x00001A52
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000385A File Offset: 0x00001A5A
		public Logger logger { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003863 File Offset: 0x00001A63
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000386B File Offset: 0x00001A6B
		public ClienteTcp conexion { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003874 File Offset: 0x00001A74
		// (set) Token: 0x060000AA RID: 170 RVA: 0x0000387C File Offset: 0x00001A7C
		public Juego juego { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003885 File Offset: 0x00001A85
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000388D File Offset: 0x00001A8D
		public ManejadorScript script { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003896 File Offset: 0x00001A96
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000389E File Offset: 0x00001A9E
		public PeleaExtensiones pelea_extension { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000038A7 File Offset: 0x00001AA7
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000038AF File Offset: 0x00001AAF
		public CuentaConf configuracion { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000038B8 File Offset: 0x00001AB8
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x000038C0 File Offset: 0x00001AC0
		public Grupo grupo { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000038C9 File Offset: 0x00001AC9
		public bool tiene_grupo
		{
			get
			{
				return this.grupo != null;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000038D4 File Offset: 0x00001AD4
		public bool es_lider_grupo
		{
			get
			{
				return !this.tiene_grupo || this.grupo.lider == this;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000B5 RID: 181 RVA: 0x000038F0 File Offset: 0x00001AF0
		// (remove) Token: 0x060000B6 RID: 182 RVA: 0x00003928 File Offset: 0x00001B28
		public event Action evento_estado_cuenta;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000B7 RID: 183 RVA: 0x00003960 File Offset: 0x00001B60
		// (remove) Token: 0x060000B8 RID: 184 RVA: 0x00003998 File Offset: 0x00001B98
		public event Action cuenta_desconectada;

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000039CD File Offset: 0x00001BCD
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000039D5 File Offset: 0x00001BD5
		public int relance { get; set; }

		// Token: 0x060000BB RID: 187 RVA: 0x000039E0 File Offset: 0x00001BE0
		public Cuenta(CuentaConf _configuracion)
		{
			this.configuracion = _configuracion;
			this.logger = new Logger();
			this.juego = new Juego(this);
			this.pelea_extension = new PeleaExtensiones(this);
			this.script = new ManejadorScript(this);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003A54 File Offset: 0x00001C54
		public void conectar()
		{
			this.conexion = new ClienteTcp(this);
			this.conexion.conexion_Servidor(IPAddress.Parse(GlobalConf.ip_conexion), (int)GlobalConf.puerto_conexion);
			if (this.juego.personaje.isBank)
			{
				this.juego.personaje.timerAfk.Enabled = true;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public void desconectar()
		{
			ClienteTcp conexion = this.conexion;
			if (conexion != null)
			{
				conexion.Dispose();
			}
			this.conexion = null;
			this.juego.personaje.timerAfk.Enabled = false;
			this.script.detener_Script("", false);
			this.juego.limpiar();
			this.Estado_Cuenta = EstadoCuenta.DECONNECTE;
			Action action = this.cuenta_desconectada;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003B1E File Offset: 0x00001D1E
		public void cambiando_Al_Servidor_Juego(string ip, int puerto)
		{
			this.conexion.get_Desconectar_Socket();
			this.conexion.conexion_Servidor(IPAddress.Parse(ip), puerto);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003B3D File Offset: 0x00001D3D
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00003B45 File Offset: 0x00001D45
		public EstadoCuenta Estado_Cuenta
		{
			get
			{
				return this.estado_cuenta;
			}
			set
			{
				this.estado_cuenta = value;
				Action action = this.evento_estado_cuenta;
				if (action == null)
				{
					return;
				}
				action();
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003B5E File Offset: 0x00001D5E
		public bool esta_ocupado()
		{
			return this.Estado_Cuenta != EstadoCuenta.CONNECTE_INATIF && this.Estado_Cuenta != EstadoCuenta.REGENERATION;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003B78 File Offset: 0x00001D78
		public bool esta_dialogando()
		{
			return this.Estado_Cuenta == EstadoCuenta.STOCKAGE || this.Estado_Cuenta == EstadoCuenta.DIALOGUE || this.Estado_Cuenta == EstadoCuenta.ECHANGE || this.Estado_Cuenta == EstadoCuenta.ACHETER || this.Estado_Cuenta == EstadoCuenta.VENDRE;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003BAB File Offset: 0x00001DAB
		public bool esta_luchando()
		{
			return this.Estado_Cuenta == EstadoCuenta.COMBAT;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003BB6 File Offset: 0x00001DB6
		public bool esta_recolectando()
		{
			return this.Estado_Cuenta == EstadoCuenta.RECOLTE;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003BC1 File Offset: 0x00001DC1
		public bool esta_desplazando()
		{
			return this.Estado_Cuenta == EstadoCuenta.MOUVEMENT;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003BCC File Offset: 0x00001DCC
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003BD8 File Offset: 0x00001DD8
		~Cuenta()
		{
			this.Dispose(false);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003C08 File Offset: 0x00001E08
		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.script.Dispose();
					ClienteTcp conexion = this.conexion;
					if (conexion != null)
					{
						conexion.Dispose();
					}
					this.juego.Dispose();
				}
				this.Estado_Cuenta = EstadoCuenta.DECONNECTE;
				this.script = null;
				this.key_bienvenida = null;
				this.conexion = null;
				this.logger = null;
				this.juego = null;
				this.apodo = null;
				this.configuracion = null;
				this.disposed = true;
			}
		}

		// Token: 0x0400002C RID: 44
		private EstadoCuenta estado_cuenta = EstadoCuenta.DECONNECTE;

		// Token: 0x0400002D RID: 45
		public bool puede_utilizar_dragopavo;

		// Token: 0x0400002F RID: 47
		private bool disposed;
	}
}
