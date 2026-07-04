using MBMScripts;
using MoreGraphicAttachments.Core;
using System.Collections.Generic;
using System.Linq;

namespace MoreGraphicAttachments.Features;

public static class GlobalCharacterData
{
    public static Dictionary<string, List<int>> AllPartTypes = [];

    public static void Initialize()
    {
        //InitializeCheck();

        AllPartTypesInitialize();
        EarHair3Add();
        CleanZeroParts();

        //DebugPrintAllPartTypes();
    }

    private static void AllPartTypesInitialize()
    {
        List<CharacterData> allData = GetAllFemaleData();

        Add("HairFront", allData.SelectMany(d => d.HairFrontTypeList ?? Enumerable.Empty<int>()));
        Add("HairSide", allData.SelectMany(d => d.HairSideTypeList ?? Enumerable.Empty<int>()));
        Add("HairBack", allData.SelectMany(d => d.HairBackTypeList ?? Enumerable.Empty<int>()));
        Add("Eye", allData.SelectMany(d => d.EyeTypeList ?? Enumerable.Empty<int>()));
        Add("EyeBall", allData.SelectMany(d => d.EyeBallTypeList ?? Enumerable.Empty<int>()));
        Add("Tits", allData.SelectMany(d => d.TitsTypeList ?? Enumerable.Empty<int>()));
        Add("PubicHair", allData.SelectMany(d => d.PubicHairTypeList ?? Enumerable.Empty<int>()));
        Add("HairBody", allData.SelectMany(d => d.HairBodyTypeList ?? Enumerable.Empty<int>()));
        Add("Horn", allData.SelectMany(d => d.HornTypeList ?? Enumerable.Empty<int>()));
        Add("HornDragon", allData.SelectMany(d => d.HornDragonTypeList ?? Enumerable.Empty<int>()));
        Add("Ear", allData.SelectMany(d => d.EarTypeList ?? Enumerable.Empty<int>()));
        Add("EarHair", allData.SelectMany(d => d.EarHairTypeList ?? Enumerable.Empty<int>()));
        Add("Tail", allData.SelectMany(d => d.TailTypeList ?? Enumerable.Empty<int>()));
        Add("TailDragon", allData.SelectMany(d => d.TailDragonTypeList ?? Enumerable.Empty<int>()));

        //Add("UpperBody", allData.SelectMany(d => d.UpperBodyTypeList ?? Enumerable.Empty<int>()));
        //Add("LowerBody", allData.SelectMany(d => d.LowerBodyTypeList ?? Enumerable.Empty<int>()));
        //Add("Balls", allData.SelectMany(d => d.BallsTypeList ?? Enumerable.Empty<int>()));
        //Add("Penis", allData.SelectMany(d => d.PenisTypeList ?? Enumerable.Empty<int>()));
        //Add("Beard", allData.SelectMany(d => d.BeardTypeList ?? Enumerable.Empty<int>()));
    }

    private static List<CharacterData> GetAllFemaleData()
    {
        return
        [
            GameManager.HumanData,
            GameManager.ElfData,
            GameManager.DwarfData,
            GameManager.NekoData,
            GameManager.InuData,
            GameManager.UsagiData,
            GameManager.HitsujiData,
            GameManager.DragonianData
        ];
    }

    private static void Add(string key, IEnumerable<int> seq)
    {
        AllPartTypes[key] = seq
            .Where(x => x >= 0)
            .Distinct()
            .OrderBy(x => x)
            .ToList();
    }

    private static void EarHair3Add()
    {
        if (AllPartTypes["EarHair"].Contains(3)) return;
        AllPartTypes["EarHair"].Add(3);
        AllPartTypes["EarHair"].Sort();
    }

    private static void CleanZeroParts()
    {
        RemoveZero("Horn");
        RemoveZero("HornDragon");
        RemoveZero("Tail");
        RemoveZero("TailDragon");
    }

    private static void RemoveZero(string key)
    {
        if (!AllPartTypes.ContainsKey(key)) return;
        if (!AllPartTypes[key].Contains(0)) return;

        AllPartTypes[key].RemoveAll(x => x == 0);
    }

    private static void InitializeCheck()
    {
        ModEntry.Log("=== GlobalCharacterData.Initialize START ===");

        void Check(string name, CharacterData data)
        {
            if (data == null)
                ModEntry.Log($"[TNID] {name} = NULL");
            else
                ModEntry.Log($"[TNID] {name} = OK)");
        }

        Check("HumanData", GameManager.HumanData);
        Check("ElfData", GameManager.ElfData);
        Check("DwarfData", GameManager.DwarfData);
        Check("NekoData", GameManager.NekoData);
        Check("InuData", GameManager.InuData);
        Check("UsagiData", GameManager.UsagiData);
        Check("HitsujiData", GameManager.HitsujiData);
        Check("DragonianData", GameManager.DragonianData);

        ModEntry.Log("=== GlobalCharacterData.Initialize END ===");
    }

    private static void DebugPrintAllPartTypes()
    {
        foreach (var kv in AllPartTypes)
        {
            ModEntry.Log($"[CharacterData] {kv.Key} = [{string.Join(", ", kv.Value)}]");
        }
    }
}
