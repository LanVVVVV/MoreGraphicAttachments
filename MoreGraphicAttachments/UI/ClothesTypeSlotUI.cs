using MBMScripts;
using MoreGraphicAttachments.Features;
using MoreGraphicAttachments.Properties;
using MoreGraphicAttachments.UIComponents;
using System.Collections.Generic;
using UnityEngine;

namespace MoreGraphicAttachments.UI;

public class ClothesTypeSlotUI
{
    private static List<ReferenceFormattingText> LabelRfyList { get; set; } = [];

    private static bool _isInjected = false;

    public static void OnLanguageChanged()
    {
        foreach (var labelRfy in LabelRfyList)
        {
            labelRfy.Value = Strings.Slot_ClothesType;
        }
    }

    public static void InjectSlot()
    {
        if (_isInjected) return;

        var root = GameObject.Find("Window Female Information (Window)");
        var canvas = root?.transform.Find("Canvas")?.gameObject;
        canvas!.SetActive(false);

        var contentRight = canvas.transform.Find("LetterBox/Frame/Window (1)/Content/Upper Right").transform;
        var referenceSlot = contentRight.Find("Voice");

        var clothTypeSlot = Object.Instantiate(referenceSlot.gameObject, contentRight);
        clothTypeSlot.name = "Clothes Type";
        clothTypeSlot.transform.SetSiblingIndex(contentRight.Find("Looks").GetSiblingIndex() + 1);

        #region Label
        var labelRfy = clothTypeSlot.transform.Find("Text").GetComponent<ReferenceFormattingText>();
        labelRfy.Value = Strings.Slot_ClothesType;
        LabelRfyList.Add(labelRfy);
        #endregion

        #region Value
        var clothBottomText = clothTypeSlot.transform.Find("Bottom/Text");

        BinderTextMeshProText binderTextText = clothBottomText.GetComponent<BinderTextMeshProText>();
        Object.DestroyImmediate(clothBottomText.GetComponent<ReferenceCharacterLook>());
        Object.DestroyImmediate(clothBottomText.GetComponent<ReferenceFormattingText>());

        var referenceClothesType = clothBottomText.gameObject.AddComponent<ReferenceClothes>();
        referenceClothesType.DataType = ReferenceClothes.EDataType.Type;

        ComponentTools.SetReferenceArray(binderTextText, [referenceClothesType]);
        #endregion

        #region ChangeArrow
        var clothBottomArrow = clothTypeSlot.transform.Find("Bottom/Arrow");
        Object.DestroyImmediate(clothBottomArrow.GetComponent<ReferenceCharacterLook>());
        Object.DestroyImmediate(clothBottomArrow.GetComponent<UpdaterGameObject>());

        var arrowLeft = clothBottomArrow.transform.Find("Left").gameObject;
        var arrowRight = clothBottomArrow.transform.Find("Right").gameObject;

        foreach (var arrow in new[] { arrowLeft, arrowRight })
        {
            GameObject.DestroyImmediate(arrow.GetComponent<InteractionClickEvent>());
            GameObject.DestroyImmediate(arrow.GetComponent<InteractionUnit>());
            ComponentTools.RemoveClickEvent(arrow);
        }
        ComponentTools.AddClickEvent(arrowLeft, () => OnLeftArrowClick(arrowLeft));
        ComponentTools.AddClickEvent(arrowRight, () => OnRightArrowClick(arrowRight));

        #endregion

        _isInjected = true;
    }

    private static void OnLeftArrowClick(GameObject arrow)
    {
        if (arrow.GetComponentInParent<TargetUnit>().Unit is Character ch)
            ch.ChangeClothesTypeLeft();
    }

    private static void OnRightArrowClick(GameObject arrow)
    {
        if (arrow.GetComponentInParent<TargetUnit>().Unit is Character ch)
            ch.ChangeClothesTypeRight();
    }
}
