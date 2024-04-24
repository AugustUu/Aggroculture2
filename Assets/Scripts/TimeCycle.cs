using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    [SerializeField]
    static public float minutes = 0.0f;
    [SerializeField]
    static public float hours = 10.0f;
    [SerializeField]
    static public int days = 1;
    [SerializeField]
    TextMeshProUGUI dayGUI;

    // Start is called before the first frame update
    void Start()
    {
        // Sets sun rotation right as the game starts
        transform.rotation = Quaternion.Euler((float)((hours * 60 + minutes) / 1440.0 * 360 - 90), 0, 0);
        // Runs addTime after 1 second every second
        InvokeRepeating("addTime", 1.0f, 1.0f);
        if (minutes < 10)
        {
            dayGUI.text = hours + " : 0" + minutes;
        }
        else
        {
            dayGUI.text = hours + " : " + minutes;
        }
        if (hours < 10)
        {
            dayGUI.text = "0" + dayGUI.text;
        }

    }

    // Update is called once per frame
    void Update(){
        //transform.rotation = Quaternion.Euler((float)Mathf.LerpAngle(transform.rotation.eulerAngles.x,(float)((hours* 60 + minutes) / 1440.0 * 360 - 90),Time.deltaTime), 0, 0);
        //transform.rotation = Quaternion.Euler((float)((hours * 60 + minutes) / 1440.0 * 360 - 90), 0, 0);
    }
    void addTime()
    {
        // Original Godot code
        // (float)Mathf.LerpAngle(rotation.X,(float)((hours* 60 + minutes) / 1440.0 * 2*Math.PI),delta);

        minutes += 5;
        recalculateTime();
        if (minutes < 10)
        {
            dayGUI.text = hours + " : 0" + minutes;
        }
        else
        {
            dayGUI.text = hours + " : " + minutes;
        }
        if (hours < 10)
        {
            dayGUI.text = "0" + dayGUI.text;
        }

        transform.rotation = Quaternion.Euler((float)((hours * 60 + minutes) / 1440.0 * 360 - 90), 0, 0);

    }
    void recalculateTime()
    {

        if (minutes >= 60)
        {
            minutes -= 60;
            hours += 1;
        }

        if (hours >= 24)
        {
            hours = 0;
            days += 1;
        }
    }
}
