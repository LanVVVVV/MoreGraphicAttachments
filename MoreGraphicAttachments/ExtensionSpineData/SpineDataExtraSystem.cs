using MBMScripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MoreGraphicAttachments.ExtensionSpineData;

public static class SpineDataExtraSystem
{
    private static readonly ConditionalWeakTable<SpineData, SpineDataExtra> ExtraTable
        = new ConditionalWeakTable<SpineData, SpineDataExtra>();

    public static SpineDataExtra Extra(this SpineData spineData)
    {
        return ExtraTable.GetOrCreateValue(spineData);
    }

    public static void Init(string[] jsonNames)
    {
        List<SpineDataFormJson> allItems = new();

        foreach (var jsonName in jsonNames)
        {
            var json = JsonTool.LoadEmbeddedJson(jsonName);

            if (json == null)
            {
                ModEntry.LogWarning($"[{jsonName}] No JSON found. Skipping.");
                continue;
            }

            SpineDataWrapper? wrapper = null;

            try
            {
                wrapper = JsonConvert.DeserializeObject<SpineDataWrapper>(json.text);
            }
            catch (Exception ex)
            {
                ModEntry.LogError($"[{jsonName}] JSON parse error: {ex}");
                continue;
            }

            if (wrapper?.list == null || wrapper.list.Count == 0)
            {
                ModEntry.LogWarning($"[{jsonName}] JSON empty.");
                continue;
            }

            allItems.AddRange(wrapper.list);
            ModEntry.Log($"[{jsonName}] Loaded {wrapper.list.Count} entries.");
        }

        if (allItems.Count == 0)
        {
            ModEntry.LogWarning("[SpineDataExtra] No SpineData entries loaded.");
            return;
        }
        ModEntry.Log($"[SpineDataExtra] Loaded all {allItems.Count} entries.");

        int num = 0;
        foreach (var spineData in Database<SpineData>.GetList())
        {
            var match = allItems.FirstOrDefault(x =>
                x.skeletonDataAssetName == spineData.SkeletonDataAssetName);

            if (match == null)
            {
                spineData.Extra().slaveClothesColorPartList = new List<string>();
                continue;
            }

            spineData.Extra().slaveClothesColorPartList =
                new List<string>(match.slaveClothesColorPartList);

            num++;
        }

        ModEntry.Log($"[SpineDataExtra] Initialized {num} SpineData entries.");
    }
}