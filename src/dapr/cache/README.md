# Getting starts

First, build and upload container image by typing:

```ps
$ build.bat
```

Next, let's deploy our application - http-api:

_The sample application has both the SDK and HTTP implementations. You can comment/uncomment code to switch between them_

```yml
$ kubectl apply -f aks-deploy-cache-demo.yml
```

This 👆 will deploy a http-api.

To get the component logs for http-api application, type: 
```ps
$ dapr logs -a http-api -k -n meetup-dapr-demo
```

Let's ascertain the IP address to use in our following HTTP Request, by typing:

```ps
$ kubectl.exe get svc
NAME        TYPE           CLUSTER-IP    EXTERNAL-IP     PORT(S)          AGE
redis-svc   LoadBalancer   10.0.212.80   20.49.164.164   6379:31300/TCP   16s
http-svc    LoadBalancer   10.0.212.81   51.145.127.142  3004:31301/TCP   122s
```

Time to test the http-api:

```ps
Invoke-RestMethod -Method POST "http://51.145.127.142:3004/cache/?key=name&data=garrardkitchen"
Invoke-RestMethod -Method GET "http://51.145.127.142:3004/cache/?key=name"
```

To get the applications logs (and follow tail) for http-api, type:
```ps
$ kubectl.exe logs -l run=http-api -c http-api -f
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://[::]:80
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /app
info: dotnet_demo.Controllers.CacheController[0]
      ATTEMPTING TO SET name=garrardkitchen
info: dotnet_demo.Controllers.CacheController[0]
      garrardkitchen SAVED WITH STATUS Created!
info: dotnet_demo.Controllers.CacheController[0]
      STATE IS "garrardkitchen" WITH OK!
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
