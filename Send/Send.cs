using System;
using RabbitMQ.Client;
using System.Text;

class Send
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        string message = null;
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {

            while (true)    
            {
                channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
                message = string.Empty;
                message = "Hello World! ";
                message += DateTime.Now.ToString("yyyyMMddHHmmss");
                message += '_' + DateTime.Now.ToFileTime().ToString();

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                Console.WriteLine(" [x] Enviado : {0}", message);

                System.Threading.Thread.Sleep( 5 * 1000 );

            }

        }

    }

}