# expense
.NET expense tracker microservices.
Expense tracker with Authentication, Grpc, RabbitMQ, SQL and NoSQL databases

In progress

## K8S Setup

```bash
kubectl create secret generic postgres-secret --from-literal=password="postgrespw"
```

```bash
kubectl create secret generic mongo-secret --from-literal=username="root" --from-literal=password="mongopw"
```
