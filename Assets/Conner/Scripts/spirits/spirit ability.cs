using UnityEngine;
using System.Collections;


public class spiritability : MonoBehaviour
{
    public Animator animator;
    public string spirit;
    public Color color;
    public SpriteRenderer spriteRenderer;
    public float spiritCoolDown;
    private bool CoolDownOver = true;

    
    //detem ability
    public GameObject pushback;
    public float DetemAnimationTime;
    
    //Conner ability
    public Vector2 minPosition;// Bottom-left
    public Vector2 maxPosition;// Bottom-right
    public GameObject Arrow;
    private int i;
    private float ConnerTime;
    
    //Mason ability
    public GameObject Reflect;
    public Transform PlayerPos;
    

    void Start()
    {
        color.a = 0.5f;
        spriteRenderer.color = color;
        
        if (spirit == "Detem")
        {
            animator.Play("DetemIdle");
        }
        if (spirit == "Conner")
        {
            animator.Play("ConnerIdle");
        }
        if (spirit == "Mason")
        {
            animator.Play("MasonIdle");
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && CoolDownOver)
        {
            StartCoroutine(CoolDown());
            SpiritSets();
            if (spirit == "Detem")
            {
                StartCoroutine(DetemAbility());
            }

            if (spirit == "Conner")
            {
                StartCoroutine(ConnerAbility());
            }
            if (spirit == "Mason")
            {
                StartCoroutine(MasonAbility());
            }
        }

        if (spirit == "Detem")
        {
            spiritCoolDown = 10f;
        }
        if (spirit == "Conner")
        {
            spiritCoolDown = 20;
        }
        if (spirit == "Mason")
        {
            spiritCoolDown = 3;
        }
    }

    IEnumerator CoolDown()
    {
        CoolDownOver = false;
        yield return new WaitForSecondsRealtime(spiritCoolDown);
        CoolDownOver = true;
    }

    void SpiritSets()
    {
        color.a = 1f;
        spriteRenderer.color = color;
    }

    void SpiritRESET()
    {
        color.a = 0.5f;
        spriteRenderer.color = color;
    }


    IEnumerator DetemAbility()
    {
        animator.Play("Detem");
        yield return new WaitForSeconds(DetemAnimationTime);
        GameObject PushBackPreFab = Instantiate(pushback, transform.position, transform.rotation);
        PushBackPreFab.transform.SetParent(transform, true); //makes parent
        yield return new WaitForSeconds(0.5f);
        Destroy(PushBackPreFab);
        animator.Play("DetemIdle");
        SpiritRESET();
    }


    IEnumerator ConnerAbility()
    {
        animator.Play("Conner");
        i = 0;
        ConnerTime = 1;
        while (i < 15)
        {
            //right arrow
            float randomY = Random.Range(minPosition.y, maxPosition.y);

            Vector2 spawnPos = new Vector2(-12, randomY);

            GameObject arrowInstance = Instantiate(Arrow, spawnPos, Quaternion.identity, transform);
            Rigidbody2D rb = arrowInstance.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(20f, 0f); // 5 units/sec to the right

            yield return new WaitForSeconds(ConnerTime);
            //left arrow
            
            randomY = Random.Range(minPosition.y, maxPosition.y);

            spawnPos = new Vector2(12, randomY);

            GameObject arrowInstance2 = Instantiate(Arrow, spawnPos, Quaternion.identity, transform);
            Rigidbody2D rb2 = arrowInstance2.GetComponent<Rigidbody2D>();
            rb2.linearVelocity = new Vector2(-20f, 0f); // 5 units/sec to the left (?)
            
            yield return new WaitForSeconds(ConnerTime);
            ConnerTime -= 0.1f;

            if (ConnerTime < 0)
            {
                ConnerTime = 0.05f;
            }
            i++;
        }
        SpiritRESET();
        animator.Play("ConnerIdle");
        
    }

    IEnumerator MasonAbility()
    {
        
        Vector2 Player = PlayerPos.position;
        GameObject ReflectScript = Instantiate(Reflect, Player, Quaternion.identity, transform);   
        MasonReflect masonReflect = ReflectScript.GetComponent<MasonReflect>();

        yield return new WaitForSecondsRealtime(0.1f);

        if (masonReflect.DidReflect)
        {
            animator.Play("Mason");
        }
        else
        {
            animator.Play("MasonMiss");
        }

        yield return new WaitForSeconds(0.5f);
        SpiritRESET();
        animator.Play("MasonIdle");

    }
}
