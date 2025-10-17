namespace Gestion_pacientes_mascotas.Interfaces
{
    // Design: IRegisterable is a lightweight interface that defines the
    // registration capability. We use it in services (OwnerService, PetService)
    // because different implementations can register entities in
    // different sources (in-memory list, database, file, API).
    //
    // Why an interface here:
    // - It doesn't need to share implementation, only a contract.
    // - It allows for dependency injection and testing with mocks.
    // - It makes the code more flexible and decoupled.
    public interface IRegisterable
    {
        void Register();
    }
}
