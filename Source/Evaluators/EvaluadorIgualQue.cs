using Ada.Framework.Expressions.Entities;

namespace Ada.Framework.Expressions.Evaluators
{
    public class EvaluadorIgualQue : Evaluador
    {
        public EvaluadorIgualQue()
        {
            codigo = "EQU";
            Parametros.Add("Value");
            delegado = ((objeto, condicion) =>
            {
                object valorExpresion = ObtenerValorExpresion(objeto, condicion.Expresion);
                return valorExpresion.ToString().Trim() == condicion.ObtenerParametro("Value").Valor.ToString().Trim();
            });
        }
    }
}
