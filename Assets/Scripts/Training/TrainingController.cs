using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrainingController : MonoBehaviour {

    public static bool manual;

    public static AI ai;
    public static string courseId;

    public GameObject coursePrefab;
    public GameObject course;

	// Use this for initialization
	void Start () {
        coursePrefab = Resources.Load<GameObject>("Courses/" + courseId);
        course = Instantiate(coursePrefab);

        if (!MenuManager.manual)
        {
            GeneticAlgorithm ga = GetComponent<GeneticAlgorithm>();

            ga.OnCourseLoaded(courseId, course);
            Debug.Log("ON START");
            ga.OnStart(ai);
        }
    }
}
