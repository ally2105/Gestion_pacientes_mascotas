using System;
using Gestion_pacientes_mascotas.Models;

namespace Gestion_pacientes_mascotas.Utils
{
    // Reusable helper for interactive editing of a Pet's fields.
    // Extracts common questions to avoid duplication between repositories and services.
    public static class PetInteractor
    {
        public static void EditInteractiveFields(Pet pet)
        {
            if (pet == null) return;

            Console.Write("New name (leave empty to keep current): ");
            string name = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(name))
                pet.Name = name;

            Console.Write("New species (leave empty to keep current): ");
            string species = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(species))
                pet.Species = species;

            Console.Write("New breed (leave empty to keep current): ");
            string breed = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(breed))
                pet.Breed = breed;

            Console.Write("New age (leave empty to keep current): ");
            string edadStr = Console.ReadLine() ?? "";
            if (int.TryParse(edadStr, out int edad))
                pet.Age = edad;
        }
    }
}
