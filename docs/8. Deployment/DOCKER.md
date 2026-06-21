# Docker Deployment Guide

Containerizing C# Academy ensures environment parity across development, testing, and production.

---

## 🏗️ Multi-Container Setup (Docker Compose)

The easiest way to run the full stack (DB + API + Frontend) is via `docker-compose.yml`.

```yaml
version: '3.8'
services:
  db:
    image: mysql:8.0
    environment:
      MYSQL_ROOT_PASSWORD: root
      MYSQL_DATABASE: csharpacademy
    ports:
      - "3306:3306"

  backend:
    build: ./src/backend
    environment:
      - CONNECTION_STRING=Server=db;Database=csharpacademy;Uid=root;Pwd=root;
      - GEMINI_API_KEY=${GEMINI_API_KEY}
    depends_on:
      - db
    ports:
      - "5000:8080"

  frontend:
    build: ./src/frontend
    ports:
      - "3000:80"
    depends_on:
      - backend
```

---

## 🛠️ Individual Dockerfiles

### 1. Backend (`src/backend/Dockerfile`)
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish "CsharpAcademy.Api/CsharpAcademy.Api.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "CsharpAcademy.Api.dll"]
```

### 2. Frontend (`src/frontend/Dockerfile`)
```dockerfile
FROM node:18-alpine AS build
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build

FROM nginx:stable-alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

---

## 🚀 Running the Containers

1.  Ensure you have **Docker Desktop** installed.
2.  Define your `GEMINI_API_KEY` in your shell environment.
3.  Run the stack:
    ```bash
    docker-compose up --build
    ```
4.  Access the platform:
    - **Frontend**: http://localhost:3000
    - **Backend API**: http://localhost:5000/swagger

---

## 🛡️ Important Notes
- **Persistence**: For production, ensure you use `volumes` in your compose file for the `db` service to prevent data loss when containers restart.
- **Networking**: In Docker Compose, the backend should connect to `db` (the service name) instead of `localhost`.
