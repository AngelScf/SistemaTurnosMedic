using System;
using System.Collections;
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Text.RegularExpressions;  

namespace Prueba_Trabajo
{
	class Program
	{
		public static void Main(string[] args)
		{
			//Declaramos las listas que vamos a utilizar durante todo el transcurso de la aplicacion
			//Para simular una base de datos
			
			ArrayList listaPacientes = new ArrayList();
			ArrayList turnosDisponibles = new ArrayList(){"08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00"};
			ArrayList turnosOcupados = new ArrayList();
			ArrayList obrasSociales  = new ArrayList();
			
			Console.WriteLine("************************************************************" +
			                 "\n*********BIENVENIDO AL SISTEMA DE GESTION DE TURNOS*********" + 
			                "\n************************************************************");
			
			Menu(listaPacientes,turnosDisponibles,turnosOcupados,obrasSociales);
			Console.ReadKey(true);
		}
	
		public static void Menu(ArrayList listaPacientes, ArrayList turnosDisponibles, ArrayList turnosOcupados, ArrayList obrasSociales){
			
			MostrarOpciones();
			
			try{ 
				//El menu de opciones esta dentro de un try para atrapar cualquier excepcion que pueda incurrir, en este caso la unica
				//excepcion posible es en caso de que una opcion sea invalida
				
				int opcion = int.Parse(Console.ReadLine());
				
				while (opcion != 0) {
				
				if (opcion == 1) {
					
					VerTodosPacientes(listaPacientes);
					MostrarOpciones();
			
					opcion = int.Parse(Console.ReadLine());
				}
				else if (opcion == 2) {
					
					
					CrearPaciente(listaPacientes,obrasSociales);
					MostrarOpciones();
					opcion = int.Parse(Console.ReadLine());
					
					
				}
				else if (opcion == 3) {
					
					if (listaPacientes.Count > 0) {
						Console.WriteLine("\nIngrese dni del paciente que desea eliminar.\n");
					    int dni = int.Parse(Console.ReadLine());
					    BorrarPaciente(listaPacientes, dni);
				    	
					}
					else{
						Console.WriteLine("\nActualmente no hay pacientes, por favor ingrese uno.\n");
					}
					MostrarOpciones();
				    opcion = int.Parse(Console.ReadLine());
					
					
				}
				else if (opcion == 4) {
					if (listaPacientes.Count > 0) { //La lista de pacientes debe ser mayor a 0 para buscar un paciente
						Console.WriteLine("\nIngrese dni del paciente que desea buscar:\n");
					    int dni = int.Parse(Console.ReadLine());
					    VerPaciente(listaPacientes,dni);
					}
					else{
						Console.WriteLine("\nActualmente no hay pacientes, por favor ingrese uno.\n");
					}
					MostrarOpciones();
					opcion = int.Parse(Console.ReadLine());
				}
				
				
				
				else if (opcion == 5) {
					
					if (listaPacientes.Count > 0) {
						Console.WriteLine("\nIngrese dni del paciente que desea actualizar diagnostico.\n");
					    int dni = int.Parse(Console.ReadLine());
					    ActualizarDiagnostico(listaPacientes,dni);
				    	
					}
					else{
						Console.WriteLine("\nActualmente no hay pacientes, por favor ingrese uno.\n");
					}
					MostrarOpciones();
					opcion = int.Parse(Console.ReadLine());
					
					
					
				}
				else if (opcion == 6) {
					
					TurnoDisponible(turnosDisponibles);
					MostrarOpciones();
					opcion = int.Parse(Console.ReadLine());
					
				}
				else if (opcion == 7) {
					
					
						if (listaPacientes.Count > 0) {
							Console.WriteLine("\nEl paciente se encuentra en el sistema? Ingrese si/no: \n");
							string dato = Console.ReadLine();
							if (dato =="si" ) {
								Console.WriteLine("\nIngrese dni del paciente al que desea agregarle un turno.\n");
						    	int dni = int.Parse(Console.ReadLine());
						    	BuscarPaciente(listaPacientes, dni, turnosDisponibles, turnosOcupados);
							}
							else{
								Console.WriteLine("\nPor favor ingrese un paciente.\n");
							}
						}
						else{
							Console.WriteLine("\nActualmente no hay pacientes, por favor ingrese uno.\n");
						}
						MostrarOpciones();
						opcion = int.Parse(Console.ReadLine());
				}
				
				else if (opcion == 8) {
					
					if (turnosOcupados.Count > 0) {
						Console.WriteLine("\nIngrese el horario del turno a eliminar: \n");
						string horario = Console.ReadLine();
						ElimiarTurno(turnosDisponibles,turnosOcupados,horario);
					}
					else{
						Console.WriteLine("\nActualmente no hay turnos ocupados, por favor ingrese un turno.\n");
					}
					MostrarOpciones();
					opcion = int.Parse(Console.ReadLine());
					
				}
				else if (opcion == 9) {
					VerTurnosOcupados(turnosOcupados);
					MostrarOpciones();
					opcion = int.Parse(Console.ReadLine());
				}
				else if (opcion == 10) {
					VerObrasSociales(obrasSociales);
					MostrarOpciones();
					opcion = int.Parse(Console.ReadLine());
				}
				else{
					Console.WriteLine("Opcion incorrecta, ingrese una opcion valida.");
					MostrarOpciones();
					opcion = int.Parse(Console.ReadLine());
				}
			}
			}catch(Exception){
				Console.WriteLine("La opcion ingresada no es valida, por favor intente nuevamente: ");
				Menu(listaPacientes,turnosDisponibles,turnosOcupados,obrasSociales);
			}
				
}
			
