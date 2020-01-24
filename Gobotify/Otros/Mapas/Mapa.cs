using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Movimientos;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;
using Bot_Dofus_1._29._1.Otros.Mapas.Interactivo;
using Bot_Dofus_1._29._1.Utilidades.Criptografia;
using Bot_Dofus_1._29._1.Utilidades.Extensiones;

namespace Bot_Dofus_1._29._1.Otros.Mapas
{
	// Token: 0x02000047 RID: 71
	public class Mapa : IEliminable, IDisposable
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00009F06 File Offset: 0x00008106
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00009F0E File Offset: 0x0000810E
		public int id { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00009F17 File Offset: 0x00008117
		// (set) Token: 0x06000289 RID: 649 RVA: 0x00009F1F File Offset: 0x0000811F
		public byte anchura { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00009F28 File Offset: 0x00008128
		// (set) Token: 0x0600028B RID: 651 RVA: 0x00009F30 File Offset: 0x00008130
		public byte altura { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00009F39 File Offset: 0x00008139
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00009F41 File Offset: 0x00008141
		public sbyte x { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00009F4A File Offset: 0x0000814A
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00009F52 File Offset: 0x00008152
		public sbyte y { get; set; }

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000290 RID: 656 RVA: 0x00009F5C File Offset: 0x0000815C
		// (remove) Token: 0x06000291 RID: 657 RVA: 0x00009F94 File Offset: 0x00008194
		public event Action mapa_actualizado;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000292 RID: 658 RVA: 0x00009FCC File Offset: 0x000081CC
		// (remove) Token: 0x06000293 RID: 659 RVA: 0x0000A004 File Offset: 0x00008204
		public event Action entidades_actualizadas;

		// Token: 0x06000294 RID: 660 RVA: 0x0000A039 File Offset: 0x00008239
		public Mapa()
		{
			this.entidades = new ConcurrentDictionary<int, Entidad>();
			this.interactivos = new ConcurrentDictionary<int, ObjetoInteractivo>();
			this.CellsTeleport = new Dictionary<MapaTeleportCeldas, List<short>>();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A064 File Offset: 0x00008264
		public void get_Actualizar_Mapa(string paquete)
		{
			try
			{
				this.entidades.Clear();
				this.interactivos.Clear();
				this.CellsTeleport.Clear();
				string[] array = paquete.Split(new char[]
				{
					'|'
				});
				this.id = int.Parse(array[0]);
				FileInfo fileInfo = new FileInfo("mapas/" + this.id.ToString() + ".xml");
				if (fileInfo.Exists)
				{
					XElement archivo_mapa = XElement.Load(fileInfo.FullName);
					this.anchura = byte.Parse(archivo_mapa.Element("ANCHURA").Value);
					this.altura = byte.Parse(archivo_mapa.Element("ALTURA").Value);
					this.x = sbyte.Parse(archivo_mapa.Element("X").Value);
					this.y = sbyte.Parse(archivo_mapa.Element("Y").Value);
					Task.Run(delegate()
					{
						this.descomprimir_mapa(archivo_mapa.Element("MAPA_DATA").Value);
					}).Wait();
					Task.Run(delegate()
					{
						this.getTeleportCell(this.celdas);
					}).Wait();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000A1E0 File Offset: 0x000083E0
		public string coordenadas
		{
			get
			{
				return string.Format("[{0},{1}]", this.x, this.y);
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000A202 File Offset: 0x00008402
		public Celda get_Celda_Id(short celda_id)
		{
			return this.celdas[(int)celda_id];
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000A20C File Offset: 0x0000840C
		public bool esta_En_Mapa(string _coordenadas)
		{
			return _coordenadas == this.id.ToString() || _coordenadas == this.coordenadas;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000A240 File Offset: 0x00008440
		public Celda get_Celda_Por_Coordenadas(int x, int y)
		{
			return this.celdas.FirstOrDefault((Celda celda) => celda.x == x && celda.y == y);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000A278 File Offset: 0x00008478
		public bool get_Puede_Luchar_Contra_Grupo_Monstruos(int monstruos_minimos, int monstruos_maximos, int nivel_minimo, int nivel_maximo, List<int> monstruos_prohibidos, List<int> monstruos_obligatorios)
		{
			return this.get_Grupo_Monstruos(monstruos_minimos, monstruos_maximos, nivel_minimo, nivel_maximo, monstruos_prohibidos, monstruos_obligatorios).Count > 0;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000A294 File Offset: 0x00008494
		public List<Celda> celdas_ocupadas()
		{
			return (from c in this.entidades.Values
			where c is Monstruos
			select c.celda).ToList<Celda>();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000A2FC File Offset: 0x000084FC
		public List<Npcs> lista_npcs()
		{
			return (from n in this.entidades.Values
			where n is Npcs
			select n as Npcs).ToList<Npcs>();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000A364 File Offset: 0x00008564
		public List<Monstruos> lista_monstruos()
		{
			return (from n in this.entidades.Values
			where n is Monstruos
			select n as Monstruos).ToList<Monstruos>();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000A3CC File Offset: 0x000085CC
		public List<Personajes> lista_personajes()
		{
			return (from n in this.entidades.Values
			where n is Personajes
			select n as Personajes).ToList<Personajes>();
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000A434 File Offset: 0x00008634
		public List<Monstruos> get_Grupo_Monstruos(int monstruos_minimos, int monstruos_maximos, int nivel_minimo, int nivel_maximo, List<int> monstruos_prohibidos, List<int> monstruos_obligatorios)
		{
			List<Monstruos> list = new List<Monstruos>();
			foreach (Monstruos monstruos in this.lista_monstruos())
			{
				if (monstruos.get_Total_Monstruos >= monstruos_minimos && monstruos.get_Total_Monstruos <= monstruos_maximos && monstruos.get_Total_Nivel_Grupo >= nivel_minimo && monstruos.get_Total_Nivel_Grupo <= nivel_maximo && monstruos.celda.tipo != TipoCelda.CELDA_TELEPORT)
				{
					bool flag = true;
					if (monstruos_prohibidos != null)
					{
						for (int i = 0; i < monstruos_prohibidos.Count; i++)
						{
							if (monstruos.get_Contiene_Monstruo(monstruos_prohibidos[i]))
							{
								flag = false;
								break;
							}
						}
					}
					if (monstruos_obligatorios != null && flag)
					{
						for (int j = 0; j < monstruos_obligatorios.Count; j++)
						{
							if (!monstruos.get_Contiene_Monstruo(monstruos_obligatorios[j]))
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						list.Add(monstruos);
					}
				}
			}
			return list;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A538 File Offset: 0x00008738
		public void getTeleportCell(Celda[] cells)
		{
			(from c in cells.ToList<Celda>()
			where c.es_Teleport()
			select c).ToList<Celda>().ForEach(delegate(Celda cell)
			{
				this.CellsTeleport.Add(cell.id);
			});
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000A585 File Offset: 0x00008785
		public bool haveTeleport()
		{
			return this.CellsTeleport.Count > 0;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000A598 File Offset: 0x00008798
		public string TransformToCellId(string cellDirection)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (cellDirection != null)
			{
				if (!(cellDirection == "RIGHT"))
				{
					if (!(cellDirection == "LEFT"))
					{
						if (!(cellDirection == "TOP"))
						{
							if (cellDirection == "BOTTOM")
							{
								stringBuilder.Append(this.CellsTeleport[MapaTeleportCeldas.BOTTOM].First<short>());
							}
						}
						else
						{
							stringBuilder.Append(this.CellsTeleport[MapaTeleportCeldas.TOP].First<short>());
						}
					}
					else
					{
						stringBuilder.Append(this.CellsTeleport[MapaTeleportCeldas.LEFT].First<short>());
					}
				}
				else
				{
					stringBuilder.Append(this.CellsTeleport[MapaTeleportCeldas.RIGHT].First<short>());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000A653 File Offset: 0x00008853
		public void get_Evento_Mapa_Cambiado()
		{
			Action action = this.mapa_actualizado;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A665 File Offset: 0x00008865
		public void evento_Entidad_Actualizada()
		{
			Action action = this.entidades_actualizadas;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000A678 File Offset: 0x00008878
		public void descomprimir_mapa(string mapa_data)
		{
			this.celdas = new Celda[mapa_data.Length / 10];
			for (int i = 0; i < mapa_data.Length; i += 10)
			{
				string celda_data = mapa_data.Substring(i, 10);
				this.celdas[i / 10] = this.descompimir_Celda(celda_data, Convert.ToInt16(i / 10));
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000A6D0 File Offset: 0x000088D0
		public Celda descompimir_Celda(string celda_data, short id_celda)
		{
			byte[] array = new byte[celda_data.Length];
			for (int i = 0; i < celda_data.Length; i++)
			{
				array[i] = Convert.ToByte(Hash.get_Hash(celda_data[i]));
			}
			TipoCelda tipo = (TipoCelda)((array[2] & 56) >> 3);
			bool esta_activa = (array[0] & 32) >> 5 != 0;
			bool es_linea_vision = (array[0] & 1) != 1;
			bool flag = (array[7] & 2) >> 1 != 0;
			short num = Convert.ToInt16(((int)(array[0] & 2) << 12) + ((int)(array[7] & 1) << 12) + ((int)array[8] << 6) + (int)array[9]);
			short layer_object_1_num = Convert.ToInt16(((int)(array[0] & 4) << 11) + ((int)(array[4] & 1) << 12) + ((int)array[5] << 6) + (int)array[6]);
			byte nivel = Convert.ToByte((int)(array[1] & 15));
			byte slope = Convert.ToByte((array[4] & 60) >> 2);
			return new Celda(id_celda, esta_activa, tipo, es_linea_vision, nivel, slope, flag ? num : Convert.ToInt16(-1), layer_object_1_num, num, this);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A7C2 File Offset: 0x000089C2
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000A7CC File Offset: 0x000089CC
		~Mapa()
		{
			this.Dispose(false);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000A7FC File Offset: 0x000089FC
		public void limpiar()
		{
			this.id = 0;
			this.x = 0;
			this.y = 0;
			this.entidades.Clear();
			this.interactivos.Clear();
			this.CellsTeleport.Clear();
			this.celdas = null;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000A83B File Offset: 0x00008A3B
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.entidades.Clear();
			this.interactivos.Clear();
			this.celdas = null;
			this.entidades = null;
			this.CellsTeleport = null;
			this.disposed = true;
		}

		// Token: 0x04000108 RID: 264
		public Celda[] celdas;

		// Token: 0x04000109 RID: 265
		public Dictionary<MapaTeleportCeldas, List<short>> CellsTeleport;

		// Token: 0x0400010A RID: 266
		public ConcurrentDictionary<int, Entidad> entidades;

		// Token: 0x0400010B RID: 267
		public ConcurrentDictionary<int, ObjetoInteractivo> interactivos;

		// Token: 0x0400010E RID: 270
		private bool disposed;
	}
}
