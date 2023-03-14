namespace IntrusionDetection
{
    public delegate Operand CompositorFunction(Operand[] operands);

    public class Compositor
    {
        public string Symbol { get; set; } = string.Empty;
        public CompositorFunction? CompositorFunction { get; set; }


        public override string ToString()
        {
            return Symbol;
        }
    }
}