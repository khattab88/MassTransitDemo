# MassTransitDemo

Simple Producer/Consumer scenario using MassTransit library over RabbitMQ Message Broker in .NET Core.

## Steps:
1. run rabbitmq server: `docker run -d --hostname rmq --name rabbit-server -p 15672:15672 -p 5672:5672 rabbitmq:3-management`
2. start InventoryService project (consumer)
3. start OrderService project (producer)
4. post new order (on order service)
5. inspect inventory service console (for incoming messages)

Reference: https://www.youtube.com/watch?v=1IF4uu0ptk4&ab_channel=DotNetCoreCentral
