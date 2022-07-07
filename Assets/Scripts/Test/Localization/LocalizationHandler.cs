using UnityEngine;
using UnityEngine.UI;

public class LocalizationHandler : MonoBehaviour
{
    public string langKey = string.Empty;

    public void OnLanguageChanged(string langType)
    {
        Text text = this.GetComponent<Text>();
        if (text)
        {
            var value = LocalizationManager.Instance().GetValue(langType, langKey);
            text.text = value;
        }
    }

    public void Awake()
    {
        LocalizationTrigger.onLanguageChanged += OnLanguageChanged;
    }

    public void OnDestroy()
    {
        LocalizationTrigger.onLanguageChanged -= OnLanguageChanged;
    }
}
