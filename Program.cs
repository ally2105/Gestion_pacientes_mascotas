
using Gestion_pacientes_mascotas.Database;
using Gestion_pacientes_mascotas.Models;
using Gestion_pacientes_mascotas.Services;
using Gestion_pacientes_mascotas.Utils;

namespace Gestion_pacientes_mascotas
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "🐾 Clínica Veterinaria - Gestión de Pacientes y Mascotas 🐶🐱";

            // ==========================================================
            // CONTEXTO COMPARTIDO
            // ==========================================================
            var context = new DataContext();

            // ==========================================================
            // CREACIÓN DE SERVICIOS
            // ==========================================================
            var pacienteService = new PacienteService(context);
            var mascotaService = new MascotaService(context, pacienteService);
            var citaService = new CitaService(context);
            var veterinarioService = new VeterinarioService(); // ✅ Servicio corregido

            // Vincular dependencias
            pacienteService.MascotaService = mascotaService;

            // ==========================================================
            // MENÚ PRINCIPAL
            // ==========================================================
            MostrarMenuPrincipal(pacienteService, mascotaService, citaService, veterinarioService);
        }

        // ==========================================================
        // MENÚ PRINCIPAL
        // ==========================================================
        private static void MostrarMenuPrincipal(
            PacienteService pacienteService,
            MascotaService mascotaService,
            CitaService citaService,
            VeterinarioService veterinarioService)
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("=========================================");
                Console.WriteLine(" 🏥 CLÍNICA VETERINARIA - MENÚ PRINCIPAL ");
                Console.WriteLine("=========================================");
                Console.WriteLine("1️⃣  Menú Pacientes");
                Console.WriteLine("2️⃣  Menú Mascotas");
                Console.WriteLine("3️⃣  Agendar cita veterinaria");
                Console.WriteLine("4️⃣  Ver citas agendadas");
                Console.WriteLine("5️⃣  Menú Veterinarios");
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
                            MostrarSubmenuVeterinarios(veterinarioService);
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
                Console.WriteLine("=== MENÚ PACIENTES ===");
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
                Console.WriteLine("=== MENÚ MASCOTAS ===");
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
        // SUBMENÚ VETERINARIOS
        // ==========================================================
        private static void MostrarSubmenuVeterinarios(VeterinarioService veterinarioService)
        {
            string opcion = "";
            while (opcion != "0")
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ VETERINARIOS ===");
                Console.WriteLine("1️⃣ Registrar veterinario");
                Console.WriteLine("2️⃣ Ver veterinarios registrados");
                Console.WriteLine("3️⃣ Buscar veterinario por nombre");
                Console.WriteLine("4️⃣ Editar veterinario");
                Console.WriteLine("5️⃣ Eliminar veterinario");
                Console.WriteLine("0️⃣ Volver");
                Console.Write("👉 Seleccione una opción: ");
                opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        veterinarioService.AgregarVeterinario();
                        break;
                    case "2":
                        veterinarioService.ListarVeterinarios();
                        break;
                    case "3":
                        veterinarioService.BuscarVeterinario();
                        break;
                    case "4":
                        veterinarioService.EditarVeterinario();
                        break;
                    case "5":
                        veterinarioService.EliminarVeterinario();
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
    }
}
