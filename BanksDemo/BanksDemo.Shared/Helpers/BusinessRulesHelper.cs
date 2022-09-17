namespace BanksDemo.Shared.Helpers;

public class BusinessRulesHelper
{
    public static string CheckRules(params Tuple<bool, string>[] rules)
    {
        foreach (var (item1, item2) in rules)
        {
            if (!item1)
                return item2;
        }
        return string.Empty;
    }
}