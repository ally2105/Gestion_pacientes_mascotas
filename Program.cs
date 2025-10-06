using System;
using System.Collections.Generic;
using System.Linq; // Necesario para usar LINQ
using Gestion_pacientes_mascotas.Models;

class Program
{
    static void Main()
    {
    // Instancias de servicios
    var pacienteService = new PacienteService();
    var mascotaService = new MascotaService();
        string opcion = "";
        while (opcion != "5")
        {
            Console.WriteLine("1. Registrar paciente y mascotas");
            Console.WriteLine("2. Ver pacientes y sus mascotas");
            Console.WriteLine("3. Mascotas haciendo sonidos");
            Console.WriteLine("4. Buscar mascota por nombre");
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
                    // Submenu para administrar mascotas (usa mascotaService local)
                    while (true)
                    {
                        Console.WriteLine("--- Menú mascotas ---");
                        Console.WriteLine("Escriba a para editar una mascota");
                        Console.WriteLine("Escriba b para eliminar una mascota");
                        Console.WriteLine("Escriba c para buscar una mascota por nombre");
                        Console.WriteLine("Escriba salir para volver al menú principal");
                        Console.Write("Opción mascotas: ");
                        var sub = (Console.ReadLine() ?? "").Trim().ToLower();

                        if (sub == "a")
                        {
                            mascotaService.VerMascotas();
                            Console.Write("Nombre de la mascota a editar: ");
                            var nombre = Console.ReadLine() ?? "";
                            var m = mascotaService.Mascotas.FirstOrDefault(x => x.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
                            if (m != null)
                                mascotaService.EditarMascota(m);
                            else
                                Console.WriteLine("Mascota no encontrada en el registro local de mascotas.");
                        }
                        else if (sub == "b")
                        {
                            mascotaService.VerMascotas();
                            Console.Write("Nombre de la mascota a eliminar: ");
                            var nombre = Console.ReadLine() ?? "";
                            var m = mascotaService.Mascotas.FirstOrDefault(x => x.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
                            if (m != null)
                                mascotaService.EliminarMascota(m);
                            else
                                Console.WriteLine("Mascota no encontrada en el registro local de mascotas.");
                        }
                        else if (sub == "c")
                        {
                            Console.Write("Nombre de la mascota a buscar: ");
                            var nombre = Console.ReadLine() ?? "";
                            var m = mascotaService.Mascotas.FirstOrDefault(x => x.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
                            if (m != null)
                            {
                                Console.WriteLine("Mascota encontrada:");
                                m.MostrarInformacion();
                            }
                            else
                                Console.WriteLine("Mascota no encontrada en el registro local de mascotas.");
                        }
                        else if (sub == "salir")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Opción no válida, intente de nuevo.");
                        }
                    }
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
