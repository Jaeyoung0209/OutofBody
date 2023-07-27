using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    public GameObject grass_1;
    public GameObject grass_2;
    public GameObject grass_3;
    public GameObject grass_4;

    private float min = -9.48f;
    private float max = 9.48f;

    private float ratio = 4.74f;


    private void Start()
    {
        for (float i = min; i < max * 2; i += 0.5f)
        {
            if (Random.Range(0, 9) < 6)
            {
                int temp = Random.Range(0, 3);
                if (temp == 0)
                    Instantiate(grass_1, new Vector3(i, Random.Range(-2.25f, -2.11f), 0), Quaternion.identity);
                else if (temp == 1)
                    Instantiate(grass_2, new Vector3(i, Random.Range(-2.25f, -2.11f), 0), Quaternion.identity);
                else if (temp == 2)
                    Instantiate(grass_3, new Vector3(i, Random.Range(-2.25f, -2.11f), 0), Quaternion.identity);
                else
                    Instantiate(grass_4, new Vector3(i, Random.Range(-2.25f, -2.11f), 0), Quaternion.identity);
            }
        }
    }
    void Update()
    {
        if(Camera.main.transform.position.x > max)
        {
            for(float i = max+ratio; i < Camera.main.transform.position.x*2; i += 0.5f)
            {
                if(Random.Range(0, 9) < 5)
                {
                    int temp = Random.Range(0, 3);
                    if (temp == 0)
                        Instantiate(grass_1, new Vector3(i, Random.Range(-2.25f, -2.11f), 0), Quaternion.identity);
                    else if (temp == 1)
                        Instantiate(grass_2, new Vector3(i, Random.Range(-2.25f, -2.11f), 0), Quaternion.identity);
                    else if (temp == 2)
                        Instantiate(grass_3, new Vector3(i, Random.Range(-2.25f, -2.11f), 0), Quaternion.identity);
                    else
                        Instantiate(grass_4, new Vector3(i, Random.Range(-2.25f, -2.11f), 0), Quaternion.identity);
                }
            }
            max += ratio;
        }
        else if(Camera.main.transform.position.x < min)
        {
            for (float i = min-ratio; i > Camera.main.transform.position.x * 2; i -= 0.5f)
            {
                if (Random.Range(0, 9) < 5)
                {
                    int temp = Random.Range(0, 3);
                    if (temp == 0)
                        Instantiate(grass_1, new Vector3(i, Random.Range(-2.25f, -2.11f), 0), Quaternion.identity);
                    else if (temp == 1)
                        Instantiate(grass_2, new Vector3(i, Random.Range(-2.25f, -2.11f), 0), Quaternion.identity);
                    else if (temp == 2)
                        Instantiate(grass_3, new Vector3(i, Random.Range(-2.25f, -2.11f), 0), Quaternion.identity);
                    else
                        Instantiate(grass_4, new Vector3(i, Random.Range(-2.25f, -2.11f), 0), Quaternion.identity);
                }
            }
            min -= ratio;
        }
    }
}
