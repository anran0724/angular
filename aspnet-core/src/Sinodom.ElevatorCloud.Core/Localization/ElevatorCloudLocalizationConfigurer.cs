using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Sinodom.ElevatorCloud.Localization
{
    public static class ElevatorCloudLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    ElevatorCloudConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(ElevatorCloudLocalizationConfigurer).GetAssembly(),
                        "Sinodom.ElevatorCloud.Localization.ElevatorCloud"
                    )
                )
            );
        }
    }
}
