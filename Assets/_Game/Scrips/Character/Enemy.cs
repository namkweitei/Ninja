using System;
using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
public class Enemy : Character
{
    [SerializeField] private float attackRange;
    private IState currentState;
    private Player target;
    public Player Target => target;
    private void Start()
    {
        OnInit();
    }
    private void Update()
    {
        if(currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
    }
    public override void OnDeSpawn()
    {
        base.OnDeSpawn();
    }
    public override void Hit(float damage)
    {
        base.Hit(damage);
    }
    protected override void OnDeath()
    {
        base.OnDeath();
        ChangeAnimation("dead");
        Invoke(nameof(LoadDeath), 1f);
        
    }
    public void ChangeState(IState state)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if(currentState != null)
        {
            currentState.OnEnter(this);
        }

    }
    public void Moving()
    {
        ChangeAnimation("run");
        rb.velocity = transform.right * speed;

    }
    public void StopMoving()
    {
        ChangeAnimation("idle");
        rb.velocity = Vector2.zero;
    }
    public void Attack()
    {
        ChangeAnimation("attack");
        Invoke(nameof(ResetAttack), 0.5f);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
        return;
    }
    public bool IsTargetInRange()
    {
        if(target != null && Vector2.Distance(this.target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
	
	protected void OnTriggerEnter2D(Collider2D other)
	{
        if (other.CompareTag("EnemyWall"))
        {
            ChangeDicrect(!isRight);
            target = null;
        }
	}

    public void SetTarget(Player character)
    {
        this.target = character;
        if ( IsTargetInRange())
        {
            ChangeState(new AttackState());
        }else
        if(Target != null)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new IdleState());
        }
    }

    private void LoadDeath()
    {
        gameObject.SetActive(false);
    }
    
}
