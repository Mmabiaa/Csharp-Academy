# Docker Deployment Guide

Containerizing C# Academy ensures environment parity across development, testing, and production.

---

## 🏗️ Multi-Container Setup (Docker Compose)

The full stack (DB + API + Frontend) is managed via `docker-compose.yml`. We use a central `docker.env` file to manage environment variables safely.

```yaml
version: '3.8'

services:
  db:
    image: mysql:8.0
    env_file: docker.env
    ports:
      - "3306:3306"
    volumes:
      - mysql_data:/var/lib/mysql

  backend:
    build: 
      context: ./src/backend
      dockerfile: Dockerfile
    env_file: docker.env
    depends_on:
      - db
    ports:
      - "5000:8080"
    restart: always

  frontend:
    build:
      context: ./src/frontend
      dockerfile: Dockerfile
      args:
        - VITE_API_URL=http://localhost:5000/api
        - VITE_GOOGLE_CLIENT_ID=${VITE_GOOGLE_CLIENT_ID}
    ports:
      - "3000:80"
    depends_on:
      - backend
    restart: always

volumes:
  mysql_data:
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
> **Optimization**: This Dockerfile is configured to use the existing `node_modules` from the host to avoid redundant downloads, provided they are compatible.

```dockerfile
FROM node:18-alpine AS build
WORKDIR /app

# Accept build arguments for Vite environment variables
ARG VITE_API_URL
ARG VITE_GOOGLE_CLIENT_ID
ENV VITE_API_URL=$VITE_API_URL
ENV VITE_GOOGLE_CLIENT_ID=$VITE_GOOGLE_CLIENT_ID

COPY . .
# Skip npm install and use local node_modules
RUN npm run build

FROM nginx:stable-alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

---

## 🚀 Running the Containers

1.  **Configuration**: Ensure `docker.env` at the root contains your `GEMINI_API_KEY` and other secrets. This file is git-ignored for safety.
2.  **Launch**: Run the following command from the root:
    ```bash
    docker-compose up --build
    ```
3.  **Access**:
    - **Frontend**: [http://localhost:3000](http://localhost:3000)
    - **Backend API**: [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## 🛡️ Best Practices Implemented
- **Layer Security**: sensitive keys are stored in `docker.env` and excluded from images via `.dockerignore`.
- **Persistence**: A named volume `mysql_data` preserves your database even if containers are destroyed.
- **Build-Time Config**: Vite variables are passed as build arguments to ensure they are baked into the production bundle.
- **Restart Policy**: Containers are set to `always` restart to ensure high availability.
