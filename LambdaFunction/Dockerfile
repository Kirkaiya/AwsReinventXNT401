ARG ECR_URI

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_ID
ARG RUNTIME_ID
WORKDIR /src
COPY . .
RUN dotnet publish "LambdaFunction/LambdaFunction.csproj" -c Release -r $RUNTIME_ID --no-self-contained -o /app/publish -p:InformationalVersion=$BUILD_ID

FROM $ECR_URI/net6-g2-sample/lambda:6.0 AS final
WORKDIR /var/task
COPY --from=build /app/publish .