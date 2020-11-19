## deploy to aks

### Demos

| demo | link | k8s manifest | reason |
|------|------|--------------|--------|
| tcp-demo | [readme](ms-tcp-demo/README.md) | aks-deployment-ms-tcp-demo.yml | show microservice using the tcp transport  |
| redis-demo | [readme](ms-redis-demo/README.md) | aks-deployment-ms-redis-demo.yml | show microservice using the redis transport  |
| nats-demo | [readme](ms-nats-demo/README.md) | aks-deployment-ms-nates-demo.yml | show microservice using the nats transport  |


#### TCP demo

```ps
kubectl apply -f aks-deployment-ms-tcp-demo.yml
```

```ps
kubectl config set-context --current --namespace=meetup-tcp-demo
kubectl get svc
(curl http://<client-svc-external-ip>:3000/).content
```

output:
```
10
```

#### Redis demo

```ps
kubectl apply -f aks-deployment-ms-redis-demo.yml
```

```ps
kubectl config set-context --current --namespace=meetup-redis-demo
kubectl get svc
(curl http://<client-svc-external-ip>:3001/).content
```

output:
```
10
```

#### NATS demo

```ps
kubectl apply -f aks-deployment-ms-nats-demo.yml
```

```ps
kubectl config set-context --current --namespace=meetup-nats-demo
kubectl get svc
(curl http://<external-ip>:3001/).content
```

output:
```
10
```

#### Redis observability demo
