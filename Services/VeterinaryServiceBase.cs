namespace Gestion_pacientes_mascotas.Models
{
    // Design: VeterinaryServiceBase is an abstract class because it provides
    // a contract with possible state or shared logic among concrete
    // services (e.g., common handling of logs, validations, etc.).
    //
    // When to use abstract class vs. interface:
    // - Use an abstract class if there is common implementation or fields/protected members
    //   to share among subclasses.
    // - Use an interface if you only need a contract without implementation.
    public abstract class VeterinaryServiceBase
    {
        // Method that concrete services must implement
        public abstract void Attend();
    }
}
