using MBMScripts;
using System.Runtime.CompilerServices;

namespace MoreGraphicAttachments.ExtensionSpine;

public static class PartListCollectionExtraSystem
{
    private static readonly ConditionalWeakTable<Unit.PartListCollection, PartListCollectionExtra> ExtraTable
        = new ConditionalWeakTable<Unit.PartListCollection, PartListCollectionExtra>();

    public static PartListCollectionExtra Extra(this Unit.PartListCollection plc)
    {
        return ExtraTable.GetOrCreateValue(plc);
    }
}