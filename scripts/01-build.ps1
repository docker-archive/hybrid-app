
# The hybrid apps builds both Linux and Windows Docker images.
# To do that you either need a hybrid environment.
# Or the latest Docker for Windows with LCOW (Linux Containers on Windows).

## CURRENTLY THIS FAILS WITRH AN LCOW ISSUE.
## Leaving here to help repro.

docker image build --platform linux --tag dockersamples/hybrid-app-db ../database

docker image build --platform linux --tag dockersamples/hybrid-app-web ../java-app

docker image build --platform windows --tag dockersamples/hybrid-app-api:netfx ../netfx-api