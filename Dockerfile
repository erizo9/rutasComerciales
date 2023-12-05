# Etapa de Construcción
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /rutascomerciales

# Copiar solo el archivo de proyecto y restaurar dependencias
COPY rutasComerciales.csproj .
RUN dotnet restore

# Copiar los archivos restantes y publicar la aplicación
COPY . .
RUN dotnet publish -c Release -o out

# Etapa de Ejecución
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /rutascomerciales

# Copiar solo los archivos necesarios para la ejecución
COPY --from=build /rutascomerciales/out .

# Configurar el puerto de la aplicación
EXPOSE 6872

# Ejecutar la aplicación al iniciar el contenedor
ENTRYPOINT ["dotnet", "rutasComerciales.dll"]
