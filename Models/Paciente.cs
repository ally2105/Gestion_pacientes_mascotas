namespace Gestion_pacientes_mascotas.Models;

public class Paciente
{
    private string direccion = string.Empty;
    private string telefono = string.Empty;

    public string Nombre { get; set; }

    private int edad;
    public int Edad
    {
        get { return edad; }
        set
        {
            if (value >= 0)
                edad = value;
            else
                throw new Exception("La edad no puede ser negativa");
        }
    }

    public string Direccion
    {
        get { return direccion; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                direccion = value;
            else
                throw new Exception("La dirección no puede estar vacía");
        }
    }

    public string Telefono
    {
        get { return telefono; }
        set
        {
            if (value.Length == 10)
                telefono = value;
            else
                throw new Exception("El teléfono debe tener 10 dígitos");
        }
    }

    public List<Mascota> Mascotas { get; set; }

    public Paciente(string nombre, int edad, string direccion, string telefono)
    {
        Nombre = nombre;
        Edad = edad;
        Direccion = direccion;
        Telefono = telefono;
        Mascotas = new List<Mascota>();
    }

    public void MostrarInformacion()
    {
        Console.WriteLine($"Nombre: {Nombre}\nEdad: {Edad}\nDirección: {Direccion}\nTeléfono: {Telefono}");
    }

    public void MostrarMascotas()
    {
        if (Mascotas == null || Mascotas.Count == 0)
        {
            Console.WriteLine("No tiene mascotas registradas.");
            return;
        }
        Console.WriteLine($"Mascotas de {Nombre}:");
        foreach (var mascota in Mascotas)
        {
            mascota.MostrarInformacion();
            Console.WriteLine("---");
        }
    }
}




