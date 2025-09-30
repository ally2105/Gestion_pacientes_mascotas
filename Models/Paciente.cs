namespace Gestion_pacientes_mascotas.Models
{
    public class Paciente
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public List<Mascota> Mascotas { get; set; }

        public Paciente(string nombre, int edad, string direccion, string telefono)
        {
            Nombre = nombre;
            Edad = edad;
            Direccion = direccion;
            Telefono = telefono;
            Mascotas = new List<Mascota>();
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"Nombre: {Nombre}\nEdad: {Edad}\nDirección: {Direccion}\nTeléfono: {Telefono}");
        }

        public void MostrarMascotas()
        {
            if (Mascotas.Count == 0)
            {
                Console.WriteLine("No tiene mascotas registradas.");
                return;
            }
            Console.WriteLine($"Mascotas de {Nombre}:");
            foreach (var mascota in Mascotas)
            {
                mascota.MostrarInformacion();
                Console.WriteLine("---");
            }
        }
    }
}