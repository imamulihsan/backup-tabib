using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSetting : MonoBehaviour
{
    private bool active = false;

    public void ChangeLanguage(int LanguageID)
    {
        if (active == true)
            return;
        StartCoroutine(SetLanguage(LanguageID));
        AudioManager.Instance.PlaySFX("Button");
    }

    IEnumerator SetLanguage(int _LanguageID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_LanguageID];
        active = false;
    }
}
