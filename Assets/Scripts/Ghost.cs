using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 3f;
    public float startX = -3f;
    public float endX = 3f;

    private SpriteRenderer spriteRenderer;
    private bool movingRight = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.position = new Vector3(startX, transform.position.y, 0f);
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameActive()) return;

        // Traslación del fantasma — Transformación Geométrica: TRASLACIÓN
        float moveDir = movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * moveDir * speed * Time.deltaTime);

        // Voltear sprite según dirección
        spriteRenderer.flipX = !movingRight;

        // Cambiar dirección al llegar a los límites
        if (transform.position.x >= endX)
            movingRight = false;
        else if (transform.position.x <= startX)
            movingRight = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.Lose("¡El fantasma te atrapó!");
    }
}
