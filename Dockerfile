FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

ARG TELERIK_USERNAME
ARG TELERIK_PASSWORD

COPY ["NuGet.Config", "."]
COPY ["PUSL2020.sln", "."]
COPY ["src/PUSL2020.Domain/PUSL2020.Domain.csproj", "PUSL2020.Domain/"]
COPY ["src/PUSL2020.Application/PUSL2020.Application.csproj", "PUSL2020.Application/"]
COPY ["src/PUSL2020.Infrastructure/PUSL2020.Infrastructure.csproj", "PUSL2020.Infrastructure/"]
COPY ["src/PUSL2020.Web/PUSL2020.Web.csproj", "PUSL2020.Web/"]
COPY ["src/PUSL2020.MasterData/PUSL2020.MasterData.csproj", "PUSL2020.MasterData/"]

RUN dotnet restore "PUSL2020.Web/PUSL2020.Web.csproj"
COPY src .
WORKDIR "/src/PUSL2020.Web"
RUN dotnet build "PUSL2020.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PUSL2020.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PUSL2020.Web.dll"]
