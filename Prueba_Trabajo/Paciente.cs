using System;
using System.Collections;
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Text.RegularExpressions;  

namespace Prueba_Trabajo
{
	/// <summary>
	/// Description of Paciente.
	/// </summary>
	public class Paciente : Persona
	{
			
		private string obra_social;
		private long nro_afiliado;
		private string diagnostico;
		
		
		
		public Paciente(string nombre, int dni, string obra_social, long nro_afiliado, string diagnostico): base(nombre, dni)
		{
			this.obra_social = obra_social;
			this.nro_afiliado = nro_afiliado;
			this.diagnostico = diagnostico;
		}
		
		public string Obra_social
		{
			get{return obra_social;}
			
		}
		public long Nro_afiliado
		{
			get{return nro_afiliado;}
		
		}
		public string Diagnostico
		{
			get{return diagnostico;}
			
		}
	}
	
	public void validarNombre(string nombre){
		
			
	}

