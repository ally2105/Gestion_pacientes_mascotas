using Gestion_pacientes_mascotas.Models;

namespace Gestion_pacientes_mascotas.Database
{
    /// <summary>
    /// Acts as an in-memory database for the application.
    /// It holds collections of all the main entities (Owners, Pets, etc.).
    /// This single context is shared across all services to ensure data consistency.
    /// </summary>
    public class DataContext
    {
        public List<Owner> Owners { get; set; } = new();
        public List<Pet> Pets { get; set; } = new();
        public List<Veterinarian> Veterinarians { get; set; } = new();
        public List<Appointment> Appointments { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of the DataContext.
        /// </summary>
        public DataContext()
        {
            // Optional example of initial data (you can remove this)
            // var petDemo = new Pet(Guid.NewGuid(), "Luna", "Cat", "Siamese", 3, "Ana");
            // var ownerDemo = new Owner("Ana", 28, "123 Street", "3001234567");
            // ownerDemo.Pets.Add(petDemo);
            // Owners.Add(ownerDemo);
            // Pets.Add(petDemo);
        }

        /// <summary>
        /// A placeholder method for future data persistence logic.
        /// In a real-world application, this would save changes to a database or a file.
        /// </summary>
        public void SaveChanges()
        {
            // For now, it's a simulation; here you could serialize to JSON, etc.
            Console.WriteLine("💾 DataContext: changes saved in memory.");
        }
    }
}
