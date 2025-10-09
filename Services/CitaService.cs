using System;
using System.Globalization;
using System.Linq;
using Gestion_pacientes_mascotas.Database;
using Gestion_pacientes_mascotas.Utils;
using Gestion_pacientes_mascotas.Interfaces;

namespace Gestion_pacientes_mascotas.Models
{
    public class CitaService
    {
        private readonly DataContext _context;

        public CitaService(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Agendar una cita (compatible con tu modelo Cita existente que usa FechaHora)
        public void AgendarCita()
        {
            Console.WriteLine("=== 🗓️ Agendar nueva cita ===");

            if (_context.Mascotas == null || _context.Mascotas.Count == 0)
            {
                Console.WriteLine("⚠️ No hay mascotas registradas. Registra una mascota primero.");
                return;
            }

            if (_context.Veterinarios == null || _context.Veterinarios.Count == 0)
            {
                Console.WriteLine("⚠️ No hay veterinarios registrados. Registra un veterinario primero.");
                return;
            }

            // Seleccionar mascota
            Console.WriteLine("\n🐾 Mascotas disponibles:");
            for (int i = 0; i < _context.Mascotas.Count; i++)
                Console.WriteLine($"{i + 1}. {_context.Mascotas[i].Nombre} ({_context.Mascotas[i].Especie})");
            Console.Write("Selecciona (número): ");
            if (!int.TryParse(Console.ReadLine(), out int idxMascota) || idxMascota < 1 || idxMascota > _context.Mascotas.Count)
            {
                Console.WriteLine("⚠️ Selección inválida.");
                return;
            }
            var mascotaSeleccionada = _context.Mascotas[idxMascota - 1];

            // Seleccionar veterinario
            Console.WriteLine("\n👩‍⚕️ Veterinarios disponibles:");
            for (int i = 0; i < _context.Veterinarios.Count; i++)
                Console.WriteLine($"{i + 1}. {_context.Veterinarios[i].Nombre} - {_context.Veterinarios[i].Especialidad}");
            Console.Write("Selecciona (número): ");
            if (!int.TryParse(Console.ReadLine(), out int idxVet) || idxVet < 1 || idxVet > _context.Veterinarios.Count)
            {
                Console.WriteLine("⚠️ Selección inválida.");
                return;
            }
            var veterinarioSeleccionado = _context.Veterinarios[idxVet - 1];

            // Leer fecha y hora en un solo input (compatible y sencillo)
            Console.Write("\nIngresa fecha y hora (formato: yyyy-MM-dd HH:mm): ");
            var fechaHoraRaw = Console.ReadLine() ?? "";
            if (!DateTime.TryParseExact(fechaHoraRaw.Trim(), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaHora))
            {
                // intento fallback más permisivo
                if (!DateTime.TryParse(fechaHoraRaw, out fechaHora))
                {
                    Console.WriteLine("⚠️ Fecha/hora inválida. Usa 'yyyy-MM-dd HH:mm' o un formato válido.");
                    return;
                }
            }

            // Validación de conflicto: mismo veterinario y misma FechaHora
            bool conflicto = _context.Citas.Any(c =>
                c.Veterinario != null
                && ReferenceEquals(c.Veterinario, veterinarioSeleccionado)
                && c.FechaHora == fechaHora);

            if (conflicto)
            {
                Console.WriteLine($"🚫 El veterinario {veterinarioSeleccionado.Nombre} ya tiene una cita el {fechaHora:yyyy-MM-dd HH:mm}. Elige otra hora o veterinario.");
                return;
            }

            Console.Write("Motivo de la cita: ");
            string motivo = Console.ReadLine() ?? "";

            // Crear Cita usando el constructor que ya tienes en Cita.cs
            var nuevaCita = new Cita(fechaHora, motivo, mascotaSeleccionada, veterinarioSeleccionado);
            _context.Citas.Add(nuevaCita);

            // Si el veterinario implementa INotificable, enviamos notificación (seguro)
            if (veterinarioSeleccionado is INotificable notificado)
            {
                notificado.EnviarNotificacion($"Nueva cita: {mascotaSeleccionada.Nombre} - {fechaHora:yyyy-MM-dd HH:mm} - {motivo}");
            }

            Logger.LogInfo($"Cita agendada: {mascotaSeleccionada.Nombre} con {veterinarioSeleccionado.Nombre} el {fechaHora:yyyy-MM-dd HH:mm}", "CitaService.AgendarCita");
            Console.WriteLine("✅ Cita agendada correctamente.");
        }

        public void VerCitas()
        {
            Console.WriteLine("=== 📋 Citas agendadas ===");
            if (_context.Citas == null || _context.Citas.Count == 0)
            {
                Console.WriteLine("No hay citas registradas.");
                return;
            }

            foreach (var cita in _context.Citas)
            {
                // Si tu Cita tiene MostrarInformacion(), úsalo; si no, imprimimos manualmente:
                try
                {
                    cita.MostrarInformacion();
                }
                catch
                {
                    Console.WriteLine($"Mascota: {cita.Mascota?.Nombre ?? "(sin mascota)"}");
                    Console.WriteLine($"Veterinario: {cita.Veterinario?.Nombre ?? "(sin vet)"}");
                    Console.WriteLine($"Fecha y hora: {cita.FechaHora:yyyy-MM-dd HH:mm}");
                    Console.WriteLine($"Motivo: {cita.Motivo}");
                    Console.WriteLine("-----------------------------");
                }
            }
        }
    }
}
