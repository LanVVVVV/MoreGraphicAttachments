using HarmonyLib;
using MBMScripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreGraphicAttachments.Features;

public static class ComponentTools
{
    public static void RemoveClickEvent(GameObject button)
    {
        var interType = AccessTools.TypeByName("MBMScripts.Interaction");
        var inter = button.GetComponent(interType);
        if (inter == null)
            return;

        var prop = AccessTools.Property(interType, "OnClickCallback");
        var callback = prop.GetValue(inter) as Delegate;
        if (callback == null)
            return;

        prop.SetValue(inter, null);

        var interce = button.GetComponent<InteractionClickEvent>();
        if (interce != null)
        {
            UnityEngine.Object.DestroyImmediate(interce);
        }
    }

    public static void AddClickEvent(GameObject button, Action handler)
    {
        var interType = AccessTools.TypeByName("MBMScripts.Interaction");
        var inter = button.GetComponent(interType);
        if(inter == null)
            return;

        var prop = AccessTools.Property(interType, "OnClickCallback");
        if(prop == null)
            return;

        var callback = prop.GetValue(inter) as Delegate;
        var newCallback = Delegate.Combine(callback, handler);
        prop.SetValue(inter, newCallback);
    }

    public static void SetReferenceArray<T>(T target, List<Reference> refs, string fieldName = "m_ReferenceArray")
    {
        var fieldRef = AccessTools.FieldRefAccess<T, List<Reference>>(fieldName);
        fieldRef(target) = refs;
    }
}