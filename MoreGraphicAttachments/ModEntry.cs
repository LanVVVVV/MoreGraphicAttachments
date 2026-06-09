using MBM.ModLoader.Core;
using MBM.ModLoader.Settings;
using MoreGraphicAttachments.ExtensionData;
using MoreGraphicAttachments.ExtensionField;
using MoreGraphicAttachments.ExtensionSpineData;
using MoreGraphicAttachments.Patches;
using MoreGraphicAttachments.Properties;
using MoreGraphicAttachments.Sprites;
using MoreGraphicAttachments.UI;
using UnityEngine;

namespace MoreGraphicAttachments;

public static class ModEntry
{
    internal const string ModName = "MoreGraphicAttachments";

    public static void Load()
    {
        UISpriteLoad.LoadSprite();
        GameManagerPatch.AfterDataInitialized += LoadData.Initialize;
        SpineDataPatch.AfterSpineDataInitialized += LoadSpineData.Initialize;

        ModConfig.ModSettingRegister();

        ModSaveData.OnBeforeSave += (slot) => ExtensionFieldSaveData.Save(slot);
        ModSaveData.OnAfterLoad += (slot) => ExtensionFieldSaveData.Load(slot);
        PlayDataPatch.AfterSaveDataInitialized += ExtensionFieldSaveData.ApplyAll;

        SeqObjectPoolManagerPatch.AfterGameInitialized += GalleryClothesColorSlotUI.InjectSlot;

        SeqObjectPoolManagerPatch.AfterGameInitialized += UIExtraction.AllForClothesColorSlotUI;
        SeqObjectPoolManagerPatch.AfterGameInitialized += ClothesColorSlotUI.InjectSlot;

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
        GalleryClothesColorSlotUI.OnLanguageChanged();
        ClothesColorSlotUI.OnLanguageChanged();

        Log($"language changed: {langCode}");
    }
}