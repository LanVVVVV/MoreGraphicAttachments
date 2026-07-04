using MBM.ModLoader.Core;
using MBM.ModLoader.Settings;
using MoreGraphicAttachments.Features;
using MoreGraphicAttachments.Patches.InitializePatches;
using MoreGraphicAttachments.Properties;
using MoreGraphicAttachments.Sprites;
using MoreGraphicAttachments.UI;
using SystemExtensionLib.Systems;
using UnityEngine;

namespace MoreGraphicAttachments.Core;

public static class ModEntry
{
    internal const string ModName = "MoreGraphicAttachments";

    public static void Load()
    {
        ConfigSystem.ExportAllEmbeddedConfig(ModName);

        UISpriteLoad.LoadSprite();

        ModConfig.ModSettingRegister();

        Loader.OnAllModsLoaded += ModCompatibility.ModCompatibilityCheck;

        SeqObjectPoolManagerPatch.AfterGameInitialized += UIRegister.Initialize;

        GameManagerPatch.AfterDataInitialized += GlobalCharacterData.Initialize;
        GameManagerPatch.AfterDataInitialized += LoadData.Initialize;
        SpineDataPatch.AfterSpineDataInitialized += LoadSpineData.Initialize;

        ModSaveData.OnBeforeSave += (slot) => ExtensionFieldSaveData.Save(slot);
        ModSaveData.OnAfterLoad += (slot) => ExtensionFieldSaveData.Load(slot);
        PlayDataPatch.AfterSaveDataInitialized += ExtensionFieldSaveData.ApplyAll;

        Localization.OnLanguageChanged += OnLanguageChanged;
        Log("MoreGraphicAttachments Mod loaded!");
    }

    internal static void Log(string msg) => Debug.Log($"[MGA] {msg}");

    internal static void LogWarning(string msg) => Debug.LogWarning($"[MGA] {msg}");

    internal static void LogError(string msg) => Debug.LogError($"[MGA] {msg}");

    private static void OnLanguageChanged(string langCode)
    {
        Strings.Culture = Localization.CurrentCulture;

        ModConfig.OnLanguageChanged();

        Log($"language changed: {langCode}");
    }
}