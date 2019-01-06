using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Diese Klasse steurert das Training
public class TrainingController : MonoBehaviour {

    public static bool manual;

    public static AI ai;
    public static string courseId;

    public GameObject coursePrefab;
    public GameObject course;

	//Wird beim Start aufgerufen
	void Start () {
        //Instanziiert den Parcours
        coursePrefab = Resources.Load<GameObject>("Courses/" + courseId);
        course = Instantiate(coursePrefab);

        //Lädt den GA und startet ihn
        if (!MenuManager.manual)
        {
            GeneticAlgorithm ga = GetComponent<GeneticAlgorithm>();

            ga.OnCourseLoaded(courseId, course);
            ga.OnStart(ai);
        }
    }
}
