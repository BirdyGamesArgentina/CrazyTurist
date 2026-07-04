using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class SaveDataUtilities
{
    public const string SettingsPath = "Settings";
    public const string SaveDataPath = "SaveData";

#if UNITY_EDITOR
    [MenuItem("Save Data Utility/Reset All Saves")]
    public static void ResetAllSavesEditor()
    {
        ResetAllSaves();
    }
#endif
    public static void ResetAllSaves()
    {
        PlayerPrefs.DeleteAll();
        var settingsDirectory = JSONSerialization.DirectoryDocuments(SettingsPath) + ".json";
        if (JSONSerialization.IsFileExist(SettingsPath)) File.Delete(settingsDirectory);
    }
    public static void ResetSettingsSave()
    {
        if(Settings.Instance != null)
        {
            Settings.Instance.ResetToDefaultValues();
        }
    }
}
