using System.ComponentModel;

public enum StageEnum
{
    [Description("stage1")]
    stage1,
    [Description("stage2")]
    stage2,
    [Description("stage3")]
    stage3,
    [Description("stage4")]
    stage4,
    [Description("stage5")]
    stage5,
    [Description("length")]
    length_empty,
}

static class StageEnumExtend
{
    // 拡張メソッド
    static string GetString(this StageEnum _enum)
    {
        var gm = _enum.GetType().GetMember(_enum.ToString());
        var attributes = gm[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        var description = ((DescriptionAttribute)attributes[0]).Description;
        return description;
    }
}