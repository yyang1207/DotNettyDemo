using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNettyDemo.Server
{
    public class ServerTestHandler: ChannelHandlerAdapter
    {
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="message">消息</param>
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            IByteBuffer msg = message as IByteBuffer;
            Console.WriteLine("收到信息：" + msg.ToString(Encoding.UTF8));
        }


        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            context.Flush();

            //Unpooled.CopiedBuffer(Encoding.UTF8.GetBytes("Hello World!"))
            context.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.UTF8.GetBytes("服务器发送信息:测试响应数据")));
            context.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.UTF8.GetBytes($"服务器响应时间:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}")));
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            base.ChannelActive(context);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine(exception);
            context.CloseAsync();
        }
    }
}
