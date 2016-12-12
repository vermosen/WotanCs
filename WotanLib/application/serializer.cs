using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wotan
{
    public abstract class serializer<T> : IDisposable where T : class, new()
    {
        public T deserializeFromFile(string relativePath)
        {
            string path = Path.GetFullPath(relativePath);

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
