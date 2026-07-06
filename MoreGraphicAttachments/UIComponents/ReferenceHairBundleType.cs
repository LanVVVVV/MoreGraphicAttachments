using MBMScripts;
using MoreGraphicAttachments.ExtensionField;

namespace MoreGraphicAttachments.UIComponents;

public class ReferenceHairBundleType : Reference
{
    public override void Initialize()
    {
        ReferenceType = EReferenceType.String;
    }

    public override string GetString()
    {
        TargetUnit targetUnit = base.Updater.TargetUnit;
        Character? character = (targetUnit?.Unit) as Character;
        if (character == null) return string.Empty;

        return string.Format(SeqLocalization.Localize("#TypeFormat"), character.Extra().HairBundleType);
    }
}