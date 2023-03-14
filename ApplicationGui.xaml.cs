using System.Windows;
using IntrusionDetection;

namespace XAMLTest
{
    public partial class ApplicationGui : Window
    {
        private Population population = new Population();

        public ApplicationGui()
        {
            InitializeComponent();
            ConfigurationChanged(this, EventArgs.Empty);
        }

        private void ConfigurationChanged(object sender, EventArgs args)
        {
            if (sldPopulationSize != null && sldIndividualLength != null)
            {
                population = new Population((int)sldPopulationSize.Value, (int)sldIndividualLength.Value);
                UpdateVisualization();
            }
        }
        private void BtnEvolveClick(object sender, EventArgs args)
        {
            population.NextGeneration((int)sldSelectionCount.Value, sldMutationRate.Value, (int)sldCrosspoints.Value);
            UpdateVisualization();
        }

        private void UpdateVisualization()
        {
            var text = "Generation " + population.GenerationCount + "\n";

            text += "There are " + population.Individuals.Count + " individuals in the population\n\n";

            foreach (var element in population.BestIndividual.Genes) text += element + " ";
            text += ": " + population.BestIndividual.Fitness;

            if (txtBestIndividual != null) txtBestIndividual.Text = text;
        }
    }
}