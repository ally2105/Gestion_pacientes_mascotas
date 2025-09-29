using System;
using System.Collections.Generic;
using System.Linq; // Necesario para usar LINQ
using Gestion_pacientes_mascotas.Models;

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
            Console.WriteLine("4. Buscar paciente por nombre");
            Console.WriteLine("Escriba 'salir' para terminar");
            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    PacienteService.RegistrarPaciente(pacientesDic);
                    break;

                case "2":
                    PacienteService.VerPacientes(pacientesDic);

                    Console.WriteLine("¿Quiere ver por edad de los pacientes? (si/no)");
                    string filtroEdad = Console.ReadLine();
                    if (filtroEdad.ToLower() == "si")
                    {
                        Console.Write("Ingrese la edad a filtrar: ");
                        if (byte.TryParse(Console.ReadLine(), out byte edadFiltro))
                        {
                            var resultado = pacientesDic.Where(kvp => kvp.Value.Edad == edadFiltro);
                            foreach (var kvp in resultado)
                            {
                                var p = kvp.Value;
                                Console.WriteLine($"ID: {p.Id}, Nombre: {p.Nombre}, Edad: {p.Edad}, Síntomas: {p.Sintomas}, Especie: {p.Especie}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Edad inválida.");
                        }
                    }

                    Console.WriteLine("¿Quiere ver por especie de su mascota? (si/no)");
                    string filtroEspecie = Console.ReadLine();
                    if (filtroEspecie.ToLower() == "si")
                    {
                        Console.Write("Ingrese la especie a filtrar: ");
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
                        Console.WriteLine($"ID: {pacienteBuscado.Id}");
                        Console.WriteLine($"Nombre: {pacienteBuscado.Nombre}");
                        Console.WriteLine($"Edad: {pacienteBuscado.Edad}");
                        Console.WriteLine($"Especie: {pacienteBuscado.Especie}");
                        Console.WriteLine($"Síntomas: {pacienteBuscado.Sintomas}");

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

                            Console.Write("Nueva especie (deje vacío para no cambiar): ");
                            string nuevaEspecie = Console.ReadLine();
                            if (!string.IsNullOrEmpty(nuevaEspecie))
                                pacienteBuscado.Especie = nuevaEspecie;

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

                case "4":
                    PacienteService.BuscarPaciente(pacientesDic);
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
