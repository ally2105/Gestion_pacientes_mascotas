namespace Gestion_pacientes_mascotas.Models
{
    // Implementan tanto la clase abstracta como la interfaz IAtendible.
    // Esto permite usar polimorfismo por herencia (ServicioVeterinario) y
    // por contrato (IAtendible) según convenga en distintos contextos.
    public class ConsultaGeneral : ServicioVeterinario, IAtendible
    {
        public override void Atender()
        {
            Console.WriteLine("Atendiendo consulta general...");
        }

        // Implementación explícita de la interfaz (mismo método en este caso).
        void IAtendible.Atender()
        {
            Atender();
        }
    }

    public class Vacunacion : ServicioVeterinario, IAtendible
    {
        public override void Atender()
        {
            Console.WriteLine("Realizando vacunación...");
        }

        void IAtendible.Atender()
        {
            Atender();
        }
    }
}
