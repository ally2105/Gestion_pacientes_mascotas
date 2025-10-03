namespace Gestion_pacientes_mascotas.Models
{
    // Diseño: ServicioVeterinario es una clase abstracta porque proporciona
    // un contrato con posible estado o lógica compartida entre servicios
    // concretos (por ejemplo, manejo común de logs, validaciones, etc.).
    //
    // Cuando usar abstracta vs interfaz:
    // - Use una clase abstracta si hay implementación común o campos/protected
    //   que compartir entre subclases.
    // - Use una interfaz si sólo necesita un contrato sin implementación.
    public abstract class ServicioVeterinario
    {
        // Método que deben implementar los servicios concretos
        public abstract void Atender();
    }
}
