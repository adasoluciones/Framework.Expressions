using Ada.Framework.Expressions.Entities;
using System;

namespace Ada.Framework.Expressions.Evaluators
{
    public class EvaluadorMayorIgualQue : Evaluador
    {
        public EvaluadorMayorIgualQue()
        {
            codigo = "GEQ";
            Parametros.Add("Value");
            delegado = ((objeto, condicion) =>
            {
                object valorExpresion = ObtenerValorExpresion(objeto, condicion.Expresion);

                if (valorExpresion is DateTime && condicion.ObtenerParametro("Value").Valor is DateTime)
                {
                    return ((DateTime)valorExpresion) >= ((DateTime)condicion.ObtenerParametro("Value").Valor);
                }
                else if (valorExpresion is int && condicion.ObtenerParametro("Value").Valor is int)
                {
                    return ((int)valorExpresion) >= ((int)condicion.ObtenerParametro("Value").Valor);
                }
                else if (valorExpresion is double && condicion.ObtenerParametro("Value").Valor is double)
                {
                    return ((double)valorExpresion) >= ((double)condicion.ObtenerParametro("Value").Valor);
                }

                throw new Exception(string.Format("¡No se puede comparar un {0} y un {1}!", valorExpresion.GetType().Name, condicion.ObtenerParametro("Value").Valor.GetType().Name));
            });
        }
    }
}
