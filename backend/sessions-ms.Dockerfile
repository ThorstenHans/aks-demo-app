FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
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

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
EXPOSE 8080
COPY --from=build-env /app/out .

CMD ["dotnet", "Sessions.API.dll"]
