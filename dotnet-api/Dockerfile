# escape=`
FROM microsoft/aspnetcore-build:2.0.3-2.1.2 AS builder

WORKDIR /src
COPY SignupApiCore/SignupApiCore.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish

# app image
FROM microsoft/aspnetcore:2.0.3-stretch

CMD ["dotnet", "SignupApiCore.dll"]

WORKDIR /dotnetapp
COPY --from=builder /src/bin/Debug/netcoreapp2.0/publish .