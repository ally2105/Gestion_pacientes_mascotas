using System;

namespace Gestion_pacientes_mascotas.Models
{
    public class Veterinarian
    {
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Veterinarian(string name, string specialty, string phone, string email)
        {
            Name = name;
            Specialty = specialty;
            Phone = phone;
            Email = email;
        }

        public void ShowInformation()
        {
            Console.WriteLine($"Name: {Name}\nSpecialty: {Specialty}\nPhone: {Phone}\nEmail: {Email}");
        }
    }
}
