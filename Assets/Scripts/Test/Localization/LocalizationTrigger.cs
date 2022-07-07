using UnityEngine;

public class LocalizationTrigger : MonoBehaviour
{
    public delegate void OnLanguageChanged(string langType);
    public static OnLanguageChanged onLanguageChanged = null;

    public void SetupLanguage(string langType)
    {
        onLanguageChanged?.Invoke(langType);
    }
}
