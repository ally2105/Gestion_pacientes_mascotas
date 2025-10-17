using System;
using System.Globalization;
using System.Linq;
using Gestion_pacientes_mascotas.Database;
using Gestion_pacientes_mascotas.Utils;
using Gestion_pacientes_mascotas.Interfaces;

namespace Gestion_pacientes_mascotas.Models
{
    /// <summary>
    /// Manages all operations related to appointments, such as scheduling and viewing.
    /// </summary>
    public class AppointmentService
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the AppointmentService with a shared data context.
        /// </summary>
        public AppointmentService(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Guides the user through the process of scheduling a new veterinary appointment.
        /// </summary>
        public void ScheduleAppointment()
        {
            Console.WriteLine("=== 🗓️ Schedule New Appointment ===");

            if (_context.Pets == null || _context.Pets.Count == 0)
            {
                Console.WriteLine("⚠️ No pets registered. Please register a pet first.");
                return;
            }

            if (_context.Veterinarians == null || _context.Veterinarians.Count == 0)
            {
                Console.WriteLine("⚠️ No veterinarians registered. Please register a veterinarian first.");
                return;
            }

            // Select pet
            Console.WriteLine("\n🐾 Available Pets:");
            for (int i = 0; i < _context.Pets.Count; i++)
                Console.WriteLine($"{i + 1}. {_context.Pets[i].Name} ({_context.Pets[i].Species})");
            Console.Write("Select (number): ");
            if (!int.TryParse(Console.ReadLine(), out int petIndex) || petIndex < 1 || petIndex > _context.Pets.Count)
            {
                Console.WriteLine("⚠️ Invalid selection.");
                return;
            }
            var selectedPet = _context.Pets[petIndex - 1];

            // Select veterinarian
            Console.WriteLine("\n👩‍⚕️ Available Veterinarians:");
            for (int i = 0; i < _context.Veterinarians.Count; i++)
                Console.WriteLine($"{i + 1}. {_context.Veterinarians[i].Name} - {_context.Veterinarians[i].Specialty}");
            Console.Write("Select (number): ");
            if (!int.TryParse(Console.ReadLine(), out int vetIndex) || vetIndex < 1 || vetIndex > _context.Veterinarians.Count)
            {
                Console.WriteLine("⚠️ Invalid selection.");
                return;
            }
            var selectedVeterinarian = _context.Veterinarians[vetIndex - 1];

            // Read date and time in a single input (compatible and simple)
            Console.Write("\nEnter date and time (format: yyyy-MM-dd HH:mm): ");
            var dateTimeRaw = Console.ReadLine() ?? "";
            if (!DateTime.TryParseExact(dateTimeRaw.Trim(), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            {
                // more permissive fallback attempt
                if (!DateTime.TryParse(dateTimeRaw, out dateTime))
                {
                    Console.WriteLine("⚠️ Invalid date/time. Use 'yyyy-MM-dd HH:mm' or a valid format.");
                    return;
                }
            }

            // --- Improved Conflict Validation ---
            // Define a standard duration for appointments (e.g., 30 minutes).
            const int appointmentDurationMinutes = 30;
            DateTime newAppointmentStart = dateTime;
            DateTime newAppointmentEnd = newAppointmentStart.AddMinutes(appointmentDurationMinutes);

            // An overlap occurs if the new appointment starts before an existing one ends,
            // AND the new appointment ends after the existing one starts.
            var conflictingAppointment = _context.Appointments.FirstOrDefault(existing =>
                existing.Veterinarian != null &&
                ReferenceEquals(existing.Veterinarian, selectedVeterinarian) &&
                newAppointmentStart < existing.DateTime.AddMinutes(appointmentDurationMinutes) &&
                existing.DateTime < newAppointmentEnd);

            if (conflictingAppointment != null)
            {
                // If a conflict is found, inform the user and stop the process.
                Console.WriteLine($"🚫 Conflict found! Veterinarian {selectedVeterinarian.Name} already has an appointment at {conflictingAppointment.DateTime:HH:mm} that overlaps with the requested time.");
                Console.WriteLine("Please choose a different time or veterinarian.");
                return;
            }

            Console.Write("Reason for the appointment: ");
            string reason = Console.ReadLine() ?? "";

            // Create Appointment using the constructor you already have in Appointment.cs
            var newAppointment = new Appointment(dateTime, reason, selectedPet, selectedVeterinarian);
            _context.Appointments.Add(newAppointment);

            // If the veterinarian implements INotifiable, send a notification (safe)
            if (selectedVeterinarian is INotifiable notifiable)
            {
                notifiable.SendNotification($"New appointment: {selectedPet.Name} - {dateTime:yyyy-MM-dd HH:mm} - {reason}");
            }

            Logger.LogInfo($"Appointment scheduled: {selectedPet.Name} with {selectedVeterinarian.Name} on {dateTime:yyyy-MM-dd HH:mm}", "AppointmentService.ScheduleAppointment");
            Console.WriteLine("✅ Appointment scheduled successfully.");
        }

        /// <summary>
        /// Displays a list of all currently scheduled appointments.
        /// </summary>
        public void ViewAppointments()
        {
            Console.WriteLine("=== 📋 Scheduled Appointments ===");
            if (_context.Appointments == null || _context.Appointments.Count == 0)
            {
                Console.WriteLine("No appointments registered.");
                return;
            }

            foreach (var appointment in _context.Appointments)
            {
                // If your Appointment has ShowInformation(), use it; otherwise, print manually:
                try
                {
                    appointment.ShowInformation();
                }
                catch
                {
                    Console.WriteLine($"Pet: {appointment.Pet?.Name ?? "(no pet)"}");
                    Console.WriteLine($"Veterinarian: {appointment.Veterinarian?.Name ?? "(no vet)"}");
                    Console.WriteLine($"Date and time: {appointment.DateTime:yyyy-MM-dd HH:mm}");
                    Console.WriteLine($"Reason: {appointment.Reason}");
                    Console.WriteLine("-----------------------------");
                }
            }
        }
    }
}
