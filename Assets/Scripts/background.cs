using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : Element
{
    [SerializeField]
    private float diff;
    public bool isDead = false;

 

    private float Abs(float num)
    {
        if (num < 0)
            return (num * -1);
        return (num);
    }
    private int closest()
    {
        float temp = 100;
        int temp2 = 4;
        if (isDead)
        {
            for (int i = 0; i < dead.Count; i++)
            {
                if (Abs(Camera.main.transform.position.x - dead[i].transform.position.x) < temp)
                {
                    temp = Camera.main.transform.position.x - dead[i].transform.position.x;
                    temp2 = i;
                }
            }
        }
        else
        {
            for (int i = 0; i < alive.Count; i++)
            {
                if (Abs(Camera.main.transform.position.x - alive[i].transform.position.x) < temp)
                {
                    temp = Camera.main.transform.position.x - alive[i].transform.position.x;
                    temp2 = i;
                }
            }
        }
        return (temp2);
    }

    void Update()
    {
        if (isDead)
        {
            if (closest() < dead.Count / 2)
            {
                for (int i = 0; i < dead.Count; i++)
                    dead[i].transform.position -= new Vector3(diff, 0, 0);
                for (int i = 0; i < alive.Count; i++)
                    alive[i].transform.position -= new Vector3(diff, 0, 0);
            }
            else if (closest() > dead.Count / 2)
            {
                for (int i = 0; i < dead.Count; i++)
                    dead[i].transform.position += new Vector3(diff, 0, 0);
                for (int i = 0; i < alive.Count; i++)
                    alive[i].transform.position += new Vector3(diff, 0, 0);
            }
        }
        else
        {
            if (closest() < alive.Count / 2)
            {
                for (int i = 0; i < dead.Count; i++)
                    dead[i].transform.position -= new Vector3(diff, 0, 0);
                for (int i = 0; i < alive.Count; i++)
                    alive[i].transform.position -= new Vector3(diff, 0, 0);
            }
            else if (closest() > dead.Count / 2)
            {
                for (int i = 0; i < dead.Count; i++)
                    dead[i].transform.position += new Vector3(diff, 0, 0);
                for (int i = 0; i < alive.Count; i++)
                    alive[i].transform.position += new Vector3(diff, 0, 0);
            }
        }
    }

    public override void OnButtonPress()
    {
        base.OnButtonPress();
        if (isDead)
            isDead = false;
        else
            isDead = true;
    }
}
