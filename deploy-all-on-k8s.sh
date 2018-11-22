#! /usr/bin/env bash


if [ "$1" = "" ]; then
    echo -e "Provide a name for the namespace"
    exit 1
fi

cd k8s
kubectl create namespace $1

echo -e "Deploying Secrets"
kubectl create -f secrets.yaml -n $1

echo -e "Deploy Azure Identity"
kubectl create -f azure-identity.yaml -n $1

echo -e "Deploy Azure Identity Binding"
kubectl create -f azure-identity-binding.yaml -n $1

echo -n "Deploying Dynamic Azure Disc PVC"
kubectl create -f azure-dynamic-disc-pvc.yaml -n $1

echo -e "Deploying Backend"
kubectl create -f backend.yaml -n $1

echo -e "Deploying Exporter (AzureDisc Backend)"
kubectl create -f exporter-azure-disc.yaml -n $1

echo -e "Deploy Exporter (Azure Files Share Backend)"
kubectl create -f exporter-azure-files-share.yaml -n $1

echo - "Deploying CronJob (Azure KeyVault enabled)"
kubectl create -f keyvault-enabled-cronjob.yaml -n $1

echo -e "Deploying Frontend"
kubectl create -f frontend.yaml -n $1

echo -e "Deploying Services"
kubectl create -f services.yaml -n $1

echo -e "Watching services..."
kubectl get svc -w
