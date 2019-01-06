using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Diese Klasse steuert einen Punkt im Koordinatensystem
public class Point : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    //Wird aufgerufen wenn der Punkt angeklickt wurde
    public void OnSelect(BaseEventData eventData)
    {
        eventData.selectedObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    //Wird aufgerufen wenn ein anderer Punkt angeklickt wurde
    public void OnDeselect(BaseEventData eventData)
    {
        eventData.selectedObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
