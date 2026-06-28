using MBMScripts;
using System;
using System.Linq;

namespace MoreGraphicAttachments.Features;

public static class CharacterClassifier
{
    public static Type[] SpeciesTypes = new[]
    {
        typeof(Human),
        typeof(Elf),
        typeof(Dwarf),
        typeof(Neko),
        typeof(Inu),
        typeof(Usagi),
        typeof(Hitsuji),
        typeof(Dragonian)
    };

    public static bool IsSpeciesType(this Character character)
    {
        if (character == null) return false;
        var type = character.GetType();
        return SpeciesTypes.Contains(type);
    }
}