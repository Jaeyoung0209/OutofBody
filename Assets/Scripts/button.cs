using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : Singleton<button>
{
    private Animator animator;
    private List<Element> backgroundelements = new List<Element>();
    private bool buttonactive = true;
    public bool life = true;
    public GameObject border;
    private bool possesstarget = false;
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 2f;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && buttonactive)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.name == "Button")
                {
                    animator.SetTrigger("click");
                    if (life)
                    {
                        life = false;
                        audioSource.PlayOneShot(clip, volume);
                        OnButton();
                    }
                    else
                    {
                        if (possesstarget)
                        {
                            life = true;
                            audioSource.PlayOneShot(clip, volume);
                            OnButton();
                        }
                    }
                }
            }
        }
    }
    void OnButton()
    {
        for (int i = 0; i < backgroundelements.Count; i++)
        {
            backgroundelements[i].OnButtonPress();
        }
        ChangeCamera();
        StartCoroutine(nameof(ButtonEnum));
    }

    private void ChangeCamera()
    {
        if (life)
        {
            border.SetActive(false);
        }
        else
        {
            border.SetActive(true);
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
                transform.GetChild(i).gameObject.SetActive(false);
            else
                transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    private IEnumerator ButtonEnum()
    {
        buttonactive = false;
        yield return new WaitForSeconds(2);
        buttonactive = true;
    }

    public void append(Element element) => backgroundelements.Add(element);

    public void possessupdate(bool possess)
    {
        if (possess)
        {
            possesstarget = true;
        }
        else
            possesstarget = false;
    }
}