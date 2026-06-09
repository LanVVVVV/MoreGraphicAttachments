using System;
using System.Collections.Generic;

namespace MoreGraphicAttachments.ExtensionSpineData;

[Serializable]
public class SpineDataFormJson
{
    public string skeletonDataAssetName = null!;

    public List<string> slaveClothesColorPartList = null!;
}

[Serializable]
public class SpineDataWrapper
{
    public List<SpineDataFormJson> list = null!;
}
