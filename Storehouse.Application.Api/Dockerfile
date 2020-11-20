FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Storehouse.Application.Api/*.csproj", "Storehouse.Application.Api/"]
COPY ["Application.Crosscutting.Filters/*.csproj", "Application.Crosscutting.Filters/"]
COPY ["Storehouse.Domain/*.csproj", "Storehouse.Domain/"]
COPY ["Domain.Crosscutting.MessageBroker/*.csproj", "Domain.Crosscutting.MessageBroker/"]
COPY ["Domain.Crosscutting/*.csproj", "Domain.Crosscutting/"]
COPY ["Storehouse.Domain.Contracts/*.csproj", "Storehouse.Domain.Contracts/"]
COPY ["Domain.Crosscutting.Repository/*.csproj", "Domain.Crosscutting.Repository/"]
COPY ["Storehouse.DataAccess/*.csproj", "Storehouse.DataAccess/"]
RUN dotnet restore "Storehouse.Application.Api/Storehouse.Application.Api.csproj"
COPY . .
WORKDIR "/src/Storehouse.Application.Api"
RUN dotnet build "Storehouse.Application.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Storehouse.Application.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Storehouse.Application.Api.dll"]