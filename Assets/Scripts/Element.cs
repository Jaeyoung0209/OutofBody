using System.Collections;
using System.Collections.Generic;
using UnityEngine;
abstract public class Element: MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> dead = new List<GameObject>();
    [SerializeField]
    protected List<GameObject> alive = new List<GameObject>();

    protected virtual void Start()
    {
        if (dead.Count > 0)
        {
            for (int i = 0; i < dead.Count; i++)
                dead[i].SetActive(false);
        }
        if (alive.Count > 0)
        {
            for (int i = 0; i < alive.Count; i++)
                alive[i].SetActive(true);
        }
        button.Instance.append(this);
    }
    
    public virtual void OnButtonPress()
    {
        if (dead[0].activeSelf)
        {

            for (int i = 0; i < dead.Count; i++)
                    dead[i].SetActive(false);
            
            if (alive.Count > 0)
            {
                for (int i = 0; i < alive.Count; i++)
                    alive[i].SetActive(true);
            }
        }
        else
        {
            if (dead.Count > 0)
            {
                for (int i = 0; i < dead.Count; i++)
                    dead[i].SetActive(true);
            }
            if (alive.Count > 0)
            {
                for (int i = 0; i < alive.Count; i++)
                    alive[i].SetActive(false);
            }
        }
    }
}
