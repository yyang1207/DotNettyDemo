using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Net;
using System.Text;
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
                var handler = new ClientTestHandler();

                Bootstrap bootstrap = new Bootstrap();

                bootstrap
                    .Group(loopGroup)
                    .Channel<TcpSocketChannel>()
                    .Handler(new ActionChannelInitializer<IChannel>(chnl =>
                    {
                        chnl.Pipeline.AddLast(handler);
                    }));

                IChannel channel = await bootstrap.ConnectAsync(IPAddress.Parse("192.168.1.5"), 3000);

                //while (true)
                //{
                //    // 根据设置建立缓存区大小
                //    IByteBuffer initialMessage = Unpooled.Buffer(1024 * 4);

                //    string str=Console.ReadLine();//阻塞线程
                //    if (str == "exit") break;
                //    await channel.WriteAndFlushAsync(initialMessage.WriteBytes(Encoding.UTF8.GetBytes(str)));

                //}

                long start = DateTime.Now.Ticks;

                for (int i = 0; i < 10_000_000; i++)
                {
                    // 根据设置建立缓存区大小
                    IByteBuffer initialMessage = Unpooled.Buffer(1024);
                    await channel.WriteAndFlushAsync(initialMessage.WriteBytes(Encoding.UTF8.GetBytes($"{i+1}")));
                }

                long end = DateTime.Now.Ticks;
                long timeclock = end - start;

                Console.WriteLine($"{timeclock/10}us--------{timeclock/10_000}ms----------{timeclock/10_000_000}s");

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
