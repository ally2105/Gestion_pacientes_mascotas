namespace Gestion_pacientes_mascotas.Models
{
    public abstract class Animal
    {
        public string Nombre { get; protected set; }
        public string Especie { get; protected set; }
        public int Edad { get; protected set; }

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