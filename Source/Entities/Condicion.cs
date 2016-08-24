using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Ada.Framework.Expressions.Entities
{
    [XmlType("Condition")]
    public class Condicion : ExpresionCondicional
    {
        [XmlAttribute(AttributeName = "Expression")]
        public virtual string Expresion { get; set; }

        [XmlAttribute(AttributeName = "Operation")]
        public virtual string Operacion { get; set; }

        [XmlArray("Parameters")]
        public List<Parametro> Parametros { get; set; }


        public Parametro ObtenerParametro(string nombre)
        {
            Parametro retorno = Parametros.First(c => c.Nombre == nombre);

            if (retorno.Valor.ToString().Equals("[Today]", StringComparison.InvariantCultureIgnoreCase))
            {
                retorno.Valor = DateTime.Today;
            }
            else if (retorno.Valor.ToString().Equals("[Now]", StringComparison.InvariantCultureIgnoreCase))
            {
                retorno.Valor = DateTime.Now;
            }

            return retorno;
        }

        public Condicion()
        {
            Parametros = new List<Parametro>();
        }
    }
}
