# GymManagement — Sistema de Gestión de Gimnasio

Proyecto Final Grupal — Programación Web — ITM 2026

## Integrantes

- Juan Esteban Montoya Mejía

## Descripción

Sistema web full-stack para la gestión integral de un gimnasio: miembros, entrenadores, clases grupales, membresías e inscripciones.

## Tecnologías

| Capa | Tecnología |
|------|-----------|
| Backend | .NET 10 Web API |
| ORM | Entity Framework Core 10 (Code-First) |
| Base de datos | SQLite |
| Mapeo | AutoMapper 12 |
| Documentación | Swagger (Swashbuckle) |
| Frontend | React 19 + Vite |
| Estilos | Bootstrap 5 |
| HTTP Client | Axios |
| Routing | React Router v7 |

## Arquitectura Backend

```
GymManagement.Domain/       → Entidades, Enums, Interfaces
GymManagement.DataAccess/   → DbContext, Repositories, Services, DataSeeder
GymManagement.API/          → Controllers, DTOs, Mappings, Program.cs
```

### Entidades (5 mínimas)
- **Member** — Miembros del gimnasio
- **Trainer** — Entrenadores
- **GymClass** — Clases grupales
- **Enrollment** — Inscripciones (relación N:M entre Member y GymClass)
- **Membership** — Membresías de miembros (relación 1:N con Member)

### Enums
- `MembershipType`: Monthly, Quarterly, Annual
- `MembershipStatus`: Pending, Active, Expired, Cancelled
- `ClassStatus`: Scheduled, InProgress, Completed, Cancelled

## Requisitos previos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org/)

## Ejecutar el Backend

```bash
cd GymManagement.API
dotnet run
```

La API quedará en: `http://localhost:5096`  
Swagger UI en: `http://localhost:5096/swagger`

La base de datos SQLite (`gymmanagement.db`) se crea automáticamente con datos de prueba al primer inicio.

## Ejecutar el Frontend

```bash
cd gym-frontend
npm install
npm run dev
```

El frontend quedará en: `http://localhost:5173`

## Endpoints disponibles

| Recurso | Endpoints |
|---------|-----------|
| Members | GET/POST `/api/members`, GET/PUT/DELETE `/api/members/{id}` |
| Trainers | GET/POST `/api/trainers`, GET/PUT/DELETE `/api/trainers/{id}` |
| GymClasses | GET/POST `/api/gymclasses`, GET/PUT/DELETE `/api/gymclasses/{id}` |
| Enrollments | GET/POST `/api/enrollments`, PATCH `/api/enrollments/{id}/unenroll` |
| Memberships | GET/POST `/api/memberships`, GET/PUT/DELETE `/api/memberships/{id}` |
