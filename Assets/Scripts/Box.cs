using UnityEngine;

public class Box : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        Debug.Log(GetComponent<BoxCollider2D>().bounds.size);
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        Destroy(gameObject);
    }
}
