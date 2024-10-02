# Use uma imagem base do .NET para o runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use a imagem do SDK para a fase de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CRUD/CRUD.csproj", "CRUD/"]
RUN dotnet restore "CRUD/CRUD.csproj"
COPY . .
WORKDIR "/src/CRUD"
RUN dotnet build "CRUD.csproj" -c Release -o /app/build

# Publicação da aplicação
FROM build AS publish
RUN dotnet publish "CRUD.csproj" -c Release -o /app/publish

# Imagem final para execução
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRUD.dll"]
