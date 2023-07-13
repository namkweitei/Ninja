
using UnityEngine;
public class Player : Character
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private float forceJump;
    [SerializeField] private Transform Kunai;
    private float maxHp;
    private Enemy target;
    private bool IsGrounded;
    public bool isAttack = false;
    private float timeHealling = 2;
    private float attackMouse;
    private Vector3 savePoint;
    float direct;
    int coin = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        coin = 0;
        SaveCheckPoint(transform.position);
        OnInit();
        maxHp = hp;

    }

    // Update is called once per frame
    void Update()
    {
        if(isDeath || isAttack){
            return;
        }
        direct = Input.GetAxis("Horizontal");   
        
        IsGrounded = CheckGround();
        
        Control(direct);
        AttackControl();
        Jump();
        HandleDirect(direct);
        HandleAnimation(direct);
        if (isAttack == false)
        {
            Healling();
        }
    }

    private void Control(float direct){
        rb.velocity = Vector2.right * direct * speed + Vector2.up * rb.velocity.y;
    }
    private void AttackControl()
    {
        if (IsGrounded)
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                Attack();
            }else if (Input.GetKeyDown(KeyCode.K)) 
            {
                Throw();
            }
            
        }
    }
    private void HandleDirect(float direct){
        if(direct != 0){
            ChangeDicrect(direct < 0);
        }
    }
    private void HandleAnimation(float direct){
        if(Mathf.Abs(rb.velocity.y) > 0.005f && rb.velocity.y > 0){
            ChangeAnimation("jump");
        }else if(Mathf.Abs(rb.velocity.y) > 0.05f && rb.velocity.y < -2f){
            ChangeAnimation("fall");
        }else{
            if(direct == 0 && IsGrounded == true && isAttack == false){
                ChangeAnimation("idle");
            }else if(direct != 0 && IsGrounded == true){
                ChangeAnimation("run");
            }
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) <= 0.05f && IsGrounded == true){
            JumpForce(forceJump);
        }
    }
    private void Attack()
    {
        ChangeAnimation("attack");
        isAttack = true;
        Invoke(nameof(ResetAttack),0.5f);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
        return;
    }

    private void Throw()
    {
        ChangeAnimation("throw");
        Transform newKunai = Instantiate(Kunai);
        newKunai.transform.position = transform.position;
        if (isRight == false)
        {
            newKunai.transform.rotation = Quaternion.identity;
        }
        else
        {
            newKunai.transform.rotation = Quaternion.Euler(Vector2.up * 180);
        }
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }
    protected override void ResetAttack()
    {
        base.ResetAttack();
        isAttack = false;
    }
    public void SaveCheckPoint(Vector3 point){
        savePoint = point;
    }
    public void LoadStatus(){
        ChangeAnimation("idle");
        OnInit();
        transform.position = savePoint; 
    }
    public override void Hit(float damage){
        base.Hit(damage);
        timeHealling = 2;
        if (isDeath){
            ChangeAnimation("death"); 
            Invoke(nameof(LoadStatus), 2f);
        }
    }
    private void Healling()
    {
        timeHealling -= Time.deltaTime;
        if(hp < maxHp && timeHealling <=0) 
        {
            hp +=Time.deltaTime * 10;
            healthBar.CurrHealth = hp;
        }
    }
    public void JumpForce(float force)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * force);
    }
    public void SetTarget(Enemy enemy)
    {
        this.target = enemy;
    }
    public void AddFruit(int amount = 1){
        coin += amount;
        GamePlay.Instance.SetFruit(coin);
        GamePlay.Instance.SetRank(coin);
    }
    private bool CheckGround()
    {   Debug.DrawLine(transform.position, transform.position + Vector3.down * 2.3f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2.3f, groundLayer);
        return hit.collider != null;
    }
  
}
