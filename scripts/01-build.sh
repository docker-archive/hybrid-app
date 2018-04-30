#!/bin/bash
docker image build --tag $DTR_HOST/dockersamples/hybrid-app-db ../database
docker image build --tag $DTR_HOST/dockersamples/hybrid-app-web ../java-app
docker image build --tag $DTR_HOST/dockersamples/hybrid-app-web:v2 ../java-app-v2
docker image build --tag $DTR_HOST/dockersamples/hybrid-app-api:dotnet ../dotnet-api
