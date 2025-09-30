using System;
using System.Collections.Generic;
using System.Linq; // Necesario para usar LINQ
using Gestion_pacientes_mascotas.Models;

class Program
{
    static void Main()
    {
        var pacientes = new List<Paciente>();
        string opcion = "";
        while (opcion != "3")
        {
            Console.WriteLine("1. Registrar paciente y mascotas");
            Console.WriteLine("2. Ver pacientes y sus mascotas");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine() ?? "";

            switch (opcion)
            {
                case "1":
                    PacienteService.RegistrarPaciente(pacientes);
                    break;
                case "2":
                    PacienteService.VerPacientes(pacientes);
                    break;
                case "3":
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.WriteLine("Opción no válida.\n");
                    break;
            }
        }
    }
}
