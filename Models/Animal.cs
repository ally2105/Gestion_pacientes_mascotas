namespace Gestion_pacientes_mascotas.Models
{
    public class Animal
    {
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public int Edad { get; set; }

        public Animal(string nombre, string especie, int edad)
        {
            Nombre = nombre;
            Especie = especie;
            Edad = edad;
        }

        public  virtual void EmitirSonido()
        {
            Console.WriteLine("El animal emite un sonido.");
        }
    }
}