using UnityEngine;
using UnityEngine.UIElements;

public class Dr_Moveset : MonoBehaviour
{
    public float DrSpeed;
    private Rigidbody2D rb;
    public GameObject Player;
    private bool Angered;
    public float idlestate;
    public float noticestate;
    public Dr_Health dr_health;

    public enum BossState
    {
        Idle,
        Attacking,
        PhaseTransition
}
    
    
    void Start()
    {
        
    }
    
    BossState state = BossState.Idle;
    void Update()
    {
        float distance = Vector2.Distance(gameObject.transform.position, Player.transform.position);
        if (distance < noticestate)
        {
            BossState state = BossState.Attacking;
        }
        if (distance < idlestate)
        {
            BossState state = BossState.Idle;
        }
        if (dr_health.DrHealth < 50f)
        {
            BossState state = BossState.PhaseTransition;
        }

        switch (state)
        {
            case BossState.Attacking:
                
                break;
            case BossState.Idle:

                break;
            case BossState.PhaseTransition:

                break;
        }
    }
    
    private void IdleFunction()
    {
        
    }
    private void AttackFunction()
    {
        
    }
    private void PhaseFunction()
    {
        
    }
    
    
    
}
