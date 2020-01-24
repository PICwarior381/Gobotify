using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Manejadores
{
	// Token: 0x02000014 RID: 20
	public class LuaManejadorScript : IDisposable
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00005249 File Offset: 0x00003449
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00005251 File Offset: 0x00003451
		public Script script { get; private set; }

		// Token: 0x06000100 RID: 256 RVA: 0x0000525A File Offset: 0x0000345A
		public void cargar_Desde_Archivo(string ruta_archivo, Action funciones_Personalizadas)
		{
			this.script = new Script();
			funciones_Personalizadas();
			this.script.DoFile(ruta_archivo, null, null);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000527C File Offset: 0x0000347C
		public IEnumerable<Table> get_Entradas_Funciones(string nombre_funcion)
		{
			DynValue dynValue = this.script.Globals.Get(nombre_funcion);
			if (dynValue.IsNil() || dynValue.Type != DataType.Function)
			{
				return null;
			}
			DynValue dynValue2 = this.script.Call(dynValue);
			if (dynValue2.Type == DataType.Table)
			{
				return from f in dynValue2.Table.Values
				where f.Type == DataType.Table
				select f.Table;
			}
			return null;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000531C File Offset: 0x0000351C
		public T get_Global_Or<T>(string key, DataType tipo, T valor_or)
		{
			DynValue dynValue = this.script.Globals.Get(key);
			if (dynValue.IsNil() || dynValue.Type != tipo)
			{
				return valor_or;
			}
			T result;
			try
			{
				result = (T)((object)dynValue.ToObject(typeof(T)));
			}
			catch
			{
				result = valor_or;
			}
			return result;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005380 File Offset: 0x00003580
		public DynValue get_Global_como_Dyn_Valor(string key)
		{
			return this.script.Globals.Get(key);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005393 File Offset: 0x00003593
		public T get_Global_Or<T>(string key, T or)
		{
			if (!this.es_Global(key))
			{
				return or;
			}
			return (T)((object)this.script.Globals[key]);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000053B8 File Offset: 0x000035B8
		public T get_Global<T>(string key)
		{
			if (!this.es_Global(key))
			{
				return default(T);
			}
			return (T)((object)this.script.Globals[key]);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000053EE File Offset: 0x000035EE
		public bool es_Global(string key)
		{
			return this.script.Globals[key] != null;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005404 File Offset: 0x00003604
		public void Set_Global(string key, object value)
		{
			this.script.Globals[key] = value;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005418 File Offset: 0x00003618
		public static void inicializar_Funciones()
		{
			UserData.RegisterAssembly(null, false);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005424 File Offset: 0x00003624
		~LuaManejadorScript()
		{
			this.Dispose(false);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005454 File Offset: 0x00003654
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000545D File Offset: 0x0000365D
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				this.script = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000057 RID: 87
		private bool disposed;
	}
}
