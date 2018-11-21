#! /usr/bin/env bash

echo -e "Logging in to ACR"
az acr login --name thhdemo
echo -e "Pushing frontend docker images to thhdemo.azurecr.io"
docker push thhdemo.azurecr.io/sessionbrowser:latest

echo -e "Pushing backend docker images to thhdemo.azurecr.io"
docker push thhdemo.azurecr.io/sessions-ms:latest
docker push thhdemo.azurecr.io/votings-ms:latest
docker push thhdemo.azurecr.io/export-ms:latest
docker push thhdemo.azurecr.io/auditlog-cleaner:latest


echo -e "All images pushed to ACR"
