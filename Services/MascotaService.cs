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
    }
}

