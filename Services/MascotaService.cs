using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestion_pacientes_mascotas.Models
{
    public class MascotaService : IRegistrable
    {
        public List<Mascota> Mascotas { get; set; } = new List<Mascota>();
        private readonly PacienteService? _pacienteService;

        // Constructor opcional para permitir que MascotaService notifique a PacienteService
        public MascotaService(PacienteService? pacienteService = null)
        {
            _pacienteService = pacienteService;
        }

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

            Mascota mascota = new Mascota(Guid.NewGuid(), nombre, especie, raza, edad, dueno);
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

        // Edita una mascota buscándola por Id
        public void EditarPorId(Guid id)
        {
            var mascota = BuscarPorId(id);
            if (mascota == null)
            {
                Console.WriteLine("No se encontró la mascota a editar.");
                return;
            }
            EditarMascota(mascota);
        }

        // Internal helper que remueve la mascota del registro global y notifica al PacienteService.
        private bool RemoveByInstance(Mascota mascota)
        {
            if (mascota == null) return false;
            var removed = Mascotas.Remove(mascota);
            if (removed && _pacienteService != null)
            {
                // Llamar al método existente que remueve la asociación en PacienteService
                _pacienteService.RemoverAsociacionMascota(mascota.Id);
            }
            return removed;
        }

        // Elimina por Id y actualiza la asociación en PacienteService si está disponible
        public void EliminarPorId(Guid id)
        {
            var mascota = BuscarPorId(id);
            if (mascota == null)
            {
                Console.WriteLine("No se encontró la mascota a eliminar.");
                return;
            }
            var removed = RemoveByInstance(mascota);
            if (removed)
                Console.WriteLine("Mascota eliminada del registro global.");
            else
                Console.WriteLine("Ocurrió un error al intentar eliminar la mascota.");
        }



        // Método que devuelve la mascota o null; único punto de consulta.
        public Mascota? BuscarPorId(Guid id)
        {
            return Mascotas.FirstOrDefault(x => x.Id == id);
        }
    }
}

