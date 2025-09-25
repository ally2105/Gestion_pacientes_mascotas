namespace Gestion_pacientes_mascotas.Models
{
    public class PacienteService
    {
        private static byte edad;

        public static void RegistrarPaciente(List<Paciente> Lista)
        {
            Console.WriteLine("===Bienvenido al sistema de registro de pacientes===");
            Console.Write("Ingrese el nombre del paciente: ");
            string nombre = Console.ReadLine();
            try
            {
                Console.Write("Ingrese la edad del paciente: ");
                byte edad = byte.Parse(Console.ReadLine() ?? "0");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: La edad debe ser un número válido.");
                return;
            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: La edad está fuera del rango permitido (0-255).");
                return;
            }
            Console.Write("Ingrese los síntomas del paciente: ");
            string sintomas = Console.ReadLine();
            Paciente nuevoPaciente = new Paciente
            {
                Id = Guid.NewGuid(),
                Nombre = nombre,
                Edad = edad,
                Sintomas = sintomas
            };
            Lista.Add(nuevoPaciente);
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
                Console.WriteLine($"Edad: {paciente.Edad}");
                Console.WriteLine($"Síntomas: {paciente.Sintomas}");
                Console.WriteLine("-------------------------------");
            }
        }
    }
}