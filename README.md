# Hybrid Application

This application demonstrates running both Linux and Windows containers within the same cluster. The application is used in the [Docker Enterprise Edition](https://github.com/dockersamples/ee-workshop) workshop.

The purpose of this tutorial is demonstrate how an application can be deployed in different scenarios using Docker Enterprise Edition or Docker Community Edition. The first scenario is a monolithic Java CRUD (Create Read Update Delete) application that uses Springboot, Java Server Pages and MySQL. The second scenario adds a REST microservice written with .Net Framework and running in a Windows container. The third scenario uses a REST microservice written with .Net Core running in a Linux container. Each scenario uses a different orchestrator to deploy the application.

## MySQL Database

All three scenarios use a MySQL database to store data. To build the container:

```
$ cd ./database
$ docker image build -t database .
``` 

## Java Application

The Java application is in the [java-app](./java-app) directory. To build the application, follow the [instructions](./java-app/README.md) in the application directory or build it locally:

```
$ cd ./java-app
$ docker image build -t java-web .
```

To deploy both the application and the database, we use a Docker Compose file.

To start the application user Docker Compose:
```
$ cd ./hybrid-app
$ docker-compose -f ./app/docker-compose-java.yml up -d
```

To try out the application go to [http://localhost:8080/java-web](http://localhost:8080/java-web). 

To shut down the application:
```
$ docker-compose down
```

## Java and .Net Framework Application

This version of the application introduces a REST microservice written in .Net Framework and running in a Windows container that reads and writes to the MySQL database. The Java application was rewritten to use the microservice instead of Spring Data JPA to communicate with the database.

Instructions for building the updated Java application are in the [java-app-v2](./java-app-v2) directory or to just build the image: 

```PowerShell
C:\> cd .\java-app-v2
C:\> docker image build -t java_web:2 .
```

 The .Net Framework microservice must be built in a Windows host, clone the repository and build the image:

``` PowerShell
C:\> git clone https://github.com/dockersamples/hybrid-app.git
C:\> cd .hybrid-app\netfx-api
C:\> docker image build -t dotnet_api .
```

Instructions for deploying on Docker EE are available in the [Docker EE Workshop](https://github.com/dockersamples/ee-workshop#task-3-deploy-the-next-version-with-a-windows-node) which uses Play With Docker, an on line Docker environment.

To run the application locally in Docker for Windows CE Edge:

```
C:\> docker swarm init
C:\> cd .\hybrid-app
C:\> docker stack deploy -c .\app\docker-stack-netfx.yml signup
```

To shutdown the application:

```
C:\> docker stack rm signup
```

## Java and .Net Core Application

In this scenario, we'll reuse the java_web:2 container and build a new image of the REST microservice using .Net Core which runs in a Linux container.

To build the the .Net Core microservice:
```
$ cd ./dotnet-api
$ docker image build -t dotnet_api:core .
```

To deploy the application in Docker EE using Kubernetes, follow the instructions in [Task 4](https://github.com/dockersamples/ee-workshop#task-4-deploy-to-kubernetes) of the [Docker EE Workshop](https://github.com/dockersamples/ee-workshop).

To deploy the application locally with Kubernetes in a single node cluster on either Docker for Mac CE Edge or Docker for Windows CE Edge, check that kubernetes is the orchestrator:

```
$ kubectl config current-context
docker-for-desktop
```

To run the application:

```
$ docker stack deploy -c ./app/docker-stack-k8s.yml signup
```
To shutdown the application:

```
$ docker stack rm signup
