# Gestion_pacientes_mascotas

Pequeña consola en C# para gestionar pacientes y sus mascotas (POCOs + servicios).

Resumen rápido
- Lenguaje: C# (.NET). Proyecto: `Gestion_pacientes_mascotas.csproj`.
- Estructura relevante:
  - `Models/` — entidades: `Paciente`, `Mascota`, `Animal`.
  - `Services/` — lógica de aplicación: `PacienteService`, `MascotaService`, `ServicioVeterinario`.
  - `Interfaces/` — contratos: `IRegistrable`, `IAtendible`, `INotificable`.
  - `Utils/Logger.cs` — logger minimalista que escribe en `logs/error.log`.

Cómo ejecutar
1. Restaurar paquetes y compilar:

```bash
dotnet build
```

2. Ejecutar la aplicación:

```bash
dotnet run --project Gestion_pacientes_mascotas.csproj
```

Puntos de diseño y por qué
- Entidades limpias (POCOs): `Paciente` y `Mascota` no contienen I/O; ayudan a testabilidad.
- Servicios: contienen la lógica de E/S, validaciones y operaciones (p. ej. `PacienteService.Registrar()`).
- Interfaces: usadas para desacoplar y permitir múltiples implementaciones (tests, persistencia).
- Abstractas (p. ej. `Animal`, `ServicioVeterinario`): proporcionan comportamiento compartido y contrato para subclases.

Depuración y pruebas manuales
- Breakpoints útiles:
  - `Program.cs`: en la creación de `PacienteService` y en la llamada a `Registrar()`.
  - `Services/PacienteService.cs`: inicio de `Registrar()`, después de validar `edad`, después de crear `nuevoPaciente`, y justo antes de `pacientes.Add(...)`.
- Forzar error para practicar depuración: en modo DEBUG la aplicación pregunta si deseas forzar un DivideByZeroException durante el registro (útil para ver manejo de excepciones y stack trace).

Registro de errores (logging)
- Archivo: `logs/error.log` (creado por `Utils/Logger.cs`).
- Uso en código: `Logger.LogError(ex, "Contexto")`, `Logger.LogWarning("Mensaje")`.
- En un entorno real: usar Serilog/NLog/Microsoft.Extensions.Logging, configurar rotación, sinks y correlación (RequestId).

Buenas prácticas implementadas
- Validaciones robustas de entrada (int.TryParse, comprobaciones de cadenas, etc.).
- Manejo de excepciones con `try-catch-finally` en puntos críticos; los catches no silencian errores: los registran y muestran mensajes útiles al usuario.
- Excepciones personalizadas: `MascotaNoEncontradaException` para casos de negocio específicos.

Siguientes pasos recomendados
- Extraer la lógica de persistencia a un `IRepository<T>` y proporcionar una implementación en memoria + una con EF Core.
- Reemplazar `Utils/Logger` por Serilog con sinks para archivo y (opcional) ElasticSearch/Seq.
- Añadir tests unitarios para `PacienteService` y `Mascota`.

Contacto y soporte
- Para reproducir un error: ejecutar, anotar la hora y revisar `logs/error.log`.
- En un entorno de soporte técnico, provee el fragmento de log (timestamp + stack trace) para acelerar el diagnóstico.

---

Si quieres, puedo:
- Añadir `IRepository<T>` de ejemplo.
- Integrar Serilog y configurar rotación de logs.
- Crear tests unitarios básicos con xUnit/NUnit.
