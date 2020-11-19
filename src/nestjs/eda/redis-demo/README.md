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
$ kubectl config set-context --current --namespace=meetup-nestjs-eda-demo
```

To get a list of services (we're looking for the http-svc), type:
```
$ kubectl.exe get svc
NAME        TYPE           CLUSTER-IP     EXTERNAL-IP     PORT(S)          AGE
http-svc    LoadBalancer   10.0.5.183     20.49.215.216   3001:30488/TCP   60s
redis-svc   ClusterIP      10.0.255.179   <none>          6379/TCP         60s
```

To confirm it works, from Powershell type:
```ps
$ (curl "http://20.49.215.216:3001/?sum=1,2,3").content
6
$ (curl "http://20.49.215.216:3001/?sum=1,2,3,4").content
10
```
