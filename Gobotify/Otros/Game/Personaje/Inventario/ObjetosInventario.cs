using System;
using System.IO;
using System.Xml.Linq;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Inventario.Enums;

namespace Bot_Dofus_1._29._1.Otros.Game.Personaje.Inventario
{
	// Token: 0x0200005F RID: 95
	public class ObjetosInventario
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		public uint id_inventario { get; private set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000DEB9 File Offset: 0x0000C0B9
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x0000DEC1 File Offset: 0x0000C0C1
		public int id_modelo { get; private set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000DECA File Offset: 0x0000C0CA
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x0000DED2 File Offset: 0x0000C0D2
		public string nombre { get; private set; } = "Inconnu";

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000DEDB File Offset: 0x0000C0DB
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x0000DEE3 File Offset: 0x0000C0E3
		public int cantidad { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000DEEC File Offset: 0x0000C0EC
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		public InventarioPosiciones posicion { get; set; } = InventarioPosiciones.NON_EQUIPEE;

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000DEFD File Offset: 0x0000C0FD
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0000DF05 File Offset: 0x0000C105
		public short pods { get; private set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000DF0E File Offset: 0x0000C10E
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x0000DF16 File Offset: 0x0000C116
		public short nivel { get; private set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000DF1F File Offset: 0x0000C11F
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x0000DF27 File Offset: 0x0000C127
		public byte tipo { get; private set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000DF30 File Offset: 0x0000C130
		public short vida_regenerada { get; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000DF38 File Offset: 0x0000C138
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0000DF40 File Offset: 0x0000C140
		public TipoObjetosInventario tipo_inventario { get; private set; } = TipoObjetosInventario.INCONNU;

		// Token: 0x06000424 RID: 1060 RVA: 0x0000DF4C File Offset: 0x0000C14C
		public ObjetosInventario(string paquete)
		{
			string[] array = paquete.Split(new char[]
			{
				'~'
			});
			if (string.IsNullOrEmpty(array[2]))
			{
				return;
			}
			this.id_inventario = Convert.ToUInt32(array[0], 16);
			this.id_modelo = Convert.ToInt32(array[1], 16);
			this.cantidad = Convert.ToInt32(array[2], 16);
			if (!string.IsNullOrEmpty(array[3]))
			{
				this.posicion = (InventarioPosiciones)Convert.ToSByte(array[3], 16);
			}
			string[] array2 = array[4].Split(new char[]
			{
				','
			});
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(new char[]
				{
					'#'
				});
				string value = array3[0];
				if (!string.IsNullOrEmpty(value) && Convert.ToInt32(value, 16) == 110)
				{
					this.vida_regenerada = Convert.ToInt16(array3[1], 16);
				}
			}
			FileInfo fileInfo = new FileInfo("items/" + this.id_modelo.ToString() + ".xml");
			if (fileInfo.Exists)
			{
				this.archivo_objeto = XElement.Load(fileInfo.FullName);
				this.nombre = this.archivo_objeto.Element("NOMBRE").Value;
				this.pods = short.Parse(this.archivo_objeto.Element("PODS").Value);
				this.tipo = byte.Parse(this.archivo_objeto.Element("TIPO").Value);
				this.nivel = short.Parse(this.archivo_objeto.Element("NIVEL").Value);
				this.tipo_inventario = InventarioUtiles.get_Objetos_Inventario(this.tipo);
				this.archivo_objeto = null;
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000E129 File Offset: 0x0000C329
		public bool objeto_esta_equipado()
		{
			return this.posicion > InventarioPosiciones.NON_EQUIPEE;
		}

		// Token: 0x040001AC RID: 428
		private readonly XElement archivo_objeto;
	}
}
