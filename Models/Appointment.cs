using System;

namespace Gestion_pacientes_mascotas.Models
{
    public class Appointment
    {
        public DateTime DateTime { get; set; }
        public string Reason { get; set; }
        public Pet Pet { get; set; }
        public Veterinarian Veterinarian { get; set; }

        public Appointment(DateTime dateTime, string reason, Pet pet, Veterinarian veterinarian)
        {
            DateTime = dateTime;
            Reason = reason;
            Pet = pet;
            Veterinarian = veterinarian;
        }

        public void ShowInformation()
        {
            Console.WriteLine($"Date and Time: {DateTime}\nReason: {Reason}\n--- Pet Information ---");
            Pet.ShowInformation();
            Console.WriteLine("--- Veterinarian Information ---");
            Veterinarian.ShowInformation();
        }
    }
}
