using MBMScripts;
using MoreGraphicAttachments.Features;
using MoreGraphicAttachments.Properties;
using MoreGraphicAttachments.UIComponents;
using System.Collections.Generic;
using SystemExtensionLib.Tools;
using UnityEngine;

namespace MoreGraphicAttachments.UI;

public static class GalleryClothesTypeSlotUI
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

        var galleyRoot = GameObject.Find("Galley");
        var slaveLayout = galleyRoot.transform.Find("Canvas/LetterBox/Frame/Slave Customize/Layout");
        var slaveAppearanceLeft = slaveLayout.Find("Appearance2/Slave");
        var clothesSlot = slaveAppearanceLeft.Find("Clothes");

        var clothTypeSlot = Object.Instantiate(clothesSlot.gameObject, slaveAppearanceLeft);
        clothTypeSlot.name = "Clothes Type";
        clothTypeSlot.transform.SetSiblingIndex(clothesSlot.GetSiblingIndex() + 1);

        #region Label
        var labelRfy = clothTypeSlot.GetComponentInChildren<ReferenceFormattingText>(true);
        labelRfy.Value = Strings.Slot_ClothesType;
        LabelRfyList.Add(labelRfy);
        #endregion

        #region TypeValue
        var referenceCustomize = clothTypeSlot.GetComponentInChildren<ReferenceCustomize>(true);
        var clothTypeText = referenceCustomize.gameObject;
        Object.DestroyImmediate(referenceCustomize);

        BinderTextMeshProText binderText = clothTypeText.GetComponent<BinderTextMeshProText>();
        var referenceClothesType = clothTypeText.AddComponent<ReferenceClothes>();
        referenceClothesType.DataType = ReferenceClothes.EDataType.Type;
        ComponentTools.SetReferenceArray(binderText, [referenceClothesType]);
        #endregion

        #region ChangeArrow
        var arrowLeft = clothTypeSlot.transform.Find("Left").gameObject;
        var arrowRight = clothTypeSlot.transform.Find("Right").gameObject;

        foreach (var arrow in new[] { arrowLeft, arrowRight })
        {
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
