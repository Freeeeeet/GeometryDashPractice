using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class AutoResizeCollider : MonoBehaviour
{
    void Reset()
    {
        UpdateColliderSize();
    }

    void OnValidate()
    {
        UpdateColliderSize();
    }

    private void UpdateColliderSize()
    {
        var sr = GetComponent<SpriteRenderer>();
        var col = GetComponent<BoxCollider2D>();

        if (sr.sprite == null || col == null)
            return;

        // Обновляем размер с учётом scale
        Vector2 size = sr.sprite.bounds.size;
        Vector3 lossyScale = transform.lossyScale;

        col.size = new Vector2(size.x * lossyScale.x, size.y * lossyScale.y);
        col.offset = new Vector2(0, size.y * lossyScale.y / 2); // Центрируем коллайдер по вертикали
        
    }
}