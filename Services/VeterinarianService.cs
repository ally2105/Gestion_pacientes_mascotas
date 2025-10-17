using System;
using System.Collections.Generic;
using System.Linq;
using Gestion_pacientes_mascotas.Database;
using Gestion_pacientes_mascotas.Models;

namespace Gestion_pacientes_mascotas.Services
{
    public class VeterinarianService : VeterinaryServiceBase
    {
        private readonly DataContext _context;

        public VeterinarianService(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // Implementation of the abstract method
        public override void Attend()
        {
            Console.WriteLine("🩺 The veterinarian is attending to a patient...");
        }

        // Create a new veterinarian
        public void AddVeterinarian()
        {
            Console.WriteLine("\n=== Veterinarian Registration ===");

            Console.Write("Name: ");
            string? name = Console.ReadLine();
            Console.Write("Specialty: ");
            string? specialty = Console.ReadLine();
            Console.Write("Phone: ");
            string? phone = Console.ReadLine();
            Console.Write("Email: ");
            string? email = Console.ReadLine();

            // Validation of mandatory fields
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(specialty) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("⚠️ All fields are mandatory. Could not register the veterinarian.");
                return;
            }

            // Create and add the veterinarian
            var veterinarian = new Veterinarian(name, specialty, phone, email);
            _context.Veterinarians.Add(veterinarian);

            Console.WriteLine("✅ Veterinarian registered successfully.");
        }

        // Show all registered veterinarians
        public void ListVeterinarians()
        {
            Console.WriteLine("\n=== List of Veterinarians ===");

            if (_context.Veterinarians.Count == 0)
            {
                Console.WriteLine("No veterinarians registered.");
                return;
            }

            int counter = 1;
            foreach (var v in _context.Veterinarians)
            {
                Console.WriteLine($"\n#{counter++}");
                v.ShowInformation();
            }
        }

        // Search for a veterinarian by name
        public void SearchVeterinarian()
        {
            Console.Write("\nEnter the name of the veterinarian to search for: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("⚠️ You must enter a valid name.");
                return;
            }

            var veterinarian = _context.Veterinarians.FirstOrDefault(v =>
                v.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (veterinarian != null)
            {
                Console.WriteLine("\n✅ Veterinarian found:");
                veterinarian.ShowInformation();
            }
            else
            {
                Console.WriteLine("❌ Veterinarian not found.");
            }
        }

        // Edit data of an existing veterinarian
        public void EditVeterinarian()
        {
            Console.Write("\nEnter the name of the veterinarian to edit: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("⚠️ You must enter a valid name.");
                return;
            }

            var veterinarian = _context.Veterinarians.FirstOrDefault(v =>
                v.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (veterinarian == null)
            {
                Console.WriteLine("❌ Veterinarian not found.");
                return;
            }

            Console.WriteLine("\n=== Edit Veterinarian ===");
            Console.Write($"New name (current: {veterinarian.Name}): ");
            string? newName = Console.ReadLine();
            Console.Write($"New specialty (current: {veterinarian.Specialty}): ");
            string? newSpecialty = Console.ReadLine();
            Console.Write($"New phone (current: {veterinarian.Phone}): ");
            string? newPhone = Console.ReadLine();
            Console.Write($"New email (current: {veterinarian.Email}): ");
            string? newEmail = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newName))
                veterinarian.Name = newName;
            if (!string.IsNullOrWhiteSpace(newSpecialty))
                veterinarian.Specialty = newSpecialty;
            if (!string.IsNullOrWhiteSpace(newPhone))
                veterinarian.Phone = newPhone;
            if (!string.IsNullOrWhiteSpace(newEmail))
                veterinarian.Email = newEmail;

            Console.WriteLine("✅ Veterinarian data updated successfully.");
        }

        // Delete a veterinarian by name
        public void DeleteVeterinarian()
        {
            Console.Write("\nEnter the name of the veterinarian to delete: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("⚠️ You must enter a valid name.");
                return;
            }

            var veterinarian = _context.Veterinarians.FirstOrDefault(v =>
                v.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (veterinarian != null)
            {
                _context.Veterinarians.Remove(veterinarian);
                Console.WriteLine("✅ Veterinarian deleted successfully.");
            }
            else
            {
                Console.WriteLine("❌ Veterinarian not found.");
            }
        }
    }
}
