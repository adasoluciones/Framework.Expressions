using Ada.Framework.Expressions.Entities;
using System;

namespace Ada.Framework.Expressions.Evaluators
{
    public class EvaluadorIgualQueIgnorarCaso: Evaluador
    {
        public EvaluadorIgualQueIgnorarCaso()
        {
            codigo = "EQI";
            Parametros.Add("Value");
            delegado = ((objeto, condicion) =>
            {
                object valorExpresion = ObtenerValorExpresion(objeto, condicion.Expresion);
                return valorExpresion.ToString().Trim().Equals(condicion.ObtenerParametro("Value").Valor.ToString().Trim(), StringComparison.InvariantCultureIgnoreCase);
            });
        }
    }
}
