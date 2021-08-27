using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await SayHelloStreaming();
        }

        private static async Task SayHelloUnary()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            var response = await client.SayHelloAsync(new HelloRequest
            {
                Name = "Brownbag demo"
            });

            Console.WriteLine(response.Message);
        }

        private static async Task SayHelloStreaming()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            var response = client.SayHelloStream(new HelloRequest
            {
                Name = "Brownbag demo straming"
            });

            await foreach(var item in response.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($"Response from server: {item.Message}");
            }
        }
    }
}
