using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNettyDemo.Client
{
    public class ClientTestHandler : SimpleChannelInboundHandler<IByteBuffer>
    {
        public void SendMessage(string msg)
        { 
            
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, IByteBuffer msg)
        {
            //if (msg != null)
            //{
            //    Console.WriteLine("Receive From Server--" + msg.ToString(Encoding.UTF8));
            //}
            //ctx.WriteAsync(Unpooled.CopiedBuffer(msg));
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine(exception);
            context.CloseAsync();
        }
    }
}
