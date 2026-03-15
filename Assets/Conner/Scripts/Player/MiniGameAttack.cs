using UnityEngine;
using System.Collections;

public class MiniGameAttack : MonoBehaviour
{
    
    [SerializeField] private PlayerMovement _PlayerMovement;

    public Transform weapon;

    public Animator animator;

    public GameObject Weapon;


    
    void Update()
    {
        float angle = 0f;

        if (_PlayerMovement.Direction == "Up") angle = 270f;
        if (_PlayerMovement.Direction == "Left") angle = 180f;
        if (_PlayerMovement.Direction == "Down") angle = 90f;
        if (_PlayerMovement.Direction == "Right") angle = 0f;

        weapon.rotation = Quaternion.Euler(0f, 0f, angle);


    }
    
    
    public IEnumerator WeaponAttack()
    {
        animator.SetBool("UltraAttack",true);
        Weapon.SetActive(true);
        yield return new WaitForSeconds(2);
        animator.SetBool("UltraAttack",false);
        Weapon.SetActive(false);
        GameManager.Instance.MiniGameDamage = 0;
    }
}
