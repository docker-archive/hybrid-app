#!/bin/bash

# You need Linux and Windows Docker servers to build the full app.
# Set the host names in these variables:
linuxDockerHost=ub1604-01
windowsDockerHost=win2016-01

# Linux images
export DOCKER_HOST=$linuxDockerHost

docker image build --tag dockersamples/hybrid-app-db ../database
docker image build --tag dockersamples/hybrid-app-web ../java-app
docker image build --tag dockersamples/hybrid-app-web:v2 ../java-app-v2
docker image build --tag dockersamples/hybrid-app-api:dotnet ../dotnet-api

# Windows images
export DOCKER_HOST=$windowsDockerHost

docker image build --tag dockersamples/hybrid-app-api:netfx ../netfx-api

