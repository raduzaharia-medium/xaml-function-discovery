using System.IO;

namespace IntrusionDetection
{
    public static class Common
    {
        private static Random generator = new Random();
        private static List<IMathExpression> mathExpressions = new List<IMathExpression>();
        private static List<IMathExpression> operands = new List<IMathExpression>();
        private static List<IMathExpression> operators = new List<IMathExpression>();
        private static List<TrainingData> date = new List<TrainingData>();

        public static Random Generator => generator;

        public static IList<IMathExpression> Operands
        {
            get
            {
                if (operands.Count == 0) operands = LoadVariables();
                return operands;
            }
        }
        public static IList<IMathExpression> Operators
        {
            get
            {
                if (operators.Count == 0) operators = LoadOperators();
                return operators;
            }
        }
        public static IList<IMathExpression> MathExpressions
        {
            get
            {
                if (mathExpressions.Count == 0)
                {
                    mathExpressions = new List<IMathExpression>();
                    mathExpressions.AddRange(Operands);
                    mathExpressions.AddRange(Operators);
                }

                return mathExpressions;
            }
        }
        public static IList<TrainingData> TrainingData
        {
            get
            {
                if (date.Count == 0) date = LoadTrainingData("TrainingData.txt");
                return date;
            }
        }

        public static IMathExpression RandomMathExpression()
        {
            return MathExpressions[Generator.Next(MathExpressions.Count)];
        }
        public static IMathExpression RandomOperand()
        {
            return Operands[Generator.Next(Operands.Count)];
        }
        public static IMathExpression RandomOperator()
        {
            return Operators[Generator.Next(Operators.Count)];
        }

        public static void SetValues(double[] values)
        {
            for (int i = 0; i < operands.Count; i++)
                operands[i].Value = values[i];
        }

