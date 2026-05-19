[![.NET 8](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)
[![EF Core](https://img.shields.io/badge/EF_Core-8.0-blue.svg)](https://docs.microsoft.com/ef/core/)
[![Arquitectura](https://img.shields.io/badge/Architecture-DDD_%7C_N--Tier-brightgreen.svg)]()

Versión modernizada y educativa de la aplicación de ejemplo oficial del libro *"Guía de Arquitectura N-Capas Orientada al Dominio"* de Microsoft. Este proyecto implementa un módulo bancario (transferencias) utilizando buenas prácticas, inyección de dependencias y pruebas unitarias.

---

## 🏗️ Estructura de la Arquitectura (DDD)

El proyecto está diseñado siguiendo una estricta separación de responsabilidades:

* **🧠 NLayerApp.Dominio:** El corazón del negocio. Contiene las Entidades (`CuentaBancaria`, `Cliente`), Reglas de Negocio encapsuladas e Interfaces (Contratos). *Cero dependencias externas.*
* **⚙️ NLayerApp.Aplicacion:** La capa orquestadora. Contiene Casos de Uso (`AppServicioBancario`) y DTOs para evitar exponer las entidades puras a la web.
* **💾 NLayerApp.Infraestructura:** El acceso a datos. Implementa el patrón Repositorio y UnidadDeTrabajo utilizando **Entity Framework Core 8** y SQL Server LocalDB.
* **🖥️ NLayerApp.Presentacion.Api:** La puerta de entrada. API RESTful documentada con **Swagger** que actúa como *Composition Root* para inyectar dependencias.

---

## 🚀 Cómo ejecutar el proyecto localmente

### Requisitos Previos
* [Visual Studio 2026](https://visualstudio.microsoft.com/)
* SDK de .NET 8.0
* SQL Server 2025 LocalDB (Instalado por defecto con VS 2026)

### Pasos
1. Clona este repositorio:
   ```bash
   git clone https://github.com/dominguezm/NLayerApp.Net8.git
   ```
2. Abre la solución `NLayerApp.Net8.sln` en Visual Studio 2026.
3. Asegúrate de que `NLayerApp.Presentacion.Api` esté establecido como el Proyecto de inicio.
	
4. Abre la Consola del Administrador de Paquetes (Herramientas > Administrador de paquetes NuGet), selecciona el proyecto NLayerApp.Infrastructure y ejecuta:
	```bash
	Update-Database
	```
5. Presiona F5 para compilar, arrancar la base de datos y lanzar la interfaz de pruebas de Swagger.

### Pruebas (Testing)
El proyecto incluye un conjunto de pruebas unitarias implementadas con xUnit y Moq para validar la integridad transaccional:

- Pruebas de reglas de negocio en Memoria (Dominio).

- Pruebas de orquestación con Mocks (Aplicación).