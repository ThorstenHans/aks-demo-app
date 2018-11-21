#! /usr/bin/evn bash

cd k8s
echo -e "Deploying Secrets"
kubectl create -f secrets.yaml

echo -e "Deploying Backend"
kubectl create -f backend.yaml

echo - "Deploying CronJob"
kubectl create -f cronjob.yaml

echo -e "Deploying Frontend"
kubectl create -f frontend.yaml

echo -e "Deploying Services"
kubectl create -f services.yaml

echo -e "Watching services..."
kubectl get svc -w
