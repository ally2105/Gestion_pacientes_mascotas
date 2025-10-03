using System;
using System.Collections.Generic;
using System.Linq; // Necesario para usar LINQ
using Gestion_pacientes_mascotas.Models;

class Program
{
    static void Main()
    {
    // Instancia de PacienteService
    var pacienteService = new PacienteService();
        string opcion = "";
        while (opcion != "4")
        {
            Console.WriteLine("1. Registrar paciente y mascotas");
            Console.WriteLine("2. Ver pacientes y sus mascotas");
            Console.WriteLine("3. Demo polimorfismo: Emitir sonidos");
            Console.WriteLine("4. Salir");
            
            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine() ?? "";

            switch (opcion)
            {
                case "1":
                    pacienteService.Registrar();
                    break;
                case "2":
                    pacienteService.VerPacientes();
                    break;
                case "3":
                    DemoPolimorfismo();
                    break;
                case "4":
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.WriteLine("Opción no válida.\n");
                    break;
            }
        }
    }

    static void DemoPolimorfismo()
    {
        var animales = new List<Animal>
        {
            new Mascota("Firulais", "Perro", "Labrador", 5, "Juan"),
            new Mascota("Misu", "Gato", "Siames", 3, "Ana"),
            new Mascota("Paco", "Pájaro", "Canario", 1, "Luis")
        };

        Console.WriteLine("Demostración de polimorfismo: EmitirSonido() de cada animal");
        foreach (var a in animales)
        {
            Console.Write($"{a.Nombre} ({a.Especie}): ");
            a.EmitirSonido();
        }
    }
}
