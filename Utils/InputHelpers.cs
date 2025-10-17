using System;
using Gestion_pacientes_mascotas.Models;

namespace Gestion_pacientes_mascotas.Utils
{
    public static class InputHelpers
    {
        public static string ReadRequired(string prompt)
        {
            string value;
            do
            {
                Console.Write(prompt);
                value = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(value))
                    Console.WriteLine("⚠️ This field cannot be empty.");
            } while (string.IsNullOrWhiteSpace(value));
            return value;
        }

        public static int ReadInt(string prompt, Func<int, bool>? validator = null)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine() ?? "";
                if (int.TryParse(input, out result) && (validator == null || validator(result)))
                    return result;
                Console.WriteLine("⚠️ Invalid input. Please try again.");
            }
        }

        // Extracts the pet editing interaction to avoid duplication
        public static void EditPetInteractively(Pet pet)
        {
            if (pet == null) return;
            Console.WriteLine("=== Edit Pet ===");
            Console.Write($"Name ({pet.Name}): ");
            string name = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(name))
                pet.Name = name;

            Console.Write($"Species ({pet.Species}): ");
            string species = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(species))
                pet.Species = species;

            Console.Write($"Breed ({pet.Breed}): ");
            string breed = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(breed))
                pet.Breed = breed;

            Console.Write($"Age ({pet.Age}): ");
            string edadInput = Console.ReadLine() ?? "";
            if (int.TryParse(edadInput, out int edad) && edad >= 0)
                pet.Age = edad;

            Console.Write($"Owner ({pet.OwnerName}): ");
            string ownerName = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(ownerName))
                pet.OwnerName = ownerName;
        }
    }
}
