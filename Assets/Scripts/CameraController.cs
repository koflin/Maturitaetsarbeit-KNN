using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float scrollingSpeed = 0.5f;

    public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
        Camera camera = GetComponent<Camera>();
        float sizeChange = Input.mouseScrollDelta.y * scrollingSpeed;

        if (camera.orthographicSize - sizeChange > 0 && camera.orthographicSize - sizeChange < 32)
        {
            GetComponent<Camera>().orthographicSize -= sizeChange;
        }
    }

    void LateUpdate()
    {
        this.transform.SetPositionAndRotation(new Vector3(target.position.x, target.position.y, transform.position.z), transform.rotation);
    }
}
