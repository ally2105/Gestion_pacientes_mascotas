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
        while (opcion != "5")
        {
            Console.WriteLine("1. Registrar paciente y mascotas");
            Console.WriteLine("2. Ver pacientes y sus mascotas");
            Console.WriteLine("3. Demo polimorfismo: Emitir sonidos");
            Console.WriteLine("4. Buscar mascota por nombre (lanza excepción personalizada si no existe)");
            Console.WriteLine("5. Salir");
            
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
                    Console.Write("Ingrese el nombre de la mascota a buscar: ");
                    var nombreMascota = Console.ReadLine() ?? "";
                    try
                    {
                        var encontrada = pacienteService.BuscarMascota(nombreMascota);
                        Console.WriteLine("Mascota encontrada:");
                        encontrada.MostrarInformacion();
                    }
                    catch (Gestion_pacientes_mascotas.Models.MascotaNoEncontradaException mex)
                    {
                        Console.WriteLine($"⚠️ {mex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("❌ Ocurrió un error inesperado al buscar la mascota.");
                        Console.WriteLine($"Detalles: {ex.Message}");
                    }
                    break;
                case "5":
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
