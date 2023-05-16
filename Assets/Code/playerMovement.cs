using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float constspeed = 10f;
    public float jump = 5000f;
    public float secondsTimer = 0.1f;
    public float dashTime = 0.1f;
    public float dashImpulse = 10f;

    public bool ver = true;
    public bool dash = true;
    public bool dashing = false;
    private bool cancelDash = false;

    private Rigidbody2D rb;
    private Vector2 vx;
    private TrailRenderer tr;
    private CircleCollider2D cir;
    private buttons but;
    public GameObject canvas;
    public GameObject Win;

    private float hor;
    private float verti;
    private float timeCooldown;
    private float gravSupremeCalamity;
    private float initialScaleX;
    private float coyoteTime = 0.3f;
    private float coyoteCooldown;
    private float jumpTime = 0.4f;
    private float jumpBuffer;

    [SerializeField] private LayerMask jumpableGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        cir = GetComponent<CircleCollider2D>();
        but = canvas.GetComponent<buttons>();

        gravSupremeCalamity = rb.gravityScale;
        initialScaleX = transform.localScale.x;
    }
    private bool isGrounded()
    {
        return Physics2D.BoxCast(cir.bounds.center, cir.bounds.size, 0f , Vector3.down, 0.1f, jumpableGround);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            Win.SetActive(true);
        }
        cancelDash = true;
        if (dashing)
        {
            dashing = false;
            rb.gravityScale = gravSupremeCalamity;
            tr.emitting = false;
            Vector3 vx = rb.velocity;
            if (ver)
            {
                vx.y = 0f;
            }
            vx.x = 0f;
            rb.velocity = vx;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "kys")
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator DashingSwag()
    {
        cancelDash = false;
        Vector2 vx = rb.velocity;
        if (ver)
        {
            vx.y = 0f;
        }
        vx.x = 0f;
        rb.velocity = vx;
        rb.gravityScale = 0;
        dash = false;
        dashing = true;
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        dashing = false;
        rb.gravityScale = gravSupremeCalamity;
        tr.emitting = false;
        if (!cancelDash)
        {
            dashing = false;
            rb.gravityScale = gravSupremeCalamity;
            tr.emitting = false;
            vx = rb.velocity;
            if (ver)
            {
                vx.y = 0f;
            }
            vx.x = 0f;
            rb.velocity = vx;
        }
    }
    private void FixedUpdate()
    {
        if (dashing || but.pause)
        {
            return;
        }

        rb.velocity = new Vector2(hor * speed, rb.velocity.y);

        if (hor < 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = -initialScaleX;
            transform.localScale = newScale;
        }

        if (hor > 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = initialScaleX;
            transform.localScale = newScale;
        }
    }
    void Update()
    {
        if (dashing || but.pause)
        {
            return;
        }

        if (isGrounded())
        {
            dash = true;
            coyoteCooldown = coyoteTime;
        }
        else
        {
            
            coyoteCooldown -= Time.fixedDeltaTime;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpBuffer = jumpTime;
        }
        else
        {
            jumpBuffer -= Time.fixedDeltaTime;
        }
        hor = Input.GetAxisRaw("Horizontal");
        verti = Input.GetAxis("Vertical");
        ver = Input.GetKey(KeyCode.W);

        if (jumpBuffer > 0 && coyoteCooldown > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump * Time.fixedDeltaTime);
            timeCooldown = Time.time + secondsTimer;

            jumpBuffer = 0f;
        }

        if(Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteCooldown = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && dash == true && Time.time > timeCooldown)
        {
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
            {
                StartCoroutine(DashingSwag());
                rb.velocity = new Vector2(hor * dashImpulse * Time.fixedDeltaTime, System.Convert.ToUInt32(ver) * dashImpulse * Time.fixedDeltaTime);
            }
            
        }
    }
}