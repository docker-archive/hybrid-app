# escape=`
FROM microsoft/dotnet:2.1-sdk-alpine as builder

WORKDIR /src
COPY SignupApiCore/SignupApiCore.csproj .
RUN dotnet restore

COPY SignupApiCore/ .
RUN dotnet publish

# app image
FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine

CMD ["dotnet", "SignupApiCore.dll"]

WORKDIR /dotnetapp
COPY --from=builder /src/bin/Debug/netcoreapp2.1/publish .