namespace Gestion_pacientes_mascotas.Models
{
    public class Mascota
    {
    private string nombre = string.Empty;
    private string especie = string.Empty;
    private string raza = string.Empty;
    private int edad;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = !string.IsNullOrEmpty(value) ? value : throw new Exception("El nombre no puede estar vacío"); }
        }
        public string Especie
        {
            get { return especie; }
            set { especie = !string.IsNullOrEmpty(value) ? value : throw new Exception("La especie no puede estar vacía"); }
        }
        public string Raza
        {
            get { return raza; }
            set { raza = !string.IsNullOrEmpty(value) ? value : throw new Exception("La raza no puede estar vacía"); }
        }
        public int Edad
        {
            get { return edad; }
            set { edad = value >= 0 ? value : throw new Exception("La edad no puede ser negativa"); }
        }

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
