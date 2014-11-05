using System;
using System.IO;

namespace Webserver.Infrastructure
{
    public class FileSystem
    {
        public static string MapPath(string basePath, string path)
        {
            var extension = Path.GetExtension(path);
            var hasExtension = Path.HasExtension(path);
            if (!path.EndsWith("/") && !hasExtension && string.IsNullOrWhiteSpace(extension))
            {
                path = path + "/";
            }

            path = path.Replace('/', Path.DirectorySeparatorChar);
            path = path.TrimStart(Path.DirectorySeparatorChar);

            path = Path.Combine(basePath, path);

            hasExtension = Path.HasExtension(path);
            var fileName = hasExtension ? Path.GetFileName(path) : String.Empty;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                path = Path.Combine(path, "index.html");
            }

            return path;
        }

    }
}
