using UnityEngine;

public class CollisionController : MonoBehaviour
{
    private string borderTag;
    private string playerTag;
    private string boxTag;

    private string killTag;

    [SerializeField]
    private BallController ballScript;

    [SerializeField]
    private BoxCollider2D _ballArea;

    public BoxCollider2D ballAreaSet
    {
        set
        {
            _ballArea = value;
        }
    }

    private void Start()
    {
        borderTag = "Border";
        playerTag = "Player";
        boxTag = "Box";
        killTag = "Kill";

        SpawnValidation();
    }

    private void SpawnValidation()
    {
        Vector3 AddAdditionalSpace(Vector3 closestPoint)
        {
            var diffrence = transform.position - closestPoint;

            if (diffrence.x == 0)
                closestPoint.x += 0;
            else if (diffrence.x < 0)
                closestPoint.x += transform.localScale.x / 2;
            else
                closestPoint.x -= transform.localScale.x / 2;

            if (diffrence.y == 0)
                closestPoint.y += 0;
            else if (diffrence.y < 0)
                closestPoint.y += transform.localScale.y / 2;
            else
                closestPoint.y -= transform.localScale.y / 2;

            return closestPoint;
        }

        if (!GetComponent<CircleCollider2D>().IsTouching(_ballArea))
        {
            Vector3 closestPoint = _ballArea.ClosestPoint(transform.position);
            closestPoint = AddAdditionalSpace(closestPoint);

            transform.position = closestPoint;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        AudioManager.Instance.PlaySound("Hit");
        if (coll.transform.CompareTag(playerTag))
            ballScript.OnPlayerHit();

        else if (coll.transform.CompareTag(borderTag) || coll.transform.CompareTag(boxTag))
            ballScript.OnHit(coll.transform.position, coll.collider.bounds.size);
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        OnCollisionEnter2D(coll);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.transform.CompareTag(killTag))
            ballScript.DestroyOnTriggerLeft();
    }
}
