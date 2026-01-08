Coink - User Registration API (.NET 9)
Este proyecto es una API REST diseñada para el registro de usuarios, cumpliendo con los requerimientos técnicos de validación de datos, persistencia en PostgreSQL mediante procedimientos almacenados y contenedores Docker.

Arquitectura y Tecnologías
Framework: .NET 9 (C# 12+)

Nota:
## Consideraciones de Arquitectura

Para efectos de esta prueba técnica, he tomado las siguientes decisiones de diseño:

* ** Omití la creación de una capa de Entidades de Dominio y Mapeadores (Mappers) manuales por simplicidad y agilidad. El flujo de datos se maneja directamente a través de DTOs (Data Transfer Objects).
* ** Sin embargo, deje por ahi libre los archivos de clases User.cs y UserDtoMapper.cs.
* ** El desarrollo lo enfoqué primordialmente en:
* ** Integridad Referencial:** Validación estricta de la jerarquía País -> Departamento -> Ciudad antes de cualquier registro.
* ** Pruebas Unitarias e Integración:** Implementación de tests con Mocks y pruebas de integración que validan el comportamiento real de los Stored Procedures.
* ** Robustez:** Implementación de un Middleware global de excepciones para garantizar respuestas estandarizadas y evitar errores 500 no controlados.

Acceso a Datos: Dapper (Micro-ORM) para alto rendimiento.

Base de Datos: PostgreSQL 16 con Stored Procedures.

Validación: FluentValidation para reglas de negocio (Teléfono numérico, campos obligatorios).

Contenedores: Docker Compose para orquestación de BD.

Pruebas: xUnit para Validaciones de negocio.

Instalación y Configuración
1. Levantar la Base de Datos
Desde la raíz Coink_Test/, ejecuta el siguiente comando para levantar el contenedor de PostgreSQL.

Nota: Se ha configurado el puerto 5433 para evitar conflictos con instalaciones locales de Postgres.

Bash

docker-compose up -d
2. Ejecutar la API
Debe Navegar a la carpeta del proyecto y lanza la aplicación:

Bash

cd UserRegistrationApi
dotnet run
La API estará disponible en: http://localhost:5233 (Swagger se cargará automáticamente en la raíz).

Pruebas Automatizadas
Para ejecutar los tests unitarios de validación de datos:

Bash

dotnet test
📁 Estructura del Proyecto
Plaintext

UserRegistrationApi/
├── Program.cs                 # Configuración de servicios y middleware
├── appsettings.json           # Cadena de conexión (Puerto 5433)
├── src/
│   ├── Controllers/           # Endpoints de la API
│   ├── Data/                  # Repositorios e interfaces (Dapper)
│   ├── Models/                # DTOs de entrada
│   └── Validators/            # Reglas de FluentValidation (Requerimiento 2.b)
└── tests/
    └── UserRegistrationApi.Tests/  # Pruebas xUnit
Notas de Implementación
Sue expresión regular para el campo Phone que asegura un formato numérico internacional y longitud válida.

Manejo de Errores: La API captura errores específicos de integridad referencial (CityId inexistente) y errores de autenticación de BD, devolviendo códigos HTTP adecuados.

usé Docker para asegurar que los scripts SQL en Database/Scripts/ inicialicen la base de datos automáticamente al primer arranque.