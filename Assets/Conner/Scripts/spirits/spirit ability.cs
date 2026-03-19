using UnityEngine;
using System.Collections;


public class spiritability : MonoBehaviour
{
    public string spirit;
    public float spiritCoolDown;

    private bool CoolDownOver = true;
    
    //detem ability
    public GameObject pushback;
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && CoolDownOver)
        {
            StartCoroutine(CoolDown());

            if (spirit == "Detem")
            {
                StartCoroutine(DetemAbility());
            }
        }
    }

    IEnumerator CoolDown()
    {
        CoolDownOver = false;
        yield return new WaitForSeconds(spiritCoolDown);
        CoolDownOver = true;
    }


    IEnumerator DetemAbility()
    {
        GameObject PushBackPreFab = Instantiate(pushback, transform.position, transform.rotation);
        PushBackPreFab.transform.SetParent(transform, true); //makes parent
        yield return new WaitForSeconds(0.5f);
        Destroy(PushBackPreFab);
    }
}
