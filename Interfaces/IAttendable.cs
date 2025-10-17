namespace Gestion_pacientes_mascotas.Interfaces
{
    // Lightweight interface that defines the capability of being attended to or executed.
    // Useful when we want to depend on a simple contract without forcing
    // inheritance from a concrete base class.
    public interface IAttendable
    {
        void Attend();
    }
}
