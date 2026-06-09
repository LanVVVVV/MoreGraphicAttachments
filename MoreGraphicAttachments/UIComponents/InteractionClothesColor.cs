using System.Collections;
using UnityEngine;
using MoreGraphicAttachments.ExtensionField;
using MBMScripts;

namespace MoreGraphicAttachments.UIComponents;

public class InteractionClothesColor : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(this.OnTrick());
    }

    private void OnDisable()
    {
        m_IsChangeable = false;
    }

    private IEnumerator OnTrick()
    {
        yield return null;
        m_IsChangeable = true;
        yield break;
    }

    public void ChangeClothesColor(Color color)
    {
        if (!m_IsChangeable) return;

        TargetUnit componentInParent = base.GetComponentInParent<TargetUnit>();
        if (componentInParent == null) return;
        Character? character = componentInParent.Unit as Character;
        if (character == null) return;

        character.Extra().ClothesColor = color;
    }

    private bool m_IsChangeable;
}