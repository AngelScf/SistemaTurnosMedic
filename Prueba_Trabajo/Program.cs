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
			
			Console.WriteLine("************************************************************" +
			                 "\n*********BIENVENIDO AL SISTEMA DE GESTION DE TURNOS*********" + 
			                "\n************************************************************");
			Menu();
			Console.WriteLine("\nGRACIAS POR ESTAR EN EL SISTEMA, VUELVA PRONTO!." +
			                  "\n------------------------------------------------------------");
			Console.ReadKey(true);
		}
	
		public static void Menu(){
			
			
			MostrarOpciones();
			
			try{ 
				//El menu de opciones esta dentro de un try para atrapar cualquier excepcion que pueda incurrir, en este caso la unica
				//excepcion posible es en caso de que una opcion sea invalida
				
				Medico medic;
				medic = new Medico();
				
				int opcion = int.Parse(Console.ReadLine());
				
				while (opcion != 0) {
				
				if (opcion == 1) {
					
					VerTodosPacientes(medic);
					MostrarOpciones();
			
					opcion = int.Parse(Console.ReadLine());
				}
				else if (opcion == 2) {
					
					
					CrearPaciente(medic);
					MostrarOpciones();
					opcion = int.Parse(Console.ReadLine());
					}
					
				else if (opcion == 3) {
					
					if (medic.verTodosPacientes().Count > 0) {
					    BorrarPaciente(medic);
					}
					else{
						Console.WriteLine("\nActualmente no hay pacientes, por favor ingrese uno.\n");
					}
					MostrarOpciones();
				    opcion = int.Parse(Console.ReadLine());
				}	
					
				else if (opcion == 4) {
					if (medic.verTodosPacientes().Count > 0) { //La lista de pacientes debe ser mayor a 0 para buscar un paciente
					    VerPaciente(medic);
					}
					else{
						Console.WriteLine("\nActualmente no hay pacientes, por favor ingrese uno.\n");
					}
					MostrarOpciones();
					opcion = int.Parse(Console.ReadLine());
				}

				else if (opcion == 5) {
					
					if (medic.verTodosPacientes().Count > 0) {
					    ActualizarDiagnostico(medic);
					}
					else{
						Console.WriteLine("\nActualmente no hay pacientes, por favor ingrese uno.\n");
					}
					MostrarOpciones();
					opcion = int.Parse(Console.ReadLine());
					
				}		
				
				else if (opcion == 6) {
					
					TurnoDisponible(medic);
					MostrarOpciones();
					opcion = int.Parse(Console.ReadLine());
					
				}					
				
				else if (opcion == 7) {
					
					
						if (medic.verTodosPacientes().Count > 0) {
							Console.WriteLine("\nEl paciente se encuentra en el sistema? Ingrese si/no: \n");
							string dato = Console.ReadLine().ToUpper();
							if (dato =="SI" ) {
								Console.WriteLine("\nIngrese el horario en el que desea el turno.\n");
								string hora = Console.ReadLine();
						    	AgregarTurno(medic, hora);
							}
							else if (dato == "NO"){
								Console.WriteLine("\nPor favor ingrese un paciente.\n");
							}
							
							else{
								Console.WriteLine("\nPor favor ingrese una opcion valida.");
							}
						}
						else{
							Console.WriteLine("\nActualmente no hay pacientes, por favor ingrese uno.\n");
						}
						MostrarOpciones();
						opcion = int.Parse(Console.ReadLine());
				}
					
				else if (opcion == 8) {
					
					if (medic.verTurnosOcupados().Count > 0) {
						Console.WriteLine("\nIngrese el horario del turno a eliminar: \n");
						string horario = Console.ReadLine();
						ElimiarTurno(medic,horario);
					}
					else{
						Console.WriteLine("\nActualmente no hay turnos ocupados, por favor ingrese un turno.\n");
					}
					MostrarOpciones();
					opcion = int.Parse(Console.ReadLine());
					
				}	
				
				else if (opcion == 9) {
					VerTurnosOcupados(medic);
					MostrarOpciones();
					opcion = int.Parse(Console.ReadLine());
				}
					
				else if (opcion == 10) {
					VerObrasSociales(medic);
					MostrarOpciones();
					opcion = int.Parse(Console.ReadLine());
				}	
					
				else{
					Console.WriteLine("\nOpcion incorrecta, ingrese una opcion valida.");
					MostrarOpciones();
					opcion = int.Parse(Console.ReadLine());
				}
			}
				
			}catch(Exception){
				Console.WriteLine("\nLa opcion ingresada no es valida, por favor intente nuevamente: ");
				Menu();
			}
		}
		
			public static Paciente BuscarPaciente(Medico medico){   //Funcion auxiliar que se usa en varias ocaciones
			ArrayList listaPacientes = medico.verTodosPacientes();
			
			Console.WriteLine("\nIngrese dni del paciente:\n");
			int dniBuscar = int.Parse(Console.ReadLine());
			
			foreach (Paciente x in listaPacientes) {
				
				if (x.Dni == dniBuscar) {
					return x;
				}
			}
			
			Paciente invalido = new Paciente("x", 0,"x", 0,"x");  //Se crea un paciente invalido para que ese se retorne
			return invalido;									  //Y luego en su funcion respectiva se hace una validacion
		
		}

		
		public static void VerTodosPacientes(Medico medico){				//Ver todos los pacientes
		
			ArrayList listaPacientes;
			listaPacientes = medico.verTodosPacientes();
			
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
		
		public static void CrearPaciente(Medico medico){
			
			
			try{
				ArrayList listaPacientes = medico.verTodosPacientes();
				ArrayList obrasSociales = medico.verObrasSociales();
				
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
				medico.agregarPaciente(paciente);									//Agregar paciente a la lista de pacientes
				if (!(obrasSociales.Contains(paciente.Obra_social))) {				//Agregar obra social del paciente a la lista
					medico.agregarObraSocial(paciente.Obra_social);
				}
				Console.WriteLine("\n***************¡Paciente agregado con exito!****************\n");		
			}
				
			else{
				string obra_social = "NO TIENE/PARTICULAR";
				int nro_afiliado = 00;
				Console.WriteLine("\n-Ingrese el diagnostico del paciente:\n");
				string diagnostico = Console.ReadLine().ToUpper();
				Paciente paciente = new Paciente(nombre, dni, obra_social, nro_afiliado,diagnostico);
			    medico.agregarPaciente(paciente);									//Agregar paciente a la lista de pacientes

			    Console.WriteLine("\n***************¡Paciente agregado con exito!****************\n");
			
			}
			}catch(FormatException){
				Console.WriteLine("\nFormato de dato ingresado no valido");
				CrearPaciente(medico);
			}catch(NombreInvalidoException){
				Console.WriteLine("\nEl nombre ingresado no es valido, por favor intente nuevamente.");
			}
			
			
			
		}
			
		public static void BorrarPaciente(Medico medico){
			
			Paciente x = BuscarPaciente(medico);				 		 //Eliminar paciente de la lista de pacientes
			  									                         // usando su dni como referencia
			if (x.Nombre == "x") {
				Console.WriteLine("\nNo se encontro ningun paciente con el dni ingresado.\n");	
			}
			else{
			 	medico.eliminarPaciente(x);
				Console.WriteLine("\nPaciente "+ x.Nombre+" eliminado con exito!.\n");
			}
		}
		
		public static void VerPaciente(Medico medico){	//Busca al paciente desde la lista a traves de su dni	
			
			Paciente x = BuscarPaciente(medico);
			
			if (x.Nombre == "x") {
				Console.WriteLine("\nNo se ha encontrado ningun paciente con el DNI ingresado.");
			}
			else{
				Console.WriteLine("\nSe ha encontrado al paciente: \n" + "\nNombre: "+ x.Nombre + "\nDni: "+ x.Dni + "\nObra Social: "+ x.Obra_social +
					               "\nNumero de Afiliado: "+ x.Nro_afiliado + "\nDiagnostico: "+x.Diagnostico+"\n");
			}
		}
		
		public static void ActualizarDiagnostico(Medico medico){//Cambiar el diagnostico de un paciente
			
			Paciente x = BuscarPaciente(medico);
			if (x.Nombre == "x") {
				Console.WriteLine("\nNo se ha encontrado ningun paciente con el DNI ingresado.");
			}
			else{
				Console.WriteLine("\nIngrese el nuevo Diagnostico de " + x.Nombre + ": \n");
				string diagnostico = Console.ReadLine().ToUpper();
				medico.actualizarPaciente(x, diagnostico);
				Console.WriteLine("\n************¡Diagnostico actualizado con exito!*************\n");
			}
		}
		
		public static void TurnoDisponible(Medico medico){				// Ver los turnos que hay disponibles
			
			ArrayList turnosDisponibles = medico.verTurnosDisponibles();
			
			
			

		
			if (medico.verTurnosDisponibles().Count > 0) {
				for (int i = 0; i < turnosDisponibles.Count; i++) {
					Console.WriteLine("\nTurno disponible: " + turnosDisponibles[i]);
				}
			}
			else{
				Console.WriteLine("\nActualmente no hay turnos disponibles.");
			}
		}
		
		
		public static void VerObrasSociales(Medico medico){					 //Ver todas las obras sociales con las que 
																			// trabaja el medico
			ArrayList obrasSociales = medico.verObrasSociales();
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
			
		public static void AgregarTurno(Medico medico, string hora){	// Se comprueba si el horario pedido esta disponible. Caso true,se elimina de la lista
				        							  									// de disponibles y añade a lista de ocupados	        							  	
			ArrayList turnosDisponibles = medico.verTurnosDisponibles();
			ArrayList turnosOcupados = medico.verTurnosOcupados();
				        							  				
			if (!(turnosDisponibles.Contains(hora)) && !(turnosOcupados.Contains(hora)) ) {	
				   	Console.WriteLine("\nPor favor ingrese un horario que este disponible.\n");						  					
			 }        							  				
			else if (turnosDisponibles.Contains(hora) ) {
					Paciente x = BuscarPaciente(medico);
					if (x.Nombre == "x") {
						Console.WriteLine("\nNo se encontron ningun paciente con el DNI ingresado.");
					}
					else{
						Turno turno = new Turno(x, hora);		
						medico.agregarTurno(turno, hora);
						Console.WriteLine("\n*********Turno de las "+ hora + " agregado exitosamente!**********");
					}
			}  							  				
			else if ((turnosDisponibles.Count == 0) && (turnosOcupados.Count == 9)){ //Si listaDisponible esta vacia y  ListaOcupada completa es 
						                                                             // que ya no hay turnos disponibles
				Console.WriteLine("\nHorarios no disponibles, llamar próximo día de atencion");	
			}
		}
		
		public static void ElimiarTurno(Medico medico, string horario){
			
			ArrayList turnosOcupados = medico.verTurnosOcupados();				//Eliminar un turno que esta ocupado
			ArrayList turnosDisponibles = medico.verTurnosDisponibles();		//Una vez eliminado vuelve a estar
			foreach (Turno x in turnosOcupados) {								//disponible
																				
				if(x.Horario.Contains(horario)){
					medico.eliminarTurno(x, horario);
					Console.WriteLine("\n**************Turno de las "+ horario +" eliminado!***************");
					return;
				}
			}
			if(turnosDisponibles.Contains(horario)){
																						
				Console.WriteLine("\nEl turno de las " + horario + " no puede ser eliminado porque esta disponible.");																			
			}
			else{
				Console.WriteLine("\nEl turno de las " + horario + " no puede ser eliminado porque es invalido.");
			}
		}
		
		public static void VerTurnosOcupados(Medico medico){				//Ver todos los turnos que estan
																					//ocupados
			ArrayList turnosOcupados = medico.verTurnosOcupados();
			
			if (turnosOcupados.Count > 0) {
				foreach (Turno x in turnosOcupados) {
					for (int i = turnosOcupados.Count; i <= turnosOcupados.Count; i++) {
						Console.WriteLine("\nEl turno de las "+ x.Horario + " esta ocupado por "+ x.Paciente.Nombre);
					}
				}
			}
			else{
				Console.WriteLine("\nActualmente no hay turnos ocupados, por favor ingrese un turno.\n" +
				                  "\nOpcion 7 del menu.");
			}
		}
		
		/*public static ArrayList ordenarPorBurbuja( Medico medico){
			ArrayList turnosOcupados = medico.verTurnosOcupados();
			int n = turnosOcupados.Count;
			int i = 0;
			Boolean ordenado=false;
			foreach (Turno x in turnosOcupados) {
			
				while((i<(n-1)) && (ordenado== false)){
				ordenado=true;
					for(int j=0; j<(n-1- i); j++){
						if(x.Horario[j] > x.Horario[j+1]){ 
							ordenado=false;
							object swap = turnosOcupados[j]; 
							turnosOcupados[j] = turnosOcupados[j+1]; 
							turnosOcupados[j+1] = swap;
						}
					}
				}				
				
			}
			return turnosOcupados;
		}*/

		public static void MostrarOpciones(){
			Console.WriteLine("------------------------------------------------------------" +
			                  "\n-Ingrese 1 para ver lista de pacientes.\n-Ingrese 2 para agregar paciente." +
			                  "\n-Ingrese 3 para eliminar paciente.\n-Ingrese 4 para ver un paciente." +
			                  "\n-Ingrese 5 para actualizar diagnostico de un paciente." +
			                  "\n-Ingrese 6 para ver turnos disponibles.\n-Ingrese 7 para agregar un turno." +
			                  "\n-Ingrese 8 para eliminar un turno.\n-Ingrese 9 para ver todos los turnos ocupados." +
			                  "\n-Ingrese 10 para ver las obras sociales que cubre el medico.\n-Ingrese 0 para salir\n" +
			                 "------------------------------------------------------------");		
		}
	}	
}