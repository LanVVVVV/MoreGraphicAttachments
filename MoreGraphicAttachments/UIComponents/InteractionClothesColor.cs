using MBMScripts;
using MoreGraphicAttachments.ExtensionField;
using System.Collections;
using UnityEngine;

namespace MoreGraphicAttachments.UIComponents
{
    public class InteractionClothesColor : MonoBehaviour
    {
        private FlexibleColorPicker m_FlexibleColorPicker = null!;
        private bool m_IsChangeable;

        private void Awake()
        {
            m_FlexibleColorPicker = GetComponent<FlexibleColorPicker>();
        }

        private void OnEnable()
        {
            StartCoroutine(EnableChangeAfterFrame());

            if (GetComponentInParent<TargetUnit>()?.Unit is Character character)
            {
                m_FlexibleColorPicker.color = character.Extra().ClothesColor;
            }
        }

        private void OnDisable()
        {
            m_IsChangeable = false;
        }

        private IEnumerator EnableChangeAfterFrame()
        {
            yield return null;
            m_IsChangeable = true;
        }

        public void ChangeClothesColor(Color color)
        {
            if (!m_IsChangeable) return;

            if (GetComponentInParent<TargetUnit>()?.Unit is Character character)
            {
                character.Extra().ClothesColor = color;
            }
        }
    }
}