using System.Collections.Generic;
using TinyFramework;
public class LocalizationManager : Singleton<LocalizationManager>
{
    private Dictionary<string, string> m_langMap_cn = new Dictionary<string, string>
    {
        {"txt_name", "名字"},
        {"txt_level", "等级"},
        {"txt_exp", "经验"},
    };

    private Dictionary<string, string> m_langMap_en = new Dictionary<string, string>
    {
        {"txt_name", "Name"},
        {"txt_level", "Levle"},
        {"txt_exp", "Exp"},
    };

    public string GetValue(string langType, string key)
    {
        if (langType == "cn")
        {
            return m_langMap_cn[key];
        }
        else if (langType == "en")
        {
            return m_langMap_en[key];
        }
        return string.Empty;
    }
}
