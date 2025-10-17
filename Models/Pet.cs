using System;

namespace Gestion_pacientes_mascotas.Models
{
    public class Pet : Animal
    {
        private string breed = string.Empty;
        private string ownerName = string.Empty;

        public string Breed
        {
            get { return breed; }
            set { breed = !string.IsNullOrEmpty(value) ? value : throw new Exception("Breed cannot be empty"); }
        }

        public string OwnerName
        {
            get { return ownerName; }
            set { ownerName = !string.IsNullOrEmpty(value) ? value : throw new Exception("Owner name cannot be empty"); }
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public Pet(Guid id, string name, string species, string breed, int age, string ownerName = "")
            : base(name, species, age)
        {
            Id = id;
            Breed = breed;
            OwnerName = ownerName;
        }

        public override void MakeSound()
        {
            switch (Species.ToLower())
            {
                case "dog":
                    Console.WriteLine("Woof 🦮");
                    break;
                case "cat":
                    Console.WriteLine("Meow 🐱");
                    break;
                case "bird":
                    Console.WriteLine("Chirp 🐣");
                    break;
                case "cow":
                    Console.WriteLine("Moo 🐄");
                    break;
                case "sheep":
                    Console.WriteLine("Baaa 🐑");
                    break;
                case "pig":
                    Console.WriteLine("Oink 🐷");
                    break;
                default:
                    Console.WriteLine("The animal makes a sound.");
                    break;
            }
        }

        public void ShowInformation()
        {
            Console.WriteLine($"Name: {Name}\nSpecies: {Species}\nBreed: {Breed}\nAge: {Age}\nOwner: {OwnerName}\nID: {Id}");
        }
    }
}
