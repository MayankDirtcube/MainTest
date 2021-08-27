using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaGUI : MonoBehaviour
{ 
    void OnEnable()
    {

        RectTransform rect = GetComponent<RectTransform>();
        Rect SafeArea = Screen.safeArea;

        Vector2 MinAnchor;
        Vector2 MaxAnchor;

        MinAnchor = SafeArea.position;
        MaxAnchor = MinAnchor + SafeArea.size;

        MinAnchor.x /= Screen.width;
        MinAnchor.y /= Screen.height;


        MaxAnchor.x /= Screen.width;
        MaxAnchor.y /= Screen.height;

        rect.anchorMin = MinAnchor;
        rect.anchorMax = MaxAnchor;  


        Debug.Log("safe area");
        
    }
}