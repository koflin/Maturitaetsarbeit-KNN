using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharController : MonoBehaviour {

    public GeneticAlgorithm geneticAlgorithm;

    CharacterController charController;
    Animator animator;

    [Header("Movement")]
    public bool manual = true;
    public float forwardSpeed = 1f;
    public float turnSpeed = 0.1f;

    public Tilemap tilemap;
    public TileBase debugTile;

    [Header("Raycast")]
    public float raycastMaxDistance = 100f;

    bool crashed = false;

    public bool IsCrashed()
    {
        return crashed;
    }

	// Use this for initialization
	void Start () {
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!crashed)
        {
            //tilemap.SetTile(new Vector3Int((int)Mathf.Floor(transform.position.x), (int)Mathf.Floor(transform.position.y), 0), debugTile);
            TileBase tileBase = tilemap.GetTile(new Vector3Int((int)Mathf.Floor(transform.position.x), (int)Mathf.Floor(transform.position.y), 0));

            if (tileBase == null || tileBase.name != "Grey Stones")
            {
                if (!manual)
                {
                    geneticAlgorithm.OnIndividualCrash(gameObject);
                }

                crashed = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (!crashed)
        {
            //Prüft ob die Steuerung manuell ist
            if (manual)
            {
                Move(Input.GetAxis("Horizontal"));
            }

            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }
    }

    public void Move(float horizontalInput)
    {
        if (charController != null)
        {
            transform.Rotate(0, 0, -1 * horizontalInput * turnSpeed);
            charController.Move(transform.TransformDirection(new Vector3(0, forwardSpeed, 0)));
        }
    }

    public List<double> CalculateDistances()
    {
        //It is important to call the raycast in FixedUpdate because of the fixed Interval!
        //Raycast Right
        RaycastHit2D right = CheckRaycastHit(transform.TransformDirection(new Vector2(1, 0)));

        //Raycast Right Front
        RaycastHit2D rightFront = CheckRaycastHit(transform.TransformDirection(new Vector2(1, 1)));

        //Raycast Front
        RaycastHit2D front = CheckRaycastHit(transform.TransformDirection(new Vector2(0, 1)));

        //Raycast Left Front
        RaycastHit2D leftFront = CheckRaycastHit(transform.TransformDirection(new Vector2(-1, 1)));

        //Raycast Left
        RaycastHit2D left = CheckRaycastHit(transform.TransformDirection(new Vector2(-1, 0)));

        return new List<double> { GetRaycastHitValue(left), GetRaycastHitValue(leftFront), GetRaycastHitValue(front), GetRaycastHitValue(rightFront), GetRaycastHitValue(right) };
    }

    RaycastHit2D CheckRaycastHit(Vector2 direction)
    {
        Vector2 startingPosition = transform.position;

        Debug.DrawRay(startingPosition, direction * raycastMaxDistance, Color.green);
        return Physics2D.Raycast(startingPosition, direction);
    }

    private float GetRaycastHitValue(RaycastHit2D raycastHit)
    {
        if (raycastHit.collider != null)
        {
            return raycastHit.distance;
        }

        return 0f;
    }
}
