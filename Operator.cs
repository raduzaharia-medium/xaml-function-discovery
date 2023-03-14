namespace IntrusionDetection
{
    public class Operator : IMathExpression
    {
        public string Symbol { get; set; } = string.Empty;
        public double Value { get; set; }
        public Evaluator Evaluator { get; set; } = new Evaluator();
        public Compositor Compositor { get; set; } = new Compositor();


        public override string ToString()
        {
            return "(" + Evaluator.ToString() + " " + Compositor.ToString() + ")";
        }
    }
}