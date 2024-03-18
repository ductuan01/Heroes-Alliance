using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class MonsterGrounding : SecondMonoBehaviour
{
    [SerializeField] protected new Rigidbody2D rigidbody;
    [SerializeField] protected CapsuleCollider2D capsuleCollider2D;
    [SerializeField] protected bool moveBack = false;
    [SerializeField] protected bool rushforward = false;


    private bool Skilling = false;
    private Vector3 SkillStartPosition;
    private Vector3 moveBackPosition;
    private Vector3 rushForwardPosition;
    private float SkillDuration = 2f;
    private float SkillTimer = 0.0f;

    [SerializeField] public Rigidbody2D rb;
    public float moveBackForce = 100f;
    public float rushForwardForce = 200f;

    Vector2 moveBackDirection;
    Vector2 rushForwardDirection;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
        this.LoadBoxCollider2D();
    }

    protected virtual void LoadRigidbody()
    {
        if (this.rigidbody != null) return;
        this.rigidbody = transform.GetComponent<Rigidbody2D>();
        this.rigidbody.bodyType = RigidbodyType2D.Static;
        this.rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        Debug.Log(transform.name + ": LoadRigidbody", gameObject);
    }

    protected virtual void LoadBoxCollider2D()
    {
        if (this.capsuleCollider2D != null) return;
        this.capsuleCollider2D = transform.GetComponent<CapsuleCollider2D>();
        /*this.capsuleCollider2D.isTrigger = true;
        this.boxCollider.offset = new Vector2(-0.25f, 0f);
        this.boxCollider.size = new Vector2(1f, 1.1f);*/
        Debug.Log(transform.name + ": LoadBoxCollider2D", gameObject);
    }
    float timecount = 0f;
    float timeDuration = 0.2f;
    private void Update()
    {
        timecount += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Chem m le");
            StartCoroutine(Skill());
        }

        if (Skilling)
        {
            //transform.position = Vector3.Lerp(SkillStartPosition, rushForwardPosition, 1f);
            if(rushforward) StartCoroutine(Back());
        }
        if (moveBack)
        {
            transform.position = Vector3.Lerp(rushForwardPosition, SkillStartPosition, 1f);
        }

    }

    private IEnumerator Skill()
    {
        Debug.Log("Chem m le1");
        Skilling = true;
        SkillStartPosition = transform.position;
        moveBackPosition = transform.position - new Vector3(0.5f, 0.0f, 0.0f);
        rushForwardPosition = transform.position + new Vector3(1f, 0.0f, 0.0f);
        SkillTimer = 0.0f;
        rushforward = false;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Chem m le2");
        Skilling = false;
        rushforward = true;
        timecount = 0;
    }
    private IEnumerator Back()
    {
        Debug.Log("Chem m le3");
        rushforward = false;
        moveBack = true;
        rushForwardPosition = transform.position;
        yield return new WaitForSeconds(0.5f);
        moveBack = false;
        timecount = 0;
    }

}
