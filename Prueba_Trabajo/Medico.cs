using System;
using System.Collections;
namespace Prueba_Trabajo
{
	/// <summary>
	/// Description of Medico.
	/// </summary>
	public class Medico : Persona
	{
		private string oficio;
		private int matricula;

		public Medico(string nombre, int dni, string oficio, int matricula):base(nombre,dni)
		{
			this.oficio = oficio;
			this.matricula = matricula;
			
		}
		
		
		public string Oficio{
			set{oficio = value;}
			get{return oficio;}
		}
		
		public int Matricula{
			set{matricula = value;}
			get{return matricula;}
		}
		
	}
}
