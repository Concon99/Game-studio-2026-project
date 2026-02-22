using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    public GameObject Weapon;
    public string WeaponType; //whatever this is set to will cause the Weapon gameobject to change to create different hitboxes
    public float WeaponAttackTime;
    public float WeaponCoolDown;
    public Animator animator;
    public bool CoolDown = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CoolDown)
        {
            StartCoroutine(AttackCoolDown());
            StartCoroutine(WeaponAttack());
        }
    }


    IEnumerator WeaponAttack()
    {
        animator.SetBool("Attack",true);
        Weapon.SetActive(true);
        yield return new WaitForSeconds(WeaponAttackTime);
        animator.SetBool("Attack",false);
        Weapon.SetActive(false);
    }

    IEnumerator AttackCoolDown()
    {
        CoolDown = false;
        yield return new WaitForSeconds(WeaponCoolDown);
        CoolDown = true;
    }

}
