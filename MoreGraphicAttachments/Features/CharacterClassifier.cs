using MBMScripts;
using System;
using System.Linq;

namespace MoreGraphicAttachments.Features;

public static class CharacterClassifier
{
    public static Type[] SpeciesTypes =
    [
        typeof(Human),
        typeof(Elf),
        typeof(Dwarf),
        typeof(Neko),
        typeof(Inu),
        typeof(Usagi),
        typeof(Hitsuji),
        typeof(Dragonian)
    ];

    public static Type[] BusinessPartnerMainMansionTypes =
    [
        typeof(Amilia),
        typeof(Flora),
        typeof(Niel),
        typeof(SenaLena),
        typeof(Barbara)
    ];

    public static bool IsSpeciesType(this Character character)
    {
        if (character == null) return false;
        var type = character.GetType();
        return SpeciesTypes.Contains(type);
    }

    public static bool IsBusinessPartnerMainMansionTypes(this Character character)
    {
        if (character == null) return false;
        var type = character.GetType();
        return BusinessPartnerMainMansionTypes.Contains(type);
    }
}