FROM microsoft/dotnet:2.1-sdk as build-env
LABEL maintainer="Thorsten Hans <thorsten.hans@thinktecture.com>"

WORKDIR /app
RUN mkdir /app/Votings.API
RUN mkdir /app/Sessions.Models

COPY ./Votings.API/*.csproj /app/Votings.API
COPY ./Sessions.Models/*.csproj /app/Sessions.Models

RUN cd Votings.API && dotnet restore

COPY ./Votings.API /app/Votings.API
COPY ./Sessions.Models /app/Sessions.Models

RUN cd Votings.API && dotnet publish -c Debug -o /app/out

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .

CMD ["dotnet", "Votings.API.dll"]
