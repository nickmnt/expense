# expense
.NET expense tracker microservices.
Expense tracker with Authentication, Grpc, RabbitMQ, SQL and NoSQL databases

- IdentityService  
- GatewayService  
- CategoryService  
- BudgetService  
- ExpenseService

In progress

## K8S Setup

```bash
docker run -d -p 5001:5001 --restart=always --name local-registry registry:2
```

```bash
docker build -t localhost:5001/categoryservice:latest .
```

```bash
kubectl create secret generic postgres-secret --from-literal=password="postgrespw"
```

```bash
kubectl create secret generic mongo-secret --from-literal=username="root" --from-literal=password="mongopw"
```
