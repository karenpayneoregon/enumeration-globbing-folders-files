using ColdFusionTool1.Models;

namespace ColdFusionTool1.Classes;

public sealed class AppData
{
    private static readonly Lazy<AppData> Lazy = new(() => new AppData());

    public static AppData Instance => Lazy.Value;
    public ApplicationConfiguration Configuration { get; set; }
    public string[] Terms { get; set; }

    private AppData()
    {
        Configuration = Configurations.LoadSettingsFromFile();
        Terms = ToCommaDelimitedStringToArray(Configuration.DelimitedItems);
    }


    private static string[] ToCommaDelimitedStringToArray(string input) =>
        string.IsNullOrWhiteSpace(input)
            ? []
            : input.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(item => item.Trim())
                .ToArray();
}