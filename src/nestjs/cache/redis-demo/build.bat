docker build math-svc/ -t cf247garrard.azurecr.io/meetup-nestjs-cache-redis-math-svc:latest
docker build http-api/ -t cf247garrard.azurecr.io/meetup-nestjs-cache-redis-http-api:latest
docker push cf247garrard.azurecr.io/meetup-nestjs-cache-redis-math-svc:latest
docker push cf247garrard.azurecr.io/meetup-nestjs-cache-redis-http-api:latest