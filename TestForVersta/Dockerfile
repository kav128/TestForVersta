FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestForVersta/TestForVersta.csproj", "TestForVersta/"]
RUN dotnet restore "TestForVersta/TestForVersta.csproj"
COPY . .
WORKDIR "/src/TestForVersta"
RUN dotnet build "TestForVersta.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestForVersta.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestForVersta.dll"]
