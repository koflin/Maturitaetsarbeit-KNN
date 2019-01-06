using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Diese Klasse ist ein Speicherbehälter einer Zeitangabe
[Serializable]
public class Time {

    public long value;

    public static implicit operator DateTime(Time time)
    {
        return DateTime.FromFileTime(time.value);
    }

    public static implicit operator Time(DateTime dateTime)
    {
        Time time = new Time()
        {
            value = dateTime.ToFileTime()
        };

        return time;
    }
}
