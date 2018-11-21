FROM microsoft/dotnet:2.1-sdk as build-env
LABEL maintainer="Thorsten Hans <thorsten.hans@thinktecture.com>"

WORKDIR /app
RUN mkdir /app/Export.API
RUN mkdir /app/Sessions.Models

COPY ./Export.API/*.csproj /app/Export.API
COPY ./Sessions.Models/*.csproj /app/Sessions.Models

RUN cd Export.API && dotnet restore

COPY ./Export.API /app/Export.API
COPY ./Sessions.Models /app/Sessions.Models

RUN cd Export.API && dotnet publish -c Debug -o /app/out

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build-env /app/out .

CMD ["dotnet", "Export.API.dll"]
