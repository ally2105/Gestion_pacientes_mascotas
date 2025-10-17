using System;

namespace Gestion_pacientes_mascotas.Models
{
    public abstract class Animal
    {
        // Animal is abstract because it represents a general concept. Having it
        // as abstract allows defining default behavior and forcing
        // subclasses to override (if necessary) certain methods.
        // We make the setters public to allow editing from services.
        // In stricter applications, it would be preferable to expose modification methods
        // in the entity to maintain invariants.
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }

        public Animal(string name, string species, int age)
        {
            Name = name;
            Species = species;
            Age = age;
        }

        // MakeSound has a default implementation; pets override
        // this method for specific behaviors (polymorphism).
        public virtual void MakeSound()
        {
            Console.WriteLine("The animal makes a sound.");
        }
    }
}