using System;
using System.Collections;

namespace Prueba_Trabajo
{
	/// <summary>
	/// Description of Turno.
	/// </summary>
	public class Turno 
	{	
		private Paciente paciente;
		private string horario;
		
		public Turno(Paciente paciente, string horario)
		{
			this.paciente = paciente;
			this.horario = horario;
		}
		
		public string Horario{
			set{horario = value;}
			get{return horario;}
		}
		
		public Paciente Paciente{
			
			set{paciente = value;}
			get {return paciente;}
		
		}
		
		
		
		
	}
}
