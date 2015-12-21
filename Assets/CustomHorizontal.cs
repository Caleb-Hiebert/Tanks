using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class CustomHorizontal : MonoBehaviour {

    public float padding, maxWidth, dist;

    public RectTransform parentForWidth;

    [Serializable]
    public struct Panel
    {
        public RectTransform trans;
        public float percent;
    }

    public Panel[] panels;

    void Update()
    {
        if(parentForWidth != null)
            maxWidth = parentForWidth.sizeDelta.x;

        dist = 0;

        if (panels == null)
            return;

        foreach (var item in panels)
        {
            if(item.trans != null && item.percent != 0)
            {
                dist += (dist > 0 ? padding : 0);

                item.trans.anchoredPosition = new Vector2(dist, 0);
                item.trans.sizeDelta = new Vector2(item.percent * maxWidth, item.trans.sizeDelta.y);

                dist += item.percent * maxWidth + padding;
            }
        }
    }
}
