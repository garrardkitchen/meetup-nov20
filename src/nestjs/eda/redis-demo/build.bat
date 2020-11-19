docker build math-svc/ -t cf247garrard.azurecr.io/meetup-nestjs-eda-redis-math-svc:latest
docker build http-api/ -t cf247garrard.azurecr.io/meetup-nestjs-eda-redis-http-api:latest
docker push cf247garrard.azurecr.io/meetup-nestjs-eda-redis-math-svc:latest
docker push cf247garrard.azurecr.io/meetup-nestjs-eda-redis-http-api:latest