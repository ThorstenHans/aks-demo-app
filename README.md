# AKS Demo App

## A simple .NET Core / Angular application

`aks-demo-app` is a simple application to demonstrate basic concepts of [kubernetes](http://kubernetes.io) (k8s) and Azure Kubernetes Services (AKS). The app consists of four Docker images and a SQL database. The SQL database won't be hosted inside of k8s, instead Microsoft's PaaS offering (SQL Azure) is used. The Sample is licensed under [MIT](./LICENSE)

-   An Angular SPA
-   .NET Core 2.0
    -   An API for reading Sessions `Sessions.API`
-   .NET Core 2.0
    -   An API for reading votes and persisting new votes `Votings.API`
-   .NET Core 2.0 CronJob
    -   Which will remove old audit logs from SQL database `Sessions.AuditLogCleaner`

## Cloud Environment

For running and hosting the application, the following environment is required.

    - SQL Azure
    - Azure Container Registry (ACR)
    - Azure Kubernetes Service (AKS)

The cloud environment can be deployed using [Terraform](https://github.com/hashicorp/terraform). The subfolder `terraform` contains already the environment and can be configured according to your needs by specifying your settings in a `terraform.tfvars` file. The `terraform/terraform.tfvars.sample` contains an example configuration.

## Developer Environment Requirements

    - Azure CLI (`az`)
    - Kubernetes Control (`kubectl`)
    - Docker (`docker`)

## Generating the Database

-   Define a firewall exception for your local IP address on SQL Azure (SQL Server).
-   Create a new (empty) SQL Database
-   Set the connection string **temporary** as _ENVIRONMENT Variable_ with the name `DbConnectionString`
-   Execute EF Migrations (Sessions.Migrations C# Project in `/backend/Sessions.Migrations`) using VS or Rider to generate the database from the current C# model

## Building Docker images

Docker images must be generated for all four components. Replace `thhdemo.azurecr.io` with your ACR instance identifier.

```
$ cd backend
backend $ docker build -t thhdemo.azurecr.io/sessions:latest -f sessions-ms.Dockerfile .
backend $ docker build -t thhdemo.azurecr.io/votings:latest -f votings-ms.Dockerfile .
backend $ docker build -t thhdemo.azurecr.io/sessions-cleaner:latest -f log-cleaner.Dockerfile .
backend $ cd ..
cd frontend
frontend $ docker build -t thhdemo.azurecr.io/frontend:latest .
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

All kubernetes resources are defined in `deployment` directory. You'll find a `secrets.template.yaml` there. Rename it to `secrets.yaml` and replace the token `<<base64 ...>>` with your DB connection string in base64. You can generate a `base64` representation using the following command:

```
$ echo -n 'my connectionstring' | base64
```

Update `backend.yaml`, `frontend.yaml` and `cronjob.yaml` and provide correct name for your docker images (again replace `thhdemo.azurecr.io` with your ACR stuff).

## Deploy to AKS

Execute the following commands to deploy everything to AKS:

```
$ cd deployment
deployment $ kubectl create -f secrets.yaml
deployment $ kubectl create -f services.yaml
deployment $ kubectl create -f backend.yaml
deployment $ kubectl create -f frontend.yaml
deployment $ kubectl create -f cronjob.yaml
```

# Scale deployments

```
$ kubectl scale deployment sessions-api --replicas 5
$ kubectl scale deployment votings-api --replicas 5
$ kubectl scale deployment sessions-spa --replicas 3
```
