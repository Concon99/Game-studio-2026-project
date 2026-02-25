using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    [SerializeField] private PlayerMovement _PlayerMovement;
    
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
        
        if (_PlayerMovement.Direction == "Up")
        {
            transform.rotation = Quaternion.Euler(-0.6f, 0f, 0);
            transform.Rotate(-0f, 0f, 90f);
        }
        if (_PlayerMovement.Direction == "Left")
        {
            transform.rotation = Quaternion.Euler(0, -0.6f, 0);
            transform.Rotate(0f, 0f, 180f);
        }
        if (_PlayerMovement.Direction == "Down")
        {
            transform.rotation = Quaternion.Euler(0.6f, 0f, 0);
            transform.Rotate(0f, 0f, 270f);
        }
        if (_PlayerMovement.Direction == "Right")
        {
            transform.rotation = Quaternion.Euler(0, 0.6f, 0);
            transform.Rotate(0f, 0f, 0f);
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
