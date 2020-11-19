# Getting starts

**WORK IN PROGRESS - IGNORE FOR NOW**

First, let's set the namespace, by:

```ps
$ kubectl create ns meetup-keda-demo
$ kubectl config set-context --current --namespace=meetup-keda-demo
$ dapr dashboard -k -n dapr-demo
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

```ps
$ kubectl apply -f aks-auth.yml
```
Here 👆 we are creating a role and binding this to the default ServiceAccount.  This role `secret-reader` allows a `get` of the `secrets` resource within the `meetup-depr-demo` namespace.


Next, let's create a State Store component:

```ps
$ kubectl apply -f aks-redis-state.yml
```

Next, let's create a Pub/Sub component:

```yml
$ kubectl apply -f aks-redis-pubsub.yml
```

To see a list of components, type:
```ps
$  dapr components -k
NAME        TYPE          AGE  CREATED
pubsub      pubsub.redis  4d   2020-10-31 19:53.36
statestore  state.redis   4d   2020-10-31 19:53.35
```

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
$ cd dotnet-demo
$ build.bat
```

To deploy the containers required for this demo, type:

```yml
$ kubectl apply -f aks-deploy-eda-demo.yml
```

This 👆 will deploy a http-eda-api and bank-consumer.

To see this in action, you need to run the following two commands in 2 separate terminals:

```ps
$ kubectl.exe logs -l run=http-eda-api -c http-eda-api -f
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
$ kubectl.exe logs -l run=bank-consumer -c bank-consumer -f
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://[::]:80
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /app
```

To see this in action, type:
```ps
Invoke-RestMethod 'http://20.49.226.98:3005/publish/deposit' -Method 'POST' -Body "{`"id`":`"1`", `"repetitions`":5,  `"lowamount`":1,`"highamount`":1000}" -ContentType "application/json"
Invoke-RestMethod 'http://20.49.226.98:3005/publish/withdraw' -Method 'POST' -Body "{`"id`":`"1`", `"repetitions`":5,  `"lowamount`":1,`"highamount`":1000}" -ContentType "application/json"
```
