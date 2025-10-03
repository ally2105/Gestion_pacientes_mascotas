namespace Gestion_pacientes_mascotas.Models
{
    public abstract class Animal
    {
        // Animal es abstracto porque representa un concepto general. Tenerlo
        // como abstracto permite definir comportamiento por defecto y forzar
        // a las subclases a sobrescribir (si es necesario) ciertos métodos.
        public string Nombre { get; protected set; }
        public string Especie { get; protected set; }
        public int Edad { get; protected set; }

        public Animal(string nombre, string especie, int edad)
        {
            Nombre = nombre;
            Especie = especie;
            Edad = edad;
        }

        // EmitirSonido tiene implementación por defecto; las mascotas sobrescriben
        // este método para comportamientos específicos (polimorfismo).
        public virtual void EmitirSonido()
        {
            Console.WriteLine("El animal emite un sonido.");
        }
    }
}