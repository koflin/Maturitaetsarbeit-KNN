using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharController : MonoBehaviour {

    CharacterController charController;
    Animator animator;

    public float forwardSpeed = 1f;
    public float turnSpeed = 0.1f;
    public Tilemap tilemap;
    public TileBase debugTile;

    bool crashed = false;

	// Use this for initialization
	void Start () {
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //tilemap.SetTile(new Vector3Int((int)Mathf.Floor(transform.position.x), (int)Mathf.Floor(transform.position.y), 0), debugTile);
        TileBase tileBase = tilemap.GetTile(new Vector3Int((int)Mathf.Floor(transform.position.x), (int)Mathf.Floor(transform.position.y), 0));

        if (tileBase == null || tileBase.name != "Grey Stones")
        {
            crashed = true;
        }

    }

    void FixedUpdate()
    {
        if (!crashed)
        {
            transform.Rotate(0, 0, -1 * Input.GetAxis("Horizontal") * turnSpeed);
            charController.Move(transform.TransformDirection(new Vector3(0, forwardSpeed, 0)));

            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }
    }
}
