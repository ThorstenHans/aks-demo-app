FROM microsoft/dotnet:2.0.5-sdk-2.1.4 as build-env
LABEL maintainer="Thorsten Hans <thorsten.hans@thinktecture.com>"

WORKDIR /app
RUN mkdir /app/Export.API


COPY ./Export.API/*.csproj /app/Export.API

RUN cd Export.API && dotnet restore

COPY ./Export.API /app/Export.API

RUN cd Export.API && dotnet publish -c Debug -o /app/out

FROM microsoft/aspnetcore:2.0.5
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .

CMD ["dotnet", "Export.API.dll"]
