using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private GameObject dustparticle;
    [SerializeField]
    private bool spawndust = false;
    public bool isgrounded;
    public Transform groundcheck;
    public float checkradius;
    public LayerMask whatisground;

    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;

    private void FixedUpdate()
    {
        isgrounded = Physics2D.OverlapCircle(groundcheck.position, checkradius, whatisground);
        if (isgrounded)
        {
            if (spawndust)
            {
                Instantiate(dustparticle, groundcheck.position, Quaternion.identity);
                audioSource.PlayOneShot(clip, volume);

                spawndust = false;
            }
        }
        else
            spawndust = true;
    }
}
