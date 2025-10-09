using System;
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

            // Crear contexto de datos compartido
            var context = new DataContext();

            // Instanciar servicios
            var pacienteService = new PacienteService(context);
            var mascotaService = new MascotaService(context, pacienteService);
            var citaService = new CitaService(context);
            pacienteService.MascotaService = mascotaService;

            // Semillas iniciales opcionales
            context.Veterinarios.Add(new Veterinario("Dra. Diego Toloza", "Medicina General", "3001112233", "ana@vet.com"));
            context.Veterinarios.Add(new Veterinario("Dr. Sofia Vergara", "Cirugía", "3002223344", "carlos@vet.com"));

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
                Console.WriteLine("1️⃣  Registrar paciente");
                Console.WriteLine("2️⃣  Ver pacientes");
                Console.WriteLine("3️⃣  Registrar mascota");
                Console.WriteLine("4️⃣  Ver mascotas");
                Console.WriteLine("5️⃣  Agendar cita veterinaria");
                Console.WriteLine("6️⃣  Ver citas agendadas");
                Console.WriteLine("7️⃣  Registrar veterinario");
                Console.WriteLine("8️⃣  Ver veterinarios");
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
                            pacienteService.Registrar();
                            break;
                        case 2:
                            pacienteService.VerPacientes();
                            break;
                        case 3:
                            mascotaService.Registrar();
                            break;
                        case 4:
                            mascotaService.VerMascotas();
                            break;
                        case 5:
                            citaService.AgendarCita();
                            break;
                        case 6:
                            citaService.VerCitas();
                            break;
                        case 7:
                            RegistrarVeterinario(context);
                            break;
                        case 8:
                            VerVeterinarios(context);
                            break;
                        case 0:
                            Console.WriteLine("👋 Gracias por usar el sistema. ¡Hasta pronto!");
                            break;
                        default:
                            Console.WriteLine("⚠️ Opción no válida. Intenta nuevamente.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Ocurrió un error inesperado.");
                    Logger.LogError(ex, "Program.MostrarMenuPrincipal");
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 0);
        }

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
            if (context.Veterinarios.Count == 0)
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
