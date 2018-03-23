#!/bin/bash

# You need a hybrid swarm to run  the full app.
# Set the manager host name in this variable:
managerDockerHost=ub1604-01

export DOCKER_HOST=$managerDockerHost
docker stack deploy -c ../app/docker-stack-v2.yml hybrid-app