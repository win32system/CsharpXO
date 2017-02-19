using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XOClient.API
{
    public static class XmlPacketDecoder
    {
        public static TTTPacket Decode(string xmlStr)
        {
            TTTPacket inPacket = null;
            XmlSerializer serializer = new XmlSerializer(typeof(TTTPacket));
            using (StringReader textReader = new StringReader(xmlStr))
            {
                inPacket = (TTTPacket)serializer.Deserialize(textReader);
            }
            return inPacket;
        }

        public static string Encode(TTTPacket packet)
        {
            string result = null;
            XmlSerializer serializer = new XmlSerializer(typeof(TTTPacket));
            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, packet);
                result = textWriter.ToString();
            }
            return result.Replace("\r\n", "");
        }
    }
}
