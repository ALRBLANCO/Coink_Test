# Coink - Prueba Técnica: Registro de Usuarios (Senior Backend):

Este repositorio contiene la solución técnica desarrollada para el proceso de selección de Coink. Se presenta una implementación de API REST construida con .NET 9 y PostgreSQL, diseñada bajo estándares de alta cohesión y principios de ingeniería de software.

## Evaluación técnica:

La evaluación de los requerimientos técnicos requeridos para un postulante se dividirá en 2 secciones, una primera evaluará el conocimiento en Bases de datos, específicamente en diseño y desarrollo en lenguaje SQL. La segunda será el desarrollo en C#, aplicación de patrones de desarrollo y buenas prácticas de desarrollo de software.

**1. Bases de datos:**
* a. Construir un esquema de base de datos que permita registrar el nombre, teléfono y dirección de un usuario.
* b. Construir tablas paramétricas para país, departamento y municipio.
* c. Usar bases de datos relacionales.

**2. Desarrollo C#.**
* a. Deberá crear un Api de servicios que deberá exponer:
    * i. Servicio que permita registrar Nombre, teléfono, País, Departamento, municipio y Dirección.
* b. Los servicios expuestos deberán validar que el dato que se ingrese como parámetro sea válido.
* c. Las consultas en base de datos deberán implementarse a través de consumo de Stored Procedures.
* d. Será apreciado el uso de patrones de diseño.

**Al final se espera repositorio preferiblemente en GitHub:**
* La solución C#.
* Scripts requeridos para la creación de la base de datos y sus respectivas tablas.
* Los scripts requeridos para la creación de los stored procedures.
* De preferencia usar PostgreSQL.

---

## 🏗️ Decisiones de Ingeniería y Arquitectura

En el desarrollo de esta solución, he aplicado criterios que buscan demostrar profundidad técnica y dominio de patrones, excediendo intencionalmente los requerimientos base sin comprometer la claridad:

* **Arquitectura y Patrones:** He decidido implementar una estructura basada en capas, utilizando el **Repository Pattern** para la abstracción de datos y una capa de **Servicios** para la orquestación. Se podrá notar el uso de **Domain Entities** y **Mappers** manuales; tomé esta decisión para evitar el acoplamiento entre los contratos externos (DTOs) y la lógica interna, manteniendo un control total del flujo de datos sin depender de librerías de mapeo automático para efectos de esta prueba.
* **Integridad y Lógica en DB:** He delegado la validación de integridad referencial geográfica (País -> Departamento -> Ciudad) directamente a los **Stored Procedures**, asegurando que el motor de base de datos garantice la consistencia. Complementé esto con **FluentValidation** en C# para validaciones de formato de entrada.
* **Dockerización y Healthchecks:** Para asegurar que la solución sea "Plug & Play", configuré el entorno con **Docker Compose**. Implementé *healthchecks* para que la API espere a que la base de datos esté totalmente lista y los scripts SQL ejecutados antes de iniciar, garantizando que la primera petición no falle por latencia de red interna.
* **Simplicidad y UX del Evaluador:** He tomado decisiones como el **versionamiento estático** (`api/v1`) y dejar las **credenciales en el appsettings.json**. Soy consciente de que en entornos productivos se deben usar Secrets o Variables de Entorno, pero decidí mantenerlas aquí para facilitar la ejecución rapida del test por el evaluador. Asimismo, dejé **Swagger habilitado en producción** dentro del contenedor para que el evaluador pueda probar los endpoints sin herramientas externas.
* **Valor Agregado:** Aunque no estaban contemplados en el requerimiento originales, he incluido **Pruebas Unitarias** con xUnit/Moq, un **Middleware Global de Excepciones** y principios **SOLID**, buscando reflejar el rigor técnico que aplico en proyectos de escala empresarial.

---

## 🚀 Instrucciones de Ejecución

### Opción 1: Docker:
Desde la raíz del proyecto, ejecute:
```bash
docker-compose up --build