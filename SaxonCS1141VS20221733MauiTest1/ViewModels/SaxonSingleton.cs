using Saxon.Api;

namespace SaxonCS1141VS20221733MauiTest1.ViewModels
{
    public sealed class SaxonSingleton
    {
        private static readonly Lazy<SaxonSingleton> lazy =
            new Lazy<SaxonSingleton>(() => new SaxonSingleton());

        public static SaxonSingleton Instance { get { return lazy.Value; } }

        private SaxonSingleton()
        {
            SaxonProcessor = new Processor(true);
        }

        public Processor SaxonProcessor { get; }
    }
}