        private static List<IMathExpression> LoadOperators()
        {
            var result = new List<IMathExpression>();
            var compositors = new List<Compositor>();
            var evaluators = new List<Evaluator>();

            compositors.Add(new Compositor
            {
                Symbol = "+",
                CompositorFunction = delegate (Operand[] operands)
            {
                double result = 0;

                for (int i = 0; i < operands.Length; i++) result += operands[i].Value;
                return new Operand { Value = result, Symbol = result.ToString() };
            }
            });
            compositors.Add(new Compositor
            {
                Symbol = "-",
                CompositorFunction = delegate (Operand[] operands)
            {
                double result = 0;
                if (operands.Length > 0) result = operands[0].Value;

                for (int i = 0; i < operands.Length; i++) result -= operands[i].Value;
                return new Operand { Value = result, Symbol = result.ToString() };
            }
            });
            compositors.Add(new Compositor
            {
                Symbol = "*",
                CompositorFunction = delegate (Operand[] operands)
            {
                double result = 0;
                if (operands.Length > 0) result = operands[0].Value;

                for (int i = 0; i < operands.Length; i++) result *= operands[i].Value;
                return new Operand { Value = result, Symbol = result.ToString() };
            }
            });
            compositors.Add(new Compositor
            {
                Symbol = "/",
                CompositorFunction = delegate (Operand[] operands)
            {
                double result = 0;
                if (operands.Length > 0) result = operands[0].Value;

                for (int i = 0; i < operands.Length; i++) result /= operands[i].Value;
                return new Operand { Value = result, Symbol = result.ToString() };
            }
            });
            compositors.Add(new Compositor
            {
                Symbol = "^",
                CompositorFunction = delegate (Operand[] operands)
            {
                double result = 0;
                if (operands.Length > 0) result = operands[0].Value;

                for (int i = 0; i < operands.Length; i++) result = Math.Pow(result, operands[i].Value);
                return new Operand { Value = result, Symbol = result.ToString() };
            }
            });

            evaluators.Add(new Evaluator
            {
                Symbol = "+",
                EvaluatorFunction = delegate (Operand operand)
            {
                return new Operand { Value = operand.Value, Symbol = operand.Value.ToString() };
            }
            });
            evaluators.Add(new Evaluator
            {
                Symbol = "-",
                EvaluatorFunction = delegate (Operand operand)
            {
                return new Operand { Value = -operand.Value, Symbol = (-operand.Value).ToString() };
            }
            });
            evaluators.Add(new Evaluator
            {
                Symbol = "*",
                EvaluatorFunction = delegate (Operand operand)
            {
                return new Operand { Value = operand.Value, Symbol = operand.Value.ToString() };
            }
            });
            evaluators.Add(new Evaluator
            {
                Symbol = "/",
                EvaluatorFunction = delegate (Operand operand)
            {
                return new Operand { Value = operand.Value, Symbol = operand.Value.ToString() };
            }
            });
            evaluators.Add(new Evaluator
            {
                Symbol = "sin",
                EvaluatorFunction = delegate (Operand operand)
            {
                return new Operand { Value = Math.Sin(operand.Value), Symbol = Math.Cos(operand.Value).ToString() };
            }
            });
            evaluators.Add(new Evaluator
            {
                Symbol = "cos",
                EvaluatorFunction = delegate (Operand operand)
            {
                return new Operand { Value = Math.Cos(operand.Value), Symbol = Math.Cos(operand.Value).ToString() };
            }
            });
            evaluators.Add(new Evaluator
            {
                Symbol = "tan",
                EvaluatorFunction = delegate (Operand operand)
            {
                return new Operand { Value = Math.Tan(operand.Value), Symbol = Math.Tan(operand.Value).ToString() };
            }
            });
            evaluators.Add(new Evaluator
            {
                Symbol = "acos",
                EvaluatorFunction = delegate (Operand operand)
            {
                return new Operand { Value = Math.Acos(operand.Value), Symbol = Math.Acos(operand.Value).ToString() };
            }
            });
            evaluators.Add(new Evaluator
            {
                Symbol = "asin",
                EvaluatorFunction = delegate (Operand operand)
            {
                return new Operand { Value = Math.Asin(operand.Value), Symbol = Math.Asin(operand.Value).ToString() };
            }
            });
            evaluators.Add(new Evaluator
            {
                Symbol = "atan",
                EvaluatorFunction = delegate (Operand operand)
            {
                return new Operand { Value = Math.Atan(operand.Value), Symbol = Math.Atan(operand.Value).ToString() };
            }
            });
            evaluators.Add(new Evaluator
            {
                Symbol = "cosh",
                EvaluatorFunction = delegate (Operand operand)
            {
                return new Operand { Value = Math.Cosh(operand.Value), Symbol = Math.Cosh(operand.Value).ToString() };
            }
            });
            evaluators.Add(new Evaluator
            {
                Symbol = "sqrt",
                EvaluatorFunction = delegate (Operand operand)
            {
                return new Operand { Value = Math.Sqrt(operand.Value), Symbol = Math.Sqrt(operand.Value).ToString() };
            }
            });
            evaluators.Add(new Evaluator
            {
                Symbol = "sqr",
                EvaluatorFunction = delegate (Operand operand)
            {
                return new Operand { Value = operand.Value, Symbol = operand.Value.ToString() };
            }
            });
            evaluators.Add(new Evaluator
            {
                Symbol = "lg",
                EvaluatorFunction = delegate (Operand operand)
            {
                return new Operand { Value = Math.Log10(operand.Value), Symbol = Math.Log10(operand.Value).ToString() };
            }
            });
            evaluators.Add(new Evaluator
            {
                Symbol = "exp",
                EvaluatorFunction = delegate (Operand operand)
            {
                return new Operand { Value = Math.Exp(operand.Value), Symbol = Math.Exp(operand.Value).ToString() };
            }
            });
            evaluators.Add(new Evaluator
            {
                Symbol = "inv",
                EvaluatorFunction = delegate (Operand operand)
            {
                return new Operand { Value = 1 / operand.Value, Symbol = (1 / operand.Value).ToString() };
            }
            });

            foreach (var evaluator in evaluators)
                foreach (var compositor in compositors)
                    result.Add(new Operator { Evaluator = evaluator, Compositor = compositor });

            return result;
        }
        private static List<IMathExpression> LoadVariables()
        {
            var result = new List<IMathExpression>();

            for (int i = 0; i < TrainingData[0].Points.Length; i++)
                result.Add(new Operand { Symbol = ((char)(97 + i)).ToString() });
            return result;
        }
        private static List<TrainingData> LoadTrainingData(string fileName)
        {
            var result = new List<TrainingData>();

            using (var reader = new StreamReader(fileName))
            {
                var row = string.Empty;

                while ((row = reader.ReadLine()) != null)
                {
                    var components = row.Split(' ');
                    var points = new double[components.Length - 1];

                    for (var i = 0; i < components.Length - 1; i++) points[i] = double.Parse(components[i]);
                    result.Add(new TrainingData { Points = points, Value = double.Parse(components[components.Length - 1]) });
                }
            }
            return result;
        }
    }
}