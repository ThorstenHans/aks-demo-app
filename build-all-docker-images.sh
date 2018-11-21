#! /usr/bin/env bash

echo -e 'building frontend docker image as \033[1;34mthhdemo.azurecr.io/sessionbrowser:latest\033[0m'

cd frontend
docker build . -t thhdemo.azurecr.io/sessionbrowser:latest --quiet
echo -e '\033[32mdone building frontend images!\033[0m'
cd ..
cd backend

echo -e 'building frontend docker image as \033[1;34mthhdemo.azurecr.io/sessions-ms:latest\033[0m'
docker build . -f sessions-ms.Dockerfile -t thhdemo.azurecr.io/sessions-ms:latest --quiet
echo -e 'building frontend docker image as \033[1;34mthhdemo.azurecr.io/votings-ms:latest\033[0m'
docker build . -f votings-ms.Dockerfile -t thhdemo.azurecr.io/votings-ms:latest --quiet
echo -e 'building frontend docker image as \033[1;34mthhdemo.azurecr.io/auditlog-cleaner:latest\033[0m'
docker build . -f auditlog-cleaner.Dockerfile -t thhdemo.azurecr.io/auditlog-cleaner:latest --quiet

echo -e '\033[32mdone building backend images\033[0m'
