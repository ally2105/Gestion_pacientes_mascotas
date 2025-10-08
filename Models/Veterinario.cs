using System;

namespace Gestion_pacientes_mascotas.Models
{
    public class Veterinario
    {
        public string Nombre { get; set; }
        public string Especialidad { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public Veterinario(string nombre, string especialidad, string telefono, string email)
        {
            Nombre = nombre;
            Especialidad = especialidad;
            Telefono = telefono;
            Email = email;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"Nombre: {Nombre}\nEspecialidad: {Especialidad}\nTeléfono: {Telefono}\nEmail: {Email}");
        }
    }
}
