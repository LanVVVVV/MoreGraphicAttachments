using HarmonyLib;
using MBMScripts;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MoreGraphicAttachments.ExtensionField;

public static class CharacterExtraSystem
{
    private static readonly ConditionalWeakTable<Character, CharacterExtra> ExtraTable
        = new ConditionalWeakTable<Character, CharacterExtra>();

    public static CharacterExtra Extra(this Character character)
    {
        return ExtraTable.GetValue(character, c => new CharacterExtra(c));
    }
    public static CharacterExtra ApplySaveData(Character character, CharacterExtraSaveData saveExtra)
    {
        return ExtraTable.GetValue(character, _ => new CharacterExtra(character, saveExtra));
    }

    public static IEnumerable<(Character character, CharacterExtra extra)> EnumerateValid()
    {
        var unitList = UnitSeqList;
        if (unitList == null) yield break;

        foreach (var unit in unitList)
        {
            if (unit is Character character)
            {
                if (ExtraTable.TryGetValue(character, out var extra))
                    yield return (character, extra);
            }
        }
    }

    private static readonly AccessTools.FieldRef<PlayData, SeqList<Unit>> unitSeqListRef =
        AccessTools.FieldRefAccess<PlayData, SeqList<Unit>>("m_UnitSeqList");

    public static SeqList<Unit>? UnitSeqList
    {
        get
        {
            var instance = PlayData.Instance;
            return instance != null ? unitSeqListRef(instance) : null;
        }
    }
}