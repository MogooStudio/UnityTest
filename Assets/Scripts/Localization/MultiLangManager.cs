using UnityEngine;

public class MultiLangManager : MonoBehaviour
{

    public delegate void OnLanguageChanged(string langType);


    public static OnLanguageChanged onLanguageChanged = null;

    public void SetupLanguage(string langType) 
    {
        if (onLanguageChanged != null) 
        {
            onLanguageChanged(langType);
        }
    }
}
