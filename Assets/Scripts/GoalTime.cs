using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoalTime : MonoBehaviour
{
    public Text timertext;
    public float starttime = 0f;
    public GameObject finishparticle;
    public float t;
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 6;

    private void Start()
    {
        starttime = Time.time;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        t = Time.time - starttime;
        string min = ((int)t / 60).ToString();
        string sec = (t % 60).ToString("f2");

        if(timertext != null)
            timertext.text = min + ":" + sec;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Instantiate(finishparticle, transform.position, Quaternion.identity);
            audioSource.PlayOneShot(clip, volume);
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2);
    }
}
