using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Diese Klasse steuert das Inviduum und ist die zweit wichtigste Klasse
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

    public bool finished = false;
    public bool crashed = false;

    //Zurückgeben ob der Charakter einen Unfall hatte
    public bool IsCrashed()
    {
        return crashed;
    }

    public bool HasFinished()
    {
        return finished;
    }

    //Wird beim Start aufgerufen
	void Start () {
        charController = GetComponent<CharacterController>();
        charController.detectCollisions = false;

        animator = GetComponent<Animator>();
	}
	
    //Wird bei jedem Bildupdate aufgerufen
	void Update ()
    {
        //Wenn der Charakter noch nicht gecrashed oder fertig ist
        if (!crashed && !finished)
        {
            //tilemap.SetTile(new Vector3Int((int)Mathf.Floor(transform.position.x), (int)Mathf.Floor(transform.position.y), 0), debugTile);
            TileBase tileBase = tilemap.GetTile(new Vector3Int((int)Mathf.Floor(transform.position.x), (int)Mathf.Floor(transform.position.y), 0));

            //Wenn der Charakter ausserhalb des Spielfelds ist, gilt er als gecrashed
            if (tileBase == null)
            {
                if (!manual)
                {
                    geneticAlgorithm.OnIndividualStopped(gameObject, false);
                }

                crashed = true;
            }

            //Wenn der Charakter die Zielfläche erreicht hat hat er es geschafft
            else if (tileBase.name == "Finish")
            {
                if (!manual)
                {
                    geneticAlgorithm.OnIndividualStopped(gameObject, true);
                }

                finished = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (!crashed && !finished)
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

    //Bewegung
    public void Move(float horizontalInput)
    {
        if (!crashed && !finished)
        {
            if (charController != null)
            {
                transform.Rotate(0, 0, -1 * horizontalInput * turnSpeed);
                charController.Move(transform.TransformDirection(new Vector3(0, forwardSpeed, 0)));
            }
        }
    }

    //Messen der Richtungsabstände
    public List<double> CalculateDistances()
    {
        RaycastHit2D right = CheckRaycastHit(transform.TransformDirection(new Vector2(1, 0)));

        RaycastHit2D rightFront = CheckRaycastHit(transform.TransformDirection(new Vector2(1, 1)));

        RaycastHit2D front = CheckRaycastHit(transform.TransformDirection(new Vector2(0, 1)));

        RaycastHit2D leftFront = CheckRaycastHit(transform.TransformDirection(new Vector2(-1, 1)));

        RaycastHit2D left = CheckRaycastHit(transform.TransformDirection(new Vector2(-1, 0)));

        return new List<double> { GetRaycastHitValue(left), GetRaycastHitValue(leftFront), GetRaycastHitValue(front), GetRaycastHitValue(rightFront), GetRaycastHitValue(right) };
    }

    //Kreieren des Raycasts
    RaycastHit2D CheckRaycastHit(Vector2 direction)
    {
        Vector2 startingPosition = transform.position;

        Debug.DrawRay(startingPosition, direction * raycastMaxDistance, Color.green);
        return Physics2D.Raycast(startingPosition, direction);
    }

    //Zurückgeben der Distanz des Raycasts von der Wand
    private float GetRaycastHitValue(RaycastHit2D raycastHit)
    {
        if (raycastHit.collider != null)
        {
            return raycastHit.distance;
        }

        return 0f;
    }
}
