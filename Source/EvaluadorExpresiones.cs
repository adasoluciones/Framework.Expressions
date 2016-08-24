using Ada.Framework.Expressions.Entities;
using Ada.Framework.Expressions.Evaluators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ada.Framework.Expressions
{
    public static class EvaluadorExpresiones
    {
        public static IList<Evaluador<object>> EvaluadoresGenericos
        {
            get
            {
                return new List<Evaluador<object>>()
                    {
                        new EvaluadorDistintoQue(),
                        new EvaluadorIgualQue(),
                        new EvaluadorIgualQueIgnorarCaso(),
                        new EvaluadorIn(),
                        new EvaluadorMayorIgualQue(),
                        new EvaluadorMayorQue(),
                        new EvaluadorMenorIgualQue(),
                        new EvaluadorMenorQue()
                    };
            }
        }

        public static Evaluador<object> ObtenerEvaluador(string nombre, IList<Evaluador<object>> evaluadores, bool buscarEvaluadorGenerico = true)
        {
            if (evaluadores != null)
            {
                foreach (Evaluador<object> evaluador in evaluadores)
                {
                    if (evaluador.Codigo.Trim().Equals(nombre, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return evaluador;
                    }
                }
            }

            if (buscarEvaluadorGenerico)
            {
                foreach (Evaluador<object> evaluadorGenerico in EvaluadoresGenericos)
                {
                    if (evaluadorGenerico.Codigo.Trim().Equals(nombre, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return evaluadorGenerico;
                    }
                }
            }

            throw new Exception("¡No se encontro un evaluador para la operación!");
        }

        public static bool EvaluarCondicion<T>(T objeto, Condicion condicion, IList<Evaluador<T>> evaluadores, bool buscarEvaluadorGenerico = true)
        {
            if (evaluadores != null)
            {
                foreach (Evaluador<T> evaluador in evaluadores)
                {
                    if (condicion.Operacion.Trim().Equals(evaluador.Codigo.Trim(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        foreach (string parametro in evaluador.Parametros)
                        {
                            if (condicion.Parametros.Count(c => c.Nombre == parametro) == 0)
                            {
                                throw new Exception(string.Format("¡No se le otorgó el parámetro {0} para el evaluador {1}!",
                                    parametro, evaluador.Codigo));
                            }
                        }

                        return evaluador.Delegado(objeto, condicion);
                    }
                }
            }

            if (buscarEvaluadorGenerico)
            {
                foreach (Evaluador<object> evaluadorGenerico in EvaluadoresGenericos)
                {
                    if (condicion.Operacion.Trim().Equals(evaluadorGenerico.Codigo.Trim(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        foreach (string parametro in evaluadorGenerico.Parametros)
                        {
                            if (condicion.Parametros.Count(c => c.Nombre == parametro) == 0)
                            {
                                throw new Exception(string.Format("¡No se le otorgó el parámetro {0} para el evaluador {1}!",
                                    parametro, evaluadorGenerico.Codigo));
                            }
                        }

                        return evaluadorGenerico.Delegado(objeto, condicion);
                    }
                }
            }
            throw new Exception("¡No se encontro un evaluador para la operación!");
        }

        public static bool EvaluarCondicion<T>(T objeto, Condicion condicion, Evaluador<T> evaluador, bool buscarEvaluadorGenerico = true)
        {
            return EvaluarCondicion<T>(objeto, condicion, new List<Evaluador<T>>() { evaluador }, buscarEvaluadorGenerico);
        }
        
        public static bool EvaluarCondicion(object objeto, Condicion condicion, Evaluador<object> evaluador, bool buscarEvaluadorGenerico = true)
        {
            return EvaluarCondicion<object>(objeto, condicion, evaluador, buscarEvaluadorGenerico);
        }

        public static bool EvaluarCondicion(object objeto, Condicion condicion, IList<Evaluador<object>> evaluadores, bool buscarEvaluadorGenerico = true)
        {
            return EvaluarCondicion<object>(objeto, condicion, evaluadores, buscarEvaluadorGenerico);
        }

        public static bool EvaluarCondicion<T>(T objeto, Condicion condicion)
        {
            return EvaluarCondicion<object>(objeto, condicion, EvaluadoresGenericos, false);
        }

        public static bool EvaluarCondicion(object objeto, Condicion condicion)
        {
            return EvaluarCondicion(objeto, condicion, EvaluadoresGenericos, false);
        }


        public static bool EvaluarCondiciones<T>(T objeto, IList<ExpresionCondicional> condiciones, IList<Evaluador<T>> evaluadores, bool buscarEvaluadorGenerico = true)
        {
            if (condiciones != null)
            {
                bool esValido = true;

                foreach (ExpresionCondicional expresionCondicional in condiciones)
                {
                    Condicion condicion = expresionCondicional as Condicion;
                    bool enControEvaluador = false;
                    
                    if (evaluadores != null)
                    {
                        foreach (Evaluador<T> evaluador in evaluadores)
                        {
                            if (condicion.Operacion.Trim().Equals(evaluador.Codigo.Trim(), StringComparison.InvariantCultureIgnoreCase))
                            {
                                foreach (string parametro in evaluador.Parametros)
                                {
                                    if (condicion.Parametros.Count(c => c.Nombre == parametro) == 0)
                                    {
                                        throw new Exception(string.Format("¡No se le otorgó el parámetro {0} para el evaluador {1}!",
                                            parametro, evaluador.Codigo));
                                    }
                                }

                                esValido = evaluador.Delegado(objeto, condicion) && esValido;
                                enControEvaluador = true;
                                break;
                            }
                        }
                    }

                    if (buscarEvaluadorGenerico)
                    {
                        foreach (Evaluador<object> evaluadorGenerico in EvaluadoresGenericos)
                        {
                            if (condicion.Operacion.Trim().Equals(evaluadorGenerico.Codigo.Trim(), StringComparison.InvariantCultureIgnoreCase))
                            {
                                foreach (string parametro in evaluadorGenerico.Parametros)
                                {
                                    if (condicion.Parametros.Count(c => c.Nombre == parametro) == 0)
                                    {
                                        throw new Exception(string.Format("¡No se le otorgó el parámetro {0} para el evaluador {1}!",
                                            parametro, evaluadorGenerico.Codigo));
                                    }
                                }

                                esValido = evaluadorGenerico.Delegado(objeto, condicion) && esValido;
                                enControEvaluador = true;
                                break;
                            }
                        }
                    }

                    if (!enControEvaluador)
                    {
                        throw new Exception("¡No se encontro un evaluador para la operación !");
                    }

                    if(!esValido)
                    {
                        return false;
                    }
                }
                return esValido;
            }
            return true;
        }

        public static bool EvaluarCondiciones(object objeto, IList<ExpresionCondicional> condiciones, IList<Evaluador<object>> evaluadores, bool buscarEvaluadorGenerico = true)
        {
            return EvaluarCondiciones<object>(objeto, condiciones, evaluadores, buscarEvaluadorGenerico);
        }
    }
}
