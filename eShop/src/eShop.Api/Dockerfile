#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV TZ=Asia/Jakarta
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["eShop/src/eShop.Api/eShop.Api.csproj", "eShop/src/eShop.Api/"]
COPY ["eShop/src/eShop.Application/eShop.Application.csproj", "eShop/src/eShop.Application/"]
COPY ["eShop/src/eShop.Domain/eShop.Domain.csproj", "eShop/src/eShop.Domain/"]
COPY ["eShop/src/eShop.Infrastructure/eShop.Infrastructure.csproj", "eShop/src/eShop.Infrastructure/"]
COPY ["eShop/src/eShop.Presentation/eShop.Presentation.csproj", "eShop/src/eShop.Presentation/"]
RUN dotnet restore "eShop/src/eShop.Api/eShop.Api.csproj"
COPY . .
WORKDIR "/src/eShop/src/eShop.Api"
RUN dotnet build "eShop.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShop.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShop.Api.dll"]
