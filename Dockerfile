FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnet-build-env
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o out

FROM node:lts as vue-build-env
WORKDIR /
COPY ./admin-app ./admin-app
# WORKDIR /admin-app
WORKDIR /admin-app
RUN npm install --production
RUN npm run build

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=dotnet-build-env /app/out .
COPY --from=vue-build-env /admin-app/dist ./admin-app/dist
# ENV ASPNETCORE_URLS=http://*:$PORT

ENTRYPOINT ["dotnet", "IoTDevicesMonitor.dll"]
# run local (cant still production build => connect to wrong api):
#   docker run -e PORT=80 -p 5002:80 --name iot-app-nnc iot-app-nnc -rm