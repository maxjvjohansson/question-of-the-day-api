FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .

RUN dotnet restore "src/QuestionsApi/QuestionsApi.csproj"
RUN dotnet publish "src/QuestionsApi/QuestionsApi.csproj" -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0.100 AS runtime
WORKDIR /app

COPY --from=build /out .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "QuestionsApi.dll"]
