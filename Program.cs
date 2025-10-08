using System;
using System.Collections.Generic;
using System.Linq; // Necesario para usar LINQ
using Gestion_pacientes_mascotas.Models;

// Instancias de servicios
var pacienteService = new PacienteService();
var mascotaService = new MascotaService(pacienteService);
// Sincronizar referencias para que ambos servicios mantengan consistencia
pacienteService.MascotaService = mascotaService;
string opcion = "";
while (opcion != "5")
{
    Console.WriteLine("1. Registrar paciente y mascotas");
    Console.WriteLine("2. Ver pacientes y sus mascotas");
    Console.WriteLine("3. Mascotas haciendo sonidos");
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
            // Submenu para administrar mascotas (usa mascotaService local)
            while (true)
            {
                Console.WriteLine("--- Menú mascotas ---");
                Console.WriteLine("Escriba a para editar una mascota");
                Console.WriteLine("Escriba b para eliminar una mascota");
                Console.WriteLine("Escriba c para buscar una mascota por id");
                Console.WriteLine("Escriba salir para volver al menú principal");
                Console.Write("Opción mascotas: ");
                var sub = (Console.ReadLine() ?? "").Trim().ToLower();

                if (sub == "a")
                {
                    Console.WriteLine("Ingrese el id de la mascota a editar:");
                    var idInput = Console.ReadLine() ?? "";
                    if (Guid.TryParse(idInput, out Guid id))
                    {
                        // Editar usando servicio de mascotas por Id
                        mascotaService.EditarPorId(id);
                    }
                    else
                    {
                        Console.WriteLine("ID inválido. Asegúrese de ingresar un GUID correcto.");
                    }
                }
                else if (sub == "b")
                {
                    Console.WriteLine("Ingrese el id de la mascota a eliminar:");
                    var idInput = Console.ReadLine() ?? "";
                    if (Guid.TryParse(idInput, out Guid id))
                    {
                        // Eliminar usando el servicio central de mascotas (actualiza asociación con paciente)
                        mascotaService.EliminarPorId(id);
                    }
                    else
                    {
                        Console.WriteLine("ID inválido. Asegúrese de ingresar un GUID correcto.");
                    }
                }
                else if (sub == "c")
                {
                    Console.WriteLine("Ingrese el id de la mascota a buscar:");
                    var idInput = Console.ReadLine() ?? "";
                    if (Guid.TryParse(idInput, out Guid id))
                    {
                        // Buscar en el servicio local y mostrar
                        var mLocal = mascotaService.BuscarPorId(id);
                        if (mLocal != null)
                        {
                            mLocal.MostrarInformacion();
                        }
                        else
                        {
                            Console.WriteLine("No se encontró ninguna mascota con ese Id en el registro local.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ID inválido. Asegúrese de ingresar un GUID correcto.");
                    }
                }
                else if (sub == "salir")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Opción no válida.\n");
                }
            }
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

void DemoPolimorfismo()
{
    var animales = new List<Animal>
        {
            new Mascota(Guid.NewGuid(),"Firulais", "Perro", "Labrador", 5, "Juan"),
            new Mascota(Guid.NewGuid(),"Misu", "Gato", "Siames", 3, "Ana"),
            new Mascota(Guid.NewGuid(),"Paco", "Pájaro", "Canario", 1, "Luis")
        };

    Console.WriteLine("Demostración de polimorfismo: EmitirSonido() de cada animal");
    foreach (var a in animales)
    {
        Console.Write($"{a.Nombre} ({a.Especie}): ");
        a.EmitirSonido();
    }
}
