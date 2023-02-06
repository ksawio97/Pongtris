using UnityEngine;

public class CollisionController : MonoBehaviour
{
    private string borderTag;
    private string playerTag;
    private string boxTag;

    [SerializeField]
    private BallController ballScript;

    private void Start()
    {
        borderTag = "Border";
        playerTag = "Player";
        boxTag = "Box";
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.CompareTag(borderTag) || coll.transform.CompareTag(boxTag))
        {
            ballScript.OnHit(coll.transform.position, coll.collider.bounds.size);
        }

        else if (coll.transform.CompareTag(playerTag))
            ballScript.OnPlayerHit();
    }
}
