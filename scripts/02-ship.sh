#!/bin/bash

# You need Linux and Windows Docker servers to build the full app.
# Set the host names in these variables:
linuxDockerHost=ub1604-01
windowsDockerHost=win2016-01

# Linux images
export DOCKER_HOST=$linuxDockerHost

docker image push dockersamples/hybrid-app-db
docker image push dockersamples/hybrid-app-web 
docker image push dockersamples/hybrid-app-web:v2 
docker image push dockersamples/hybrid-app-api:dotnet 

# Windows images
export DOCKER_HOST=$windowsDockerHost

docker image push dockersamples/hybrid-app-api:netfx