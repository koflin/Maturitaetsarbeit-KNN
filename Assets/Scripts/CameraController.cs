using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Diese Klasse steuert die Kamera
public class CameraController : MonoBehaviour {

    public float scrollingSpeed = 0.5f;
    public float baseArrowSpeed = 0.5f;

    public Transform target;
    public Camera cam;

    //Wird beim Start aufgerufen
    void Start () {
        cam = GetComponent<Camera>();
    }

    //Wird in einem fixen Intervall aufgerufen
    void FixedUpdate()
    {
        float sizeChange = Input.mouseScrollDelta.y * scrollingSpeed;

        //Beim Scrollen zoom die Kamera herein oder heraus
        if (cam.orthographicSize - sizeChange > 0 && cam.orthographicSize - sizeChange < 32)
        {
            GetComponent<Camera>().orthographicSize -= sizeChange;
        }
    }

    //Wird am Schluss eines Updates aufgerufen
    void LateUpdate()
    {
        //Wenn eine Ziel ausgweählt ist wird dies verfolgt
        if (target != null)
        {
            this.transform.SetPositionAndRotation(new Vector3(target.position.x, target.position.y, transform.position.z), transform.rotation);
        }

        //Sonst gilt ie manuelle Steuerung mit Pfeilen
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.SetPositionAndRotation(transform.position + new Vector3(0, baseArrowSpeed * (cam.orthographicSize / 32) + 0.1f, 0), transform.rotation);
            }

            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.SetPositionAndRotation(transform.position + new Vector3(baseArrowSpeed * (cam.orthographicSize / 32) + 0.1f, 0, 0), transform.rotation);
            }

            else if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.SetPositionAndRotation(transform.position - new Vector3(0, baseArrowSpeed * (cam.orthographicSize / 32) + 0.1f, 0), transform.rotation);
            }

            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.SetPositionAndRotation(transform.position - new Vector3(baseArrowSpeed * (cam.orthographicSize / 32) + 0.1f, 0, 0), transform.rotation);
            }
        }
    }
}