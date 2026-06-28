using MBMScripts;
using MoreGraphicAttachments.Features;
using MoreGraphicAttachments.Properties;
using MoreGraphicAttachments.UIComponents;
using System.Collections.Generic;
using UnityEngine;

namespace MoreGraphicAttachments.UI;

public class GalleryClothesTypeSlotUI
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

        var slaveAppearanceLeft = GameObject.Find("Galley/Canvas/LetterBox/Frame/Slave Customize/Layout/Appearance2/Slave").transform;
        var clothesSlot = slaveAppearanceLeft.transform.Find("Clothes");

        var clothTypeSlot = Object.Instantiate(clothesSlot.gameObject, slaveAppearanceLeft);
        clothTypeSlot.name = "Clothes Type";
        clothTypeSlot.transform.SetSiblingIndex(clothesSlot.GetSiblingIndex() + 1);

        #region Label
        var labelRfy = clothTypeSlot.GetComponentInChildren<ReferenceFormattingText>(true);
        labelRfy.Value = Strings.Slot_ClothesType;
        LabelRfyList.Add(labelRfy);
        #endregion

        #region Value
        var referenceCustomize = clothTypeSlot.GetComponentInChildren<ReferenceCustomize>(true);
        var clothTypeText = referenceCustomize.gameObject;

        BinderTextMeshProText binderTextText = clothTypeText.GetComponent<BinderTextMeshProText>();
        Object.DestroyImmediate(referenceCustomize);

        var referenceClothesType = clothTypeText.AddComponent<ReferenceClothes>();
        referenceClothesType.DataType = ReferenceClothes.EDataType.Type;

        ComponentTools.SetReferenceArray(binderTextText, [referenceClothesType]);
        #endregion

        #region ChangeArrow
        var arrowLeft = clothTypeSlot.transform.Find("Left").gameObject;
        var arrowRight = clothTypeSlot.transform.Find("Right").gameObject;

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
            if (ch.IsSpeciesType())
                ch.ChangeClothesTypeLeftInAll();
            else ch.ChangeClothesTypeLeft();
    }

    private static void OnRightArrowClick(GameObject arrow)
    {
        if (arrow.GetComponentInParent<TargetUnit>().Unit is Character ch)
            if (ch.IsSpeciesType())
                ch.ChangeClothesTypeRightInAll();
            else ch.ChangeClothesTypeRight();
    }
}
