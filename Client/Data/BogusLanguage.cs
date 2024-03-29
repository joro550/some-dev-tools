﻿namespace DevTools.Client.Data;

public static class BogusLanguageExtensions
{
    public static string GetLocaleString(this BogusLanguage bogusLanguage)
    {
        return bogusLanguage switch
        {
            BogusLanguage.Afrikaans => "af_ZA",
            BogusLanguage.Arabic => "ar",
            BogusLanguage.Azerbaijani => "az",
            BogusLanguage.Czech => "cz",
            BogusLanguage.German => "de",
            BogusLanguage.AustrianGerman => "en_AU",
            BogusLanguage.SwissGerman => "de_CH",
            BogusLanguage.Greek => "el",
            BogusLanguage.English => "en_GB",
            BogusLanguage.AustraliaEnglish => "en_AU",
            BogusLanguage.BorkEnglish => "en_BORK",
            BogusLanguage.CanadianEnglish => "en_CA",
            BogusLanguage.Irish => "en_IE",
            BogusLanguage.NigerianEnglish => "en_NG",
            BogusLanguage.UnitedStatesEnglish => "en_US",
            BogusLanguage.SouthAfricanEnglish => "en_ZA",
            BogusLanguage.Spanish => "es",
            BogusLanguage.MexicanSpanish => "es_MX",
            BogusLanguage.Farsi => "fa",
            BogusLanguage.Finnish => "fi",
            BogusLanguage.French => "fr",
            BogusLanguage.CanadianFrench => "fr_CA",
            BogusLanguage.SwissFrench => "fr_CH",
            BogusLanguage.Georgian => "ge",
            BogusLanguage.Hrvatski => "hr",
            BogusLanguage.Indonesia => "id_ID",
            BogusLanguage.Italian => "it",
            BogusLanguage.Japanese => "ja",
            BogusLanguage.Korean => "ko",
            BogusLanguage.Latvian => "lv",
            BogusLanguage.Norwegian => "nb_NO",
            BogusLanguage.Nepalese => "ne",
            BogusLanguage.Dutch => "nl",
            BogusLanguage.BelgiumDutch => "nl_BE",
            BogusLanguage.Polish => "pl",
            BogusLanguage.BrazilianPortuguese => "pt_BR",
            BogusLanguage.Portuguese => "pt_PT",
            BogusLanguage.Romanian => "ro",
            BogusLanguage.Russian => "ru",
            BogusLanguage.Slovakian => "sk",
            BogusLanguage.Swedish => "sv",
            BogusLanguage.Turkish => "tr",
            BogusLanguage.Ukrainian => "uk",
            BogusLanguage.Vietnamese => "vi",
            BogusLanguage.Chinese => "zh_CN",
            BogusLanguage.TaiwaneseChinese => "zh_TW",
            BogusLanguage.Zulu => "zu_ZA",
            _ => "en_GB",

        };
    }
}

public enum BogusLanguage
    {
        Afrikaans,
        Arabic,
        Azerbaijani,
        Czech,
        German,
        AustrianGerman,
        SwissGerman,
        Greek,
        English,
        AustraliaEnglish,
        BorkEnglish,
        CanadianEnglish,
        Irish,
        NigerianEnglish,
        UnitedStatesEnglish,
        SouthAfricanEnglish,
        Spanish,
        MexicanSpanish,
        Farsi,
        Finnish,
        French,
        CanadianFrench,
        SwissFrench,
        Georgian,
        Hrvatski,
        Indonesia,
        Italian,
        Japanese,
        Korean,
        Latvian,
        Norwegian,
        Nepalese,
        Dutch,
        BelgiumDutch,
        Polish,
        BrazilianPortuguese,
        Portuguese,
        Romanian,
        Russian,
        Slovakian,
        Swedish,
        Turkish,
        Ukrainian,
        Vietnamese,
        Chinese,
        TaiwaneseChinese,
        Zulu
    }