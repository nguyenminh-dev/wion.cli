using Volo.Abp.Settings;

namespace Wion.Template.Settings;

public class TemplateSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(TemplateSettings.MySetting1));
    }
}
