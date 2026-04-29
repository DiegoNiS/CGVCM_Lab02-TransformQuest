using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("Referencia a la puerta")]
    public Door door;

    [Header("Rotación al activar")]
    public float rotationAmount = 90f;
    public float rotationSpeed = 180f;

    private bool isActivated = false;
    private bool isRotating = false;
    private float currentRotation = 0f;
    private bool playerNearby = false;

    void Update()
    {
        if (!GameManager.Instance.IsGameActive()) return;

        // Animar rotación — Transformación Geométrica: ROTACIÓN
        if (isRotating)
        {
            float step = rotationSpeed * Time.deltaTime;
            float remaining = rotationAmount - currentRotation;

            if (step >= remaining)
            {
                transform.Rotate(0f, 0f, remaining);
                currentRotation = rotationAmount;
                isRotating = false;

                // Abrir la puerta
                if (door != null)
                    door.Open();
            }
            else
            {
                transform.Rotate(0f, 0f, step);
                currentRotation += step;
            }
        }

        // Activar palanca con ESPACIO
        if (playerNearby && Input.GetKeyDown(KeyCode.Space) && !isActivated)
        {
            PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
            if (player != null && !player.isSmall)
            {
                isActivated = true;
                isRotating = true;
                currentRotation = 0f;
            }
            else
            {
                Debug.Log("¡Debes estar en tamaño normal para activar la palanca!");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }
}
