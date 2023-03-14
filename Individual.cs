namespace IntrusionDetection
{
    public class Individual : IComparable<Individual>
    {
        private List<Operand> operands = new List<Operand>();
        private double? fitness;

        public IMathExpression[] Genes { get; set; }

        public double Fitness
        {
            get
            {
                if (fitness.HasValue == false)
                {
                    fitness = 0;
                    foreach (var element in Common.TrainingData)
                    {
                        Common.SetValues(element.Points);

                        var evaluare = Evaluate();
                        if (evaluare == 100)
                        {
                            fitness = 100;
                            break;
                        }
                        else fitness += Math.Abs(evaluare - element.Value);
                    }
                }
                return fitness.Value + Common.TrainingData[0].Points.Length - Operands.Count;
            }
        }
        internal List<Operand> Operands
        {
            get
            {
                if (operands == null)
                {
                    operands = new List<Operand>();

                    for (int i = 0; i < Genes.Length; i++)
                        if (Genes[i] is Operand && operands.Contains((Operand)Genes[i]) == false)
                            operands.Add((Operand)Genes[i]);
                }
                return operands;
            }
        }

        public Individual(IMathExpression[] setGenes)
        {
            Genes = setGenes;
            fitness = null;
        }
        public Individual(int lungime) : this(new IMathExpression[lungime])
        {
            for (int i = 0; i < lungime; i++) Genes[i] = Common.RandomMathExpression();
        }

        public Population Cross(Individual parent, int crossPointCount)
        {
            var result = new Population();
            var crossPoints = new List<int>();
            var genes1 = new IMathExpression[Genes.Length];
            var genes2 = new IMathExpression[Genes.Length];

            crossPoints.Add(0);
            crossPoints.Add(Genes.Length);

            for (int i = 0; i < crossPointCount; i++) crossPoints.Add(Common.Generator.Next(1, Genes.Length - 1));

            crossPoints.Sort();

            for (int i = 1; i < crossPointCount + 2; i++)
            {
                if (i % 2 == 0)
                {
                    Array.Copy(Genes, crossPoints[i - 1], genes1, crossPoints[i - 1], crossPoints[i] - crossPoints[i - 1]);
                    Array.Copy(parent.Genes, crossPoints[i - 1], genes2, crossPoints[i - 1], crossPoints[i] - crossPoints[i - 1]);
                }
                else
                {
                    Array.Copy(parent.Genes, crossPoints[i - 1], genes1, crossPoints[i - 1], crossPoints[i] - crossPoints[i - 1]);
                    Array.Copy(Genes, crossPoints[i - 1], genes2, crossPoints[i - 1], crossPoints[i] - crossPoints[i - 1]);
                }
            }

            result.Individuals.Add(new Individual(genes1));
            result.Individuals.Add(new Individual(genes2));
            return result;
        }
        public Individual Mutation(double probability)
        {
            var result = (IMathExpression[])Genes.Clone();

            for (var i = 0; i < result.Length; i++)
                if (Common.Generator.NextDouble() < probability)
                    result[i] = Common.RandomMathExpression();

            return new Individual(result);
        }

        public int CompareTo(Individual? other)
        {
            if (other == null) throw new ArgumentNullException("other");
            else return Fitness.CompareTo(other.Fitness);
        }

        private double Evaluate()
        {
            var result = new List<Operand>();

            for (int i = 0; i < Genes.Length; i++)
            {
                if (Genes[i] is Operand) result.Add((Operand)Genes[i]);
                else
                {
                    var element = (Operator)Genes[i];
                    var temp = result.ToArray();

                    result.Clear();

                    if (element.Evaluator.EvaluatorFunction != null && element.Compositor.CompositorFunction != null)
                    {
                        result.Add(element.Evaluator.EvaluatorFunction(element.Compositor.CompositorFunction(temp)));
                    }
                }
            }

            if (double.IsNaN(result[0].Value) == true || double.IsPositiveInfinity(result[0].Value) == true || double.IsNegativeInfinity(result[0].Value) == true) return 100;
            else if (result.Count > 1) return 100;
            else return result[0].Value;
        }
    }
}