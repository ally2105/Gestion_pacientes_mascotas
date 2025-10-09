using System;
using Gestion_pacientes_mascotas.Models;

namespace Gestion_pacientes_mascotas.Utils
{
    // Helper reutilizable para la edición interactiva de campos de una Mascota.
    // Extrae las preguntas comunes para evitar duplicación entre repositorios y servicios.
    public static class MascotaInteractor
    {
        public static void EditarCamposInteractivos(Mascota mascota)
        {
            if (mascota == null) return;

            Console.Write("Nuevo nombre (dejar vacío para mantener): ");
            string nombre = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(nombre))
                mascota.Nombre = nombre;

            Console.Write("Nueva especie (dejar vacío para mantener): ");
            string especie = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(especie))
                mascota.Especie = especie;

            Console.Write("Nueva raza (dejar vacío para mantener): ");
            string raza = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(raza))
                mascota.Raza = raza;

            Console.Write("Nueva edad (dejar vacío para mantener): ");
            string edadStr = Console.ReadLine() ?? "";
            if (int.TryParse(edadStr, out int edad))
                mascota.Edad = edad;
        }
    }
}
