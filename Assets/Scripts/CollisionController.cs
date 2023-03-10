using UnityEngine;

public class CollisionController : MonoBehaviour
{
    private string borderTag;
    private string playerTag;
    private string boxTag;

    private string killTag;

    [SerializeField]
    private BallController ballScript;

    private void Start()
    {
        borderTag = "Border";
        playerTag = "Player";
        boxTag = "Box";
        killTag = "Kill";
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.CompareTag(playerTag))
            ballScript.OnPlayerHit();

        else if (ballScript.hitInvincibility)
            return;

        else if (coll.transform.CompareTag(borderTag) || coll.transform.CompareTag(boxTag))      
            ballScript.OnHit(coll.transform.position, coll.collider.bounds.size);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.transform.CompareTag(killTag))
            Destroy(gameObject);
    }
}
