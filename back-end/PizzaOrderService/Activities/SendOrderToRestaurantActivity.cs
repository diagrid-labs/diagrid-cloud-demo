using Dapr.Client;
using Dapr.Workflow;
using Shared.Models;

namespace OrderService.Activities
{
    public class SendOrderToKitchenActivity : WorkflowActivity<Order, object?>
    {
        readonly ILogger _logger;
        readonly DaprClient _daprClient;

        public SendOrderToKitchenActivity(ILoggerFactory loggerFactory, DaprClient daprClient)
        {
            _logger = loggerFactory.CreateLogger<SendOrderToKitchenActivity>();
            _daprClient = daprClient;
        }

        public override async Task<object?> RunAsync(WorkflowActivityContext context, Order order)
        {
            _logger.LogInformation($"Sending order {order.OrderId} to the kitchen.");
            await _daprClient.PublishEventAsync("pizza-pubsub", "pizza-orders", order);

            return null;
        }
    }
}