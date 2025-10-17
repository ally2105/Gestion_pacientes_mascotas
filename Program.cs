
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
            Console.Title = "🐾 Veterinary Clinic - Pet and Owner Management 🐶🐱";

            // --- DEPENDENCY INJECTION SETUP (MANUAL) ---
            // A single DataContext instance is created and shared across all services.
            // This ensures that all parts of the application work with the same set of data.
            var context = new DataContext();

            // Instantiate services, injecting the shared context.
            var ownerService = new OwnerService(context);
            var petService = new PetService(context, ownerService);
            var appointmentService = new AppointmentService(context);
            var veterinarianService = new VeterinarianService(context);

            // Manually link services that depend on each other.
            // PetService needs OwnerService to handle pet-owner associations.
            ownerService.PetService = petService;

            // Start the main application loop.
            ShowMainMenu(ownerService, petService, appointmentService, veterinarianService);
        }

        /// <summary>
        /// Displays the main menu and handles user navigation.
        /// </summary>
        private static void ShowMainMenu(
            OwnerService ownerService,
            PetService petService,
            AppointmentService appointmentService,
            VeterinarianService veterinarianService)
        {
            int opcion;
            do
            {
                // Main menu UI
                Console.Clear();
                Console.WriteLine("=========================================");
                Console.WriteLine(" 🏥 VETERINARY CLINIC - MAIN MENU ");
                Console.WriteLine("=========================================");
                Console.WriteLine("1️⃣  Owners Menu");
                Console.WriteLine("2️⃣  Pets Menu");
                Console.WriteLine("3️⃣  Schedule a veterinary appointment");
                Console.WriteLine("4️⃣  View scheduled appointments");
                Console.WriteLine("5️⃣  Veterinarians Menu");
                Console.WriteLine("0️⃣  Exit");
                Console.WriteLine("=========================================");
                Console.Write("👉 Select an option: ");

                string input = Console.ReadLine() ?? "";
                int.TryParse(input, out opcion);
                Console.Clear();

                // Main application logic switch
                try
                {
                    switch (opcion)
                    {
                        case 1:
                            ShowOwnersSubmenu(ownerService);
                            break;
                        case 2:
                            ShowPetsSubmenu(petService);
                            break;
                        case 3:
                            appointmentService.ScheduleAppointment();
                            break;
                        case 4:
                            appointmentService.ViewAppointments();
                            break;
                        case 5:
                            ShowVeterinariansSubmenu(veterinarianService);
                            break;
                        case 0:
                            Console.WriteLine("👋 Thank you for using the system. See you soon!");
                            break;
                        default:
                            Console.WriteLine("⚠️ Invalid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // Global exception handler for unexpected errors in the main loop.
                    Console.WriteLine("❌ Unexpected error: " + ex.Message);
                    Logger.LogError(ex, "Program.ShowMainMenu");
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }

            } while (opcion != 0);
        }

        /// <summary>
        /// Displays the submenu for owner management.
        /// </summary>
        private static void ShowOwnersSubmenu(OwnerService ownerService)
        {
            string opcion = "";
            while (opcion != "0")
            {
                Console.Clear();
                Console.WriteLine("=== OWNERS MENU ===");
                Console.WriteLine("1️⃣ Register new owner");
                Console.WriteLine("2️⃣ View all owners");
                Console.WriteLine("3️⃣ Search owner by name");
                Console.WriteLine("4️⃣ Update owner");
                Console.WriteLine("5️⃣ Delete owner");
                Console.WriteLine("0️⃣ Back");
                Console.Write("👉 Select an option: ");
                opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        ownerService.Register();
                        break;
                    case "2":
                        ownerService.ViewOwners();
                        break;
                    case "3":
                        ownerService.SearchOwnerByName();
                        break;
                    case "4":
                        ownerService.UpdateOwner();
                        break;
                    case "5":
                        ownerService.DeleteOwner();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("⚠️ Invalid option.");
                        break;
                }

                Console.WriteLine("\nPress a key to continue...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Displays the submenu for pet management.
        /// </summary>
        private static void ShowPetsSubmenu(PetService petService)
        {
            string opcion = "";
            while (opcion != "0")
            {
                Console.Clear();
                Console.WriteLine("=== PETS MENU ===");
                Console.WriteLine("1️⃣ Register pet");
                Console.WriteLine("2️⃣ View all pets");
                Console.WriteLine("3️⃣ Edit pet by Id");
                Console.WriteLine("4️⃣ Delete pet by Id");
                Console.WriteLine("0️⃣ Back");
                Console.Write("👉 Select an option: ");
                opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        petService.Register();
                        break;
                    case "2":
                        petService.ViewPets();
                        break;
                    case "3":
                        Console.Write("Enter pet Id (GUID) to edit: ");
                        if (Guid.TryParse(Console.ReadLine(), out Guid idEdit))
                        {
                            var pet = petService.FindById(idEdit);
                            if (pet != null)
                                petService.EditPet(pet);
                            else
                                Console.WriteLine("⚠️ No pet found with that Id.");
                        }
                        else
                        {
                            Console.WriteLine("⚠️ Invalid Id.");
                        }
                        break;
                    case "4":
                        Console.Write("Enter pet Id (GUID) to delete: ");
                        if (Guid.TryParse(Console.ReadLine(), out Guid idDel))
                        {
                            petService.DeleteById(idDel);
                        }
                        else
                        {
                            Console.WriteLine("⚠️ Invalid Id.");
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("⚠️ Invalid option.");
                        break;
                }

                Console.WriteLine("\nPress a key to continue...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Displays the submenu for veterinarian management.
        /// </summary>
        private static void ShowVeterinariansSubmenu(VeterinarianService veterinarianService)
        {
            string opcion = "";
            while (opcion != "0")
            {
                Console.Clear();
                Console.WriteLine("=== VETERINARIANS MENU ===");
                Console.WriteLine("1️⃣ Register veterinarian");
                Console.WriteLine("2️⃣ View registered veterinarians");
                Console.WriteLine("3️⃣ Search veterinarian by name");
                Console.WriteLine("4️⃣ Edit veterinarian");
                Console.WriteLine("5️⃣ Delete veterinarian");
                Console.WriteLine("0️⃣ Back");
                Console.Write("👉 Select an option: ");
                opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        veterinarianService.AddVeterinarian();
                        break;
                    case "2":
                        veterinarianService.ListVeterinarians();
                        break;
                    case "3":
                        veterinarianService.SearchVeterinarian();
                        break;
                    case "4":
                        veterinarianService.EditVeterinarian();
                        break;
                    case "5":
                        veterinarianService.DeleteVeterinarian();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("⚠️ Invalid option.");
                        break;
                }

                Console.WriteLine("\nPress a key to continue...");
                Console.ReadKey();
            }
        }
    }
}
