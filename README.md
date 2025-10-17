## Patient and Pet Management

A C# (.NET) console application to manage patients and their pets using POCOs and services, following best practices in architecture and design.

## Project Overview

- Language: C# (.NET 6+ recommended)

- Project: Gestion_pacientes_mascotas.csproj

- Architecture:

- Models/ — Clean entities (POCOs): Paciente, Mascota, Animal (abstract).

- Services/ — Business and application logic: PacienteService, MascotaService, ServicioVeterinario (abstract).

- Interfaces/ — Contracts for decoupling implementations: IRegistrable, IAtendible, INotificable.

- Utils/Logger.cs — Minimalistic logger for error logging (logs/error.log).

## How to Run

1. Restore packages and build:

dotnet build

2. Run the application:

dotnet run --project Gestion_pacientes_mascotas.csproj

## Design and Rationale
- POCOs (Plain Old CLR Objects):

Entities without I/O logic or direct dependencies to ease testing and maintenance.

- Services:

Contain business logic, validations, and operational workflows. For example: PacienteService.Registrar().

- Interfaces:

Promote decoupling and allow multiple implementations (e.g., in-memory or real database repositories).

- Abstract classes:

Define shared behavior and contracts for subclasses (like Animal or ServicioVeterinario).

##  Debugging and Manual Testing
- Recommended breakpoints:

  - Program.cs: service creation and key method calls (Registrar()).

  - Services/PacienteService.cs: critical points such as start/end of registration, validations, and list additions.

- Error simulation:

  - In DEBUG mode, the app prompts if you want to force a   DivideByZeroException to practice exception handling and stack trace analysis.

## Implemented Best Practices

- Thorough validations (using TryParse, null/empty checks).

- Robust exception handling with logs and user-friendly messages.

- Custom exceptions for business cases (e.g., MascotaNotFoundException).

- Clean, modular code facilitating extensibility.

## Support and Contact

- To reproduce an error:
- Run the app, note the time, and check logs/error.log for the relevant entry.

- For technical support:
- Provide log snippets (timestamp + stack trace) to speed up diagnosis.