using System;

namespace Prueba_Trabajo
{
	/// <summary>
	/// Description of Persona.
	/// </summary>
	public class Persona
	{
		protected string nombre;
		protected int dni;
		
		
		public Persona(string nombre, int dni)
		{
			this.nombre = nombre;
			this.dni = dni;
		}
		
		
		public string Nombre{
		
			set{nombre = value;}
			get{return nombre;}
		}
		
		
		
		public int Dni{
		
			set{dni = value;}
			get{return dni;}
		}
	}
}
