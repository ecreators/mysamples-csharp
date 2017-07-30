using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace mysamples.api.util
{
    public class GenericTypeLoader<T> : GenericTypeLoad
    {
        public GenericTypeLoader(byte[] typeData)
        {
            Instance = upgrade(typeData);
        }

        public GenericTypeLoader(T codeBehindHandler)
        {
            Instance = codeBehindHandler;
        }

        public T Instance { get; private set; }

        public T upgrade(byte[] typeData)
        {
            Instance = FromByteArray<T>(typeData);
            return Instance;
        }
    }

    public class GenericTypeLoad
    {
        public static byte[] ToByteArray<TIn>(TIn obj)
        {
            if (obj == null)
            {
                return null;
            }
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static TOut FromByteArray<TOut>(byte[] data)
        {
            if (data == null)
            {
                return default(TOut);
            }
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream(data))
            {
                var value = bf.Deserialize(ms);
                return (TOut)value;
            }
        }
    }
}