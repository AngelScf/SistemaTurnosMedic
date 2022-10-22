using System;
using System.Collections;

namespace Prueba_Trabajo
{
	/// <summary>
	/// Description of Medico.
	/// </summary>
	public class Medico
	{
		private ArrayList listaPacientes;
		private ArrayList turnosDisponibles;
		private ArrayList turnosOcupados;
		private ArrayList obrasSociales;
		
		
		public Medico()
		{
			
			 listaPacientes = listaPacientes = new ArrayList();
			 turnosDisponibles = new ArrayList(){"08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00"};
			 turnosOcupados = new ArrayList();
			 obrasSociales  = new ArrayList();
			
		}
		
		public ArrayList verTodosPacientes (){
			return listaPacientes;
		}
		
		public void agregarPaciente(Paciente paciente){
			listaPacientes.Add(paciente);
		}
		
		public void agregarObraSocial(string obraSocial){
			obrasSociales.Add(obraSocial);
		}
		
		public void eliminarPaciente (Paciente paciente){
			listaPacientes.Remove(paciente);
		}
		
		public Paciente verUnPaciente(Paciente paciente){
			return paciente;
		}
		
		public void actualizarPaciente(Paciente paciente, string diag){
			paciente.Diagnostico = diag;
		}
		
		public ArrayList verObrasSociales (){
			return obrasSociales;
		}
		
		
		public ArrayList verTurnosDisponibles(){
			return turnosDisponibles;
		}
		
		public void agregarTurno(Turno turno, string horario){
			
			turnosOcupados.Add(turno);
			turnosDisponibles.Remove(horario);
		}
		
		public void eliminarTurno (Turno turno, string horario){
			turnosDisponibles.Add(horario);
			turnosOcupados.Remove(turno);
		}
		
		
		public ArrayList verTurnosOcupados(){
			return turnosOcupados;
		}
	}
}
