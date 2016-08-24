using Ada.Framework.Expressions.Entities;

namespace Ada.Framework.Expressions.Evaluators
{
    public class EvaluadorIn : Evaluador
    {
        public EvaluadorIn()
        {
            codigo = "In";
            Parametros.Add("Value");
            delegado = ((objeto, condicion) =>
            {
                object valorExpresion = ObtenerValorExpresion(objeto, condicion.Expresion);
                return condicion.ObtenerParametro("Value").Valor.ToString().Trim().Contains(valorExpresion.ToString().Trim());
            });
        }
    }
}
