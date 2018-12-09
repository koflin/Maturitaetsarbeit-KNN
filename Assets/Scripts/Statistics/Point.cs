using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Point : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("SELECT");
        eventData.selectedObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("DESELECT");
        eventData.selectedObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
