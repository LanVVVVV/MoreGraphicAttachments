using UnityEngine;

namespace MoreGraphicAttachments.Features;

internal static class InformationLookWindowHeight
{
    //cellSize = (148, 54)
    private const float CellHeight = 54f;

    //spacing = (0, 1)
    private const float Spacing = 1f;

    //private const float RightOverGap = 36f;

    //FemaleInformationLook: (524 838)
    internal static Vector2 AdjustHeight(Vector2 sizeDelta, int leftCount, int rightCount)
    {
        //leftCount = leftCount - 1;

        int count = Mathf.Max(leftCount, rightCount);

        if(count == 0) return sizeDelta;

        //if (leftCount < rightCount)
        //{
        //    sizeDelta.y -= RightOverGap;
        //}

        sizeDelta.y += (CellHeight + Spacing) * count;

        return sizeDelta;
    }
}
