using MBM.ModLoader.Settings;
using MBMScripts;
using MoreGraphicAttachments.Core;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MoreGraphicAttachments.ExtensionField;

public static class ExtensionFieldSaveData
{
    public static void Save(int slot)
    {
        var list = new CharacterExtraSaveDataWrapper();
        foreach (var (character, extra) in CharacterExtraSystem.EnumerateValid())
        {
            if (extra.NotNeedSave())continue;
            list.Characters.Add(new CharacterExtraSaveData(character, extra));
        }
        string json = JsonConvert.SerializeObject(list, Formatting.None);
        ModSaveData.SetString(ModEntry.ModName, "CharacterExtraList", json);
    }

    private static readonly Dictionary<int, CharacterExtraSaveData> _characterSaveExtra = new();

    public static void Load(int slot)
    {
        _characterSaveExtra.Clear();
        string json = ModSaveData.GetString(ModEntry.ModName, "CharacterExtraList", "");
        if (string.IsNullOrEmpty(json)) return;

        var list = JsonConvert.DeserializeObject<CharacterExtraSaveDataWrapper>(json);
        foreach (var data in list!.Characters)
        {
            _characterSaveExtra[data.UnitId] = data;
        }
    }

    public static void ApplyAll()
    {
        foreach (var kv in _characterSaveExtra)
        {
            int unitId = kv.Key;
            CharacterExtraSaveData saveExtra = kv.Value;

            var c = PlayData.Instance.GetUnit(unitId);
            if (c is Character character)
            {
                CharacterExtraSystem.ApplySaveData(character, saveExtra);
            }
            else
            {
                ModEntry.LogWarning($"UnitId {unitId} not found or not a Character.");
            }
        }
        _characterSaveExtra.Clear();
    }
}