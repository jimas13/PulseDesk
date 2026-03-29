- dotnet ef commands

    - list migrations

        `dotnet ef migrations list --project ./src/PulseDesk.Infrastructure/PulseDesk.Infrastructure.csproj --startup-project ./src/PulseDesk.Api/PulseDesk.Api.csproj`

    - create migration

        `dotnet ef migrations add <migration-name> --project ./src/PulseDesk.Infrastructure/PulseDesk.Infrastructure.csproj --startup-project ./src/PulseDesk.Api/PulseDesk.Api.csproj`