/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollSnap : MonoBehaviour {


    //UI guns.
    public static GameObject[] guns;
    //Distance between guns.
    private float[] points;
    //How many screens or pages are there within the content (steps).
    private int screens;
    //How much length left.
    private float stepSize;
    //Scroll rect.
    private ScrollRect scroll;
    //Used to check if guns lerps.
    private bool LerpH;
    //Sets center target.
    private float targetH;
    //Snaps object in center.
    private bool snapInH = true;

    void Start ()
    {
        //Find how many guns are.
        screens = Data.gunsMenu.Length;
        //Allocates memory for gun array.
        guns = new GameObject[screens];

        //Expands scroll size according to gun amount. 
        transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(screens*100, 100);

        //Load all guns.
        for(int i = 0; i < screens; i++)
        {
            guns[i] = Instantiate(Data.gunsMenu[i], transform.GetChild(0));            
        }
        //Enable background for better scrolling.
        StartCoroutine(EnableBackground());

        //Get scroll rect.
        scroll = gameObject.GetComponent<ScrollRect>();
        scroll.inertia = false;

        //Sets scroll settings.
        if(screens > 0)
        {
            points = new float[screens];
            stepSize = 1/(float)(screens-1);
        
            for(int i = 0; i < screens; i++)
            {
                points[i] = i * stepSize;
            }
        }
        else
        {
            points[0] = 0;
        }
    }

IEnumerator EnableBackground()
{
    yield return new WaitForSeconds(1f);
    //Spawn background.
    transform.GetChild(0).GetComponent<HorizontalLayoutGroup>().enabled = false;
    transform.GetChild(1).SetParent(transform.GetChild(0));
}

    void Update()
    {
        //If object is not in the center, then move it.
        if(LerpH)
        {
            scroll.horizontalNormalizedPosition = Mathf.Lerp( scroll.horizontalNormalizedPosition, targetH, 100*scroll.elasticity*Time.deltaTime);
            if(Mathf.Approximately(scroll.horizontalNormalizedPosition, targetH)) LerpH = false;            
        }
    }
    //If player is not dragging any gun.
    public void DragEnd()
    {
        if(scroll.horizontal && snapInH)
        {
            targetH = points[FindNearest(scroll.horizontalNormalizedPosition, points)];
            LerpH = true;
        }
    }

    //If player is dragging gun.
    public void OnDrag()
    {
        LerpH = false;
    }

    //Find which gun is nearest to the center.
    int FindNearest(float f, float[] array)
    {
        float distance = Mathf.Infinity;
        int output = 0;
        for(int index = 0; index < array.Length; index++)
        {
            if(Mathf.Abs(array[index]-f) < distance)
            {
                distance = Mathf.Abs(array[index]-f);
                output = index;
            }
        }
        return output;    
    }
}
 
