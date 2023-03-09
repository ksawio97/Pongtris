using UnityEngine;

public class CollisionController : MonoBehaviour
{
    private string borderTag;
    private string playerTag;
    private string boxTag;

    private string killName;

    [SerializeField]
    private BallController ballScript;

    private void Start()
    {
        borderTag = "Border";
        playerTag = "Player";
        boxTag = "Box";
        killName = "BottomBorder";
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.name == killName && coll.transform.CompareTag(borderTag))
            Destroy(gameObject);

        else if (coll.transform.CompareTag(playerTag))
            ballScript.OnPlayerHit();

        else if (ballScript.hitInvincibility)
            return;

        else if (coll.transform.CompareTag(borderTag) || coll.transform.CompareTag(boxTag))      
            ballScript.OnHit(coll.transform.position, coll.collider.bounds.size);
    }
}
