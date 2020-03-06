using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Threading.Tasks;

namespace DotNettyDemo.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            RunClientAsync().Wait();
        }

        static async Task RunClientAsync()
        {
            IEventLoopGroup loopGroup = new MultithreadEventLoopGroup();

            try
            {
                Bootstrap bootstrap = new Bootstrap();

                bootstrap
                    .Group(loopGroup)
                    .Channel<TcpSocketChannel>()
                    .Handler(new ActionChannelInitializer<IChannel>(chnl =>
                    {
                        chnl.Pipeline.AddLast(new ClientTestHandler());
                    }));

                IChannel channel = await bootstrap.ConnectAsync("192.168.1.4", 9001);

                Console.ReadLine();//阻塞线程

                await channel.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                await loopGroup.ShutdownGracefullyAsync();
            }
        }
    }
}
