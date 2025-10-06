using System;
using System.Collections.Generic;

namespace Gestion_pacientes_mascotas.Models
{
    public class MascotaService : IRegistrable
    {
        public List<Mascota> Mascotas { get; set; } = new List<Mascota>();

        public void Registrar()
        {
            Console.WriteLine("=== Registro de nueva mascota ===");
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine() ?? "";
            Console.Write("Especie: ");
            string especie = Console.ReadLine() ?? "";
            Console.Write("Raza: ");
            string raza = Console.ReadLine() ?? "";
            Console.Write("Edad: ");
            int edad = 0;
            int.TryParse(Console.ReadLine(), out edad);
            Console.Write("Dueño: ");
            string dueno = Console.ReadLine() ?? "";

            Mascota mascota = new Mascota(nombre, especie, raza, edad, dueno);
            Mascotas.Add(mascota);

            Console.WriteLine("Mascota registrada exitosamente.");
        }
        public void VerMascotas()
        {
            if (Mascotas.Count == 0)
            {
                Console.WriteLine("No hay mascotas registradas.");
                return;
            }

            Console.WriteLine("=== Lista de Mascotas ===");
            foreach (var mascota in Mascotas)
            {
                mascota.MostrarInformacion();
                Console.WriteLine("---");
            }
        }
        public void EditarMascota(Mascota mascota)
        {
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

            Console.WriteLine("Mascota actualizada exitosamente.");
        }
        public void EliminarMascota(Mascota mascota)
        {
            Mascotas.Remove(mascota);
            Console.WriteLine("Mascota eliminada exitosamente.");
        }
    }
}

