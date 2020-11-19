

# Getting starts

First, let's set the namespace, by typing:

```ps
$ kubectl create ns meetup-keda-only-demo
$ kubectl config set-context --current --namespace=meetup-keda-only-demo
```

Next, deploy keda to kubernetes cluster, by typing:

```ps
$ kubectl apply -f https://github.com/kedacore/keda/releases/download/v2.0.0/keda-2.0.0.yaml
```

## Components

Before we set up any components, let's first create some secrets.

```ps
$ kubectl create secret generic db-passwords --from-literal=redis-password='<password>'
```
_👆 replace <password> with your own_

Next, let's deploy Redis:

```yml
$ kubectl apply -f aks-redis.yml
```

Now let's confirm service is running and we've an public endpoint to access (purely for demo, wouldn't need to publicly expose this):
```ps
$ kubectl.exe get svc
NAME        TYPE           CLUSTER-IP    EXTERNAL-IP     PORT(S)          AGE
redis-svc   LoadBalancer   10.0.212.80   20.49.164.164   6379:31300/TCP   16s
```

In order for secret's to be accessed by the Dapr component, we're going to have to allow the service account access to the secrets in the meetup-dapr-demo namespace by running:

To access the data, install RedisInsight by:
```ps
$ docker run -v redisinsight:/db -p 8001:8001 redislabs/redisinsight:latest
```

## Cache

First, build and upload container image by typing:

```ps
$ build.bat
```

First, build and upload container images by typing:

```ps
$ build.bat
```

To deploy the containers required for this demo, type:

```yml
$ kubectl apply -f aks-deploy-demo.yml
```

This 👆 will deploy a http-api and consumer.

Next, we want to deploy the Keda scaling manifest, by typing:

```ps
$ kubectl apply -f aks-scale-consumer.yml
```

You may notice that the single pod that was running your consumer, has now terminated.  This is due to the scaling configuration; `minReplicaCount: 0`.

To see this in action, you need to run the following two commands in 2 separate terminals:

```ps
$ kubectl.exe logs -l run=http-api -f
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://[::]:80
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /app
```

```ps
$ kubectl.exe logs -l run=consumer -f
<empty>
```
There are no consumers running so the above output will be blank.

Let's get the IPv4 address for the `http-svc`, by typing:
```ps
$ kubectl.exe get svc
NAME        TYPE           CLUSTER-IP     EXTERNAL-IP     PORT(S)          AGE
http-svc    LoadBalancer   10.0.164.26    51.145.125.69   3007:31066/TCP   111m
redis-svc   LoadBalancer   10.0.215.212   20.49.147.146   6379:32746/TCP   10h
```

Let's now observe the scaling in action, by typing:

```ps
$ artillery run .\load-test\stream.yml
```

You will see the `consumer` scale up:

```ps
$ kubectl.exe get po -w
NAME                        READY   STATUS    RESTARTS   AGE
consumer-786d9c5bdf-ntfkj   1/1     Running   0          6m22s
http-api-598b65f498-zmvgf   1/1     Running   0          106m
redis-74698b987b-c5rkf      1/1     Running   0          10h
consumer-786d9c5bdf-xczv9   0/1     Pending   0          0s
consumer-786d9c5bdf-xczv9   0/1     Pending   0          0s
consumer-786d9c5bdf-xczv9   0/1     ContainerCreating   0          0s
consumer-786d9c5bdf-xczv9   1/1     Running             0          2s
consumer-786d9c5bdf-xczv9   1/1     Terminating         0          5m1s
consumer-786d9c5bdf-xczv9   0/1     Terminating         0          5m2s
consumer-786d9c5bdf-xczv9   0/1     Terminating         0          5m10s
consumer-786d9c5bdf-xczv9   0/1     Terminating         0          5m10s
```

kubectl.exe scale --replicas=1 deployment consumer