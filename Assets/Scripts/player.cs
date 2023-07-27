using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : Element
{
    public float speed;
    private Rigidbody2D rb;
    private Rigidbody2D ghostrb;
    private Rigidbody2D enemyrb;
    private Vector2 velocity;
    private int direction = -1;
    private Animator animator;
    private Animator ghostanimator;
    private Animator enemyanimator;
    public float jumpforce;
    [SerializeField]
    private bool jump = false;
    public GameObject dustparticle;
    public GameObject highlight;
 

    [SerializeField]
    private bool isdead = false;
    [SerializeField]
    private GameObject ghost;
    [SerializeField]
    private GameObject character;
    private int potentialstate = 1;
    public int currentstate = 0;
    public GameObject possessenemy = null;

    private GameObject[] enemylist;
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;

    protected override void Start()
    {
        base.Start();
        rb = character.GetComponent<Rigidbody2D>();
        ghostrb = ghost.GetComponent<Rigidbody2D>();
        animator = character.GetComponent<Animator>();
        ghostanimator = ghost.GetComponent<Animator>();
        enemylist = GameObject.FindGameObjectsWithTag("Enemy");
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        ghostrb.constraints = RigidbodyConstraints2D.FreezeRotation;
        for (int i = 0; i < enemylist.Length; i++)
        {
            enemylist[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    void Update()
    {
        if (isdead)
            checkstate();

        if (potentialstate == 0 || potentialstate == 2)
            button.Instance.possessupdate(true);
        else
            button.Instance.possessupdate(false);
        int moveInput = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1;
        }

        if (currentstate == 0)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            ghostrb.velocity = new Vector2(moveInput * speed, ghostrb.velocity.y);

        }
        else if (currentstate == 1)
        {
            ghostrb.velocity = new Vector2(moveInput * speed, ghostrb.velocity.y);
        }
        else
        {
            enemyrb.velocity = new Vector2(moveInput * speed, enemyrb.velocity.y);
            ghostrb.velocity = new Vector2(moveInput * speed, ghostrb.velocity.y);
        }
        if (moveInput != 0)
        {
            animator.SetBool("isRunning", true);
            if (moveInput != direction)
            {
                direction = moveInput;
                if (currentstate == 0) {
                    character.transform.Rotate(0, 180, 0, Space.Self);
                    ghost.transform.Rotate(0, 180, 0, Space.Self);
                }
                else if (currentstate == 1)
                {
                    ghost.transform.Rotate(0, 180, 0, Space.Self);
                }
                else
                {
                    ghost.transform.Rotate(0, 180, 0, Space.Self);
                    possessenemy.transform.Rotate(0, 180, 0, Space.Self);
                }
            }
        }
        else
            animator.SetBool("isRunning", false);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            if (currentstate == 0 && character.GetComponent<Character>().isgrounded == true)
            {
                jump = false;
                jumpforce = 3.5f;
                rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                ghostrb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                audioSource.PlayOneShot(clip, volume);

            }
            else if(currentstate == 1 && ghost.GetComponent<Character>().isgrounded == true)
            {
                jump = false;
                jumpforce = 8;
                ghostrb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                audioSource.PlayOneShot(clip, volume);

            }
            else if(currentstate == 2 && possessenemy.GetComponent<Character>().isgrounded == true)
            {
                jump = false;
                jumpforce = 8;
                ghostrb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                enemyrb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                audioSource.PlayOneShot(clip, volume);

            }
        }
    }

    private float distance(Vector3 point1, Vector3 point2)
    {
        return (Mathf.Sqrt(Mathf.Abs(point1.x - point2.x) * Mathf.Abs(point1.x - point2.x) + Mathf.Abs(point1.y - point2.y) * Mathf.Abs(point1.y - point2.y)));
    }
    private void checkstate()
    {
        float temp = Mathf.Infinity;
        int index = 0;
        for (int i = 0; i < enemylist.Length; i++)
        {
            if (distance(ghost.transform.position, enemylist[i].transform.position) < temp)
            {
                temp = distance(ghost.transform.position, enemylist[i].transform.position);
                index = i;
            }
        }
        possessenemy = enemylist[index];
        if(temp < distance(ghost.transform.position, character.transform.position))
        {
            if (temp < 2.5f)
            {
                potentialstate = 2;
                for(int i = 0; i < enemylist.Length; i++)
                {
                    enemylist[i].transform.GetChild(2).gameObject.SetActive(false);
                }
                character.transform.GetChild(4).gameObject.SetActive(false);
                possessenemy.transform.GetChild(2).gameObject.SetActive(true);
                return;
            }
        }
        else
        {
            if(distance(ghost.transform.position, character.transform.position) < 2.5f)
            {
                potentialstate = 0;
                for (int i = 0; i < enemylist.Length; i++)
                {
                    enemylist[i].transform.GetChild(2).gameObject.SetActive(false);
                }
                character.transform.GetChild(4).gameObject.SetActive(true);
                return;
            }
            potentialstate = 1;
        }
    }
    public override void OnButtonPress()
    {
        if (!isdead)
        {
            isdead = true;
            if (currentstate == 0)
                ghost.transform.position = character.transform.position;
            else
                ghost.transform.position = possessenemy.transform.position;
            animator.SetBool("isDead", true);
            if (enemyanimator != null)
                enemyanimator.SetBool("ispossessed", false);
            if (possessenemy != null)
                possessenemy.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
            if (enemyrb != null)
                enemyrb.constraints = RigidbodyConstraints2D.FreezeRotation;

            Instantiate(dustparticle, ghost.transform.position, Quaternion.identity);
            ghost.SetActive(true);
            ghost.transform.GetChild(0).gameObject.SetActive(true);
            Physics2D.IgnoreCollision(character.transform.GetComponent<Collider2D>(), ghost.transform.GetComponent<Collider2D>());
            for (int i = 0; i < enemylist.Length; i++)
            {
                Physics2D.IgnoreCollision(enemylist[i].transform.GetComponent<Collider2D>(), ghost.transform.GetComponent<Collider2D>());
                enemylist[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            currentstate = 1;
        }
        else
        {
            if (potentialstate == 0)
            {
                animator.SetBool("isDead", false);
                character.transform.rotation = ghost.transform.rotation;
                currentstate = 0;
                Instantiate(dustparticle, character.transform.position, Quaternion.identity);
                for (int i = 0; i< enemylist.Length; i++)
                    enemylist[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else if (potentialstate == 2)
            {
                ghost.SetActive(false);
                possessenemy.transform.rotation = ghost.transform.rotation;
                enemyrb = possessenemy.GetComponent<Rigidbody2D>();
                enemyanimator = possessenemy.GetComponent<Animator>();
                enemyanimator.SetBool("ispossessed", true);
                possessenemy.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                Instantiate(dustparticle, possessenemy.transform.position, Quaternion.identity);
                currentstate = 2;
                for (int i = 0; i < enemylist.Length; i++)
                {
                    enemylist[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                    enemylist[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                }

            }
            isdead = false;
            Instantiate(dustparticle, ghost.transform.position, Quaternion.identity);
            ghost.SetActive(false);
            Physics2D.IgnoreCollision(character.transform.GetComponent<Collider2D>(), ghost.transform.GetComponent<Collider2D>(), false);
            for (int i = 0; i < enemylist.Length; i++)
                Physics2D.IgnoreCollision(enemylist[i].transform.GetComponent<Collider2D>(), ghost.transform.GetComponent<Collider2D>(), false);
            for (int i = 0; i < enemylist.Length; i++)
            {
                enemylist[i].transform.GetChild(2).gameObject.SetActive(false);
            }
            character.transform.GetChild(4).gameObject.SetActive(false);
            possessenemy.transform.GetChild(2).gameObject.SetActive(false);

        }
    }
}
