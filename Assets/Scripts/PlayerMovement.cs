using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float normalSpeed = 4f;
    public float smallSpeed = 2f;

    [Header("Escala")]
    public float normalScale = 1f;
    public float smallScale = 0.5f;
    public float scaleSpeed = 2f; // velocidad de transición

    [Header("Estado")]
    public bool isSmall = false;
    public bool leverActivated = false;

    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private float targetScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetScale = normalScale;
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameActive()) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if (movement.x > 0) spriteRenderer.flipX = false;
        else if (movement.x < 0) spriteRenderer.flipX = true;

        // Interpolación gradual de escala — ESCALADO ANIMADO
        float currentScale = transform.localScale.x;
        if (!Mathf.Approximately(currentScale, targetScale))
        {
            float newScale = Mathf.MoveTowards(currentScale, targetScale, scaleSpeed * Time.deltaTime);
            transform.localScale = new Vector3(newScale, newScale, 1f);
        }
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.IsGameActive()) return;
        float speed = isSmall ? smallSpeed : normalSpeed;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    public void Shrink()
    {
        isSmall = true;
        targetScale = smallScale; // solo cambia el objetivo, la animación lo hace gradual
    }

    public void Grow()
    {
        isSmall = false;
        targetScale = normalScale;
    }
}