using UnityEngine;

public class Potion : MonoBehaviour
{
    public enum PotionType { Shrink, Grow }
    public PotionType potionType;

    [Header("Rotación")]
    public float rotationSpeed = 90f; // grados por segundo

    void Update()
    {
        // Rotación constante — Transformación Geométrica: ROTACIÓN
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player == null) return;

        if (potionType == PotionType.Shrink)
            player.Shrink();
        else
            player.Grow();

        Destroy(gameObject);
    }
}
