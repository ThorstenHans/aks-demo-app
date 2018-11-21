FROM microsoft/dotnet:2.1-sdk as build-env
LABEL maintainer="Thorsten Hans <thorsten.hans@thinktecture.com>"

WORKDIR /app
RUN mkdir /app/Sessions.API
RUN mkdir /app/Sessions.Models

COPY ./Sessions.API/*.csproj /app/Sessions.API
COPY ./Sessions.Models/*.csproj /app/Sessions.Models

RUN cd Sessions.API && dotnet restore

COPY ./Sessions.API /app/Sessions.API
COPY ./Sessions.Models /app/Sessions.Models

RUN cd Sessions.API && dotnet publish -c Debug -o /app/out

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build-env /app/out .

CMD ["dotnet", "Sessions.API.dll"]
