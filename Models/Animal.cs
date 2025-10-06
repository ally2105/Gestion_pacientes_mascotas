using System;

namespace Gestion_pacientes_mascotas.Models
{
    public abstract class Animal
    {
        // Animal es abstracto porque representa un concepto general. Tenerlo
        // como abstracto permite definir comportamiento por defecto y forzar
        // a las subclases a sobrescribir (si es necesario) ciertos métodos.
    // Hacemos los setters públicos para permitir edición desde servicios.
    // En aplicaciones más estrictas se preferiría exponer métodos de modificación
    // en la entidad para mantener invariantes.
    public string Nombre { get; set; }
    public string Especie { get; set; }
    public int Edad { get; set; }

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