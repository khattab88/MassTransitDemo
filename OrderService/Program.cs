
using MassTransit;
using Models;

namespace OrderService
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

            // configure masstransit
            builder.Services.AddMassTransit(options =>
            {
                options.UsingRabbitMq((ctx, config) => 
                {
                    config.Host("amqp://guest:guest@localhost:5672");
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

            #region basic setup for Masstransit/RabbitMQ

            //var bus = Bus.Factory.CreateUsingRabbitMq(config =>
            //{
            //    config.Host("amqp://guest:guest@localhost:5672");

            //    config.ReceiveEndpoint("temp-queue", endpoint =>
            //    {
            //        endpoint.Handler<Order>(ctx =>
            //        {
            //            return Console.Out.WriteLineAsync(ctx.Message.Name);
            //        });
            //    });
            //});

            //bus.Start();

            //bus.Publish(new Order { Name = "test order" });

            #endregion

            app.Run();
        }
    }
}