using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float scrollingSpeed = 0.5f;
    public float baseArrowSpeed = 0.5f;

    public Transform target;
    public Camera cam;

    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
        float sizeChange = Input.mouseScrollDelta.y * scrollingSpeed;

        if (cam.orthographicSize - sizeChange > 0 && cam.orthographicSize - sizeChange < 32)
        {
            GetComponent<Camera>().orthographicSize -= sizeChange;
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            this.transform.SetPositionAndRotation(new Vector3(target.position.x, target.position.y, transform.position.z), transform.rotation);
        }

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
