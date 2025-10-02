namespace Gestion_pacientes_mascotas.Models
{
    public class ConsultaGeneral : ServicioVeterinario
    {
        public override void Atender()
        {
            Console.WriteLine("Atendiendo consulta general...");
        }
    }

    public class Vacunacion : ServicioVeterinario
    {
        public override void Atender()
        {
            Console.WriteLine("Realizando vacunación...");
        }
    }
}
