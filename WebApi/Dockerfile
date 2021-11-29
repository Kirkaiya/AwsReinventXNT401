#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_ID
WORKDIR /src
COPY ["EC2BenchmarkingNet6.csproj", "EC2BenchmarkingNet6/"]
RUN dotnet restore "EC2BenchmarkingNet6/EC2BenchmarkingNet6.csproj"
WORKDIR "/src/EC2BenchmarkingNet6"
COPY . .
RUN dotnet publish "EC2BenchmarkingNet6.csproj" -c Release -o /app/publish -p:InformationalVersion=$BUILD_ID

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EC2BenchmarkingNet6.dll"]