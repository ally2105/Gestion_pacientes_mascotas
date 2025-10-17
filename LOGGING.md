# Error Logging

This project includes a minimalist logger (`Utils/Logger.cs`) that writes timestamped entries to `logs/error.log`.

What it logs
- Errors (exceptions) with type, message, and stack trace.
- Warnings and informational messages.

Format
- Entries use a timestamp in ISO 8601 format (`DateTime.UtcNow:O`) and a readable separator.

Why this helps in a real environment
- Traceability: support can correlate the incident time with server logs.
- Diagnostics: the stack trace and exception type speed up root cause identification.
- Privacy: we do not write sensitive data by default; in production, personal data should be masked or redacted.
- Auditing: logs allow reconstructing the steps before a failure.

Best practices for production
- Use robust frameworks (Serilog, NLog, or Microsoft.Extensions.Logging) with sinks (files, ElasticSearch, Seq).
- Add request correlation (RequestId) to group related logs.
- Configure levels (Error/Warn/Info/Debug) and log rotation to avoid excessive disk usage.
- Encrypt or protect logs containing personal information.

How to use it
- From any catch block: `Logger.LogError(ex, "OptionalContext")`.
- For warnings: `Logger.LogWarning("Message")`.
- For information: `Logger.LogInfo("Message")`.

Log file location: `logs/error.log` in the application's base directory.
