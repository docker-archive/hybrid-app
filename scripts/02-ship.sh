#!/bin/bash
docker image push $DTR_HOST/dockersamples/hybrid-app-db
docker image push $DTR_HOST/dockersamples/hybrid-app-web 
docker image push $DTR_HOST/dockersamples/hybrid-app-web:v2 
docker image push $DTR_HOST/dockersamples/hybrid-app-api:dotnet 