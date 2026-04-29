using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite closedSprite;
    public Sprite openSprite;

    private SpriteRenderer spriteRenderer;
    private Collider2D doorCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<Collider2D>();

        if (closedSprite != null)
            spriteRenderer.sprite = closedSprite;
    }

    public void Open()
    {
        if (openSprite != null)
            spriteRenderer.sprite = openSprite;

        if (doorCollider != null)
            doorCollider.enabled = false;
    }
}