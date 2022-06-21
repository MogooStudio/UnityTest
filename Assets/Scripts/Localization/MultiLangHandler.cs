using UnityEngine;
using UnityEngine.UI;

public class MultiLangHandler : MonoBehaviour
{
    public string langKey = "";

    public void OnLanguageChanged(string langType) 
    {
        Text text = this.GetComponent<Text>();
        if (text) 
        {
            var value = MultiLangTool.GetValue(langType, langKey);
            text.text = value;
        }
    }

    public void Awake()
    {
        MultiLangManager.onLanguageChanged += OnLanguageChanged;
    }

    public void OnDestroy()
    {
        MultiLangManager.onLanguageChanged -= OnLanguageChanged;
    }
}
