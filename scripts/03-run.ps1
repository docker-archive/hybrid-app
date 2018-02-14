
# Run in a test environment - hybrid Docker swarm

docker-compose `
 -f ../app/docker-compose.yml `
 -f ../docker-compose-test.yml `
 config > docker-stack.yml

docker stack deploy -c docker-stack.yml hybrid-app