using MBMScripts;
using MoreGraphicAttachments.ExtensionField;
using UnityEngine;

namespace MoreGraphicAttachments.UIComponents;

public class ClothesColorPickerInitialization : MonoBehaviour
{
    private void Awake()
    {
        m_FlexibleColorPicker = GetComponent<FlexibleColorPicker>();
    }

    private void OnEnable()
    {
        TargetUnit componentInParent = GetComponentInParent<TargetUnit>();
        if (componentInParent == null)
        {
            return;
        }
        Character? character = componentInParent.Unit as Character;
        if (character == null)
        {
            return;
        }
        m_FlexibleColorPicker.color = character.Extra().ClothesColor;

        return;
    }

    private FlexibleColorPicker m_FlexibleColorPicker = null!;
}