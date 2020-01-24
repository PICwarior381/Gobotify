using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Forms;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Hechizos;
using Bot_Dofus_1._29._1.Otros.Mapas.Interactivo;
using Bot_Dofus_1._29._1.Otros.Scripts.Manejadores;
using Bot_Dofus_1._29._1.Properties;
using Bot_Dofus_1._29._1.Utilidades.Configuracion;

namespace Bot_Dofus_1._29._1
{
	// Token: 0x02000002 RID: 2
	internal static class Program
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			AppDomain.CurrentDomain.UnhandledException += Program.MyHandler;
			Task.Run(delegate()
			{
				GlobalConf.cargar_Todas_Cuentas();
				LuaManejadorScript.inicializar_Funciones();
				XElement.Parse(Resources.interactivos).Descendants("SKILL").ToList<XElement>().ForEach(delegate(XElement i)
				{
					new ObjetoInteractivoModelo(i.Element("nombre").Value, i.Element("gfx").Value, bool.Parse(i.Element("caminable").Value), i.Element("habilidades").Value, bool.Parse(i.Element("recolectable").Value));
				});
				PaqueteRecibido.Inicializar();
			}).ContinueWith(delegate(Task t)
			{
				XElement.Parse(Resources.hechizos).Descendants("HECHIZO").ToList<XElement>().ForEach(delegate(XElement mapa)
				{
					Hechizo hechizo = new Hechizo(short.Parse(mapa.Attribute("ID").Value), mapa.Element("NOMBRE").Value);
					mapa.Descendants("NIVEL").ToList<XElement>().ForEach(delegate(XElement stats)
					{
						HechizoStats hechizo_stats = new HechizoStats();
						hechizo_stats.coste_pa = byte.Parse(stats.Attribute("COSTE_PA").Value);
						hechizo_stats.alcanze_minimo = byte.Parse(stats.Attribute("RANGO_MINIMO").Value);
						hechizo_stats.alcanze_maximo = byte.Parse(stats.Attribute("RANGO_MAXIMO").Value);
						hechizo_stats.es_lanzado_linea = bool.Parse(stats.Attribute("LANZ_EN_LINEA").Value);
						hechizo_stats.es_lanzado_con_vision = bool.Parse(stats.Attribute("NECESITA_VISION").Value);
						hechizo_stats.es_celda_vacia = bool.Parse(stats.Attribute("NECESITA_CELDA_LIBRE").Value);
						hechizo_stats.es_alcanze_modificable = bool.Parse(stats.Attribute("RANGO_MODIFICABLE").Value);
						hechizo_stats.lanzamientos_por_turno = byte.Parse(stats.Attribute("MAX_LANZ_POR_TURNO").Value);
						hechizo_stats.lanzamientos_por_objetivo = byte.Parse(stats.Attribute("MAX_LANZ_POR_OBJETIVO").Value);
						hechizo_stats.intervalo = byte.Parse(stats.Attribute("COOLDOWN").Value);
						stats.Descendants("EFECTO").ToList<XElement>().ForEach(delegate(XElement efecto)
						{
							hechizo_stats.agregar_efecto(new HechizoEfecto(int.Parse(efecto.Attribute("TIPO").Value), Zonas.Parse(efecto.Attribute("ZONA").Value)), bool.Parse(efecto.Attribute("ES_CRITICO").Value));
						});
						hechizo.get_Agregar_Hechizo_Stats(byte.Parse(stats.Attribute("NIVEL").Value), hechizo_stats);
					});
				});
			}).Wait();
			try
			{
				new Principal().Show();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F0 File Offset: 0x000002F0
		private static void MyHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Exception ex = (Exception)args.ExceptionObject;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ERROR] :" + ex.Message + "\n");
			stringBuilder.Append("[ERROR] :" + ex.StackTrace);
			File.AppendAllText(Path.GetDirectoryName(Application.ExecutablePath) + "\\log.txt", stringBuilder.ToString());
			stringBuilder.Clear();
		}
	}
}
