using Autofac;
using System;
using TextStatistics.Main.Startup;

namespace TextStatistics.Main
{
    class Program
    {
        private const string PRIDE_AND_PREJUDICE = "PrideAndPrejudice.txt";
        private const string SHERLOCK_HOLMES = "SherlockHolmes.txt";
        private const string WAR_AND_PEACE = "WarAndPeace.txt";

        static void Main(string[] args)
        {
            ShowStatisticsForSingleText();
            ShowStatisticsForAllText();

            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Press any key to leave");

            Console.ReadKey();
        }

        private static void ShowStatisticsForSingleText()
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();

            using (var lifetime = container.BeginLifetimeScope())
            {
                var prideAndPrejudiceContainer = lifetime.Resolve<SingleTextStatistics>(new TypedParameter(typeof(string), PRIDE_AND_PREJUDICE));
                prideAndPrejudiceContainer.ShowStatistics();

                var sherlockHolmesContainer = lifetime.Resolve<SingleTextStatistics>(new TypedParameter(typeof(string), SHERLOCK_HOLMES));
                sherlockHolmesContainer.ShowStatistics();

                var warAndPeaceContainer = lifetime.Resolve<SingleTextStatistics>(new TypedParameter(typeof(string), WAR_AND_PEACE));
                warAndPeaceContainer.ShowStatistics();
            }
        }

        private static void ShowStatisticsForAllText()
        {
            TotalTextStatistics totalTextStatistics = new TotalTextStatistics();
            totalTextStatistics.CalculateAllStatistics(PRIDE_AND_PREJUDICE, SHERLOCK_HOLMES, WAR_AND_PEACE);
        }

    }
}
