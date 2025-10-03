namespace Gestion_pacientes_mascotas.Models
{
    // Interfaz ligera que define la capacidad de ser atendido/ejecutado.
    // Útil cuando queremos depender de un contrato sencillo sin forzar
    // herencia a una clase base concreta.
    public interface IAtendible
    {
        void Atender();
    }
}
