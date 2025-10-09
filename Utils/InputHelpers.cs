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
                    Console.WriteLine("⚠️ Este campo no puede estar vacío.");
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
                Console.WriteLine("⚠️ Entrada inválida. Intente nuevamente.");
            }
        }

        // Extrae la interacción de edición de una mascota para evitar duplicación
        public static void EditarMascotaInteractiva(Mascota mascota)
        {
            if (mascota == null) return;
            Console.WriteLine("=== Editar Mascota ===");
            Console.Write($"Nombre ({mascota.Nombre}): ");
            string nombre = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(nombre))
                mascota.Nombre = nombre;

            Console.Write($"Especie ({mascota.Especie}): ");
            string especie = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(especie))
                mascota.Especie = especie;

            Console.Write($"Raza ({mascota.Raza}): ");
            string raza = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(raza))
                mascota.Raza = raza;

            Console.Write($"Edad ({mascota.Edad}): ");
            string edadInput = Console.ReadLine() ?? "";
            if (int.TryParse(edadInput, out int edad) && edad >= 0)
                mascota.Edad = edad;

            Console.Write($"Dueño ({mascota.Dueno}): ");
            string dueno = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(dueno))
                mascota.Dueno = dueno;
        }
    }
}
