# Getting started

First, build and upload container images by typing:

```ps
$ build.bat
```

To deploy the containers required for this demo, type:

```yml
$ kubectl apply -f aks-deploy-eda-demo.yml
```

This 👆 will deploy a http-eda-api and bank-consumer.

Let's ascertain the IP address to use in our following HTTP Request, by typing:

```ps
$ kubectl.exe get svc
NAME        TYPE           CLUSTER-IP    EXTERNAL-IP     PORT(S)          AGE
redis-svc   LoadBalancer   10.0.212.80   20.49.164.164   6379:31300/TCP   16s
http-svc    LoadBalancer   10.0.212.81   51.145.127.142  3004:31301/TCP   122s
```



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

Now, let's deposit some money:

```ps
$ Invoke-RestMethod 'http://20.49.215.75:3005/publish/deposit' -Method 'POST' -Body "{`"id`":`"1`", `"amount`":116.57}" -ContentType "application/json"
...
info: dotnet_demo.Controllers.PublishController[0]
      AMOUNT TO DEPOSIT: 116.57
...
info: dotnet_demo.Controllers.ConsumerController[0]
      DEPOSITING 116.57 TO ACCOUNT 1
info: dotnet_demo.Controllers.ConsumerController[0]
      BALANCE 116.57 TO ACCOUNT 1
```

Now, let's withdraw some money:

```ps
$ Invoke-RestMethod 'http://20.49.215.75:3005/publish/withdraw' -Method 'POST' -Body "{`"id`":`"1`", `"amount`":16.57}" -ContentType "application/json"
...
info: dotnet_demo.Controllers.PublishController[0]
      AMOUNT TO WITHDRAW: 16.57
...
info: dotnet_demo.Controllers.ConsumerController[0]
      WITHDRAWING 16.57 FROM ACCOUNT 1
info: dotnet_demo.Controllers.ConsumerController[0]
      BALANCE 100.00 FROM ACCOUNT 1
```