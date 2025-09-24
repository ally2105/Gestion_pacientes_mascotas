using System;
using System.Collections.Generic;
using Gestion_pacientes_mascotas.Models;

class Program
{
    static void Main()
    {
        List<Paciente> listaPacientes = new List<Paciente>();
        string opcion = "";

        while (opcion.ToLower() != "salir")
        {
            Console.WriteLine("=== Clínica Salud - Menú Principal ===");
            Console.WriteLine("1. Registrar Paciente");
            Console.WriteLine("2. Ver Pacientes");
            Console.WriteLine("3. Buscar Paciente");
            Console.WriteLine("Escribe 'salir' para salir");
            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine();

            Console.WriteLine();

            switch (opcion)
            {
                case "1":
                    PacienteService.RegistrarPaciente(listaPacientes);
                    break;
                case "2":
                    PacienteService.VerPacientes(listaPacientes);
                    break;
                case "3":
                    PacienteService.BuscarPaciente(listaPacientes);
                    break;
                case "salir":
                    Console.WriteLine("Saliendo del programa...");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }

            if (opcion.ToLower() != "salir")
            {
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