		public static void CrearPaciente(ArrayList listaPacientes, ArrayList obraSociales){
			
			try{
				
				Console.WriteLine("\n-Ingrese nombre del paciente:\n");
				
				string nombre = Console.ReadLine().ToUpper();
				//El metodo Match corrobora si el nombre ingresado respeta el patron establecido en la regex
				//Luego indicamos que nos devuelva un booleano con Success
				
				if(!Regex.Match(nombre, @"^([a-zA-Z]{2,}\s?[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\s?([a-zA-Z]{1,})?)").Success){
					throw new NombreInvalidoException();
				}
				
				Console.WriteLine("\n-Ingrese dni del paciente:\n");
				
				int dni = int.Parse(Console.ReadLine());
				
				foreach (Paciente paciente in listaPacientes) {
					
					if (dni == paciente.Dni) {
						Console.WriteLine("\nEl dni " + dni + " ya le corresponde a un paciente que esta en el sistema.\n" );
						return;
					}
				}
				
				Console.WriteLine("\n-Tiene obra social?:	      (Ingrese si/no)\n");
				
				string condicion = Console.ReadLine().ToUpper();
				
				if (condicion == "SI") {
				
					Console.WriteLine("\n-Ingrese la obra social del paciente:\n");
					string obra_social = Console.ReadLine().ToUpper();
					Console.WriteLine("\n-Ingrese el numero de afiliado del paciente:\n");
					long nro_afiliado = long.Parse(Console.ReadLine());
					Console.WriteLine("\n-Ingrese el diagnostico del paciente:\n");
					string diagnostico = Console.ReadLine().ToUpper();
					
					Paciente paciente = new Paciente(nombre, dni, obra_social, nro_afiliado, diagnostico);
					listaPacientes.Add(paciente);										//Agregar paciente a la lista de pacientes
					
					if (!(obraSociales.Contains(paciente.Obra_social))) {				//Agregar obra social del paciente a la lista
						
						obraSociales.Add(paciente.Obra_social);
					}
					
				Console.WriteLine("\n***************¡Paciente agregado con exito!****************\n");		
			}
				else{
					string obra_social = "NO TIENE/PARTICULAR";
					int nro_afiliado = 00;
					Console.WriteLine("\n-Ingrese el diagnostico del paciente:\n");
					string diagnostico = Console.ReadLine().ToUpper();
					Paciente paciente = new Paciente(nombre, dni, obra_social, nro_afiliado,diagnostico);
			    	listaPacientes.Add(paciente);										//Agregar paciente a la lista de pacientes

			    	Console.WriteLine("\n***************¡Paciente agregado con exito!****************\n");
			
			}
			}catch(FormatException){
				Console.WriteLine("Formato de dato ingresado no valido");
				CrearPaciente(listaPacientes,obraSociales);
			}catch(NombreInvalidoException){
				Console.WriteLine("El nombre ingresado no es valido, por favor intente nuevamente.");
			}
			
			
			
		}
		
