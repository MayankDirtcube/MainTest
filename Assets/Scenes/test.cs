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
        popup.HideOnBackButton = true;
        popup.HideOnClickAnywhere = true;
        popup.HideOnClickOverlay = true;
        popup.HideOnClickContainer = true;
        popup.Show();
    }
}
