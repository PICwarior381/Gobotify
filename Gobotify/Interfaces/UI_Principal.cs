using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Bot_Dofus_1._29._1.Controles.ColorCheckBox;
using Bot_Dofus_1._29._1.Controles.ProgresBar;
using Bot_Dofus_1._29._1.Forms;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game.Personaje;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Inventario;
using Bot_Dofus_1._29._1.Properties;
using Bot_Dofus_1._29._1.Utilidades.Extensiones;
using Bot_Dofus_1._29._1.Utilidades.Logs;

namespace Bot_Dofus_1._29._1.Interfaces
{
	// Token: 0x02000079 RID: 121
	public class UI_Principal : UserControl
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x000192FF File Offset: 0x000174FF
		public UI_Principal(Cuenta _cuenta)
		{
			this.InitializeComponent();
			this.cuenta = _cuenta;
			this.nombre_cuenta = this.cuenta.configuracion.nombre_cuenta;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001932C File Offset: 0x0001752C
		private void UI_Principal_Load(object sender, EventArgs e)
		{
			this.desconectarOconectarToolStripMenuItem.Text = "Connecté";
			this.escribir_mensaje("[" + DateTime.Now.ToString("HH:mm:ss") + "] -> [INFORMATION] Gobotify 1.0.0.0 - Reprise des sources de Salesprendres", LogTipos.ERROR.ToString("X"));
			this.cuenta.evento_estado_cuenta += this.eventos_Estados_Cuenta;
			this.cuenta.cuenta_desconectada += this.desconectar_Cuenta;
			this.cuenta.logger.log_evento += delegate(LogMensajes mensaje, string color)
			{
				this.escribir_mensaje(mensaje.ToString(), color);
			};
			this.cuenta.script.evento_script_cargado += this.evento_Scripts_Cargado;
			this.cuenta.script.evento_script_iniciado += this.evento_Scripts_Iniciado;
			this.cuenta.script.evento_script_detenido += this.evento_Scripts_Detenido;
			this.cuenta.juego.personaje.caracteristicas_actualizadas += this.caracteristicas_Actualizadas;
			this.cuenta.juego.personaje.pods_actualizados += this.pods_Actualizados;
			this.cuenta.juego.personaje.servidor_seleccionado += this.servidor_Seleccionado;
			this.cuenta.juego.personaje.personaje_seleccionado += this.personaje_Seleccionado;
			if (this.cuenta.tiene_grupo)
			{
				this.escribir_mensaje("[" + DateTime.Now.ToString("HH:mm:ss") + "] -> Le chef de groupe est : " + this.cuenta.grupo.lider.configuracion.nombre_cuenta, LogTipos.ERROR.ToString("X"));
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00019508 File Offset: 0x00017708
		private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Principal.cuentas_cargadas.ContainsKey(this.nombre_cuenta))
			{
				if (this.cuenta.tiene_grupo && this.cuenta.es_lider_grupo)
				{
					this.cuenta.grupo.desconectar_Cuentas();
				}
				else if (this.cuenta.tiene_grupo)
				{
					this.cuenta.grupo.eliminar_Miembro(this.cuenta);
				}
				this.cuenta.Dispose();
				Principal.cuentas_cargadas[this.nombre_cuenta].contenido.Dispose();
				Principal.cuentas_cargadas.Remove(this.nombre_cuenta);
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000195AF File Offset: 0x000177AF
		private void cambiar_Tab_Imagen(Image image)
		{
			if (Principal.cuentas_cargadas.ContainsKey(this.nombre_cuenta))
			{
				Principal.cuentas_cargadas[this.nombre_cuenta].cabezera.propiedad_Imagen = image;
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000195DE File Offset: 0x000177DE
		private void button_limpiar_consola_Click(object sender, EventArgs e)
		{
			this.textbox_logs.Clear();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000195EC File Offset: 0x000177EC
		private void desconectarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.desconectarOconectarToolStripMenuItem.Text.Equals("Connecté"))
			{
				while (this.tabControl_principal.TabPages.Count > 2)
				{
					this.tabControl_principal.TabPages.RemoveAt(2);
				}
				this.cuenta.conectar();
				this.cuenta.conexion.paquete_recibido += this.debug.paquete_Recibido;
				this.cuenta.conexion.paquete_enviado += this.debug.paquete_Enviado;
				this.cuenta.conexion.socket_informacion += new Action<string>(this.get_Mensajes_Socket_Informacion);
				this.desconectarOconectarToolStripMenuItem.Text = "Déconnecté";
				return;
			}
			if (this.desconectarOconectarToolStripMenuItem.Text.Equals("Déconnecté"))
			{
				this.cuenta.desconectar();
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000196D5 File Offset: 0x000178D5
		private void desconectar_Cuenta()
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			base.BeginInvoke(new Action(delegate()
			{
				this.cambiar_Todos_Controles_Chat(false);
				for (int i = 2; i < this.tabControl_principal.TabPages.Count; i++)
				{
					this.tabControl_principal.TabPages[i].Enabled = false;
				}
				this.desconectarOconectarToolStripMenuItem.Text = "Connecté";
			}));
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000196F4 File Offset: 0x000178F4
		private void cambiar_Todos_Controles_Chat(bool estado)
		{
			base.BeginInvoke(new Action(delegate()
			{
				this.canal_informaciones.Enabled = estado;
				this.canal_general.Enabled = estado;
				this.canal_privado.Enabled = estado;
				this.canal_gremio.Enabled = estado;
				this.canal_alineamiento.Enabled = estado;
				this.canal_reclutamiento.Enabled = estado;
				this.canal_comercio.Enabled = estado;
				this.canal_incarnam.Enabled = estado;
				this.comboBox_lista_canales.Enabled = estado;
				this.textBox_enviar_consola.Enabled = estado;
				this.cargarScriptToolStripMenuItem.Enabled = estado;
			}));
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00019728 File Offset: 0x00017928
		private void eventos_Estados_Cuenta()
		{
			EstadoCuenta estado_Cuenta = this.cuenta.Estado_Cuenta;
			if (estado_Cuenta != EstadoCuenta.CONNECTE)
			{
				if (estado_Cuenta == EstadoCuenta.DECONNECTE)
				{
					this.cambiar_Tab_Imagen(Resources.circulo_rojo);
				}
				else
				{
					this.cambiar_Tab_Imagen(Resources.circulo_verde);
				}
			}
			else
			{
				this.cambiar_Tab_Imagen(Resources.circulo_naranja);
			}
			if (this.cuenta != null && Principal.cuentas_cargadas.ContainsKey(this.nombre_cuenta))
			{
				Principal.cuentas_cargadas[this.nombre_cuenta].cabezera.propiedad_Estado = this.cuenta.Estado_Cuenta.cadena_Amigable();
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000197B4 File Offset: 0x000179B4
		private void agregar_Tab_Pagina(string nombre, UserControl control, int imagen_index)
		{
			this.tabControl_principal.BeginInvoke(new Action(delegate()
			{
				control.Dock = DockStyle.Fill;
				TabPage tabPage = new TabPage(nombre)
				{
					ImageIndex = imagen_index
				};
				tabPage.Controls.Add(control);
				this.tabControl_principal.TabPages.Add(tabPage);
			}));
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000197FB File Offset: 0x000179FB
		private void cargar_Canales_Chat()
		{
			base.BeginInvoke(new Action(delegate()
			{
				this.canal_informaciones.Checked = this.cuenta.juego.personaje.canales.Contains("i");
				this.canal_general.Checked = this.cuenta.juego.personaje.canales.Contains("*");
				this.canal_privado.Checked = this.cuenta.juego.personaje.canales.Contains("#");
				this.canal_gremio.Checked = this.cuenta.juego.personaje.canales.Contains("%");
				this.canal_alineamiento.Checked = this.cuenta.juego.personaje.canales.Contains("!");
				this.canal_reclutamiento.Checked = this.cuenta.juego.personaje.canales.Contains("?");
				this.canal_comercio.Checked = this.cuenta.juego.personaje.canales.Contains(":");
				this.canal_incarnam.Checked = this.cuenta.juego.personaje.canales.Contains("^");
				this.comboBox_lista_canales.SelectedIndex = 0;
			}));
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00019810 File Offset: 0x00017A10
		private void canal_Chat_Click(object sender, EventArgs e)
		{
			Cuenta cuenta = this.cuenta;
			if (cuenta == null || cuenta.Estado_Cuenta != EstadoCuenta.DECONNECTE)
			{
				Cuenta cuenta2 = this.cuenta;
				if (cuenta2 == null || cuenta2.Estado_Cuenta > EstadoCuenta.CONNECTE)
				{
					string[] array = new string[]
					{
						"i",
						"*",
						"#$p",
						"%",
						"!",
						"?",
						":",
						"^"
					};
					CheckBox checkBox = sender as CheckBox;
					this.cuenta.conexion.enviar_Paquete((checkBox.Checked ? "cC+" : "cC-") + array[checkBox.TabIndex], false);
				}
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000198D4 File Offset: 0x00017AD4
		private void textBox_enviar_consola_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return && this.textBox_enviar_consola.TextLength > 0 && this.textBox_enviar_consola.TextLength < 255)
			{
				string text = this.textBox_enviar_consola.Text.ToUpper();
				if (text != null)
				{
					if (text == "/MAPID")
					{
						this.escribir_mensaje(this.cuenta.juego.mapa.id.ToString(), "0040FF");
						goto IL_247;
					}
					if (text == "/CELLID")
					{
						this.escribir_mensaje(this.cuenta.juego.personaje.celda.id.ToString(), "0040FF");
						goto IL_247;
					}
					if (text == "/PING")
					{
						if (this.cuenta.conexion != null)
						{
							this.cuenta.conexion.enviar_Paquete("ping", true);
							goto IL_247;
						}
						this.escribir_mensaje("Vous n'êtes pas connecté à dofus", "0040FF");
						goto IL_247;
					}
				}
				switch (this.comboBox_lista_canales.SelectedIndex)
				{
				case 0:
					this.cuenta.conexion.enviar_Paquete("BM*|" + this.textBox_enviar_consola.Text + "|", true);
					break;
				case 1:
					this.cuenta.conexion.enviar_Paquete("BM?|" + this.textBox_enviar_consola.Text + "|", true);
					break;
				case 2:
					this.cuenta.conexion.enviar_Paquete("BM:|" + this.textBox_enviar_consola.Text + "|", true);
					break;
				case 3:
					this.cuenta.conexion.enviar_Paquete(string.Concat(new string[]
					{
						"BM",
						this.textBox_nombre_privado.Text,
						"|",
						this.textBox_enviar_consola.Text,
						"|"
					}), true);
					break;
				case 4:
					this.cuenta.conexion.enviar_Paquete("BM%|" + this.textBox_enviar_consola.Text + "|", true);
					break;
				}
				IL_247:
				e.Handled = true;
				e.SuppressKeyPress = true;
				this.textBox_nombre_privado.Clear();
				this.textBox_enviar_consola.Clear();
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00019B4C File Offset: 0x00017D4C
		private void comboBox_lista_canales_Valor_Cambiado(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			this.textBox_nombre_privado.Enabled = (comboBox.SelectedIndex == 3);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00019B74 File Offset: 0x00017D74
		private void cargarScriptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog())
				{
					openFileDialog.Title = "Sélectionnez le script pour le bot";
					openFileDialog.Filter = "Extension (.lua) | *.lua";
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						this.cuenta.script.get_Desde_Archivo(openFileDialog.FileName);
					}
				}
			}
			catch (Exception ex)
			{
				this.cuenta.logger.log_Error("SCRIPT", ex.Message);
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00019C04 File Offset: 0x00017E04
		private void caracteristicas_Actualizadas()
		{
			base.BeginInvoke(new Action(delegate()
			{
				PersonajeJuego personaje = this.cuenta.juego.personaje;
				this.progresBar_vitalidad.valor_Maximo = personaje.caracteristicas.vitalidad_maxima;
				this.progresBar_vitalidad.Valor = personaje.caracteristicas.vitalidad_actual;
				this.progresBar_energia.valor_Maximo = personaje.caracteristicas.maxima_energia;
				this.progresBar_energia.Valor = personaje.caracteristicas.energia_actual;
				this.progresBar_experiencia.Text = personaje.nivel.ToString();
				this.progresBar_experiencia.Valor = personaje.porcentaje_experiencia;
				this.label_kamas_principal.Text = personaje.kamas.ToString("0,0");
			}));
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00019C19 File Offset: 0x00017E19
		private void pods_Actualizados()
		{
			base.BeginInvoke(new Action(delegate()
			{
				this.progresBar_pods.valor_Maximo = (int)this.cuenta.juego.personaje.inventario.pods_maximos;
				this.progresBar_pods.Valor = (int)this.cuenta.juego.personaje.inventario.pods_actuales;
			}));
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00019C2E File Offset: 0x00017E2E
		private void servidor_Seleccionado()
		{
			this.agregar_Tab_Pagina("Personnage", new UI_Personaje(this.cuenta), 2);
			this.agregar_Tab_Pagina("Inventaire", new UI_Inventario(this.cuenta), 3);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00019C60 File Offset: 0x00017E60
		private void personaje_Seleccionado()
		{
			this.cuenta.pelea_extension.configuracion.cargar();
			this.agregar_Tab_Pagina("Carte", new UI_Mapa(this.cuenta), 4);
			this.agregar_Tab_Pagina("Combats", new UI_Pelea(this.cuenta), 5);
			this.cambiar_Todos_Controles_Chat(true);
			this.cargar_Canales_Chat();
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00019CC0 File Offset: 0x00017EC0
		private void get_Mensajes_Socket_Informacion(object error)
		{
			this.escribir_mensaje("[" + DateTime.Now.ToString("HH:mm:ss") + "] [Connexion] " + ((error != null) ? error.ToString() : null), LogTipos.PELIGRO.ToString("X"));
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00019D14 File Offset: 0x00017F14
		private void escribir_mensaje(string mensaje, string color)
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			this.textbox_logs.BeginInvoke(new Action(delegate()
			{
				this.textbox_logs.Select(this.textbox_logs.TextLength, 0);
				this.textbox_logs.SelectionColor = ColorTranslator.FromHtml("#" + color);
				this.textbox_logs.AppendText(mensaje + Environment.NewLine);
				this.textbox_logs.ScrollToCaret();
			}));
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00019D5D File Offset: 0x00017F5D
		private void iniciarScriptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!this.cuenta.script.activado)
			{
				this.cuenta.script.activar_Script();
				return;
			}
			this.cuenta.script.detener_Script("", false);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00019D98 File Offset: 0x00017F98
		private void evento_Scripts_Cargado(string nombre)
		{
			this.cuenta.logger.log_informacion("SCRIPT", "'" + nombre + "' Chargé.");
			base.BeginInvoke(new Action(delegate()
			{
				this.ScriptTituloStripMenuItem.Text = (((nombre.Length > 16) ? nombre.Substring(0, 16) : nombre) ?? "");
				this.iniciarScriptToolStripMenuItem.Enabled = true;
			}));
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00019DF6 File Offset: 0x00017FF6
		private void evento_Scripts_Iniciado()
		{
			this.cuenta.logger.log_informacion("SCRIPT", "Lancé");
			base.BeginInvoke(new Action(delegate()
			{
				this.cargarScriptToolStripMenuItem.Enabled = false;
				this.iniciarScriptToolStripMenuItem.Image = Resources.boton_stop;
			}));
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00019E28 File Offset: 0x00018028
		private void evento_Scripts_Detenido(string motivo)
		{
			if (string.IsNullOrEmpty(motivo))
			{
				this.cuenta.logger.log_informacion("SCRIPT", "Arrêté");
			}
			else
			{
				this.cuenta.logger.log_informacion("SCRIPT", "Arrêté " + motivo);
			}
			base.BeginInvoke(new Action(delegate()
			{
				this.iniciarScriptToolStripMenuItem.Image = Resources.boton_play;
				this.cargarScriptToolStripMenuItem.Enabled = true;
				this.ScriptTituloStripMenuItem.Text = "-";
			}));
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00013157 File Offset: 0x00011357
		private void pictureBox3_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00019E8C File Offset: 0x0001808C
		private void potionrappelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ObjetosInventario objeto = this.cuenta.juego.personaje.inventario.get_Objeto_Modelo_Id(548);
			this.cuenta.juego.personaje.inventario.utilizar_Objeto(objeto);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00019ED4 File Offset: 0x000180D4
		private void bontaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ObjetosInventario objeto = this.cuenta.juego.personaje.inventario.get_Objeto_Modelo_Id(6965);
			this.cuenta.juego.personaje.inventario.utilizar_Objeto(objeto);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00019F1C File Offset: 0x0001811C
		private void brakToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			ObjetosInventario objeto = this.cuenta.juego.personaje.inventario.get_Objeto_Modelo_Id(6964);
			this.cuenta.juego.personaje.inventario.utilizar_Objeto(objeto);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00019F64 File Offset: 0x00018164
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00019F84 File Offset: 0x00018184
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.desconectarOconectarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScriptTituloStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cargarScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.potionrappelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bontaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brakToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl_principal = new System.Windows.Forms.TabControl();
            this.tabPage_consola = new System.Windows.Forms.TabPage();
            this.tableLayout_Canales = new System.Windows.Forms.TableLayoutPanel();
            this.textbox_logs = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_nombre_privado = new System.Windows.Forms.TextBox();
            this.comboBox_lista_canales = new System.Windows.Forms.ComboBox();
            this.button_limpiar_consola = new System.Windows.Forms.Button();
            this.textBox_enviar_consola = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lista_imagenes = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label_kamas_principal = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tabControl_principal.SuspendLayout();
            this.tabPage_consola.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.desconectarOconectarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.ScriptTituloStripMenuItem,
            this.cargarScriptToolStripMenuItem,
            this.iniciarScriptToolStripMenuItem,
            this.potionrappelToolStripMenuItem,
            this.bontaToolStripMenuItem,
            this.brakToolStripMenuItem1});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(804, 35);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // desconectarOconectarToolStripMenuItem
            // 
            this.desconectarOconectarToolStripMenuItem.Name = "desconectarOconectarToolStripMenuItem";
            this.desconectarOconectarToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.desconectarOconectarToolStripMenuItem.Size = new System.Drawing.Size(127, 29);
            this.desconectarOconectarToolStripMenuItem.Text = "Déconnecter";
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(111, 29);
            this.eliminarToolStripMenuItem.Text = "Supprimer";
            // 
            // ScriptTituloStripMenuItem
            // 
            this.ScriptTituloStripMenuItem.Name = "ScriptTituloStripMenuItem";
            this.ScriptTituloStripMenuItem.Size = new System.Drawing.Size(35, 29);
            this.ScriptTituloStripMenuItem.Text = "-";
            // 
            // cargarScriptToolStripMenuItem
            // 
            this.cargarScriptToolStripMenuItem.Enabled = false;
            this.cargarScriptToolStripMenuItem.Name = "cargarScriptToolStripMenuItem";
            this.cargarScriptToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.cargarScriptToolStripMenuItem.Size = new System.Drawing.Size(138, 29);
            this.cargarScriptToolStripMenuItem.Text = "Charger script";
            // 
            // iniciarScriptToolStripMenuItem
            // 
            this.iniciarScriptToolStripMenuItem.Enabled = false;
            this.iniciarScriptToolStripMenuItem.Name = "iniciarScriptToolStripMenuItem";
            this.iniciarScriptToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.iniciarScriptToolStripMenuItem.Size = new System.Drawing.Size(16, 29);
            // 
            // potionrappelToolStripMenuItem
            // 
            this.potionrappelToolStripMenuItem.Name = "potionrappelToolStripMenuItem";
            this.potionrappelToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.potionrappelToolStripMenuItem.Size = new System.Drawing.Size(16, 29);
            // 
            // bontaToolStripMenuItem
            // 
            this.bontaToolStripMenuItem.Name = "bontaToolStripMenuItem";
            this.bontaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.bontaToolStripMenuItem.Size = new System.Drawing.Size(16, 29);
            // 
            // brakToolStripMenuItem1
            // 
            this.brakToolStripMenuItem1.Name = "brakToolStripMenuItem1";
            this.brakToolStripMenuItem1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.brakToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.brakToolStripMenuItem1.Size = new System.Drawing.Size(16, 29);
            // 
            // tabControl_principal
            // 
            this.tabControl_principal.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl_principal.Controls.Add(this.tabPage_consola);
            this.tabControl_principal.Controls.Add(this.tabPage2);
            this.tabControl_principal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_principal.ImageList = this.lista_imagenes;
            this.tabControl_principal.ItemSize = new System.Drawing.Size(67, 26);
            this.tabControl_principal.Location = new System.Drawing.Point(0, 35);
            this.tabControl_principal.Name = "tabControl_principal";
            this.tabControl_principal.SelectedIndex = 0;
            this.tabControl_principal.Size = new System.Drawing.Size(804, 536);
            this.tabControl_principal.TabIndex = 0;
            // 
            // tabPage_consola
            // 
            this.tabPage_consola.Controls.Add(this.tableLayout_Canales);
            this.tabPage_consola.Controls.Add(this.textbox_logs);
            this.tabPage_consola.Controls.Add(this.tableLayoutPanel1);
            this.tabPage_consola.ImageIndex = 0;
            this.tabPage_consola.Location = new System.Drawing.Point(4, 30);
            this.tabPage_consola.Name = "tabPage_consola";
            this.tabPage_consola.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_consola.Size = new System.Drawing.Size(796, 502);
            this.tabPage_consola.TabIndex = 0;
            this.tabPage_consola.Text = "Console";
            this.tabPage_consola.UseVisualStyleBackColor = true;
            // 
            // tableLayout_Canales
            // 
            this.tableLayout_Canales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tableLayout_Canales.ColumnCount = 1;
            this.tableLayout_Canales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout_Canales.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayout_Canales.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayout_Canales.Location = new System.Drawing.Point(772, 3);
            this.tableLayout_Canales.Name = "tableLayout_Canales";
            this.tableLayout_Canales.RowCount = 8;
            this.tableLayout_Canales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayout_Canales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayout_Canales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayout_Canales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayout_Canales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayout_Canales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayout_Canales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayout_Canales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayout_Canales.Size = new System.Drawing.Size(21, 465);
            this.tableLayout_Canales.TabIndex = 0;
            // 
            // textbox_logs
            // 
            this.textbox_logs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textbox_logs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textbox_logs.Cursor = System.Windows.Forms.Cursors.Default;
            this.textbox_logs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textbox_logs.Location = new System.Drawing.Point(3, 3);
            this.textbox_logs.MaxLength = 200;
            this.textbox_logs.Name = "textbox_logs";
            this.textbox_logs.Size = new System.Drawing.Size(790, 465);
            this.textbox_logs.TabIndex = 5;
            this.textbox_logs.Text = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.76676F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.31367F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.91957F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tableLayoutPanel1.Controls.Add(this.textBox_nombre_privado, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_lista_canales, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_limpiar_consola, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_enviar_consola, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 468);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(790, 31);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBox_nombre_privado
            // 
            this.textBox_nombre_privado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox_nombre_privado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_nombre_privado.Enabled = false;
            this.textBox_nombre_privado.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox_nombre_privado.Location = new System.Drawing.Point(128, 3);
            this.textBox_nombre_privado.MaxLength = 80;
            this.textBox_nombre_privado.Name = "textBox_nombre_privado";
            this.textBox_nombre_privado.Size = new System.Drawing.Size(136, 33);
            this.textBox_nombre_privado.TabIndex = 3;
            // 
            // comboBox_lista_canales
            // 
            this.comboBox_lista_canales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.comboBox_lista_canales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_lista_canales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_lista_canales.Enabled = false;
            this.comboBox_lista_canales.FormattingEnabled = true;
            this.comboBox_lista_canales.Items.AddRange(new object[] {
            "Général",
            "Recrutement",
            "Commerce",
            "Message Privé",
            "Guilde"});
            this.comboBox_lista_canales.Location = new System.Drawing.Point(3, 3);
            this.comboBox_lista_canales.Name = "comboBox_lista_canales";
            this.comboBox_lista_canales.Size = new System.Drawing.Size(119, 36);
            this.comboBox_lista_canales.TabIndex = 2;
            // 
            // button_limpiar_consola
            // 
            this.button_limpiar_consola.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button_limpiar_consola.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_limpiar_consola.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_limpiar_consola.Location = new System.Drawing.Point(669, 3);
            this.button_limpiar_consola.Name = "button_limpiar_consola";
            this.button_limpiar_consola.Size = new System.Drawing.Size(118, 25);
            this.button_limpiar_consola.TabIndex = 1;
            this.button_limpiar_consola.UseVisualStyleBackColor = false;
            // 
            // textBox_enviar_consola
            // 
            this.textBox_enviar_consola.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox_enviar_consola.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_enviar_consola.Enabled = false;
            this.textBox_enviar_consola.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox_enviar_consola.Location = new System.Drawing.Point(270, 3);
            this.textBox_enviar_consola.MaxLength = 80;
            this.textBox_enviar_consola.Name = "textBox_enviar_consola";
            this.textBox_enviar_consola.Size = new System.Drawing.Size(393, 33);
            this.textBox_enviar_consola.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(796, 502);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Debugger";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lista_imagenes
            // 
            this.lista_imagenes.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.lista_imagenes.ImageSize = new System.Drawing.Size(16, 16);
            this.lista_imagenes.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tableLayoutPanel4.ColumnCount = 10;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.19867F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.80491F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.197404F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.8017F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.197404F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.8017F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.197404F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.8017F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.197404F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.8017F));
            this.tableLayoutPanel4.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.pictureBox2, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.pictureBox3, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.pictureBox4, 8, 0);
            this.tableLayoutPanel4.Controls.Add(this.pictureBox5, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.label_kamas_principal, 9, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 571);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(804, 33);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 27);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(163, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(35, 27);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Location = new System.Drawing.Point(483, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(35, 27);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox4.Location = new System.Drawing.Point(643, 3);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(35, 27);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox5.Location = new System.Drawing.Point(323, 3);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(35, 27);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox5.TabIndex = 4;
            this.pictureBox5.TabStop = false;
            // 
            // label_kamas_principal
            // 
            this.label_kamas_principal.AutoSize = true;
            this.label_kamas_principal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label_kamas_principal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_kamas_principal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_kamas_principal.Location = new System.Drawing.Point(684, 0);
            this.label_kamas_principal.Name = "label_kamas_principal";
            this.label_kamas_principal.Size = new System.Drawing.Size(117, 33);
            this.label_kamas_principal.TabIndex = 9;
            this.label_kamas_principal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UI_Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl_principal);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UI_Principal";
            this.Size = new System.Drawing.Size(804, 604);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl_principal.ResumeLayout(false);
            this.tabPage_consola.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		// Token: 0x040002D7 RID: 727
		private Cuenta cuenta;

		// Token: 0x040002D8 RID: 728
		private string nombre_cuenta;

		// Token: 0x040002D9 RID: 729
		private IContainer components;

		// Token: 0x040002DA RID: 730
		private MenuStrip menuStrip1;

		// Token: 0x040002DB RID: 731
		private ToolStripMenuItem desconectarOconectarToolStripMenuItem;

		// Token: 0x040002DC RID: 732
		private ToolStripMenuItem eliminarToolStripMenuItem;

		// Token: 0x040002DD RID: 733
		private ToolStripMenuItem ScriptTituloStripMenuItem;

		// Token: 0x040002DE RID: 734
		public TabControl tabControl_principal;

		// Token: 0x040002DF RID: 735
		private TabPage tabPage_consola;

		// Token: 0x040002E0 RID: 736
		private TabPage tabPage2;

		// Token: 0x040002E1 RID: 737
		private TableLayoutPanel tableLayout_Canales;

		// Token: 0x040002E2 RID: 738
		private ColorCheckBox canal_informaciones;

		// Token: 0x040002E3 RID: 739
		private ColorCheckBox canal_comercio;

		// Token: 0x040002E4 RID: 740
		private ColorCheckBox canal_alineamiento;

		// Token: 0x040002E5 RID: 741
		private ColorCheckBox canal_reclutamiento;

		// Token: 0x040002E6 RID: 742
		private ColorCheckBox canal_gremio;

		// Token: 0x040002E7 RID: 743
		private ColorCheckBox canal_privado;

		// Token: 0x040002E8 RID: 744
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x040002E9 RID: 745
		private TextBox textBox_enviar_consola;

		// Token: 0x040002EA RID: 746
		private Button button_limpiar_consola;

		// Token: 0x040002EB RID: 747
		private RichTextBox textbox_logs;

		// Token: 0x040002EC RID: 748
		private TableLayoutPanel tableLayoutPanel4;

		// Token: 0x040002ED RID: 749
		private PictureBox pictureBox1;

		// Token: 0x040002EE RID: 750
		private PictureBox pictureBox2;

		// Token: 0x040002EF RID: 751
		private PictureBox pictureBox3;

		// Token: 0x040002F0 RID: 752
		private PictureBox pictureBox4;

		// Token: 0x040002F1 RID: 753
		private PictureBox pictureBox5;

		// Token: 0x040002F2 RID: 754
		private ProgresBar progresBar_vitalidad;

		// Token: 0x040002F3 RID: 755
		private ProgresBar progresBar_energia;

		// Token: 0x040002F4 RID: 756
		private ProgresBar progresBar_experiencia;

		// Token: 0x040002F5 RID: 757
		private ProgresBar progresBar_pods;

		// Token: 0x040002F6 RID: 758
		private UI_Debugger debug;

		// Token: 0x040002F7 RID: 759
		private ColorCheckBox canal_incarnam;

		// Token: 0x040002F8 RID: 760
		private ColorCheckBox canal_general;

		// Token: 0x040002F9 RID: 761
		private Label label_kamas_principal;

		// Token: 0x040002FA RID: 762
		private ToolStripMenuItem cargarScriptToolStripMenuItem;

		// Token: 0x040002FB RID: 763
		private ToolStripMenuItem iniciarScriptToolStripMenuItem;

		// Token: 0x040002FC RID: 764
		private ComboBox comboBox_lista_canales;

		// Token: 0x040002FD RID: 765
		private TextBox textBox_nombre_privado;

		// Token: 0x040002FE RID: 766
		private ImageList lista_imagenes;

		// Token: 0x040002FF RID: 767
		private ToolStripMenuItem potionrappelToolStripMenuItem;

		// Token: 0x04000300 RID: 768
		private ToolStripMenuItem bontaToolStripMenuItem;

		// Token: 0x04000301 RID: 769
		private ToolStripMenuItem brakToolStripMenuItem1;
	}
}
