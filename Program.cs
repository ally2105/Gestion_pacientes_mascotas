using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<Guid, Paciente> pacientesDic = new Dictionary<Guid, Paciente>();
        string opcion = "";

        while (opcion.ToLower() != "salir")
        {
            Console.WriteLine("1. Registrar paciente");
            Console.WriteLine("2. Ver pacientes");
            Console.WriteLine("3. Buscar paciente por ID");
            Console.WriteLine("Escriba 'salir' para terminar");
            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("=== Registro de nuevo paciente ===");
                    Paciente nuevo = new Paciente();

                    nuevo.Id = Guid.NewGuid(); // Generar ID automático con Guid

                    Console.Write("Nombre: ");
                    nuevo.Nombre = Console.ReadLine();

                    Console.Write("Edad: ");
                    nuevo.Edad = byte.Parse(Console.ReadLine());

                    Console.Write("Síntomas: ");
                    nuevo.Sintomas = Console.ReadLine();
                    Console.Write("Especie: ");
                    nuevo.Especie = Console.ReadLine();

                    pacientesDic.Add(nuevo.Id, nuevo);
                    Console.WriteLine($"Paciente registrado con ID: {nuevo.Id}");
                    break;

                case "2":
                    Console.WriteLine("=== Lista de pacientes ===");
                    foreach (var kvp in pacientesDic)
                    {
                        var p = kvp.Value;
                        Console.WriteLine($"ID: {p.Id}, Nombre: {p.Nombre}, Edad: {p.Edad}, Síntomas: {p.Sintomas} Especie: {p.Especie}");
                    }

                    Console.WriteLine("¿Quiere ver por edad de los pacientes? (si/no)");
                    opcion = Console.ReadLine();
                    if (opcion.ToLower() == "si")
                    {
                        Console.WriteLine("Ingrese la edad a filtrar:");
                        byte edadFiltro = byte.Parse(Console.ReadLine());
                        var resultado = pacientesDic.Where(kvp => kvp.Value.Edad == edadFiltro);
                        foreach (var kvp in resultado)
                        {
                            var p = kvp.Value;
                            Console.WriteLine($"ID: {p.Id}, Nombre: {p.Nombre}, Edad: {p.Edad}, Síntomas: {p.Sintomas} Especie: {p.Especie} ");
                        }
                    }
                    Console.WriteLine("Quiere ver por especie de su mascota? (si/no)");
                    opcion = Console.ReadLine();
                    if (opcion.ToLower() == "si")
                    {
                        Console.WriteLine("Ingrese la especie a filtrar:");
                        string especieFiltro = Console.ReadLine();
                        var resultado = pacientesDic.Where(kvp => kvp.Value.Especie.Equals(especieFiltro, StringComparison.OrdinalIgnoreCase));
                        foreach (var kvp in resultado)
                        {
                            var p = kvp.Value;
                            Console.WriteLine($"ID: {p.Id}, Nombre: {p.Nombre}, Edad: {p.Edad}, Síntomas: {p.Sintomas}, Especie: {p.Especie}");
                        }
                    }
                    break;


                case "3":
                    Console.Write("Ingrese ID del paciente a buscar (copie y pegue el GUID): ");
                    string idInput = Console.ReadLine();

                    if (Guid.TryParse(idInput, out Guid idBuscar) &&
                        pacientesDic.TryGetValue(idBuscar, out Paciente pacienteBuscado))
                    {
                        Console.WriteLine($"ID: {pacienteBuscado.Id}, Nombre: {pacienteBuscado.Nombre}, Edad: {pacienteBuscado.Edad}, Síntomas: {pacienteBuscado.Sintomas} Especie: {pacienteBuscado.Especie}");

                        Console.WriteLine("1. Editar paciente");
                        Console.WriteLine("2. Eliminar paciente");
                        Console.WriteLine("Otro número para regresar al menú principal");
                        Console.Write("Seleccione una opción: ");
                        string subOpcion = Console.ReadLine();

                        if (subOpcion == "1")
                        {
                            Console.Write("Nuevo nombre (deje vacío para no cambiar): ");
                            string nuevoNombre = Console.ReadLine();
                            if (!string.IsNullOrEmpty(nuevoNombre))
                                pacienteBuscado.Nombre = nuevoNombre;

                            Console.Write("Nueva edad (deje vacío para no cambiar): ");
                            string nuevaEdad = Console.ReadLine();
                            if (!string.IsNullOrEmpty(nuevaEdad) && byte.TryParse(nuevaEdad, out byte edadNueva))
                                pacienteBuscado.Edad = edadNueva;

                            Console.Write("Nuevos síntomas (deje vacío para no cambiar): ");
                            string nuevosSintomas = Console.ReadLine();
                            if (!string.IsNullOrEmpty(nuevosSintomas))
                                pacienteBuscado.Sintomas = nuevosSintomas;

                            Console.WriteLine("Paciente actualizado.");
                        }
                        else if (subOpcion == "2")
                        {
                            pacientesDic.Remove(idBuscar);
                            Console.WriteLine("Paciente eliminado.");
                        }
                        else
                        {
                            Console.WriteLine("Regresando al menú principal.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ID inválido o paciente no encontrado.");
                    }
                    break;

                case "salir":
                    Console.WriteLine("Saliendo...");
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

public class Paciente
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public byte Edad { get; set; }
    public string Sintomas { get; set; }
    public string Especie { get; set; }
}