		public static void BorrarPaciente(ArrayList listaPacientes ,int dniBuscar){
			
																		//Eliminar paciente de la lista de pacientes
			  									                         // usando su dni como referencia
			foreach (Paciente x  in listaPacientes ) {
				if (dniBuscar == x.Dni) {
				    listaPacientes.RemoveAt(listaPacientes.IndexOf(x));
					Console.WriteLine("\nPaciente "+ x.Nombre+" eliminado con exito!.\n");
					return;
		        }
																		
			}
			
			Console.WriteLine("\nNo se encontro ningun paciente con el dni ingresado.\n");											
						  									                         
		}
		
		public static void VerPaciente(ArrayList listaPacientes, int dniBuscar){	//Busca al paciente desde la lista a traves de su dni	
			
			foreach (Paciente x  in listaPacientes ) {
				if (dniBuscar == x.Dni) {	
					Console.WriteLine("\nSe ha encontrado al paciente: " + "\nNombre: "+ x.Nombre + "\nDni: "+ x.Dni + "\nObra Social: "+ x.Obra_social +
					                  "\nNumero de Afiliado: "+ x.Nro_afiliado + "\nDiagnostico: "+x.Diagnostico+"\n");
					break;
				}
				else{
					Console.WriteLine("\nNo se ha encontrado ningun paciente con el DNI ingresado.");
				}
			}
		}
		
		public static void VerTodosPacientes(ArrayList listaPacientes){				//Ver todos los pacientes
		
			if (listaPacientes.Count > 0) {
				foreach (Paciente x  in listaPacientes ) {
					
				Console.WriteLine("\nNombre: "+ x.Nombre + "\nDni: "+ x.Dni + "\nObra Social: "+ x.Obra_social +
					                  "\nNumero de Afiliado: "+ x.Nro_afiliado + "\nDiagnostico: "+x.Diagnostico+"\n");
			    }
			}
			else{
				Console.WriteLine("\nActualmente no hay pacientes, por favor ingrese uno.\n");		
			}	
		}
		
		public static void VerObrasSociales(ArrayList obrasSociales){		 //Ver todas las obras sociales con las que 
																			// trabaja el medico
			
			if (obrasSociales.Count == 0) {
				Console.WriteLine("\nActualmente no se trabaja con ninguna obra social.\n");
			}																
			else{
				Console.WriteLine("\nLas Obras Sociales con las que trabaja el medico son: \n");																	
				for (int i = 0; i < obrasSociales.Count; i++) {
				Console.WriteLine(obrasSociales[i]);
				}		
			}
		}
		
		public static void ActualizarDiagnostico(ArrayList listaPacientes,int dniBuscar){//Cambiar el diagnostico de un paciente
			
			foreach (Paciente x in listaPacientes) {
				if (x.Dni == dniBuscar) {
					
					Console.WriteLine("\nIngrese el nuevo Diagnostico del paciente: \n");
					string diagnostico = Console.ReadLine().ToUpper();
					x.Diagnostico = diagnostico;
					Console.WriteLine("\n************¡Diagnostico actualizado con exito!*************\n");
				}
				else{
					Console.WriteLine("\nNo se ha encontrado ningun paciente con el DNI ingresado.");
				}
			}
		}
		
		public static void TurnoDisponible(ArrayList turnosDisponibles){				// Ver los turnos que hay disponibles
			if (turnosDisponibles.Count > 0) {
				for (int i = 0; i < turnosDisponibles.Count; i++) {

					Console.WriteLine("\nTurno disponible: " + turnosDisponibles[i]);
				}
			}
		}
		
