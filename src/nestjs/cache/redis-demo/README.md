# Getting started

## docker-compose

To run using docker-compose, type:

```
$ docker-compose -f .\docker-compose-demo.yml up --build
```

To confirm it works, from Powershell type:
```ps
$ (curl "http://localhost:3000/?sum=1,2,3").content
6
$ (curl "http://localhost:3000/random").content
0.9972011809570078
```

## AKS

To deploy to AKS, type:

### Step 1

Build & push images

_You will have to rename the images in `build.bat` and `aks-deploy-demo.yml`_

```
$ build.bat
```

### Step 2

To deploy to AKS, type:
```
$kubectl apply -f aks-deploy-demo.yml
```

To set the default namespace to `meetup-nestjs-eda-demo`, type:
```
$ kubectl config set-context --current --namespace=meetup-nestjs-cache-demo
```

To get a list of services (we're looking for the http-svc), type:
```
$ kubectl.exe get svc
NAME        TYPE           CLUSTER-IP     EXTERNAL-IP     PORT(S)          AGE
http-svc    LoadBalancer   10.0.5.183     20.49.215.216   3003:30488/TCP   60s
redis-svc   ClusterIP      10.0.255.179   <none>          6379/TCP         60s
```

To confirm it works, from Powershell type:
```ps
$ (curl "http://20.49.215.216:3003/?sum=1,2,3").content
6
$ (curl "http://20.49.215.216:3003/?sum=1,2,3,4").content
10
$ (curl "http://20.49.215.216:3003/random").content
0.6343634216033378
```

## OpenAPI

From a browser, pasted this in `http://20.49.215.216:3003/api` (making sure you replace the Service IP address).

This will reveal the Swagger UI representing the HTTP API controller methods.  From here you can `try it out` the `/` & `/random` endpoints.
