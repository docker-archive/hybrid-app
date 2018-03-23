# escape=`
FROM dockersamples/mta-dev-web-builder:4.7.1 AS builder
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop';"]

WORKDIR C:\src\SignupApi
COPY .\SignupApi\packages.config .
RUN nuget restore packages.config -PackagesDirectory ..\packages

COPY . C:\src
RUN msbuild SignupApi.csproj /p:OutputPath=c:\out /p:DeployOnBuild=true

# app image
FROM microsoft/aspnet:4.7.1-windowsservercore-10.0.14393.1884
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