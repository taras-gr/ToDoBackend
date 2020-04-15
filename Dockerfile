FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ToDo.Api/ToDo.Api.csproj", "ToDo.Api/"]
COPY ["ToDo.Services/ToDo.Services.csproj", "ToDo.Services/"]
COPY ["ToDo.Domain/ToDo.Domain.csproj", "ToDo.Domain/"]
COPY ["ToDo.IoC/ToDo.IoC.csproj", "ToDo.IoC/"]
COPY ["ToDo.Repositories/ToDo.Repositories.csproj", "ToDo.Repositories/"]
RUN dotnet restore "ToDo.Api/ToDo.Api.csproj"
COPY . .
WORKDIR "/src/ToDo.Api"
RUN dotnet build "ToDo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDo.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDo.Api.dll"]