using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateTime : MonoBehaviour
{
    public Text time;
    [SerializeField]
    private float endtime;

    private void Start()
    {
        endtime = GameObject.Find("goalcontainer").GetComponent<GoalTime>().t;
        string min = ((int)endtime / 60).ToString();
        string sec = (endtime % 60).ToString("f2");

        time.text = min + " minutes " + sec + " seconds";
    }

}
