using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    public bool onAtDay = true;
    public GameObject item;

    private void FixedUpdate()
    {
        if (item != null)
        {
            if (onAtDay)
            {
                if (button.Instance.life)
                    item.SetActive(true);
                else
                    item.SetActive(false);
            }
            else
            {
                if (button.Instance.life)
                    item.SetActive(false);
                else
                    item.SetActive(true);
            }
        }

    }
}
