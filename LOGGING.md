# Registro de errores (Logging)

Este proyecto incluye un logger minimalista (`Utils/Logger.cs`) que escribe entradas con timestamp en `logs/error.log`.

Qué registra
- Errores (excepciones) con tipo, mensaje y stack trace.
- Advertencias y mensajes informativos.

Formato
- Las entradas usan timestamp en formato ISO 8601 (`DateTime.UtcNow:O`) y un separador legible.

Por qué esto ayuda en un entorno real
- Trazabilidad: el soporte puede correlacionar la hora del incidente con registros del servidor.
- Diagnóstico: el stack trace y el tipo de excepción aceleran la identificación de la causa raíz.
- Privacidad: no escribimos datos sensibles por defecto; en producción se deben enmascarar o redaccionar datos personales.
- Auditoría: los registros permiten reconstruir pasos antes de un fallo.

Buenas prácticas para producción
- Usar frameworks robustos (Serilog, NLog o Microsoft.Extensions.Logging) con sinks (files, ElasticSearch, Seq).
- Añadir correlación de requests (RequestId) para agrupar logs relacionados.
- Configurar niveles (Error/Warn/Info/Debug) y rotación de logs para evitar uso excesivo de disco.
- Encriptar o proteger logs que contengan información personal.

Cómo usarlo
- Desde cualquier catch: `Logger.LogError(ex, "ContextoOpcional")`.
- Para advertencias: `Logger.LogWarning("Mensaje")`.
- Para información: `Logger.LogInfo("Mensaje")`.

Ubicación del archivo de logs: `logs/error.log` en el directorio base de la aplicación.
