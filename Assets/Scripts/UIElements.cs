using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIElements : MonoBehaviour
{
    public Image controlpanel;
    public Button startbutton;
    public Button controlbutton;
    public Button xbutton;
    private Image xkey;
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;

    private void Start()
    {
        Button Btn1 = startbutton.GetComponent<Button>();
        Button Btn2 = controlbutton.GetComponent<Button>();
        Button Btn3 = xbutton.GetComponent<Button>();
        Btn1.onClick.AddListener(startbuttonpress);
        Btn2.onClick.AddListener(controlbuttonpress);
        Btn3.onClick.AddListener(xbuttonpress);
        controlpanel.enabled = false;
        xkey = xbutton.GetComponent<Image>();
        xkey.enabled = false;
    }

    void controlbuttonpress()
    {
        controlpanel.enabled = true;
        xkey.enabled = true;
        audioSource.PlayOneShot(clip, volume);
    }
    void xbuttonpress()
    {
        controlpanel.enabled = false;
        xkey.enabled = false;
        audioSource.PlayOneShot(clip, volume);
    }

    void startbuttonpress()
    {
        SceneManager.LoadScene(1);
        audioSource.PlayOneShot(clip, volume);
    }
}
