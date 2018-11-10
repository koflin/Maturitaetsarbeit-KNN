using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distances {

    public float dilation = 100;
    public float left;
    public float leftMiddle;
    public float middle;
    public float rightMiddle;
    public float right;

    public Distances(float left, float leftMiddle, float middle, float rightMiddle, float right)
    {
        this.left = left;
        this.leftMiddle = leftMiddle;
        this.middle = middle;
        this.rightMiddle = rightMiddle;
        this.right = right;
    }

    //Die Fitness ist der normierte Durchschnitt der Abstände von der Wand (Normierung rein aus Übersichtlichkeitsgründen)
    public int CalcFitness()
    {
        return (int)Mathf.Round(CalcMedian() * dilation);
    }

    //Durchschnittsberechnung
    private float CalcMedian()
    {
        return (left + leftMiddle + middle + rightMiddle + right) / 5;
    }
}
