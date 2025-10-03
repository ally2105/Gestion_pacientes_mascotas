namespace Gestion_pacientes_mascotas.Models
{
    // Diseño: IRegistrable es una interfaz ligera que define la capacidad
    // de registro. La usamos en los servicios (PacienteService, MascotaService)
    // porque diferentes implementaciones pueden registrar entidades en
    // orígenes distintos (lista en memoria, base de datos, archivo, API).
    //
    // Por qué interfaz aquí:
    // - No necesita compartir implementación, solo contrato.
    // - Permite inyección de dependencias y pruebas mediante mocks.
    // - Hace el código más flexible y desacoplado.
    public interface IRegistrable
    {
        void Registrar();
    }
}
