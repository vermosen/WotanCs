using System;
using System.IO;

namespace Wotan
{
    public abstract class serializer<T> : IDisposable where T : class, new()
    {
        public T deserializeFromFile(string relativePath)
        {
            using (FileStream file = new FileStream(Path.GetFullPath(relativePath), 
                FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return toObject(file);
            }
        }

        public abstract T toObject(Stream xml);
        public abstract Stream fromObject(T obj);

        public void Dispose() {}
    }
}
