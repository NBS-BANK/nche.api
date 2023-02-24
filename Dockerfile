#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VasMicroservices.NCHE.Presentation.Api/VasMicroservices.NCHE.Presentation.Api.csproj", "VasMicroservices.NCHE.Presentation.Api/"]
COPY ["VasMicroservices.NCHE.Infra.IoC/VasMicroservices.NCHE.Infra.IoC.csproj", "VasMicroservices.NCHE.Infra.IoC/"]
COPY ["VasMicroservices.NCHE.Infra.Data/VasMicroservices.NCHE.Infra.Data.csproj", "VasMicroservices.NCHE.Infra.Data/"]
COPY ["VasMicroservices.NCHE.Application/VasMicroservices.NCHE.Application.csproj", "VasMicroservices.NCHE.Application/"]
COPY ["VasMicroservices.NCHE.Presentation.Api/CommonLibraries.Application.dll", "VasMicroservices.NCHE.Presentation.Api/"]
COPY ["VasMicroservices.NCHE.Presentation.Api/CommonLibraries.Infrastrucure.IoC.dll", "VasMicroservices.NCHE.Presentation.Api/"]
COPY ["VasMicroservices.NCHE.Presentation.Api/CommonLibraries.Domain.dll", "VasMicroservices.NCHE.Presentation.Api/"]
COPY ["VasMicroservices.NCHE.Presentation.Api/CommonLibraries.Infrastructure.MessageQueue.dll", "VasMicroservices.NCHE.Presentation.Api/"]
COPY ["VasMicroservices.NCHE.Presentation.Api/CommonLibraries.Domain.Core.dll", "VasMicroservices.NCHE.Presentation.Api/"]

RUN curl https://api.nuget.org/v3/index.json -k

RUN dotnet restore "VasMicroservices.NCHE.Presentation.Api/VasMicroservices.NCHE.Presentation.Api.csproj"
COPY . .
WORKDIR "/src/VasMicroservices.NCHE.Presentation.Api"
RUN dotnet build "VasMicroservices.NCHE.Presentation.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VasMicroservices.NCHE.Presentation.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VasMicroservices.NCHE.Presentation.Api.dll"]