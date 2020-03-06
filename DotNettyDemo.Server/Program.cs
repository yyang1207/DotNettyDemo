using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Threading.Tasks;

namespace DotNettyDemo.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            RunServerAsync().Wait();
        }

        static async Task RunServerAsync()
        {
            IEventLoopGroup eventLoop = new MultithreadEventLoopGroup();

            try
            {
                ServerBootstrap bootstrap = new ServerBootstrap();
                bootstrap.Group(eventLoop);
                bootstrap.Channel<TcpServerSocketChannel>();
                bootstrap.ChildHandler(new ActionChannelInitializer<IChannel>(chl =>
                {
                    chl.Pipeline.AddLast(new ServerTestHandler());
                }));

                IChannel channel = await bootstrap.BindAsync(9001);

                Console.ReadLine();//阻塞线程

                await channel.CloseAsync();
            }
            catch (Exception ex)
            { }
            finally
            {
                await eventLoop.ShutdownGracefullyAsync();
            }
        }
    }
}