		public static void BuscarPaciente(ArrayList listaPacientes, int dniBuscar, ArrayList turnosDisponibles, ArrayList turnosOcupados){
			
			foreach (Paciente x in listaPacientes) {
				
				if (x.Dni == dniBuscar) {
					
					Console.WriteLine("\nSe encontro el siguiente paciente: "+ x.Nombre);
					Console.WriteLine("\nIngrese la hora en la que solicita el turno: \n");
					string hora = Console.ReadLine();
					AgregarTurno(x, hora, turnosDisponibles, turnosOcupados);
					return;
				}
			}
			
			Console.WriteLine("\nNo se encontro ningun paciente con el dni ingresado, por favor ingresar uno.");
					
				
		}
		
		public static void AgregarTurno(Paciente paciente,string hora, ArrayList turnosDisponibles, ArrayList turnosOcupados){	// Se comprueba si el horario pedido esta disponible. Caso true,se elimina de la lista
				        							  				// de disponibles y añade a lista de ocupados	        							  	
				
				if (!(turnosDisponibles.Contains(hora)) && !(turnosOcupados.Contains(hora)) ) {
				 	
				   	Console.WriteLine("\nPor favor ingrese un horario que este disponible.\n");						  					
				 }
				        							  				
				else if (turnosDisponibles.Contains(hora) ) {
					Turno turno = new Turno(paciente, hora);		
					turnosOcupados.Add(turno);
					turnosDisponibles.RemoveAt(turnosDisponibles.IndexOf(hora));
					Console.WriteLine("\n*********Turno de las "+ hora + " agregado exitosamente!**********");
				}
				        							  				
			else if ((turnosDisponibles.Count == 0) && (turnosOcupados.Count == 9)){ //Si listaDisponible esta vacia y  ListaOcupada completa es 
						                                                             // que ya no hay turnos disponibles
				Console.WriteLine("\nHorarios no disponibles, llamar próximo día de atencion");	
			}
		}
		
		public static void ElimiarTurno(ArrayList turnosDisponibles,ArrayList turnosOcupados, string horario){
																				//Eliminar un turno que esta ocupado
																				//Una vez eliminado vuelve a estar
			foreach (Turno x in turnosOcupados) {								//disponible
				
				if(x.Horario.Contains(horario)){
					
					turnosOcupados.RemoveAt(turnosOcupados.IndexOf(x));
					turnosDisponibles.Add(horario);
					Console.WriteLine("\n**************Turno de las "+ horario +" eliminado!***************");
					break;
				}
				else{
					Console.WriteLine("\nEl turno de las " + horario + " no puede ser eliminado porque esta disponible.");					
					break;
				}
			}
		}
		
		public static void VerTurnosOcupados(ArrayList turnosOcupados){				//Ver todos los turnos que estan
																					//ocupados
			if (turnosOcupados.Count > 0) {
				foreach (Turno x in turnosOcupados) {
			
					for (int i = turnosOcupados.Count; i <= turnosOcupados.Count; i++) {
					
						Console.WriteLine("\nEl turno de las "+ x.Horario + " esta ocupado por "+ x.Paciente.Nombre);
					}
				}
			}
			else{
				Console.WriteLine("\nActualmente no hay turnos ocupados, por favor ingrese un turno.\n");
			}
		
		}
		
		public static void MostrarOpciones(){
			
			Console.WriteLine("------------------------------------------------------------" +
			                  "\n-Opcion 1 para ver lista de pacientes.\n-Opcion 2 para agregar paciente." +
			                  "\n-Opcion 3 para eliminar paciente.\n-Opcion 4 para ver un paciente." +
			                  "\n-Opcion 5 para actualizar diagnostico de un paciente." +
			                  "\n-Opcion 6 para ver turnos disponibles.\n-Opcion 7 para agregar un turno." +
			                  "\n-Opcion 8 para eliminar un turno.\n-Opcion 9 para ver todos los turnos ocupados." +
			                  "\n-Opcion 10 para ver las obras sociales que cubre el medico.\n-Opcion 0 para salir\n" +
			                 "------------------------------------------------------------");		}
		
	}
}