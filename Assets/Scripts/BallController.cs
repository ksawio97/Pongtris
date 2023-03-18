using UnityEngine;

public class BallController : MonoBehaviour
{
    private Vector3 _moveAmount;
    private Vector3 defaultMoveAmount = new Vector3(0.1f, 0.1f);

    public Vector3 moveAmount
    {
        get
        {
            return _moveAmount;
        }
        set
        {
            defaultMoveAmount = value;
        }
    }

    private float maxTotalSpeed;

    [SerializeField]
    private GameObject _player;

    public GameObject playerSet { set { _player = value; } }

    [SerializeField]
    private GameManager _gameManager;

    public GameManager gameManagerSet { set { _gameManager = value;  }}

    private float playerHalfSize;
    private float speedPerDegree;
    private float pieceLength;

    private bool hitThisFrame;

    void Start()
    {
        var size = DynamicGameSize.GetAppropriateSize(transform.localScale);
        transform.localScale = new Vector3(size.x, size.x, transform.localScale.z);

        _gameManager.BallCreated();

        _moveAmount = defaultMoveAmount;

        maxTotalSpeed = (DynamicGameSize.ratio.x * DynamicGameSize.ratio.y) * 0.2f;

        if (_player != null)
            playerHalfSize = _player.GetComponent<BoxCollider2D>().bounds.size.x / 2;

        int pieces = 45;
        speedPerDegree = maxTotalSpeed / 2 / pieces;
        pieceLength = playerHalfSize / pieces;

        hitThisFrame = false;
    }

    void FixedUpdate()
    {
        hitThisFrame = false;
        transform.position = transform.position + _moveAmount;
    }

    public void OnPlayerHit()
    {
        float distanceFromCenter = Mathf.Abs(_player.transform.position.x - transform.position.x);

        float inRangeDegree(float num) => num / pieceLength <= 45 ? num / pieceLength : 45;
        float newSpeedX = inRangeDegree(distanceFromCenter) * speedPerDegree;

        _moveAmount.y = (maxTotalSpeed - Mathf.Abs(newSpeedX));

        if (_player.transform.position.x <= transform.position.x)
            _moveAmount.x = newSpeedX;
        else
            _moveAmount.x = -newSpeedX;
    }

    public void OnHit(Vector2 obsPos, Vector2 obsSize)
    {
        //prevent hitting 2 boxes at one time and not bouncing 
        if (hitThisFrame)
            return;
        if (obsPos.y - obsSize.y / 2 <= transform.position.y && transform.position.y <= obsPos.y + obsSize.y / 2)
            _moveAmount.x = -moveAmount.x;
        else
            _moveAmount.y = -moveAmount.y;
        hitThisFrame = true;
    }

    public void DestroyOnTriggerLeft()
    {
        _gameManager.BallDestroyed();
        Destroy(gameObject);
    }
}
