using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;

public class test : MonoBehaviour
{
    public string PopUpName;
   public void showPopup()
    {
        UIPopup popup = UIPopup.GetPopup(PopUpName);
        popup.Data.SetLabelsTexts("Mayank Error");
        popup.HideOnBackButton = true;
        popup.HideOnClickAnywhere = true;
        popup.HideOnClickOverlay = true;
        popup.HideOnClickContainer = true;
        popup.Show();
    }

    public void OnScreenPushNotification()
    {
        UIPopup popup = UIPopup.GetPopup(PopUpName);
    }
    public void IsLandscape(bool flag)
    {
        if (flag)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        
    }
}


