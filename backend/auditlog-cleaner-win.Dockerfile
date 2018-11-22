FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build-env
LABEL MAINTAINER="Thorsten Hans <thorsten.hans@gmail.com>"

WORKDIR /app
RUN md Sessions.AuditLogClearer
RUN md Sessions.Models

COPY ./Sessions.AuditLogClearer/*.csproj /app/Sessions.AuditLogClearer
COPY ./Sessions.Models/*.csproj /app/Sessions.Models

RUN cd Sessions.AuditLogClearer && dotnet restore

COPY ./Sessions.AuditLogClearer /app/Sessions.AuditLogClearer
COPY ./Sessions.Models /app/Sessions.Models

RUN cd Sessions.AuditLogClearer && dotnet publish -c Debug -o /app/out

FROM microsoft/dotnet:2.1-runtime-nanoserver-1709
WORKDIR /app
COPY --from=build-env /app/out .

CMD ["dotnet", "Sessions.AuditLogClearer.dll"]
