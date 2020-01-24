using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Otros;

namespace Bot_Dofus_1._29._1.Comun.Network
{
	// Token: 0x0200008C RID: 140
	public class ClienteTcp : IDisposable
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x000231B8 File Offset: 0x000213B8
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x000231C0 File Offset: 0x000213C0
		private Socket socket { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x000231C9 File Offset: 0x000213C9
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x000231D1 File Offset: 0x000213D1
		private byte[] buffer { get; set; }

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060005BE RID: 1470 RVA: 0x000231DC File Offset: 0x000213DC
		// (remove) Token: 0x060005BF RID: 1471 RVA: 0x00023214 File Offset: 0x00021414
		public event Action<string> paquete_recibido;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060005C0 RID: 1472 RVA: 0x0002324C File Offset: 0x0002144C
		// (remove) Token: 0x060005C1 RID: 1473 RVA: 0x00023284 File Offset: 0x00021484
		public event Action<string> paquete_enviado;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060005C2 RID: 1474 RVA: 0x000232BC File Offset: 0x000214BC
		// (remove) Token: 0x060005C3 RID: 1475 RVA: 0x000232F4 File Offset: 0x000214F4
		public event Action<string> socket_informacion;

		// Token: 0x060005C4 RID: 1476 RVA: 0x00023329 File Offset: 0x00021529
		public ClienteTcp(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00023338 File Offset: 0x00021538
		public void conexion_Servidor(IPAddress ip, int puerto)
		{
			try
			{
				this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				this.buffer = new byte[this.socket.ReceiveBufferSize];
				this.semaforo = new SemaphoreSlim(1);
				this.pings = new List<int>(50);
				this.socket.BeginConnect(ip, puerto, new AsyncCallback(this.conectar_CallBack), this.socket);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				Action<string> action = this.socket_informacion;
				if (action != null)
				{
					action(ex.ToString());
				}
				this.get_Desconectar_Socket();
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000233DC File Offset: 0x000215DC
		private void conectar_CallBack(IAsyncResult ar)
		{
			try
			{
				if (this.esta_Conectado())
				{
					this.socket = (ar.AsyncState as Socket);
					this.socket.EndConnect(ar);
					this.socket.BeginReceive(this.buffer, 0, this.buffer.Length, SocketFlags.None, new AsyncCallback(this.recibir_CallBack), this.socket);
					Action<string> action = this.socket_informacion;
					if (action != null)
					{
						action("Socket connectée correctement");
					}
				}
				else
				{
					this.get_Desconectar_Socket();
					Action<string> action2 = this.socket_informacion;
					if (action2 != null)
					{
						action2("Impossible de joindre le serveur hôte");
					}
				}
			}
			catch (Exception ex)
			{
				Action<string> action3 = this.socket_informacion;
				if (action3 != null)
				{
					action3(ex.ToString());
				}
				this.get_Desconectar_Socket();
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000234A4 File Offset: 0x000216A4
		public void recibir_CallBack(IAsyncResult ar)
		{
			try
			{
				if (!this.esta_Conectado() || this.disposed)
				{
					this.get_Desconectar_Socket();
				}
				else
				{
					SocketError socketError;
					int num = this.socket.EndReceive(ar, out socketError);
					if (num > 0 && socketError == SocketError.Success)
					{
						foreach (string text in from x in Encoding.UTF8.GetString(this.buffer, 0, num).Replace("\n", string.Empty).Split(new char[1])
						where x != string.Empty
						select x)
						{
							Action<string> action = this.paquete_recibido;
							if (action != null)
							{
								action(text);
							}
							if (this.esta_esperando_paquete)
							{
								this.pings.Add(Environment.TickCount - this.ticks);
								if (this.pings.Count > 48)
								{
									this.pings.RemoveAt(1);
								}
								this.esta_esperando_paquete = false;
							}
							PaqueteRecibido.Recibir(this, text);
						}
						this.socket.BeginReceive(this.buffer, 0, this.buffer.Length, SocketFlags.None, new AsyncCallback(this.recibir_CallBack), this.socket);
					}
					else
					{
						this.cuenta.desconectar();
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0002362C File Offset: 0x0002182C
		public async Task enviar_Paquete_Async(string paquete, bool necesita_respuesta)
		{
			try
			{
				if (this.esta_Conectado())
				{
					paquete += "\n\0";
					byte[] byte_paquete = Encoding.UTF8.GetBytes(paquete);
					await this.semaforo.WaitAsync().ConfigureAwait(false);
					if (necesita_respuesta)
					{
						this.esta_esperando_paquete = true;
					}
					this.socket.Send(byte_paquete);
					if (necesita_respuesta)
					{
						this.ticks = Environment.TickCount;
					}
					Action<string> action = this.paquete_enviado;
					if (action != null)
					{
						action(paquete);
					}
					this.semaforo.Release();
					byte_paquete = null;
				}
			}
			catch (Exception ex)
			{
				Action<string> action2 = this.socket_informacion;
				if (action2 != null)
				{
					action2(ex.ToString());
				}
				this.get_Desconectar_Socket();
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00023681 File Offset: 0x00021881
		public void enviar_Paquete(string paquete, bool necesita_respuesta = false)
		{
			this.enviar_Paquete_Async(paquete, necesita_respuesta).Wait();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00023690 File Offset: 0x00021890
		public void get_Desconectar_Socket()
		{
			if (this.esta_Conectado())
			{
				if (this.socket != null && this.socket.Connected)
				{
					this.socket.Shutdown(SocketShutdown.Both);
					this.socket.Disconnect(false);
					this.socket.Close();
				}
				Action<string> action = this.socket_informacion;
				if (action == null)
				{
					return;
				}
				action("Socket deconnecté de l'hôte");
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000236F4 File Offset: 0x000218F4
		public bool esta_Conectado()
		{
			bool result;
			try
			{
				result = (!this.disposed && this.socket != null && (this.socket.Connected || this.socket.Available != 0));
			}
			catch (SocketException)
			{
				result = false;
			}
			catch (ObjectDisposedException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0002375C File Offset: 0x0002195C
		public int get_Total_Pings()
		{
			return this.pings.Count<int>();
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00023769 File Offset: 0x00021969
		public int get_Promedio_Pings()
		{
			return (int)this.pings.Average();
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00023777 File Offset: 0x00021977
		public int get_Actual_Ping()
		{
			return Environment.TickCount - this.ticks;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00023788 File Offset: 0x00021988
		~ClienteTcp()
		{
			this.Dispose(false);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000237B8 File Offset: 0x000219B8
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000237C4 File Offset: 0x000219C4
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (this.socket != null && this.socket.Connected)
				{
					this.socket.Shutdown(SocketShutdown.Both);
					this.socket.Disconnect(false);
					this.socket.Close();
				}
				if (disposing)
				{
					this.socket.Dispose();
					this.semaforo.Dispose();
				}
				this.semaforo = null;
				this.cuenta = null;
				this.socket = null;
				this.buffer = null;
				this.paquete_recibido = null;
				this.paquete_enviado = null;
				this.disposed = true;
			}
		}

		// Token: 0x040003A5 RID: 933
		public Cuenta cuenta;

		// Token: 0x040003A6 RID: 934
		private SemaphoreSlim semaforo;

		// Token: 0x040003A7 RID: 935
		private bool disposed;

		// Token: 0x040003AB RID: 939
		private bool esta_esperando_paquete;

		// Token: 0x040003AC RID: 940
		private int ticks;

		// Token: 0x040003AD RID: 941
		private List<int> pings;
	}
}
