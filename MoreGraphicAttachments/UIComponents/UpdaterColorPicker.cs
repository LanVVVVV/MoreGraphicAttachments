using UnityEngine;
using MBMScripts;

namespace MoreGraphicAttachments.UIComponents;

public class UpdaterColorPicker : Updater
{
    private FlexibleColorPicker m_ColorPicker = null!;

    protected override void Awake()
    {
        base.Awake();
        m_ColorPicker = GetComponent<FlexibleColorPicker>();
    }

    protected override void Display()
    {
        foreach (Reference item in ReferenceArray)
        {
            switch (item.ReferenceType)
            {
                case EReferenceType.Color:
                    m_ColorPicker.color = item.GetColor();
                    break;
            }
        }
    }
}