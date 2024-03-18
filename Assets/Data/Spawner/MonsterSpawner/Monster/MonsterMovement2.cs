/*using UnityEngine;

public class MonsterMovement2 : MonsterAbstract
{
    public Transform enemyTransform;
    public float patrolSpeed = 2.0f;
    public float patrolDistance = 5.0f;
    public float raycastDistance = 0.5f;

    private Vector3 startPoint;
    private Vector3 endPoint;
    private bool isMovingRight = true;

    public float cooldownCount = 0f;
    public float cooldownTimer = 0f; 
    public float timeStop = 0f;


    protected override void Start()
    {
        this.cooldownTimer = Random.Range(5f, 9f);
        this.timeStop = Random.Range(3f, 5f);
    }

    private void FixedUpdate()
    {
        this.cooldownCount += Time.fixedDeltaTime;
        if(cooldownCount < cooldownTimer)
        {
            Patrol();
        }
        if(cooldownCount > cooldownTimer)
        {
            Stop();
        }
        if (cooldownCount > cooldownTimer + 3f)
        {
            if (Random.Range(0, 2) == 0) Flip();
            cooldownCount = 0;
            this.cooldownTimer = Random.Range(5f, 9f);
            this.timeStop = Random.Range(3f, 5f);
        }
    }

    void Patrol()
    {
        Vector3 forwardRaycastDirection = isMovingRight ? Vector2.right : Vector2.left;
        Vector3 forwardRaycastPos = new Vector3(transform.position.x + 0.5f * enemyTransform.localScale.x, transform.position.y);
        RaycastHit2D forwardHit = Physics2D.Raycast(forwardRaycastPos, forwardRaycastDirection, raycastDistance);
        Debug.DrawRay(forwardRaycastPos, forwardRaycastDirection * raycastDistance, Color.red);

        // Raycast to check for ground beneath the enemy (e.g., abyss)
        Vector3 groundRaycastDirection = Vector2.down;
        Vector3 groundRaycastPos = new Vector3(transform.position.x + 0.4f * enemyTransform.localScale.x, transform.position.y);
        RaycastHit2D groundHit = Physics2D.Raycast(groundRaycastPos, groundRaycastDirection, 1f);
        Debug.DrawRay(groundRaycastPos, groundRaycastDirection * 1f, Color.green);

        // If there's no ground beneath the enemy or it encounters a wall, flip direction
        if (groundHit.collider == null || forwardHit.collider?.gameObject.name == "Wall")
        {
            Flip();
        }

        // Move the enemy in the current direction
        enemyTransform.Translate(patrolSpeed * Time.deltaTime * (isMovingRight ? Vector3.right : Vector3.left));
        monsterCtrl.Animator.SetFloat("Run", 1);
    }

    void Flip()
    {
        isMovingRight = !isMovingRight; // Toggle the direction
        Vector3 newScale = enemyTransform.localScale;
        newScale.x *= -1; // Flip the x scale
        enemyTransform.localScale = newScale;
    }

    void Stop()
    {
        monsterCtrl.Animator.SetFloat("Run", 0);
    }
}*/