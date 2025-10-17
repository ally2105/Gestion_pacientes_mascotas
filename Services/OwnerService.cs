using System;
using System.Linq;  
using Gestion_pacientes_mascotas.Database;
using Gestion_pacientes_mascotas.Utils;
using Gestion_pacientes_mascotas.Interfaces;

namespace Gestion_pacientes_mascotas.Models
{
    /// <summary>
    /// Handles business logic for Owner entities, including CRUD operations.
    /// </summary>
    public class OwnerService : IRegisterable
    {
        private readonly DataContext _context;
        public PetService? PetService { get; set; }

        public OwnerService(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Interactively registers a new owner and their pets.
        /// </summary>
        public void Register()
        {
            try
            {
                Console.WriteLine("=== Register New Owner ===");

                // --- Get Owner Details ---
                Console.Write("Enter the owner's name: ");
                string name = Console.ReadLine() ?? "";

                // Ensure owner name is unique before proceeding.
                if (_context.Owners.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("⚠️ An owner with that name already exists. Use another name or update the existing one.");
                    return;
                }

                int age;
                // Loop until a valid, non-negative age is entered.
                while (true)
                {
                    Console.Write("Enter the owner's age: ");
                    if (int.TryParse(Console.ReadLine(), out age) && age >= 0)
                        break;
                    Console.WriteLine("⚠️ Error: Age must be a valid number and greater than or equal to 0.");
                }

                Console.Write("Enter the owner's address: ");
                string address = Console.ReadLine() ?? "";
                // Loop until a non-empty address is provided.
                while (string.IsNullOrWhiteSpace(address))
                {
                    Console.WriteLine("⚠️ Error: Address cannot be empty.");
                    address = Console.ReadLine() ?? "";
                }

                Console.Write("Enter the owner's phone number (10 digits): ");
                string phone = Console.ReadLine() ?? "";
                // Loop until a 10-digit phone number is entered.
                while (phone.Length != 10 || !phone.All(char.IsDigit))
                {
                    Console.WriteLine("⚠️ Error: The phone number must have 10 numeric digits.");
                    phone = Console.ReadLine() ?? "";
                }

                var newOwner = new Owner(name, age, address, phone);

                // --- Optionally Register Pets for the New Owner ---
                Console.WriteLine("Do you want to add pets for this owner? (yes/no)");
                string addPetsResponse = Console.ReadLine() ?? "no";
                while (addPetsResponse.Trim().ToLower() == "yes")
                {
                    Console.Write("Pet's name: ");
                    string petName = Console.ReadLine() ?? "";
                    while (string.IsNullOrWhiteSpace(petName))
                    {
                        Console.WriteLine("⚠️ Error: The pet's name cannot be empty.");
                        petName = Console.ReadLine() ?? "";
                    }

                    Console.Write("Enter the species: ");
                    string petSpecies = Console.ReadLine() ?? "";
                    while (string.IsNullOrWhiteSpace(petSpecies))
                    {
                        Console.WriteLine("⚠️ Error: The species cannot be empty.");
                        petSpecies = Console.ReadLine() ?? "";
                    }

                    Console.Write("Breed: ");
                    string petBreed = Console.ReadLine() ?? "";
                    while (string.IsNullOrWhiteSpace(petBreed))
                    {
                        Console.WriteLine("⚠️ Error: The breed cannot be empty.");
                        petBreed = Console.ReadLine() ?? "";
                    }

                    int petAge;
                    while (true)
                    {
                        Console.Write("Age: ");
                        if (int.TryParse(Console.ReadLine(), out petAge) && petAge >= 0)
                            break;
                        Console.WriteLine("⚠️ Error: Age must be valid.");
                    }

                    var pet = new Pet(Guid.NewGuid(), petName, petSpecies, petBreed, petAge, newOwner.Name);
                    newOwner.Pets.Add(pet);
                    _context.Pets.Add(pet);

                    Console.WriteLine("Do you want to add another pet? (yes/no)");
                    addPetsResponse = Console.ReadLine() ?? "no";
                }

                // Add the fully configured owner to the central data context.
                _context.Owners.Add(newOwner);
                Logger.LogInfo($"Owner registered: {newOwner.Name}", "OwnerService.Register");
                Console.WriteLine("✅ Owner registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ An error occurred during registration.");
                Logger.LogError(ex, "OwnerService.Register");
            }
            finally
            {
                Console.WriteLine("(Registration finished)\n");
            }
        }

        /// <summary>
        /// Displays information for all registered owners and their pets.
        /// </summary>
        public void ViewOwners()
        {
            Console.WriteLine("=== List of Registered Owners ===");
            if (!_context.Owners.Any())
            {
                Console.WriteLine("No owners registered.");
                return;
            }

            foreach (var owner in _context.Owners)
            {
                owner.ShowInformation();
                owner.ShowPets();
                Console.WriteLine("-------------------------------");
            }
        }

        /// <summary>
        /// Searches for and displays a single owner by their name.
        /// </summary>
        public void SearchOwnerByName()
        {
            Console.Write("Enter the name of the owner to search for: ");
            string nameToSearch = Console.ReadLine() ?? "";

            var owner = _context.Owners.FirstOrDefault(p =>
                p.Name.Equals(nameToSearch, StringComparison.OrdinalIgnoreCase));

            if (owner == null)
            {
                Console.WriteLine("⚠️ Owner not found.");
                return;
            }

            owner.ShowInformation();
            owner.ShowPets();
        }

        /// <summary>
        /// Finds an owner by name and allows interactive editing of their details.
        /// </summary>
        public void UpdateOwner()
        {
            Console.WriteLine("=== Update Owner ===");

            if (!_context.Owners.Any())
            {
                Console.WriteLine("No owners registered.");
                return;
            }

            Console.Write("Enter the name of the owner to update: ");
            string nameToSearch = Console.ReadLine() ?? "";

            var owner = _context.Owners.FirstOrDefault(p =>
                p.Name.Equals(nameToSearch, StringComparison.OrdinalIgnoreCase));

            if (owner == null)
            {
                Console.WriteLine("⚠️ Owner not found.");
                return;
            }

            Console.WriteLine($"Owner found: {owner.Name}");
            Console.WriteLine("Leave any field empty that you do not wish to modify.");

            Console.Write("New name: ");
            string newName = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newName))
                owner.Name = newName;

