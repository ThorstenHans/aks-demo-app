# Sessions App

This app is for demonstration purpose. It's a small app consisting of four main components

* Angular SPA
* .NET Core 2.0 (Sessions.API)
* .NET Core 2.0 (Votings.API)
* .NET Core 2.0 CronJob (Sessions.AuditLogCleaner)

## Requirements

* SQL Server on Azure
  * Database can be generated using `/backend/Sessions.Migrations/` (.NET Core Executable) see _Generating the Database_
* AKS instance
* ACR instance
* latest Azure CLI installed
* kubectl installed and configured to use AKS instance

## Generating the Database

* Define a firewall exception for your local IP address on SQL Azure (SQL Server).
* Create a new (empty) SQL Database
* Set the connection string temporary as ENVIRONMENT Variable with the name `DbConnectionString`
* Execute EF Migrations (Sessions.Migrations C# Project in `/backend/Sessions.Migrations`) using VS or Rider to generate the database from the current C# model

## Building Docker images

Docker images must be generated for all four components. Replace `thhdemo.azurecr.io` with your ACR instance identifier.

```
$ cd backend
backend $ docker build -t thhdemo.azurecr.io/sessions-api:1.0.0 -f sessions-ms.Dockerfile .
backend $ docker build -t thhdemo.azurecr.io/votings-api:1.0.0 -f votings-ms.Dockerfile .
backend $ docker build -t thhdemo.azurecr.io/sessions-cleaner:1.0.0 -f log-cleaner.Dockerfile .
backend $ cd ..
cd frontend
frontend $ docker build -t thhdemo.azurecr.io/sessions-spa:1.0.0 .
```

## Push images to ACR

Replace `thhdemo` with your ACR instance identifier

```
$ az acr login --name thhdemo
$ docker push thhdemo.azurecr.io/sessions-api:1.0.0
$ docker push thhdemo.azurecr.io/votings-api:1.0.0
$ docker push thhdemo.azurecr.io/sessions-cleaner:1.0.0
$ docker push thhdemo.azurecr.io/sessions-spa:1.0.0
```

## Prepare AKS deployment

All kubernetes resources are defined in `deployment` directory. You'll find a `secrets.deployment.template.yaml` there. Rename it to `secrets.deployment.yaml` and replace the token `<<base64 ...>>` with your DB connection string in base64. You can generate a `base64` representation using the following command:

```
$ echo -n 'my connectionstring' | base64
```

## Deploy to AKS

Execute the following commands to deploy everything to AKS:

```
$ cd deployment
deployment $ kubectl create -f secrets.deployment.yaml
deployment $ kubectl create -f services.deployment.yaml
deployment $ kubectl create -f backend.deployment.yaml
deployment $ kubectl create -f frontend.deployment.yaml
deployment $ kubectl create -f cron.deployment.yaml
```
