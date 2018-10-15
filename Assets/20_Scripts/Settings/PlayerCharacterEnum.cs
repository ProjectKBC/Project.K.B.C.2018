using System.ComponentModel;

public enum PlayerCharacterEnum
{
    [Description("random")]
    random,
    [Description("airos")]
    airos,
    [Description("anoma")]
    anoma,
    [Description("emilia")]
    emilia,
    [Description("held")]
    held,
    [Description("kaito")]
    kaito,
    [Description("kaoru")]
    kaoru,
    [Description("laxa")]
    laxa,
    [Description("twist")]
    twist,
    [Description("vega_al")]
    vega_al,
    [Description("veronica")]
    veronica,
    [Description("length")]
    length_empty,
}

static class PlayerCharacterEnumExtend
{
    // 拡張メソッド
    static string GetString(this PlayerCharacterEnum _enum)
    {
        var gm = _enum.GetType().GetMember(_enum.ToString());
        var attributes = gm[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        var description = ((DescriptionAttribute)attributes[0]).Description;
        return description;
    }
}