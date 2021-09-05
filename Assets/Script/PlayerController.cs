using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private bool isGoal;

    private float coefficient = 0.985f;
    private float stopValue = 2.5f;

    private Animator anim;

    private int score;

    [Header("移動速度")]
    public float moveSpeed;

    [Header("加速速度")]
    public float accelerationSpeed;

    [Header("ジャンプ力")]
    public float jumpPower;

    [SerializeField]
    private PhysicMaterial pmNoFriction;

    [SerializeField, Header("地面判定プレイヤー")]
    private LayerMask groundLayer;

    [SerializeField, Header("斜面との接地判定")]
    private bool isGrounded;

    [SerializeField]
    private UIManager uiManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        if (isGoal == true)
        {
            return;
        }
        float x = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(x * moveSpeed,
            rb.velocity.y, rb.velocity.z);

        Debug.Log(rb.velocity);
    }

    private void Update()
    {
        Brake();

        Accelerate();

        if (isGoal == true)
        {
            rb.velocity *= coefficient;

            if (rb.velocity.z <= stopValue)
            {
                rb.velocity = Vector3.zero;

                rb.isKinematic = true;
            }
        }

        CheckGround();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Jump();
        }
    }

    /// <summary>
    /// ブレーキ
    /// </summary>
    private void Brake()
    {
        float vertical = Input.GetAxis("Vertical");

        if (vertical < 0)
        {
            pmNoFriction.dynamicFriction += Time.deltaTime;

            if (pmNoFriction.dynamicFriction > 1.0f)
            {
                pmNoFriction.dynamicFriction = 1.0f;
            }
        }
        else
        {
            pmNoFriction.dynamicFriction = 0;
        }
    }

    private void Accelerate()
    {
        float vertical = Input.GetAxis("Vertical");

        if (vertical > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x,
                rb.velocity.y, accelerationSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            Debug.Log("Goal");

            isGoal = true;

            Debug.Log(isGoal);
        }
    }
    /// <summary>
    /// ジャンプ
    /// </summary>
    private void Jump()
    {
        rb.AddForce(transform.up * jumpPower);

        anim.SetTrigger("jump");
    }

    private void CheckGround()
    {
        isGrounded = Physics.Linecast(transform.position,
            transform.position - transform.up * 0.3f, groundLayer);

        Debug.DrawLine(transform.position,
            transform.position - transform.up * 0.3f, Color.red);
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="amount"></param>
    public void AddScore(int amount)
    {
        score += amount;

        Debug.Log("現在の得点：" + score);

        uiManager.UpdateDisplayScore(score);
    }
}