            Console.Write("New age: ");
            string newAgeStr = Console.ReadLine() ?? "";
            if (int.TryParse(newAgeStr, out int age))
                owner.Age = age;

            Console.Write("New address: ");
            string newAddress = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newAddress))
                owner.Address = newAddress;

            Console.Write("New phone: ");
            string newPhone = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newPhone))
                owner.Phone = newPhone;

            Console.WriteLine("✅ Owner updated successfully.");
            Logger.LogInfo($"Owner updated: {owner.Name}", "OwnerService.UpdateOwner");
        }

        /// <summary>
        /// Deletes an owner and all their associated pets from the system.
        /// </summary>
        public void DeleteOwner()
        {
            Console.WriteLine("=== Delete Owner ===");

            if (!_context.Owners.Any())
            {
                Console.WriteLine("No owners registered.");
                return;
            }

            Console.Write("Enter the name of the owner to delete: ");
            string nameToSearch = Console.ReadLine() ?? "";

            var owner = _context.Owners.FirstOrDefault(p =>
                p.Name.Equals(nameToSearch, StringComparison.OrdinalIgnoreCase));

            if (owner == null)
            {
                Console.WriteLine("⚠️ Owner not found.");
                return;
            }

            Console.WriteLine($"Are you sure you want to delete the owner {owner.Name}? (yes/no)");
            string confirmation = Console.ReadLine() ?? "no";
            if (confirmation.ToLower() != "yes")
            {
                Console.WriteLine("❌ Operation canceled.");
                return;
            }

            // Also remove their pets from the context
            foreach (var pet in owner.Pets.ToList())
                _context.Pets.Remove(pet);

            _context.Owners.Remove(owner);
            Console.WriteLine("✅ Owner deleted successfully.");
            Logger.LogInfo($"Owner deleted: {owner.Name}", "OwnerService.DeleteOwner");
        }

        /// <summary>
        /// Internal utility to remove a pet's association from its owner when the pet is deleted.
        /// </summary>
        /// <param name="id">The ID of the pet to disassociate.</param>
        public void RemovePetAssociation(Guid id)
        {
            foreach (var owner in _context.Owners)
            {
                var m = owner.Pets.FirstOrDefault(x => x.Id == id);
                if (m != null)
                {
                    owner.Pets.Remove(m);
                    Console.WriteLine($"Association: pet with Id {id} removed from owner {owner.Name}.");
                    return;
                }
            }
        }
    }
}
