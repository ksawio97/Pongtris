using UnityEngine;

public class BallController : MonoBehaviour
{
    private Vector3 moveAmount;
    private Vector3 defaultMoveAmount;

    private float maxTotalSpeed;

    [SerializeField]
    private GameObject _player;

    public GameObject playerSet { set { _player = value; } }

    [SerializeField]
    private GameOverController _gameOverController;

    public GameOverController gameOverControllerSet {set { _gameOverController = value;  }}

    private float playerHalfSize;
    private float speedPerDegree;
    private float pieceLength;

    void Start()
    {
        _gameOverController.BallCreated();

        defaultMoveAmount = new Vector3(0.1f, 0.1f);
        moveAmount = defaultMoveAmount;

        maxTotalSpeed = 0.2f;

        if (_player != null)
            playerHalfSize = _player.GetComponent<BoxCollider2D>().bounds.size.x / 2;

        int pieces = 45;
        speedPerDegree = moveAmount.y / pieces;
        pieceLength = playerHalfSize / pieces;
    }

    void FixedUpdate()
    {
        transform.position = transform.position + moveAmount;
    }

    private int PositiveOrNegative(float num) => num < 0 ? -1 : 1;

    public void OnPlayerHit()
    {
        float distanceFromCenter = Mathf.Abs(_player.transform.position.x - transform.position.x);

        float inRangeDegree(float num) => num / pieceLength < 45 ? num / pieceLength : 45;
        float newSpeedX = inRangeDegree(distanceFromCenter) * speedPerDegree;

        int direction = - PositiveOrNegative(moveAmount.y);
        moveAmount.y = (maxTotalSpeed - newSpeedX) * direction;

        if (_player.transform.position.x <= transform.position.x)
            moveAmount.x = newSpeedX;
        else
            moveAmount.x = -newSpeedX;
    }

    public void OnHit(Vector2 obsPos, Vector2 obsSize)
    {
        if (obsPos.y - obsSize.y / 2 <= transform.position.y && transform.position.y <= obsPos.y + obsSize.y / 2)
            moveAmount.x = -moveAmount.x;
        else
            moveAmount.y = -moveAmount.y;

        //moveAmount = new Vector3(PositiveOrNegative(moveAmount.x) * defaultMoveAmount.x, PositiveOrNegative(moveAmount.y) * defaultMoveAmount.y);
    }

    private void OnDestroy()
    {
        _gameOverController.BallDestroyed();
    }
}
