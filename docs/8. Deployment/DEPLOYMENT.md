# Deployment Guide

The C# Academy platform is designed to be easily deployable to modern cloud providers like **Azure**, **AWS**, or a **Private VPS**.

---

## 🏗️ Building for Production

### 1. Frontend (Vite)
Generate a highly-optimized static build:
```bash
cd src/frontend
npm install
npm run build
```
The output will be in `src/frontend/dist`. These files can be hosted on Nginx, Azure Static Web Apps, or Netlify.

### 2. Backend (ASP.NET Core)
Package the API for deployment:
```bash
cd src/backend
dotnet publish -c Release -o ./publish
```
The output can be run as a service, a Docker container, or as an Azure App Service.

---

## ⚙️ Production Checklist

1. **Environment Config**: Set all variables (`CONNECTION_STRING`, `GEMINI_API_KEY`) in the environment settings of your cloud provider.
2. **Database Migrations**: Ensure your production MySQL server is reachable and `dotnet ef database update` has been run against it.
3. **CORS Policy**: Update the `AllowAnyOrigin` in `Program.cs` to explicitly list your production frontend domain (e.g., `https://csharp-academy.com`).
4. **HTTPS**: Both the frontend and API must be served over HTTPS. 
5. **Static Assets**: Ensure the `/attachments` and `/profiles` upload directories have write permissions on the server.

---

## 🐳 Docker (Optional)

A `Dockerfile` can be created to containerize the solution:

```dockerfile
# Example for Backend
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish "CsharpAcademy.Api/CsharpAcademy.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "CsharpAcademy.Api.dll"]
```
