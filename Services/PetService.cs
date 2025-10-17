using Gestion_pacientes_mascotas.Database;
using Gestion_pacientes_mascotas.Utils;
using Gestion_pacientes_mascotas.Interfaces; 
using System;
using System.Linq;

namespace Gestion_pacientes_mascotas.Models
{
    public class PetService : IRegisterable
    {
        private readonly DataContext _context;
        private readonly OwnerService? _ownerService;

        // Constructor: now receives shared DataContext
        public PetService(DataContext context, OwnerService? ownerService = null)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _ownerService = ownerService;
        }

        // Register new pet (interactive)
        public void Register()
        {
            Console.WriteLine("=== Register New Pet ===");
            Console.Write("Name: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Species: ");
            string species = Console.ReadLine() ?? "";
            Console.Write("Breed: ");
            string breed = Console.ReadLine() ?? "";
            Console.Write("Age: ");
            int edad = 0;
            int.TryParse(Console.ReadLine(), out edad);
            Console.Write("Owner (owner's name, optional): ");
            string ownerName = Console.ReadLine() ?? "";

            var pet = new Pet(Guid.NewGuid(), name, species, breed, edad, ownerName);

            // Add to the central context
            _context.Pets.Add(pet);

            // If there is an owner with that name, also associate the pet with their list
            if (!string.IsNullOrWhiteSpace(ownerName))
            {
                var owner = _context.Owners
                    .FirstOrDefault(p => p.Name.Equals(ownerName, StringComparison.OrdinalIgnoreCase));
                if (owner != null)
                {
                    owner.Pets.Add(pet);
                }
            }

            Logger.LogInfo($"Pet registered: {pet.Name}", "PetService.Register");
            Console.WriteLine("✅ Pet registered successfully.");
        }

        // Show all pets from DataContext
        public void ViewPets()
        {
            if (!_context.Pets.Any())
            {
                Console.WriteLine("No pets registered.");
                return;
            }

            Console.WriteLine("=== List of Pets ===");
            foreach (var pet in _context.Pets)
            {
                pet.ShowInformation();
                Console.WriteLine($"The pet {pet.Name} says");
                pet.MakeSound();
                Console.WriteLine("---");
            }
        }

        // Edit using instance (maintains original owner reassignment logic)
        public void EditPet(Pet pet)
        {
            if (pet == null)
            {
                Console.WriteLine("Pet not found.");
                return;
            }

            Console.WriteLine("=== Edit Pet ===");
            // Common questions (name, species, breed, age)
            Utils.PetInteractor.EditInteractiveFields(pet);

            // Now we handle owner reassignment (if the user modifies it)
            Console.Write($"Owner ({pet.OwnerName}): ");
            string ownerName = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(ownerName) && !ownerName.Equals(pet.OwnerName, StringComparison.OrdinalIgnoreCase))
            {
                var previousOwnerName = pet.OwnerName;
                pet.OwnerName = ownerName;

                // remove previous association (if it existed)
                if (!string.IsNullOrWhiteSpace(previousOwnerName))
                {
                    var previousOwner = _context.Owners.FirstOrDefault(p => p.Name.Equals(previousOwnerName, StringComparison.OrdinalIgnoreCase));
                    if (previousOwner != null)
                    {
                        var m = previousOwner.Pets.FirstOrDefault(x => x.Id == pet.Id);
                        if (m != null) previousOwner.Pets.Remove(m);
                    }
                }

                // associate with the new owner if they exist
                var newOwner = _context.Owners.FirstOrDefault(p => p.Name.Equals(ownerName, StringComparison.OrdinalIgnoreCase));
                if (newOwner != null && !newOwner.Pets.Any(x => x.Id == pet.Id))
                {
                    newOwner.Pets.Add(pet);
                }
            }

            Logger.LogInfo($"Pet updated: {pet.Id}", "PetService.EditPet");
            Console.WriteLine("Pet updated successfully.");
        }

        // Internal helper that removes the pet from the context and notifies OwnerService.
        private bool RemoveByInstance(Pet pet)
        {
            if (pet == null) return false;
            var removed = _context.Pets.Remove(pet);
            if (removed && _ownerService != null)
            {
                _ownerService.RemovePetAssociation(pet.Id);
            }
            return removed;
        }

        // Deletes by Id and updates the association in OwnerService if available
        public void DeleteById(Guid id)
        {
            var pet = FindById(id);
            if (pet == null)
            {
                Console.WriteLine("Could not find the pet to delete.");
                return;
            }
            var removed = RemoveByInstance(pet);
            if (removed)
            {
                Logger.LogInfo($"Pet deleted: {pet.Name}", "PetService.DeleteById");
                Console.WriteLine("Pet deleted from the registry.");
            }
            else
            {
                Console.WriteLine("An error occurred while trying to delete the pet.");
            }
        }

        // Method that returns the pet or null; single point of query.
        public Pet? FindById(Guid id)
        {
            return _context.Pets.FirstOrDefault(x => x.Id == id);
        }
    }
}
