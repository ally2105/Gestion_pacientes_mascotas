namespace Gestion_pacientes_mascotas.Models
{
    public class Mascota
    {
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public int Edad { get; set; }

        public Mascota(string nombre, string especie, string raza, int edad)
        {
            Nombre = nombre;
            Especie = especie;
            Raza = raza;
            Edad = edad;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"Nombre: {Nombre}\nEspecie: {Especie}\nRaza: {Raza}\nEdad: {Edad}");
        }
    }
}
