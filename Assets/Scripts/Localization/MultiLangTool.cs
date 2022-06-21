using System.Collections.Generic;
using UnityEngine;

public class MultiLangTool : MonoBehaviour
{
    static Dictionary<string, string> m_langMap_cn = new Dictionary<string, string>
    {
        {"txt_name", "����"},
        {"txt_level", "�ȼ�"},
        {"txt_exp", "����"},
    };

    static Dictionary<string, string> m_langMap_en = new Dictionary<string, string>
    {
        {"txt_name", "Name"},
        {"txt_level", "Levle"},
        {"txt_exp", "Exp"},
    };

    public static string GetValue(string langType, string key) 
    {
        if (langType == "cn")
        {
            return m_langMap_cn[key];
        } else if (langType == "en") 
        {
            return m_langMap_en[key];
        }
        return "N/A";
    }
}
