namespace DotNetty.Codecs.MessagePack {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using DotNetty.Buffers;
    using DotNetty.Transport.Channels;

    using global::MessagePack;

    public sealed class MessagePackEncoder<T> : MessageToMessageEncoder<T> {
        protected override void Encode (IChannelHandlerContext context, T message, List<object> output) {
            Contract.Requires (context != null);
            Contract.Requires (message != null);
            Contract.Requires (output != null);

            IByteBuffer buffer = null;

            try {
                buffer = Unpooled.WrappedBuffer (LZ4MessagePackSerializer.Serialize (message));
                output.Add (buffer);
                buffer = null;
            } catch (Exception exception) {
                throw new CodecException (exception);
            } finally {
                buffer?.Release ();
            }
        }
    }
}