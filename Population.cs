[assembly: CLSCompliant(true)]
namespace IntrusionDetection
{
    public class Population
    {
        private List<Individual> individuals;
        private int generationCount;

        public List<Individual> Individuals
        {
            get { return individuals; }
        }
        public Individual BestIndividual
        {
            get { return individuals[0]; }
        }
        public int GenerationCount
        {
            get { return generationCount; }
        }

        public Population()
        {
            individuals = new List<Individual>();
            generationCount = 0;
        }
        public Population(int size, int individualLength) : this()
        {
            for (int i = 0; i < size; i++) individuals.Add(new Individual(individualLength));
        }

        public void NextGeneration(int selectionCount, double mutationProbability, int crossPointCount)
        {
            var newIndividuals = new List<Individual>();

            for (int i = 0; i < 3; i++) newIndividuals.Add(individuals[i]);
            for (int i = 3; i < individuals.Count; i++)
            {
                var child = Selection(selectionCount).Cross(Selection(selectionCount), crossPointCount).BestIndividual;
                var mutant = child.Mutation(mutationProbability);

                if (mutant.Fitness < child.Fitness) child = mutant;
                newIndividuals.Add(child);
            }

            newIndividuals.Sort();
            generationCount += 1;
            individuals = newIndividuals;
        }

        private Individual Selection(int size)
        {
            var result = new Population();

            for (int i = 0; i < size; i++) result.individuals.Add(individuals[Common.Generator.Next(individuals.Count)]);
            return result.BestIndividual;
        }
    }
}