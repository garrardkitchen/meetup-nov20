docker build consumer-svc/ -t cf247garrard.azurecr.io/meetup-nestjs-queue-redis-consumer-svc:latest
docker build http-api/ -t cf247garrard.azurecr.io/meetup-nestjs-quque-redis-http-api:latest
docker push cf247garrard.azurecr.io/meetup-nestjs-queue-redis-consumer-svc:latest
docker push cf247garrard.azurecr.io/meetup-nestjs-quque-redis-http-api:latest