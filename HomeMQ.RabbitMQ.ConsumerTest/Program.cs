using HomeMQ.RabbitMQ.Consumer;
using HomeMQ.Core;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace HomeMQ.RabbitMQ.ConsumerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var username = "devin";
            var password = "Ikorgil19";
            var factory = new ConnectionFactory()
            {
                HostName = "192.168.68.109",
                UserName = username,
                Password = password
            };

            using var conn = factory.CreateConnection();

            var exchangeName = "rtsh_topics";
            var routeKey = "master.control.*";

            var firstConsumer = new MasterControlConsumer(conn, null, exchangeName, routeKey).WithPrefetchCount(100);
            firstConsumer.Consume();



            Console.ReadLine();
        }

        static void HelloWorld(IModel channel)
        {
            channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[x] Received {message}");
            };

            channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);
        }

        static void PiStatus(IModel channel)
        {
            var exchangeName = "rtsh_topics";
            channel.ExchangeDeclare(exchange: exchangeName, type: "topic");
            var queueName = channel.QueueDeclare().QueueName;

            var routingKey = "master.control.*";
            channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var routingKey = ea.RoutingKey;
                Console.WriteLine($"[x] Received {routingKey} : {message}");

                //var json = JsonConvert.DeserializeObject<LightControlMessage>(message);
                //Console.WriteLine($"[x] Received message brightness = {json.Brightness}");

            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }

        static void NanoLeafTest(IModel channel)
        {
            var exchangeName = "topic_logs";
            channel.ExchangeDeclare(exchange: exchangeName, type: "topic");
            var queueName = channel.QueueDeclare().QueueName;

            var routingKey = "control.nanoleaf";
            channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var routingKey = ea.RoutingKey;
                Console.WriteLine($"[x] Received {routingKey} : {message}");

                var json = JsonConvert.DeserializeObject<LightControlMessage>(message);
                Console.WriteLine($"[x] Received message brightness = {json.Brightness}");

            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }

        static IPAddress FindMyNanoLeaf()
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 400;
            for (int i = 0; i < 200; i++)
            {
                byte[] b = { 192, 168, 68, (byte)(i) };
                var addr = new IPAddress(b);
                PingReply reply = pingSender.Send(addr, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine("Address: {0}", reply.Address.ToString());
                    Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                    Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                    Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                    Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
                }
            }

            var arpEntities = new ArpHelper().GetArpResult();
            //return arpEntities.First().ToString();
            //Console.WriteLine();
            var macAddress = PhysicalAddress.Parse("00-55-da-57-44-31");
            return arpEntities.FirstOrDefault(a => a.MacAddress.Equals(macAddress))?.Ip;

        }

        static void TopicTest(IModel channel)
        {
            var exchangeName = "topic_logs";
            channel.ExchangeDeclare(exchange: exchangeName, type: "topic");
            var queueName = channel.QueueDeclare().QueueName;

            var routingKey = "control.light";
            channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var routingKey = ea.RoutingKey;
                Console.WriteLine($"[x] Received {routingKey} : {message}");

                var json = JsonConvert.DeserializeObject<LightControlMessage>(message);
                Console.WriteLine($"[x] Received message brightness = {json.Brightness}");

            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }


        //static void GetLocalAddresses()
        //{
        //    var interfaces = NetworkInterface.GetAllNetworkInterfaces();
        //    foreach (var netInterface in interfaces)
        //    {
        //        if(netInterface.OperationalStatus != OperationalStatus.Up) { continue; }

        //        var props = netInterface.GetIPProperties();
        //        if(props.GatewayAddresses.Count == 0) { continue; }

        //        foreach (var addr in props.UnicastAddresses)
        //        {
        //            if(addr.Address.AddressFamily != AddressFamily.InterNetwork) { continue; }
        //            if (IPAddress.IsLoopback(addr.Address)) { continue; }
        //        }


        //    }


        //    for(int i = 101; i < 140; i++)
        //    {
        //        var ping = new Ping();
        //        //var reply = ping.Send(host, 50);
        //    }
        //}
    }
}
