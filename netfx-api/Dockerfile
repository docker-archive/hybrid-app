# escape=`
FROM microsoft/dotnet-framework:4.7.2-sdk AS builder
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop';"]

WORKDIR C:\src
COPY .\SignupApi.sln C:\src\
COPY .\SignupApi\SignupApi.csproj .\SignupApi\
COPY .\SignupApi\packages.config .\SignupApi\
RUN nuget restore SignupApi.sln

COPY . C:\src
RUN msbuild SignupApi.sln /p:OutputPath=c:\out /p:Configuration=Release

# app image
FROM microsoft/aspnet:4.7.2-windowsservercore-ltsc2016
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop';"]

ENV APP_ROOT="C:\web-app" `
    DB_CONNECTION_STRING_PATH=""

WORKDIR $APP_ROOT
RUN Import-Module WebAdministration; `
    Set-ItemProperty 'IIS:\AppPools\DefaultAppPool' -Name processModel.identityType -Value LocalSystem; `
    Remove-Website -Name 'Default Web Site'; `
    New-Website -Name 'web-app' -Port 80 -PhysicalPath $env:APP_ROOT

COPY start.ps1 .
ENTRYPOINT ["powershell", ".\\start.ps1"]

HEALTHCHECK --interval=5s `
 CMD powershell -command `
    try { `
     $response = iwr http://localhost/api/diagnostics -UseBasicParsing; `
     if ($response.StatusCode -eq 200) { return 0} `
     else {return 1}; `
    } catch { return 1 }

COPY --from=builder C:\out\_PublishedWebsites\SignupApi .