using System;
using System.Linq;
using Gestion_pacientes_mascotas.Database;
using Gestion_pacientes_mascotas.Models;
using Gestion_pacientes_mascotas.Utils;

namespace Gestion_pacientes_mascotas
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "🐾 Clínica Veterinaria - Gestión de Pacientes y Mascotas 🐶🐱";

            // Crear contexto compartido
            var context = new DataContext();

            // Crear servicios
            var pacienteService = new PacienteService(context);
            var mascotaService = new MascotaService(context, pacienteService);
            var citaService = new CitaService(context);
            pacienteService.MascotaService = mascotaService;

            // Semillas iniciales de veterinarios
            context.Veterinarios.Add(new Veterinario("Dra. Ana López", "Medicina General", "3001112233", "ana@vet.com"));
            context.Veterinarios.Add(new Veterinario("Dr. Carlos Ruiz", "Cirugía", "3002223344", "carlos@vet.com"));

            MostrarMenuPrincipal(pacienteService, mascotaService, citaService, context);
        }

        private static void MostrarMenuPrincipal(
            PacienteService pacienteService,
            MascotaService mascotaService,
            CitaService citaService,
            DataContext context)
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("=========================================");
                Console.WriteLine(" 🏥 CLÍNICA VETERINARIA - MENÚ PRINCIPAL ");
                Console.WriteLine("=========================================");
                Console.WriteLine("1️⃣  Menu Pacientes");
                Console.WriteLine("2️⃣  Menu Mascotas");
                Console.WriteLine("3️⃣  Agendar cita veterinaria");
                Console.WriteLine("4️⃣  Ver citas agendadas");
                Console.WriteLine("5️⃣  Registrar veterinario");
                Console.WriteLine("6️⃣  Ver veterinarios");
                Console.WriteLine("0️⃣  Salir");
                Console.WriteLine("=========================================");
                Console.Write("👉 Selecciona una opción: ");

                string input = Console.ReadLine() ?? "";
                int.TryParse(input, out opcion);
                Console.Clear();

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            MostrarSubmenuPacientes(pacienteService);
                            break;
                        case 2:
                            MostrarSubmenuMascotas(mascotaService);
                            break;
                        case 3:
                            citaService.AgendarCita();
                            break;
                        case 4:
                            citaService.VerCitas();
                            break;
                        case 5:
                            RegistrarVeterinario(context);
                            break;
                        case 6:
                            VerVeterinarios(context);
                            break;
                        case 0:
                            Console.WriteLine("👋 Gracias por usar el sistema. ¡Hasta pronto!");
                            break;
                        default:
                            Console.WriteLine("⚠️ Opción no válida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Error inesperado: " + ex.Message);
                    Logger.LogError(ex, "Program.MostrarMenuPrincipal");
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 0);
        }

        // ==========================================================
        // SUBMENÚ PACIENTES
        // ==========================================================
        private static void MostrarSubmenuPacientes(PacienteService pacienteService)
        {
            string opcion = "";
            while (opcion != "0")
            {
                Console.Clear();
                Console.WriteLine("=== MENU PACIENTES ===");
                Console.WriteLine("1️⃣ Registrar nuevo paciente");
                Console.WriteLine("2️⃣ Ver todos los pacientes");
                Console.WriteLine("3️⃣ Buscar paciente por nombre");
                Console.WriteLine("4️⃣ Actualizar paciente");
                Console.WriteLine("5️⃣ Eliminar paciente");
                Console.WriteLine("0️⃣ Volver");
                Console.Write("👉 Seleccione una opción: ");
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
                        pacienteService.BuscarPacientePorNombre();
                        break;
                    case "4":
                        pacienteService.ActualizarPaciente();
                        break;
                    case "5":
                        pacienteService.EliminarPaciente();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("⚠️ Opción no válida.");
                        break;
                }

                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
            }
        }

        // ==========================================================
        // SUBMENÚ MASCOTAS
        // ==========================================================
        private static void MostrarSubmenuMascotas(MascotaService mascotaService)
        {
            string opcion = "";
            while (opcion != "0")
            {
                Console.Clear();
                Console.WriteLine("=== MENU MASCOTAS ===");
                Console.WriteLine("1️⃣ Registrar mascota");
                Console.WriteLine("2️⃣ Ver todas las mascotas");
                Console.WriteLine("3️⃣ Editar mascota por Id");
                Console.WriteLine("4️⃣ Eliminar mascota por Id");
                Console.WriteLine("0️⃣ Volver");
                Console.Write("👉 Seleccione una opción: ");
                opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        mascotaService.Registrar();
                        break;
                    case "2":
                        mascotaService.VerMascotas();
                        break;
                    case "3":
                        Console.Write("Ingrese Id (GUID) de la mascota a editar: ");
                        if (Guid.TryParse(Console.ReadLine(), out Guid idEdit))
                        {
                            var mascota = mascotaService.BuscarPorId(idEdit);
                            if (mascota != null)
                                mascotaService.EditarMascota(mascota);
                            else
                                Console.WriteLine("⚠️ No se encontró mascota con ese Id.");
                        }
                        else
                        {
                            Console.WriteLine("⚠️ Id inválido.");
                        }
                        break;
                    case "4":
                        Console.Write("Ingrese Id (GUID) de la mascota a eliminar: ");
                        if (Guid.TryParse(Console.ReadLine(), out Guid idDel))
                        {
                            mascotaService.EliminarPorId(idDel);
                        }
                        else
                        {
                            Console.WriteLine("⚠️ Id inválido.");
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("⚠️ Opción no válida.");
                        break;
                }

                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
            }
        }

        // ==========================================================
        // VETERINARIOS
        // ==========================================================
        private static void RegistrarVeterinario(DataContext context)
        {
            Console.WriteLine("=== Registro de Veterinario ===");
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine() ?? "";
            Console.Write("Especialidad: ");
            string especialidad = Console.ReadLine() ?? "";
            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            var veterinario = new Veterinario(nombre, especialidad, telefono, email);
            context.Veterinarios.Add(veterinario);
            Logger.LogInfo($"Veterinario registrado: {nombre}", "Program.RegistrarVeterinario");

            Console.WriteLine("✅ Veterinario registrado correctamente.");
        }

        private static void VerVeterinarios(DataContext context)
        {
            Console.WriteLine("=== Lista de Veterinarios ===");
            if (!context.Veterinarios.Any())
            {
                Console.WriteLine("No hay veterinarios registrados.");
                return;
            }

            foreach (var v in context.Veterinarios)
            {
                v.MostrarInformacion();
                Console.WriteLine("-------------------------");
            }
        }
    }
}
