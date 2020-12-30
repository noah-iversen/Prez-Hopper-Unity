using UnityEngine;
public class FenceScale : MonoBehaviour
{
    private void Awake()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;
        transform.localScale = new Vector3(1, 1, 1);
        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;
        transform.localScale = new Vector2(Screen.width / width * 2, Screen.height / height * (float)1.2);
        transform.position = new Vector3(9, 1, 0);
    }
}