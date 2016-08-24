using System;
using System.Collections.Generic;

namespace Ada.Framework.Expressions.Entities
{
    public abstract class Evaluador<T>
    {
        public IList<string> Parametros { get; set; }
        protected string codigo;
        protected Func<T, Condicion, bool> delegado;

        public string Codigo { get { return codigo; } }

        public Func<T, Condicion, bool> Delegado { get { return delegado; } }

        public Evaluador()
        {
            Parametros = new List<string>();
        }

        protected virtual object ObtenerValorExpresion(object objeto, string expresion)
        {
            if (expresion == "this")
            {
                return objeto;
            }

            if (expresion.Contains("."))
            {
                foreach (string prop in expresion.Split('.'))
                {
                    objeto = objeto.GetType().GetProperty(prop).GetValue(objeto, null);
                }
                return objeto;
            }
            return objeto.GetType().GetProperty(expresion).GetValue(objeto, null);
        }
    }

    public abstract class Evaluador : Evaluador<object>{ }
}
