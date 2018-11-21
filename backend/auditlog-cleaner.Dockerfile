FROM microsoft/dotnet:2.1-sdk as build-env
LABEL maintainer="Thorsten Hans <thorsten.hans@thinktecture.com>"

WORKDIR /app
RUN mkdir /app/Sessions.AuditLogClearer
RUN mkdir /app/Sessions.Models

COPY ./Sessions.AuditLogClearer/*.csproj /app/Sessions.AuditLogClearer
COPY ./Sessions.Models/*.csproj /app/Sessions.Models

RUN cd Sessions.AuditLogClearer && dotnet restore

COPY ./Sessions.AuditLogClearer /app/Sessions.AuditLogClearer
COPY ./Sessions.Models /app/Sessions.Models

RUN cd Sessions.AuditLogClearer && dotnet publish -c Debug -o /app/out

FROM microsoft/dotnet:2.1-runtime
WORKDIR /app
COPY --from=build-env /app/out .

CMD ["dotnet", "Sessions.AuditLogClearer.dll"]
