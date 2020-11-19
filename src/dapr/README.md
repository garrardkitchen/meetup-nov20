# Demos

This is a table of the Dapr demos, but **FIRST follow the instructions** in [**Getting started**](#getting-started) section.

| demo | link | k8s manifest | reason |
|------|------|--------------|--------|
| eda demo | [**readme**](eda/README.md) | aks-deploy-demo.yml | show microservice using the REDIS transport for EDA |
| cache demo | [**readme**](cache/README.md) | aks-deploy-demo.yml | show microservice using the REDIS transport for caching  |


# Getting started

First, let's set the namespace, by:

```ps
$ kubectl create ns meetup-dapr-demo
$ kubectl config set-context --current --namespace=meetup-dapr-demo
```

To install [**Dapr click here**](https://docs.dapr.io/getting-started/install-dapr/).

To deploy on AKS, type:

```ps
dapr init -k -n <namespace>
``` 

To see status of deployment, type:
```ps
$ dapr status -k
NAME                   NAMESPACE  HEALTHY  STATUS   REPLICAS  VERSION  AGE  CREATED
dapr-operator          dapr-demo  True     Running  1         0.11.3   4d   2020-10-31 19:45.29
dapr-dashboard         dapr-demo  True     Running  1         0.3.0    4d   2020-10-31 19:45.29
dapr-sentry            dapr-demo  True     Running  1         0.11.3   4d   2020-10-31 19:45.29
dapr-sidecar-injector  dapr-demo  True     Running  1         0.11.3   4d   2020-10-31 19:45.29
dapr-placement         dapr-demo  True     Running  1         0.11.3   4d   2020-10-31 19:45.29
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

To access the data, install RedisInsight by and follow the onscreen instructions to configure a connection:
```ps
$ docker run -v redisinsight:/db -p 8001:8001 redislabs/redisinsight:latest
```