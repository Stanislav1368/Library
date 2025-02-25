# �������� ����� ��� ���������� ����������
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# �������� ����� ��� ������
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# ����������� ������ �������� � �������������� ������������
COPY ["Library.API/Library.API.csproj", "Library.API/"]
COPY ["Library.Application/Library.Application.csproj", "Library.Application/"]
COPY ["Library.Infrastructure/Library.Infrastructure.csproj", "Library.Infrastructure/"]
COPY ["Library.Domain/Library.Domain.csproj", "Library.Domain/"]
RUN dotnet restore "./Library.API/Library.API.csproj"

# ����������� ���� ������ � ������ �������
COPY . .
WORKDIR "/src/Library.API"
RUN dotnet build "./Library.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ���������� ����������
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Library.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ��������� ����: ����������� ���� ����������� ������
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/Library.Application /app/Library.Application
COPY --from=build /src/Library.Infrastructure /app/Library.Infrastructure
COPY --from=build /src/Library.Domain /app/Library.Domain
ENTRYPOINT ["dotnet", "Library.API.dll"]
