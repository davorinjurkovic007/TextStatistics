using Autofac;
using TextStatistics.Core;
using TextStatistics.Data;

namespace TextStatistics.Main.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SingleTextStatistics>()
                .AsSelf()
                .WithParameter(new TypedParameter(typeof(string), "nameOfFile"));

            builder.RegisterType<TextFileReader>()
                .As<ITextFileReader>();

            builder.RegisterType<StatisticsCalculation>()
                .As<ITextStatistics>();

            return builder.Build();
        }
    }
}
