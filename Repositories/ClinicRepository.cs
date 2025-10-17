using Gestion_pacientes_mascotas.Models;
using Gestion_pacientes_mascotas.Utils;

namespace Gestion_pacientes_mascotas.Repositories
{
    public class ClinicRepository
    {
        private readonly List<Owner> owners = new List<Owner>();
        private readonly List<Pet> pets = new List<Pet>();


        // -------------------- OWNERS --------------------

        public void AddOwner(Owner p)
        {
            owners.Add(p);
            Logger.LogInfo($"Owner added: {p.Name}", "ClinicRepository");
        }

        public List<Owner> GetOwners() => owners;

        // -------------------- PETS --------------------

        public void AddPet(Pet m)
        {
            pets.Add(m);
            Logger.LogInfo($"Pet added: {m.Name}", "ClinicRepository");
        }

        public List<Pet> GetPets() => pets;

        public Pet? FindPetById(Guid id)
        {
            var pet = pets.FirstOrDefault(m => m.Id == id);
            if (pet == null)
                throw new PetNotFoundException($"No pet found with Id {id}");
            return pet;
        }

        public void EditPet(Guid id)
        {
            var pet = FindPetById(id);
            if (pet == null)
            {
                Console.WriteLine($"No pet found with Id {id}");
                return;
            }
            Console.WriteLine($"Editing pet: {pet.Name}");
            Utils.PetInteractor.EditInteractiveFields(pet);
            Logger.LogInfo($"Pet {id} updated successfully", "ClinicRepository");
            Console.WriteLine("✅ Pet updated successfully.");
        }

        public void DeletePet(Guid id)
        {
            var pet = FindPetById(id);
            if (pet != null)
            {
                pets.Remove(pet);
                Logger.LogInfo($"Pet deleted: {pet.Name}", "ClinicRepository");
                Console.WriteLine($"✅ Pet {pet.Name} deleted successfully.");
            }
        }
        //---------------------Veterinarians---------------------
        private readonly List<Veterinarian> veterinarians = new List<Veterinarian>();
        public void AddVeterinarian(Veterinarian v)
        {
            veterinarians.Add(v);
            Logger.LogInfo($"Veterinarian added: {v.Name}", "ClinicRepository");
        }
        public List<Veterinarian> GetVeterinarians() => veterinarians;
        //----------------------Appointments-------------------------------
        private readonly List<Appointment> appointments = new List<Appointment>();
        public void AddAppointment(Appointment c)
        {
            appointments.Add(c);
            Logger.LogInfo($"Appointment added for pet: {c.Pet.Name}", "ClinicRepository");
        }
        public List<Appointment> GetAppointments() => appointments;
    }
}
