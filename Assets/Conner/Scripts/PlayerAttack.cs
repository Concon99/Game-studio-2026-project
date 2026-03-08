using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    [SerializeField] private PlayerMovement _PlayerMovement;

    public Transform weapon;
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
        
        float angle = 0f;

        if (_PlayerMovement.Direction == "Up") angle = 270f;
        if (_PlayerMovement.Direction == "Left") angle = 180f;
        if (_PlayerMovement.Direction == "Down") angle = 90f;
        if (_PlayerMovement.Direction == "Right") angle = 0f;

        weapon.rotation = Quaternion.Euler(0f, 0f, angle);


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
