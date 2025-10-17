

using Gestion_pacientes_mascotas.Interfaces; 
namespace Gestion_pacientes_mascotas.Models
{
    // They implement both the abstract class and the IAttendable interface.
    // This allows using polymorphism by inheritance (VeterinaryServiceBase) and
    // by contract (IAttendable) as appropriate in different contexts.
    public class GeneralConsultation : VeterinaryServiceBase, IAttendable
    {
        public override void Attend()
        {
            Console.WriteLine("Attending general consultation...");
        }

        // Explicit interface implementation (same method in this case).
        void IAttendable.Attend()
        {
            Attend();
        }
    }

    public class Vaccination : VeterinaryServiceBase, IAttendable
    {
        public override void Attend()
        {
            Console.WriteLine("Performing vaccination...");
        }

        void IAttendable.Attend()
        {
            Attend();
        }
    }
}
