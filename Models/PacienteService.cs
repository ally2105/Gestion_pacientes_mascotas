namespace Gestion_pacientes_mascotas.Models
{
    public class PacienteService
    {
        public static void RegistrarPaciente(List<Paciente> Lista)
        {
            Console.WriteLine("===Bienvenido al sistema de registro de pacientes===");
            Console.Write("Ingrese el nombre del paciente: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese el apellido del paciente: ");
            string apellido = Console.ReadLine();
            Console.Write("Ingrese la edad del paciente: ");
            Byte edad = Byte.Parse(Console.ReadLine());
            Console.Write("Ingrese los síntomas del paciente: ");
            string sintomas = Console.ReadLine();
            Paciente nuevoPaciente = new Paciente
            {
                Id = Guid.NewGuid(),
                Nombre = nombre,
                Apellido = apellido,
                Edad = edad,
                Sintomas = sintomas
            };
            Lista.Add(nuevoPaciente);
            Console.WriteLine("Paciente registrado con éxito.");
        }
        public static void VerPacientes(List<Paciente> Lista)
        {
            Console.WriteLine("===Lista de Pacientes Registrados exitosamente===");
            if (Lista.Count == 0)
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }
            foreach (var paciente in Lista)
            {
                Console.WriteLine($"ID: {paciente.Id}");
                Console.WriteLine($"Nombre: {paciente.Nombre}");
                Console.WriteLine($"Apellido: {paciente.Apellido}");
                Console.WriteLine($"Edad: {paciente.Edad}");
                Console.WriteLine($"Síntomas: {paciente.Sintomas}");
                Console.WriteLine("-------------------------------");
            }
        }
        public static void BuscarPaciente(List<Paciente> Lista)
        {
            Console.Write("Ingrese el nombre del paciente a buscar: ");
            string nombreBusqueda = Console.ReadLine();
            var pacientesEncontrados = Lista.Where(p => p.Nombre.Equals(nombreBusqueda, StringComparison.OrdinalIgnoreCase)).ToList();
            if (pacientesEncontrados.Count == 0)
            {
                Console.WriteLine("No se encontraron pacientes con ese nombre.");
                return;
            }
            Console.WriteLine($"===Resultados de la búsqueda para '{nombreBusqueda}'===");
            foreach (var paciente in pacientesEncontrados)
            {
                Console.WriteLine($"ID: {paciente.Id}");
                Console.WriteLine($"Nombre: {paciente.Nombre}");
                Console.WriteLine($"Apellido: {paciente.Apellido}");
                Console.WriteLine($"Edad: {paciente.Edad}");
                Console.WriteLine($"Síntomas: {paciente.Sintomas}");
                Console.WriteLine("-------------------------------");
            }
        }
    }
}