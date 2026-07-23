using System.Xml;

namespace Estoque.Infrastructure.Utilidades
{
    public class Constants
    {
        public static string Connection { get; set; }

        public string ConfigFilePath {
            set
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(value);
                XmlNode? node = xml.DocumentElement?.SelectSingleNode("connectionStrings/add[@name='STOCKFLOW']");
                Connection = node?.Attributes?["value"]?.Value;
            }
        }
    }
}
