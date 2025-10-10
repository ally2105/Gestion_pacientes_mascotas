using Gestion_pacientes_mascotas.Database;
using Gestion_pacientes_mascotas.Utils;
using Gestion_pacientes_mascotas.Interfaces;
using System;
using System.Linq;

namespace Gestion_pacientes_mascotas.Models
{
    public class MascotaService : IRegistrable
    {
        private readonly DataContext _context;
        private readonly PacienteService? _pacienteService;

        // Constructor: ahora recibe DataContext compartido
        public MascotaService(DataContext context, PacienteService? pacienteService = null)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _pacienteService = pacienteService;
        }

        // Registrar nueva mascota (interactiva)
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
            Console.Write("Dueño (nombre del paciente, opcional): ");
            string dueno = Console.ReadLine() ?? "";

            var mascota = new Mascota(Guid.NewGuid(), nombre, especie, raza, edad, dueno);

            // Agregamos al contexto central
            _context.Mascotas.Add(mascota);

            // Si hay un paciente con ese nombre, también asociamos la mascota a su lista
            if (!string.IsNullOrWhiteSpace(dueno))
            {
                var paciente = _context.Pacientes
                    .FirstOrDefault(p => p.Nombre.Equals(dueno, StringComparison.OrdinalIgnoreCase));
                if (paciente != null)
                {
                    paciente.Mascotas.Add(mascota);
                }
            }

            Logger.LogInfo($"Mascota registrada: {mascota.Nombre}", "MascotaService.Registrar");
            Console.WriteLine("✅ Mascota registrada exitosamente.");
        }

        // Mostrar todas las mascotas desde DataContext
        public void VerMascotas()
        {
            if (!_context.Mascotas.Any())
            {
                Console.WriteLine("No hay mascotas registradas.");
                return;
            }

            Console.WriteLine("=== Lista de Mascotas ===");
            foreach (var mascota in _context.Mascotas)
            {
                mascota.MostrarInformacion();
                Console.WriteLine($"La mascota {mascota.Nombre} hace");
                mascota.EmitirSonido();
                Console.WriteLine("---");
            }
        }

        // Editar usando instancia (mantiene la lógica original de reasignación de dueño)
        public void EditarMascota(Mascota mascota)
        {
            if (mascota == null)
            {
                Console.WriteLine("Mascota no encontrada.");
                return;
            }

            Console.WriteLine("=== Editar Mascota ===");
            // Preguntas comunes (nombre, especie, raza, edad)
            Utils.MascotaInteractor.EditarCamposInteractivos(mascota);

            // Ahora manejamos la reasignación de dueño (si el usuario lo modifica)
            Console.Write($"Dueño ({mascota.Dueno}): ");
            string dueno = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(dueno) && !dueno.Equals(mascota.Dueno, StringComparison.OrdinalIgnoreCase))
            {
                var anterior = mascota.Dueno;
                mascota.Dueno = dueno;

                // remover asociación anterior (si existiera)
                if (!string.IsNullOrWhiteSpace(anterior))
                {
                    var pacienteAnterior = _context.Pacientes.FirstOrDefault(p => p.Nombre.Equals(anterior, StringComparison.OrdinalIgnoreCase));
                    if (pacienteAnterior != null)
                    {
                        var m = pacienteAnterior.Mascotas.FirstOrDefault(x => x.Id == mascota.Id);
                        if (m != null) pacienteAnterior.Mascotas.Remove(m);
                    }
                }

                // asociar al nuevo dueño si existe
                var pacienteNuevo = _context.Pacientes.FirstOrDefault(p => p.Nombre.Equals(dueno, StringComparison.OrdinalIgnoreCase));
                if (pacienteNuevo != null && !pacienteNuevo.Mascotas.Any(x => x.Id == mascota.Id))
                {
                    pacienteNuevo.Mascotas.Add(mascota);
                }
            }

            Logger.LogInfo($"Mascota actualizada: {mascota.Id}", "MascotaService.EditarMascota");
            Console.WriteLine("Mascota actualizada exitosamente.");
        }

        // Internal helper que remueve la mascota del contexto y notifica a PacienteService.
        private bool RemoveByInstance(Mascota mascota)
        {
            if (mascota == null) return false;
            var removed = _context.Mascotas.Remove(mascota);
            if (removed && _pacienteService != null)
            {
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
            {
                Logger.LogInfo($"Mascota eliminada: {mascota.Nombre}", "MascotaService.EliminarPorId");
                Console.WriteLine("Mascota eliminada del registro.");
            }
            else
            {
                Console.WriteLine("Ocurrió un error al intentar eliminar la mascota.");
            }
        }

        // Método que devuelve la mascota o null; único punto de consulta.
        public Mascota? BuscarPorId(Guid id)
        {
            return _context.Mascotas.FirstOrDefault(x => x.Id == id);
        }
    }
}
