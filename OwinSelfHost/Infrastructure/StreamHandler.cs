using System.IO;

namespace Webserver.Infrastructure
{
    public class StreamHandler
    {
        public static void CopyStream(Stream outputStream, Stream inputStream)
        {
            int bytesRead;
            var buffer = new byte[Globals.BufferSize];

            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                outputStream.Write(buffer, 0, bytesRead);
                outputStream.Flush();
            }
            outputStream.Flush();
        }
    }
}
