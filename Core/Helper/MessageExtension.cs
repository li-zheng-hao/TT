//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//
//namespace Core
//{
//    /// <summary>
//    /// Extension methods on <see cref="IMessage"/> and <see cref="IMessage{T}"/>.
//    /// </summary>
//    public static class MessageExtension
//    {
//        public static CodedInputStream inputStream = new CodedInputStream(new byte[0]);
//
//        /// <summary>
//        /// Merges data from the given byte array into an existing message.
//        /// </summary>
//        /// <param name="message">The message to merge the data into.</param>
//        /// <param name="data">The data to merge, which must be protobuf-encoded binary data.</param>
//        public static void MergeFrom(this IMessage message, byte[] data)
//        {
//            ProtoPreconditions.CheckNotNull(message, "message");
//            ProtoPreconditions.CheckNotNull(data, "data");
//            inputStream.Reset(data, 0, data.Length);
//            CodedInputStream input = inputStream;
//            message.MergeFrom(input);
//            input.CheckReadEndOfStreamTag();
//        }
//
//        /// <summary>
//        /// Merges data from the given byte array into an existing message.
//        /// </summary>
//        /// <param name="message">The message to merge the data into.</param>
//        /// <param name="data">The data to merge, which must be protobuf-encoded binary data.</param>
//        public static void MergeFrom(this IMessage message, byte[] data, int offset, int length)
//        {
//            ProtoPreconditions.CheckNotNull(message, "message");
//            ProtoPreconditions.CheckNotNull(data, "data");
//            inputStream.Reset(data, offset, length);
//            CodedInputStream input = inputStream;
//            //CodedInputStream input = new CodedInputStream(data, offset, length);
//            message.MergeFrom(input);
//            input.CheckReadEndOfStreamTag();
//        }
//
//        /// <summary>
//        /// Merges data from the given byte string into an existing message.
//        /// </summary>
//        /// <param name="message">The message to merge the data into.</param>
//        /// <param name="data">The data to merge, which must be protobuf-encoded binary data.</param>
//        public static void MergeFrom(this IMessage message, ByteString data)
//        {
//            ProtoPreconditions.CheckNotNull(message, "message");
//            ProtoPreconditions.CheckNotNull(data, "data");
//            CodedInputStream input = data.CreateCodedInput();
//            message.MergeFrom(input);
//            input.CheckReadEndOfStreamTag();
//        }
//
//        /// <summary>
//        /// Merges data from the given stream into an existing message.
//        /// </summary>
//        /// <param name="message">The message to merge the data into.</param>
//        /// <param name="input">Stream containing the data to merge, which must be protobuf-encoded binary data.</param>
//        public static void MergeFrom(this IMessage message, Stream input)
//        {
//            ProtoPreconditions.CheckNotNull(message, "message");
//            ProtoPreconditions.CheckNotNull(input, "input");
//            CodedInputStream codedInput = new CodedInputStream(input);
//            message.MergeFrom(codedInput);
//            codedInput.CheckReadEndOfStreamTag();
//        }
//
//        /// <summary>
//        /// Merges length-delimited data from the given stream into an existing message.
//        /// </summary>
//        /// <remarks>
//        /// The stream is expected to contain a length and then the data. Only the amount of data
//        /// specified by the length will be consumed.
//        /// </remarks>
//        /// <param name="message">The message to merge the data into.</param>
//        /// <param name="input">Stream containing the data to merge, which must be protobuf-encoded binary data.</param>
//        public static void MergeDelimitedFrom(this IMessage message, Stream input)
//        {
//            ProtoPreconditions.CheckNotNull(message, "message");
//            ProtoPreconditions.CheckNotNull(input, "input");
//            int size = (int)CodedInputStream.ReadRawVarint32(input);
//            Stream limitedStream = new LimitedInputStream(input, size);
//            message.MergeFrom(limitedStream);
//        }
//
//        /// <summary>
//        /// Converts the given message into a byte array in protobuf encoding.
//        /// </summary>
//        /// <param name="message">The message to convert.</param>
//        /// <returns>The message data as a byte array.</returns>
//        public static byte[] ToByteArray(this IMessage message)
//        {
//            ProtoBuf.(message, "message");
//            byte[] result = new byte[message.CalculateSize()];
//            CodedOutputStream output = new CodedOutputStream(result);
//            message.WriteTo(output);
//            output.CheckNoSpaceLeft();
//            return result;
//        }
//
//        /// <summary>
//        /// Writes the given message data to the given stream in protobuf encoding.
//        /// </summary>
//        /// <param name="message">The message to write to the stream.</param>
//        /// <param name="output">The stream to write to.</param>
//        public static void WriteTo(this IMessage message, Stream output)
//        {
//            ProtoPreconditions.CheckNotNull(message, "message");
//            ProtoPreconditions.CheckNotNull(output, "output");
//            CodedOutputStream codedOutput = new CodedOutputStream(output);
//            message.WriteTo(codedOutput);
//            codedOutput.Flush();
//        }
//
//        /// <summary>
//        /// Writes the length and then data of the given message to a stream.
//        /// </summary>
//        /// <param name="message">The message to write.</param>
//        /// <param name="output">The output stream to write to.</param>
//        public static void WriteDelimitedTo(this IMessage message, Stream output)
//        {
//            ProtoPreconditions.CheckNotNull(message, "message");
//            ProtoPreconditions.CheckNotNull(output, "output");
//            CodedOutputStream codedOutput = new CodedOutputStream(output);
//            codedOutput.WriteRawVarint32((uint)message.CalculateSize());
//            message.WriteTo(codedOutput);
//            codedOutput.Flush();
//        }
//
//        /// <summary>
//        /// Converts the given message into a byte string in protobuf encoding.
//        /// </summary>
//        /// <param name="message">The message to convert.</param>
//        /// <returns>The message data as a byte string.</returns>
//        public static ByteString ToByteString(this IMessage message)
//        {
//            ProtoPreconditions.CheckNotNull(message, "message");
//            return ByteString.AttachBytes(message.ToByteArray());
//        }
//    }
//}
