namespace DotNetty.Codecs.MessagePack {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using DotNetty.Buffers;
    using DotNetty.Transport.Channels;

    using global::MessagePack;

    public sealed class MessagePackDecoder<T> : MessageToMessageDecoder<IByteBuffer> {
        protected override void Decode (IChannelHandlerContext context, IByteBuffer message, List<object> output) {
            Contract.Requires (context != null);
            Contract.Requires (message != null);
            Contract.Requires (output != null);
            Contract.Requires (message.ReadableBytes > 0);

            try {
                T decoded = LZ4MessagePackSerializer.Deserialize<T> (message.Array.Take (message.ReadableBytes).ToArray ());

                if (decoded != null)
                    output.Add (decoded);
            } catch (Exception exception) {
                throw new CodecException (exception);
            }
        }
    }
}
