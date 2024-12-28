# expense
.NET expense tracker microservices.
Expense tracker with Authentication, Grpc, RabbitMQ, SQL and NoSQL databases

- IdentityService  
- GatewayService  
- CategoryService  
- BudgetService  
- ExpenseService

In progress

## Image Setup

```bash
docker build -t categoryservice:latest .
```

```bash
docker build -t expenseservice:latest .
```

## K8S Setup

```bash
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.3.0/deploy/static/provider/cloud/deploy.yaml
```

```bash
kubectl create secret generic postgres-secret --from-literal=password="postgrespw"
```

```bash
kubectl create secret generic mongo-secret --from-literal=username="root" --from-literal=password="mongopw"
```
