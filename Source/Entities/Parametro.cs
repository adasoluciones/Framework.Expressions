using System.Xml.Serialization;

namespace Ada.Framework.Expressions.Entities
{
    [XmlType("Parameter")]
    public class Parametro
    {
        [XmlAttribute("Name")]
        public string Nombre { get; set; }

        [XmlElement("Value", typeof(object))]
        public object Valor { get; set; }
    }
}
