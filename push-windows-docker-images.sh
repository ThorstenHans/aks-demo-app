#! /usr/bin/env bash

echo -e "Logging in to ACR"
az acr login --name thhdemo
echo -e "Pushing backend docker images to thhdemo.azurecr.io"
docker push thhdemo.azurecr.io/auditlog-cleaner:win-latest
echo -e "All windows images pushed to ACR"
