docker build dotnet-demo/http-api -t cf247garrard.azurecr.io/meetup-keda-redis-http-api:latest
docker build dotnet-demo/Consumer-demo/Consumer -t cf247garrard.azurecr.io/meetup-keda-redis-consumer-demo:latest
docker push cf247garrard.azurecr.io/meetup-keda-redis-http-api:latest
docker push cf247garrard.azurecr.io/meetup-keda-redis-consumer-demo:latest