using MassTransit;
using Models;

namespace InventoryService
{
    public class OrderConsumer : IConsumer<Order>
    {
        private readonly ILogger<OrderConsumer> _logger;

        public OrderConsumer(ILogger<OrderConsumer> logger)
        {
            this._logger = logger;
        }

        public async Task Consume(ConsumeContext<Order> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Name);

            _logger.LogInformation($"message received: {context.Message.Name}");
        }
    }
}