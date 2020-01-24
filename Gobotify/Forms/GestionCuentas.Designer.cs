namespace Bot_Dofus_1._29._1.Forms
{
	// Token: 0x0200007C RID: 124
	public partial class GestionCuentas : global::System.Windows.Forms.Form
	{
		// Token: 0x06000521 RID: 1313 RVA: 0x0001D62E File Offset: 0x0001B82E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001D650 File Offset: 0x0001B850
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Bot_Dofus_1._29._1.Forms.GestionCuentas));
			this.tabControlPrincipalCuentas = new global::System.Windows.Forms.TabControl();
			this.ListaCuentas = new global::System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new global::System.Windows.Forms.TableLayoutPanel();
			this.pictureBox_informacion = new global::System.Windows.Forms.PictureBox();
			this.label_informacionClickCuentas = new global::System.Windows.Forms.Label();
			this.listViewCuentas = new global::System.Windows.Forms.ListView();
			this.ColumnaNombreCuenta = new global::System.Windows.Forms.ColumnHeader();
			this.ColumnaNombreServidor = new global::System.Windows.Forms.ColumnHeader();
			this.ColumnaNombrePersonaje = new global::System.Windows.Forms.ColumnHeader();
			this.contextMenuStripFormCuentas = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.conectarToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.modificarToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.cuentaToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.contraseñaToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.nombreDelPersonajeToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.eliminarToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.AgregarCuenta = new global::System.Windows.Forms.TabPage();
			this.tableLayoutPanel3 = new global::System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel4 = new global::System.Windows.Forms.TableLayoutPanel();
			this.pictureBox_informacion_agregar_cuenta = new global::System.Windows.Forms.PictureBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.checkBox_Agregar_Retroceder = new global::System.Windows.Forms.CheckBox();
			this.tableLayoutPanel5 = new global::System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel6 = new global::System.Windows.Forms.TableLayoutPanel();
			this.label_Nombre_Cuenta = new global::System.Windows.Forms.Label();
			this.label_Password = new global::System.Windows.Forms.Label();
			this.label_Eleccion_Servidor = new global::System.Windows.Forms.Label();
			this.label_Nombre_Personaje = new global::System.Windows.Forms.Label();
			this.tableLayoutPanel7 = new global::System.Windows.Forms.TableLayoutPanel();
			this.textBox_nombre_personaje = new global::System.Windows.Forms.TextBox();
			this.tableLayoutPanel8 = new global::System.Windows.Forms.TableLayoutPanel();
			this.comboBox_Servidor = new global::System.Windows.Forms.ComboBox();
			this.tableLayoutPanel9 = new global::System.Windows.Forms.TableLayoutPanel();
			this.textBox_Password = new global::System.Windows.Forms.TextBox();
			this.tableLayoutPanel10 = new global::System.Windows.Forms.TableLayoutPanel();
			this.textBox_Nombre_Cuenta = new global::System.Windows.Forms.TextBox();
			this.boton_Agregar_Cuenta = new global::System.Windows.Forms.Button();
			this.tabPage1 = new global::System.Windows.Forms.TabPage();
			this.imagenesFormCuentas = new global::System.Windows.Forms.ImageList(this.components);
			this.tabControlPrincipalCuentas.SuspendLayout();
			this.ListaCuentas.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox_informacion).BeginInit();
			this.contextMenuStripFormCuentas.SuspendLayout();
			this.AgregarCuenta.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox_informacion_agregar_cuenta).BeginInit();
			this.tableLayoutPanel5.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			this.tableLayoutPanel7.SuspendLayout();
			this.tableLayoutPanel8.SuspendLayout();
			this.tableLayoutPanel9.SuspendLayout();
			this.tableLayoutPanel10.SuspendLayout();
			base.SuspendLayout();
			this.tabControlPrincipalCuentas.Controls.Add(this.ListaCuentas);
			this.tabControlPrincipalCuentas.Controls.Add(this.AgregarCuenta);
			this.tabControlPrincipalCuentas.Controls.Add(this.tabPage1);
			this.tabControlPrincipalCuentas.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tabControlPrincipalCuentas.Font = new global::System.Drawing.Font("Segoe UI", 9.75f);
			this.tabControlPrincipalCuentas.ImageList = this.imagenesFormCuentas;
			this.tabControlPrincipalCuentas.ItemSize = new global::System.Drawing.Size(137, 28);
			this.tabControlPrincipalCuentas.Location = new global::System.Drawing.Point(0, 0);
			this.tabControlPrincipalCuentas.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tabControlPrincipalCuentas.Name = "tabControlPrincipalCuentas";
			this.tabControlPrincipalCuentas.SelectedIndex = 0;
			this.tabControlPrincipalCuentas.Size = new global::System.Drawing.Size(784, 561);
			this.tabControlPrincipalCuentas.TabIndex = 0;
			this.ListaCuentas.BackColor = global::System.Drawing.Color.Gray;
			this.ListaCuentas.Controls.Add(this.tableLayoutPanel1);
			this.ListaCuentas.ImageKey = "lista_cuentas.png";
			this.ListaCuentas.Location = new global::System.Drawing.Point(4, 32);
			this.ListaCuentas.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ListaCuentas.Name = "ListaCuentas";
			this.ListaCuentas.Padding = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ListaCuentas.Size = new global::System.Drawing.Size(776, 525);
			this.ListaCuentas.TabIndex = 0;
			this.ListaCuentas.Text = "Liste des comptes";
			this.tableLayoutPanel1.BackColor = global::System.Drawing.Color.Gray;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.listViewCuentas, 0, 0);
			this.tableLayoutPanel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new global::System.Drawing.Point(3, 4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 87.00565f));
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 12.99435f));
			this.tableLayoutPanel1.Size = new global::System.Drawing.Size(770, 517);
			this.tableLayoutPanel1.TabIndex = 1;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 10.15801f));
			this.tableLayoutPanel2.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 89.84199f));
			this.tableLayoutPanel2.Controls.Add(this.pictureBox_informacion, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.label_informacionClickCuentas, 1, 0);
			this.tableLayoutPanel2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new global::System.Drawing.Point(3, 452);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.tableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 40f));
			this.tableLayoutPanel2.Size = new global::System.Drawing.Size(764, 62);
			this.tableLayoutPanel2.TabIndex = 2;
			this.pictureBox_informacion.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.pictureBox_informacion.Image = global::Bot_Dofus_1._29._1.Properties.Resources.informacion;
			this.pictureBox_informacion.Location = new global::System.Drawing.Point(3, 3);
			this.pictureBox_informacion.Name = "pictureBox_informacion";
			this.pictureBox_informacion.Size = new global::System.Drawing.Size(71, 56);
			this.pictureBox_informacion.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox_informacion.TabIndex = 0;
			this.pictureBox_informacion.TabStop = false;
			this.label_informacionClickCuentas.AutoSize = true;
			this.label_informacionClickCuentas.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.label_informacionClickCuentas.Location = new global::System.Drawing.Point(80, 0);
			this.label_informacionClickCuentas.Name = "label_informacionClickCuentas";
			this.label_informacionClickCuentas.Size = new global::System.Drawing.Size(681, 62);
			this.label_informacionClickCuentas.TabIndex = 1;
			this.label_informacionClickCuentas.Text = "Clic droit pour connecter/modifier/supprimer un compte\r\\double clic sur un compte pour le connecter";
			this.label_informacionClickCuentas.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.listViewCuentas.BackColor = global::System.Drawing.Color.Gray;
			this.listViewCuentas.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.ColumnaNombreCuenta,
				this.ColumnaNombreServidor,
				this.ColumnaNombrePersonaje
			});
			this.listViewCuentas.ContextMenuStrip = this.contextMenuStripFormCuentas;
			this.listViewCuentas.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listViewCuentas.FullRowSelect = true;
			this.listViewCuentas.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewCuentas.HideSelection = false;
			this.listViewCuentas.Location = new global::System.Drawing.Point(3, 4);
			this.listViewCuentas.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.listViewCuentas.Name = "listViewCuentas";
			this.listViewCuentas.Size = new global::System.Drawing.Size(764, 441);
			this.listViewCuentas.TabIndex = 1;
			this.listViewCuentas.UseCompatibleStateImageBehavior = false;
			this.listViewCuentas.View = global::System.Windows.Forms.View.Details;
			this.listViewCuentas.ColumnWidthChanging += new global::System.Windows.Forms.ColumnWidthChangingEventHandler(this.listViewCuentas_ColumnWidthChanging);
			this.listViewCuentas.MouseDoubleClick += new global::System.Windows.Forms.MouseEventHandler(this.listViewCuentas_MouseDoubleClick);
			this.ColumnaNombreCuenta.Text = "Nom de compte";
			this.ColumnaNombreCuenta.Width = 148;
			this.ColumnaNombreServidor.Text = "Serveur";
			this.ColumnaNombreServidor.Width = 107;
			this.ColumnaNombrePersonaje.Text = "Nom du personnage";
			this.ColumnaNombrePersonaje.Width = 184;
			this.contextMenuStripFormCuentas.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.conectarToolStripMenuItem,
				this.modificarToolStripMenuItem,
				this.eliminarToolStripMenuItem
			});
			this.contextMenuStripFormCuentas.Name = "contextMenuStripFormCuentas";
			this.contextMenuStripFormCuentas.Size = new global::System.Drawing.Size(132, 70);
			this.conectarToolStripMenuItem.Image = global::Bot_Dofus_1._29._1.Properties.Resources.flecha_direccion_izquierda;
			this.conectarToolStripMenuItem.Name = "conectarToolStripMenuItem";
			this.conectarToolStripMenuItem.Size = new global::System.Drawing.Size(131, 22);
			this.conectarToolStripMenuItem.Text = "Connexion";
			this.conectarToolStripMenuItem.Click += new global::System.EventHandler(this.conectarToolStripMenuItem_Click);
			this.modificarToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.cuentaToolStripMenuItem,
				this.contraseñaToolStripMenuItem,
				this.nombreDelPersonajeToolStripMenuItem
			});
			this.modificarToolStripMenuItem.Image = global::Bot_Dofus_1._29._1.Properties.Resources.boton_ajustes;
			this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
			this.modificarToolStripMenuItem.Size = new global::System.Drawing.Size(131, 22);
			this.modificarToolStripMenuItem.Text = "Modifier";
			this.cuentaToolStripMenuItem.Name = "cuentaToolStripMenuItem";
			this.cuentaToolStripMenuItem.Size = new global::System.Drawing.Size(183, 22);
			this.cuentaToolStripMenuItem.Text = "Compte";
			this.cuentaToolStripMenuItem.Click += new global::System.EventHandler(this.modificar_Cuenta);
			this.contraseñaToolStripMenuItem.Name = "contraseñaToolStripMenuItem";
			this.contraseñaToolStripMenuItem.Size = new global::System.Drawing.Size(183, 22);
			this.contraseñaToolStripMenuItem.Text = "Mot de passe";
			this.contraseñaToolStripMenuItem.Click += new global::System.EventHandler(this.modificar_Cuenta);
			this.nombreDelPersonajeToolStripMenuItem.Name = "nombreDelPersonajeToolStripMenuItem";
			this.nombreDelPersonajeToolStripMenuItem.Size = new global::System.Drawing.Size(183, 22);
			this.nombreDelPersonajeToolStripMenuItem.Text = "Nom du personnage";
			this.nombreDelPersonajeToolStripMenuItem.Click += new global::System.EventHandler(this.modificar_Cuenta);
			this.eliminarToolStripMenuItem.Image = global::Bot_Dofus_1._29._1.Properties.Resources.cruz_roja;
			this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
			this.eliminarToolStripMenuItem.Size = new global::System.Drawing.Size(131, 22);
			this.eliminarToolStripMenuItem.Text = "Supprimer";
			this.eliminarToolStripMenuItem.Click += new global::System.EventHandler(this.eliminarToolStripMenuItem_Click);
			this.AgregarCuenta.BackColor = global::System.Drawing.Color.Gray;
			this.AgregarCuenta.Controls.Add(this.tableLayoutPanel3);
			this.AgregarCuenta.ImageKey = "agregar_cuenta.png";
			this.AgregarCuenta.Location = new global::System.Drawing.Point(4, 32);
			this.AgregarCuenta.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.AgregarCuenta.Name = "AgregarCuenta";
			this.AgregarCuenta.Padding = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.AgregarCuenta.Size = new global::System.Drawing.Size(576, 525);
			this.AgregarCuenta.TabIndex = 1;
			this.AgregarCuenta.Text = "Ajouter un compte";
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.checkBox_Agregar_Retroceder, 0, 2);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 1);
			this.tableLayoutPanel3.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new global::System.Drawing.Point(3, 4);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 3;
			this.tableLayoutPanel3.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 11.71429f));
			this.tableLayoutPanel3.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 80.28571f));
			this.tableLayoutPanel3.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 7.714286f));
			this.tableLayoutPanel3.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutPanel3.Size = new global::System.Drawing.Size(570, 517);
			this.tableLayoutPanel3.TabIndex = 0;
			this.tableLayoutPanel4.ColumnCount = 2;
			this.tableLayoutPanel4.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 8.374384f));
			this.tableLayoutPanel4.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 91.62562f));
			this.tableLayoutPanel4.Controls.Add(this.pictureBox_informacion_agregar_cuenta, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.label1, 1, 0);
			this.tableLayoutPanel4.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new global::System.Drawing.Point(3, 3);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.tableLayoutPanel4.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 35f));
			this.tableLayoutPanel4.Size = new global::System.Drawing.Size(564, 54);
			this.tableLayoutPanel4.TabIndex = 2;
			this.pictureBox_informacion_agregar_cuenta.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.pictureBox_informacion_agregar_cuenta.Image = global::Bot_Dofus_1._29._1.Properties.Resources.informacion;
			this.pictureBox_informacion_agregar_cuenta.Location = new global::System.Drawing.Point(3, 3);
			this.pictureBox_informacion_agregar_cuenta.Name = "pictureBox_informacion_agregar_cuenta";
			this.pictureBox_informacion_agregar_cuenta.Size = new global::System.Drawing.Size(41, 48);
			this.pictureBox_informacion_agregar_cuenta.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox_informacion_agregar_cuenta.TabIndex = 1;
			this.pictureBox_informacion_agregar_cuenta.TabStop = false;
			this.label1.AutoSize = true;
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new global::System.Drawing.Point(50, 0);
			this.label1.Name = "label1";
			this.label1.RightToLeft = global::System.Windows.Forms.RightToLeft.No;
			this.label1.Size = new global::System.Drawing.Size(511, 54);
			this.label1.TabIndex = 1;
			this.label1.Text = "Laissez le champ \"Personnage\" vide si vous voulez que le robot connecte la première personne sur le compte.";
			this.checkBox_Agregar_Retroceder.AutoSize = true;
			this.checkBox_Agregar_Retroceder.Checked = true;
			this.checkBox_Agregar_Retroceder.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBox_Agregar_Retroceder.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.checkBox_Agregar_Retroceder.Location = new global::System.Drawing.Point(3, 479);
			this.checkBox_Agregar_Retroceder.Name = "checkBox_Agregar_Retroceder";
			this.checkBox_Agregar_Retroceder.Size = new global::System.Drawing.Size(564, 35);
			this.checkBox_Agregar_Retroceder.TabIndex = 51;
			this.checkBox_Agregar_Retroceder.Text = "Retour à l'onglet \"Liste des comptes\" après l'ajout du compte";
			this.checkBox_Agregar_Retroceder.UseVisualStyleBackColor = true;
			this.tableLayoutPanel5.ColumnCount = 1;
			this.tableLayoutPanel5.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.boton_Agregar_Cuenta, 0, 1);
			this.tableLayoutPanel5.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel5.Location = new global::System.Drawing.Point(3, 63);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 2;
			this.tableLayoutPanel5.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 87.63636f));
			this.tableLayoutPanel5.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 12.36364f));
			this.tableLayoutPanel5.Size = new global::System.Drawing.Size(564, 410);
			this.tableLayoutPanel5.TabIndex = 5;
			this.tableLayoutPanel6.ColumnCount = 2;
			this.tableLayoutPanel6.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 28.96552f));
			this.tableLayoutPanel6.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 71.03448f));
			this.tableLayoutPanel6.Controls.Add(this.label_Nombre_Cuenta, 0, 0);
			this.tableLayoutPanel6.Controls.Add(this.label_Password, 0, 1);
			this.tableLayoutPanel6.Controls.Add(this.label_Eleccion_Servidor, 0, 2);
			this.tableLayoutPanel6.Controls.Add(this.label_Nombre_Personaje, 0, 3);
			this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 1, 3);
			this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel8, 1, 2);
			this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel9, 1, 1);
			this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel10, 1, 0);
			this.tableLayoutPanel6.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel6.Location = new global::System.Drawing.Point(3, 3);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 4;
			this.tableLayoutPanel6.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.tableLayoutPanel6.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.tableLayoutPanel6.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.tableLayoutPanel6.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.tableLayoutPanel6.Size = new global::System.Drawing.Size(558, 353);
			this.tableLayoutPanel6.TabIndex = 2;
			this.label_Nombre_Cuenta.AutoSize = true;
			this.label_Nombre_Cuenta.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.label_Nombre_Cuenta.Font = new global::System.Drawing.Font("Segoe UI", 14.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label_Nombre_Cuenta.Location = new global::System.Drawing.Point(3, 0);
			this.label_Nombre_Cuenta.Name = "label_Nombre_Cuenta";
			this.label_Nombre_Cuenta.Size = new global::System.Drawing.Size(155, 88);
			this.label_Nombre_Cuenta.TabIndex = 1;
			this.label_Nombre_Cuenta.Text = "Compte:";
			this.label_Nombre_Cuenta.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label_Password.AutoSize = true;
			this.label_Password.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.label_Password.Font = new global::System.Drawing.Font("Segoe UI", 14.25f, global::System.Drawing.FontStyle.Bold);
			this.label_Password.Location = new global::System.Drawing.Point(3, 88);
			this.label_Password.Name = "label_Password";
			this.label_Password.Size = new global::System.Drawing.Size(155, 88);
			this.label_Password.TabIndex = 3;
			this.label_Password.Text = "Mot de passe:";
			this.label_Password.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label_Eleccion_Servidor.AutoSize = true;
			this.label_Eleccion_Servidor.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.label_Eleccion_Servidor.Font = new global::System.Drawing.Font("Segoe UI", 14.25f, global::System.Drawing.FontStyle.Bold);
			this.label_Eleccion_Servidor.Location = new global::System.Drawing.Point(3, 176);
			this.label_Eleccion_Servidor.Name = "label_Eleccion_Servidor";
			this.label_Eleccion_Servidor.Size = new global::System.Drawing.Size(155, 88);
			this.label_Eleccion_Servidor.TabIndex = 5;
			this.label_Eleccion_Servidor.Text = "Serveur:";
			this.label_Eleccion_Servidor.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label_Nombre_Personaje.AutoSize = true;
			this.label_Nombre_Personaje.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.label_Nombre_Personaje.Font = new global::System.Drawing.Font("Segoe UI", 14.25f, global::System.Drawing.FontStyle.Bold);
			this.label_Nombre_Personaje.Location = new global::System.Drawing.Point(3, 264);
			this.label_Nombre_Personaje.Name = "label_Nombre_Personaje";
			this.label_Nombre_Personaje.Size = new global::System.Drawing.Size(155, 89);
			this.label_Nombre_Personaje.TabIndex = 7;
			this.label_Nombre_Personaje.Text = "Personnage:";
			this.label_Nombre_Personaje.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.tableLayoutPanel7.ColumnCount = 1;
			this.tableLayoutPanel7.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutPanel7.Controls.Add(this.textBox_nombre_personaje, 0, 1);
			this.tableLayoutPanel7.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel7.Location = new global::System.Drawing.Point(164, 267);
			this.tableLayoutPanel7.Name = "tableLayoutPanel7";
			this.tableLayoutPanel7.RowCount = 3;
			this.tableLayoutPanel7.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel7.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel7.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel7.Size = new global::System.Drawing.Size(391, 83);
			this.tableLayoutPanel7.TabIndex = 4;
			this.textBox_nombre_personaje.BackColor = global::System.Drawing.Color.FromArgb(224, 224, 224);
			this.textBox_nombre_personaje.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.textBox_nombre_personaje.Location = new global::System.Drawing.Point(3, 30);
			this.textBox_nombre_personaje.MaxLength = 25;
			this.textBox_nombre_personaje.Name = "textBox_nombre_personaje";
			this.textBox_nombre_personaje.Size = new global::System.Drawing.Size(385, 25);
			this.textBox_nombre_personaje.TabIndex = 5;
			this.tableLayoutPanel8.ColumnCount = 1;
			this.tableLayoutPanel8.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutPanel8.Controls.Add(this.comboBox_Servidor, 0, 1);
			this.tableLayoutPanel8.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel8.Location = new global::System.Drawing.Point(164, 179);
			this.tableLayoutPanel8.Name = "tableLayoutPanel8";
			this.tableLayoutPanel8.RowCount = 3;
			this.tableLayoutPanel8.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 39.39f));
			this.tableLayoutPanel8.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 60.61f));
			this.tableLayoutPanel8.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 18f));
			this.tableLayoutPanel8.Size = new global::System.Drawing.Size(391, 82);
			this.tableLayoutPanel8.TabIndex = 3;
			this.comboBox_Servidor.BackColor = global::System.Drawing.Color.Gray;
			this.comboBox_Servidor.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.comboBox_Servidor.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_Servidor.FormattingEnabled = true;
			this.comboBox_Servidor.Items.AddRange(new object[]
			{
				"Eratz",
				"Henual",
				"Clustus",
				"Nabur",
				"Arty",
				"Algathe",
				"Hogmeiser",
				"Droupik",
				"Bilby",
				"Issering",
				"Ayuto"
			});
			this.comboBox_Servidor.Location = new global::System.Drawing.Point(3, 28);
			this.comboBox_Servidor.Name = "comboBox_Servidor";
			this.comboBox_Servidor.Size = new global::System.Drawing.Size(385, 25);
			this.comboBox_Servidor.TabIndex = 6;
			this.tableLayoutPanel9.ColumnCount = 1;
			this.tableLayoutPanel9.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutPanel9.Controls.Add(this.textBox_Password, 0, 1);
			this.tableLayoutPanel9.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel9.Location = new global::System.Drawing.Point(164, 91);
			this.tableLayoutPanel9.Name = "tableLayoutPanel9";
			this.tableLayoutPanel9.RowCount = 3;
			this.tableLayoutPanel9.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel9.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel9.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel9.Size = new global::System.Drawing.Size(391, 82);
			this.tableLayoutPanel9.TabIndex = 2;
			this.textBox_Password.BackColor = global::System.Drawing.Color.FromArgb(224, 224, 224);
			this.textBox_Password.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.textBox_Password.Location = new global::System.Drawing.Point(3, 30);
			this.textBox_Password.MaxLength = 30;
			this.textBox_Password.Name = "textBox_Password";
			this.textBox_Password.PasswordChar = '*';
			this.textBox_Password.Size = new global::System.Drawing.Size(385, 25);
			this.textBox_Password.TabIndex = 4;
			this.textBox_Password.TextChanged += new global::System.EventHandler(this.textBox_Password_TextChanged);
			this.tableLayoutPanel10.ColumnCount = 1;
			this.tableLayoutPanel10.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutPanel10.Controls.Add(this.textBox_Nombre_Cuenta, 0, 1);
			this.tableLayoutPanel10.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel10.Location = new global::System.Drawing.Point(164, 3);
			this.tableLayoutPanel10.Name = "tableLayoutPanel10";
			this.tableLayoutPanel10.RowCount = 3;
			this.tableLayoutPanel10.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel10.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel10.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel10.Size = new global::System.Drawing.Size(391, 82);
			this.tableLayoutPanel10.TabIndex = 1;
			this.textBox_Nombre_Cuenta.BackColor = global::System.Drawing.Color.FromArgb(224, 224, 224);
			this.textBox_Nombre_Cuenta.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.textBox_Nombre_Cuenta.Location = new global::System.Drawing.Point(3, 30);
			this.textBox_Nombre_Cuenta.MaxLength = 25;
			this.textBox_Nombre_Cuenta.Name = "textBox_Nombre_Cuenta";
			this.textBox_Nombre_Cuenta.Size = new global::System.Drawing.Size(385, 25);
			this.textBox_Nombre_Cuenta.TabIndex = 2;
			this.boton_Agregar_Cuenta.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.boton_Agregar_Cuenta.Location = new global::System.Drawing.Point(3, 362);
			this.boton_Agregar_Cuenta.Name = "boton_Agregar_Cuenta";
			this.boton_Agregar_Cuenta.Size = new global::System.Drawing.Size(558, 45);
			this.boton_Agregar_Cuenta.TabIndex = 9;
			this.boton_Agregar_Cuenta.Text = "Ajouter le compte";
			this.boton_Agregar_Cuenta.UseVisualStyleBackColor = true;
			this.boton_Agregar_Cuenta.Click += new global::System.EventHandler(this.boton_Agregar_Cuenta_Click);
			this.tabPage1.BackColor = global::System.Drawing.Color.Gray;
			this.tabPage1.ImageIndex = 0;
			this.tabPage1.Location = new global::System.Drawing.Point(4, 32);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new global::System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new global::System.Drawing.Size(576, 525);
			this.tabPage1.TabIndex = 2;
			this.tabPage1.Text = "Ajouter plusieurs comptes";
			this.imagenesFormCuentas.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imagenesFormCuentas.ImageStream");
			this.imagenesFormCuentas.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imagenesFormCuentas.Images.SetKeyName(0, "agregar_cuenta.png");
			this.imagenesFormCuentas.Images.SetKeyName(1, "lista_cuentas.png");
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 17f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = global::System.Drawing.Color.Gray;
			base.ClientSize = new global::System.Drawing.Size(784, 561);
			base.Controls.Add(this.tabControlPrincipalCuentas);
			this.Font = new global::System.Drawing.Font("Segoe UI", 9.75f);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.MaximizeBox = false;
			this.MaximumSize = new global::System.Drawing.Size(800, 600);
			base.MinimizeBox = false;
			this.MinimumSize = new global::System.Drawing.Size(479, 437);
			base.Name = "GestionCuentas";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Gestion des comptes";
			base.Load += new global::System.EventHandler(this.GestionCuentas_Load);
			this.tabControlPrincipalCuentas.ResumeLayout(false);
			this.ListaCuentas.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox_informacion).EndInit();
			this.contextMenuStripFormCuentas.ResumeLayout(false);
			this.AgregarCuenta.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox_informacion_agregar_cuenta).EndInit();
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel6.ResumeLayout(false);
			this.tableLayoutPanel6.PerformLayout();
			this.tableLayoutPanel7.ResumeLayout(false);
			this.tableLayoutPanel7.PerformLayout();
			this.tableLayoutPanel8.ResumeLayout(false);
			this.tableLayoutPanel9.ResumeLayout(false);
			this.tableLayoutPanel9.PerformLayout();
			this.tableLayoutPanel10.ResumeLayout(false);
			this.tableLayoutPanel10.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000316 RID: 790
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000317 RID: 791
		private global::System.Windows.Forms.TabControl tabControlPrincipalCuentas;

		// Token: 0x04000318 RID: 792
		private global::System.Windows.Forms.TabPage ListaCuentas;

		// Token: 0x04000319 RID: 793
		private global::System.Windows.Forms.TabPage AgregarCuenta;

		// Token: 0x0400031A RID: 794
		private global::System.Windows.Forms.ImageList imagenesFormCuentas;

		// Token: 0x0400031B RID: 795
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStripFormCuentas;

		// Token: 0x0400031C RID: 796
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

		// Token: 0x0400031D RID: 797
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

		// Token: 0x0400031E RID: 798
		private global::System.Windows.Forms.PictureBox pictureBox_informacion;

		// Token: 0x0400031F RID: 799
		private global::System.Windows.Forms.Label label_informacionClickCuentas;

		// Token: 0x04000320 RID: 800
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;

		// Token: 0x04000321 RID: 801
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;

		// Token: 0x04000322 RID: 802
		private global::System.Windows.Forms.PictureBox pictureBox_informacion_agregar_cuenta;

		// Token: 0x04000323 RID: 803
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000324 RID: 804
		private global::System.Windows.Forms.CheckBox checkBox_Agregar_Retroceder;

		// Token: 0x04000325 RID: 805
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;

		// Token: 0x04000326 RID: 806
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;

		// Token: 0x04000327 RID: 807
		private global::System.Windows.Forms.Label label_Nombre_Cuenta;

		// Token: 0x04000328 RID: 808
		private global::System.Windows.Forms.Label label_Password;

		// Token: 0x04000329 RID: 809
		private global::System.Windows.Forms.Label label_Eleccion_Servidor;

		// Token: 0x0400032A RID: 810
		private global::System.Windows.Forms.Button boton_Agregar_Cuenta;

		// Token: 0x0400032B RID: 811
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;

		// Token: 0x0400032C RID: 812
		private global::System.Windows.Forms.Label label_Nombre_Personaje;

		// Token: 0x0400032D RID: 813
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;

		// Token: 0x0400032E RID: 814
		private global::System.Windows.Forms.ComboBox comboBox_Servidor;

		// Token: 0x0400032F RID: 815
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;

		// Token: 0x04000330 RID: 816
		private global::System.Windows.Forms.TextBox textBox_Password;

		// Token: 0x04000331 RID: 817
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;

		// Token: 0x04000332 RID: 818
		private global::System.Windows.Forms.TextBox textBox_Nombre_Cuenta;

		// Token: 0x04000333 RID: 819
		private global::System.Windows.Forms.ColumnHeader ColumnaNombreCuenta;

		// Token: 0x04000334 RID: 820
		private global::System.Windows.Forms.ColumnHeader ColumnaNombreServidor;

		// Token: 0x04000335 RID: 821
		private global::System.Windows.Forms.ColumnHeader ColumnaNombrePersonaje;

		// Token: 0x04000336 RID: 822
		private global::System.Windows.Forms.ToolStripMenuItem conectarToolStripMenuItem;

		// Token: 0x04000337 RID: 823
		private global::System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;

		// Token: 0x04000338 RID: 824
		private global::System.Windows.Forms.TabPage tabPage1;

		// Token: 0x04000339 RID: 825
		private global::System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;

		// Token: 0x0400033A RID: 826
		private global::System.Windows.Forms.TextBox textBox_nombre_personaje;

		// Token: 0x0400033B RID: 827
		private global::System.Windows.Forms.ToolStripMenuItem cuentaToolStripMenuItem;

		// Token: 0x0400033C RID: 828
		private global::System.Windows.Forms.ToolStripMenuItem contraseñaToolStripMenuItem;

		// Token: 0x0400033D RID: 829
		private global::System.Windows.Forms.ToolStripMenuItem nombreDelPersonajeToolStripMenuItem;

		// Token: 0x0400033E RID: 830
		private global::System.Windows.Forms.ListView listViewCuentas;
	}
}
