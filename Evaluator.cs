namespace IntrusionDetection
{
    public delegate Operand EvaluatorFunction(Operand operand);

    public class Evaluator : IMathExpression
    {
        public EvaluatorFunction? EvaluatorFunction { get; set; }
        public double Value { get; set; }
        public string Symbol { get; set; } = string.Empty;

        public override string ToString()
        {
            return Symbol;
        }
    }
}