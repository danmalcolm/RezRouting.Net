using RezRouting.Configuration.Options;

namespace RezRouting.Configuration.Builders
{
    public class OptionsBuilder : IOptionsConfigurator
    {
        private IIdNameFormatter idNameFormatter = new DefaultIdNameFormatter();
        private UrlPathSettings urlPathSettings = new UrlPathSettings();
        
        public void UrlPaths(UrlPathSettings settings)
        {
            this.urlPathSettings = settings;
        }

        public void IdFormat(IIdNameFormatter formatter)
        {
            this.idNameFormatter = formatter;
        }

        public ConfigurationOptions Build()
        {
            return new ConfigurationOptions(urlPathSettings, idNameFormatter);
        }
    }
}