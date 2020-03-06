using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNettyDemo.Client
{
    public class ClientTestHandler : SimpleChannelInboundHandler<IByteBuffer>
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, IByteBuffer msg)
        {
            if (msg != null)
            {
                Console.WriteLine("Receive From Server--" + msg.ToString(Encoding.UTF8));
            }
            //ctx.WriteAsync(Unpooled.CopiedBuffer(msg));
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            context.Flush();
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            Console.WriteLine("发送Hello World");
            context.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.UTF8.GetBytes("Hello World!")));
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine(exception);
            context.CloseAsync();
        }
    }
}
