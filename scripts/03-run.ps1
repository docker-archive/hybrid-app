
# Run in a test environment - hybrid Docker swarm

docker-compose `
 -f ../app/docker-compose.yml `
 -f ../app/docker-compose-test.yml `
 config > ../app/docker-stack.yml

docker stack deploy -c ../app/docker-stack.yml hybrid-app