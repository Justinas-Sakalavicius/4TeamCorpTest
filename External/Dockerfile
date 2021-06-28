#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["External/External.csproj", "External/"]
RUN dotnet restore "External/External.csproj"
COPY . .
WORKDIR "/src/External"
RUN dotnet build "External.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "External.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "External.dll"]