
using MassTransit;

namespace InventoryService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // configure masstransit (consumer)
            builder.Services.AddMassTransit(options =>
            {
                options.AddConsumer<OrderConsumer>();

                options.UsingRabbitMq((ctx, config) =>
                {
                    config.Host("amqp://guest:guest@localhost:5672");

                    config.ReceiveEndpoint("order-queue", endpoint =>
                    {
                        endpoint.ConfigureConsumer<OrderConsumer>(ctx);
                    });
                });
            });

            builder.Services.AddMassTransitHostedService();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}