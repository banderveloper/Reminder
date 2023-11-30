FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

ARG JWT_SECRET
ARG POSTGRES_DB_USERNAME
ARG POSTGRES_DB_PASSWORD

ENV JWT__SecretKey=$JWT_SECRET
ENV Database__Username=$POSTGRES_DB_USERNAME
ENV Database__Password=$POSTGRES_DB_PASSWORD

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Reminder.Domain/Reminder.Domain.csproj", "Reminder.Domain/Reminder.Domain.csproj"]
COPY ["Reminder.Application/Reminder.Application.csproj", "Reminder.Application/Reminder.Application.csproj"]
COPY ["Reminder.Persistence/Reminder.Persistence.csproj", "Reminder.Persistence/Reminder.Persistence.csproj"]
COPY ["Reminder.WebApp/Reminder.WebApp.csproj", "Reminder.WebApp/Reminder.WebApp.csproj"]

COPY . .
WORKDIR "/src/Reminder.WebApp"
RUN dotnet build "Reminder.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Reminder.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Reminder.WebApp.dll"]