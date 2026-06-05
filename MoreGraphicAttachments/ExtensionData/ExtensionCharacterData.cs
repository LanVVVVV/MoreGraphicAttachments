using System;
using System.Collections.Generic;

namespace MoreGraphicAttachments.ExtensionData;

[Serializable]
public class ExtensionCharacterData
{
}

[Serializable]
public class Wrapper<T>
{
    public List<T> list = null!;
}
