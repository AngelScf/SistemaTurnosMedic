using System;

namespace Prueba_Trabajo
{
	/// <summary>
	/// Description of Paciente.
	/// </summary>
	public class Paciente : Persona
	{
			
		private string obra_social;
		private int nro_afiliado;
		private string diagnostico;
		
		
		
		public Paciente(string nombre, int dni, string obra_social, int nro_afiliado, string diagnostico): base(nombre, dni)
		{
			this.obra_social = obra_social;
			this.nro_afiliado = nro_afiliado;
			this.diagnostico = diagnostico;
		}
		
		public string Obra_social
		{
			set{obra_social = value;}
			get{return obra_social;}
			
		}
		public int Nro_afiliado
		{
			set{nro_afiliado = value;}
			get{return nro_afiliado;}
		
		}
		public string Diagnostico
		{
			set{diagnostico = value;}
			get{return diagnostico;}
			
		}
	}
			
	}

