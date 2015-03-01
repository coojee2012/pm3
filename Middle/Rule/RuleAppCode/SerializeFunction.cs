using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace RuleAppCode
{
    public class SerializeFunction
    {
        //序列化
        public static byte[] SerializeObject(object pObj)
        {
            if (pObj == null)
                return null;
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, pObj);
            memoryStream.Position = 0;
            byte[] read = new byte[memoryStream.Length];
            memoryStream.Read(read, 0, read.Length);
            memoryStream.Close();
            return read;
        }
        //反序列化
        public static T DeserializeObject<T>(byte[] pBytes)
        {
            object newOjb = null;
            if (pBytes == null)
            {
                return default(T);
            }

            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(pBytes))
            {
                memoryStream.Position = 0;
                BinaryFormatter formatter = new BinaryFormatter();
                newOjb = formatter.Deserialize(memoryStream);
                memoryStream.Close();
            }
            if (newOjb is T)
            {
                return (T)newOjb;
            }

            return default(T);
        }
    }
}
