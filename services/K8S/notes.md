### Starting Minikube
```shell
minikube start --driver=docker
```

### Manually Launching without the config files

1. Create a Kubernetes deployment
    
    ```shell
    kubectl create deployment platforms-deploy --image=sw4mpd0nkey/platformservice:latest
    ```
    
    Copy
    
2. Create a Kubernetes service type NodePort
    
    ```shell
    kubectl expose deployment platforms-deploy --type=NodePort --port=8080
    ```
    
    Copy
    
3. Check Node Port
    
    ```shell
    $ kubectl get svc
    NAME              TYPE        CLUSTER-IP      EXTERNAL-IP   PORT(S)          AGE
    hello-minikube1   NodePort    10.100.238.34   <none>        8080:31389/TCP   3s
    ```
    
    Copy
    
4. Run service tunnel
    
    ```shell
    minikube service platform-deploy --url
    ```
    
    Copy
    
    `minikube service hello-minikube1 --url` runs as a process, creating a [tunnel](https://en.wikipedia.org/wiki/Port_forwarding#Local_port_forwarding) to the cluster. The command exposes the service directly to any program running on the host operating system.
    
    service output example
    
     $ minikube service hello-minikube1 --url
     http://127.0.0.1:57123
     ❗  Because you are using a Docker driver on darwin, the terminal needs to be open to run it.
     
    
    Check ssh tunnel in another terminal
    
    ```shell
    $ ps -ef | grep docker@127.0.0.1
    ssh -o UserKnownHostsFile=/dev/null -o StrictHostKeyChecking=no -N docker@127.0.0.1 -p 55972 -i /Users/FOO/.minikube/machines/minikube/id_rsa -L TUNNEL_PORT:CLUSTER_IP:TARGET_PORT
    ```
    
    
5. Try in your browser
    
    Open in your browser (ensure there is no proxy set)
    
    ```shell
    http://127.0.0.1:TUNNEL_PORT
    ```
    