using System;
using System.IO;

namespace Bot_Dofus_1._29._1.Utilidades.Configuracion
{
	// Token: 0x0200000B RID: 11
	public class CuentaConf
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002C00 File Offset: 0x00000E00
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002C08 File Offset: 0x00000E08
		public string nombre_cuenta { get; set; } = string.Empty;

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002C11 File Offset: 0x00000E11
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002C19 File Offset: 0x00000E19
		public string password { get; set; } = string.Empty;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002C22 File Offset: 0x00000E22
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002C2A File Offset: 0x00000E2A
		public string servidor { get; set; } = string.Empty;

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002C33 File Offset: 0x00000E33
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002C3B File Offset: 0x00000E3B
		public string nombre_personaje { get; set; } = string.Empty;

		// Token: 0x0600003F RID: 63 RVA: 0x00002C44 File Offset: 0x00000E44
		public CuentaConf(string _nombre_cuenta, string _password, string _servidor, string _nombre_personaje)
		{
			this.nombre_cuenta = _nombre_cuenta;
			this.password = _password;
			this.servidor = _servidor;
			this.nombre_personaje = _nombre_personaje;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public void guardar_Cuenta(BinaryWriter bw)
		{
			bw.Write(this.nombre_cuenta);
			bw.Write(this.password);
			bw.Write(this.servidor);
			bw.Write(this.nombre_personaje);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public static CuentaConf cargar_Una_Cuenta(BinaryReader br)
		{
			CuentaConf result;
			try
			{
				result = new CuentaConf(br.ReadString(), br.ReadString(), br.ReadString(), br.ReadString());
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002D18 File Offset: 0x00000F18
		public int get_Servidor_Id()
		{
			string servidor = this.servidor;
			if (servidor != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(servidor);
				if (num <= 703811073U)
				{
					if (num <= 19890187U)
					{
						if (num != 15800944U)
						{
							if (num == 19890187U)
							{
								if (servidor == "Eratz")
								{
									return 601;
								}
							}
						}
						else if (servidor == "Hogmeiser")
						{
							return 606;
						}
					}
					else if (num != 562859689U)
					{
						if (num != 624135955U)
						{
							if (num == 703811073U)
							{
								if (servidor == "Droupik")
								{
									return 607;
								}
							}
						}
						else if (servidor == "Ayuto")
						{
							return 608;
						}
					}
					else if (servidor == "Arty")
					{
						return 604;
					}
				}
				else if (num <= 3739496927U)
				{
					if (num != 1380765114U)
					{
						if (num != 3450482321U)
						{
							if (num == 3739496927U)
							{
								if (servidor == "Algathe")
								{
									return 605;
								}
							}
						}
						else if (servidor == "Nabur")
						{
							return 603;
						}
					}
					else if (servidor == "Henual")
					{
						return 602;
					}
				}
				else if (num != 3878991803U)
				{
					if (num != 4014150122U)
					{
						if (num == 4274870901U)
						{
							if (servidor == "Bilby")
							{
								return 609;
							}
						}
					}
					else if (servidor == "Clustus")
					{
						return 610;
					}
				}
				else if (servidor == "Issering")
				{
					return 611;
				}
			}
			return 601;
		}
	}
}
