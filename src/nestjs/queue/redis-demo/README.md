# Getting started

## docker-compose

To run using docker-compose, type:

```
$ docker-compose -f .\docker-compose-demo.yml up --build
```

To confirm it works, from Powershell type:
```ps
$ (curl "http://localhost:3000/?sum=1,2,3").content
{"jobId":"1"}
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
```ps
$ kubectl config set-context --current --namespace=meetup-nestjs-queue-demo
```

To get a list of services (we're looking for the http-svc), type:
```ps
$ kubectl.exe get svc
NAME        TYPE           CLUSTER-IP     EXTERNAL-IP     PORT(S)          AGE
http-svc    LoadBalancer   10.0.5.183     20.49.212.206   3002:30488/TCP   60s
redis-svc   ClusterIP      10.0.255.179   <none>          6379/TCP         60s
```

To confirm it works, from Powershell type:
```ps
$ (curl "http://20.49.212.206:3002/?sum=1,2,3").content
{"jobId":"1"}
$ (curl "http://20.49.212.206:3002/?sum=1,2,3,4").content
{"jobId":"2"}
```

## OpenAPI

From a browser, pasted this in `http://20.49.215.216:3002/api` (making sure you replace the Service IP address).

This will reveal the Swagger UI representing the HTTP API controller methods.  From here you can `try it out` the `/` endpoint.

To see the tail of the log, type:
```ps
$ kubectl.exe logs -l run=consumer -f
[Nest] 63   - 11/05/2020, 8:27:50 AM   [Consumer.onProcess] Job Attempt: 0
[Nest] 63   - 11/05/2020, 8:27:50 AM   [Consumer.onActive] Processing job 1 with data 2,3,4,5,6,7...
[Nest] 63   - 11/05/2020, 8:27:50 AM   [Consumer.onQueueFailed] on failure: 1
[Nest] 62   - 11/05/2020, 8:27:50 AM   [Consumer.onProcess] Job Attempt: 1
[Nest] 62   - 11/05/2020, 8:27:50 AM   [Consumer.onActive] Processing job 1 with data 2,3,4,5,6,7...
[Nest] 62   - 11/05/2020, 8:27:50 AM   [Consumer.onQueueFailed] on failure: 1
[Nest] 63   - 11/05/2020, 8:27:50 AM   [Consumer.onProcess] Job Attempt: 2
[Nest] 63   - 11/05/2020, 8:27:50 AM   [Consumer.onActive] Processing job 1 with data 2,3,4,5,6,7...
[Nest] 63   - 11/05/2020, 8:27:50 AM   [Consumer.onProgress] Progressing job 1 is 0...
[Nest] 63   - 11/05/2020, 8:27:50 AM   [Consumer.onProgress] Progressing job 1 is 16.666666666666664...
[Nest] 63   - 11/05/2020, 8:27:50 AM   [Consumer.onProgress] Progressing job 1 is 33.33333333333333...
[Nest] 63   - 11/05/2020, 8:27:50 AM   [Consumer.onProgress] Progressing job 1 is 50...
[Nest] 63   - 11/05/2020, 8:27:50 AM   [Consumer.onProgress] Progressing job 1 is 66.66666666666666...
[Nest] 63   - 11/05/2020, 8:27:50 AM   [Consumer.onProgress] Progressing job 1 is 83.33333333333334...
[Nest] 63   - 11/05/2020, 8:27:50 AM   [Consumer.onQueueCompleted] on completed job: 1
```

There are 2 replicas and so this is why we're seeing 👆 2 different pids (62 & 63).