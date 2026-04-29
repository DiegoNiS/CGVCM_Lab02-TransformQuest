using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float normalSpeed = 4f;
    public float smallSpeed = 2f;

    [Header("Escala")]
    public float normalScale = 1f;
    public float smallScale = 0.5f;

    [Header("Estado")]
    public bool isSmall = false;
    public bool leverActivated = false;

    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameActive()) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        // Voltear sprite según dirección
        if (movement.x > 0)
            spriteRenderer.flipX = false;
        else if (movement.x < 0)
            spriteRenderer.flipX = true;
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.IsGameActive()) return;

        float speed = isSmall ? smallSpeed : normalSpeed;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    // Llamado por la poción verde — achica al jugador
    public void Shrink()
    {
        isSmall = true;
        transform.localScale = new Vector3(smallScale, smallScale, 1f);
    }

    // Llamado por la poción azul — regresa al tamaño normal
    public void Grow()
    {
        isSmall = false;
        transform.localScale = new Vector3(normalScale, normalScale, 1f);
    }
}