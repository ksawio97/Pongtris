using UnityEngine;
using System;
using System.Collections;

namespace Assets.Scripts
{
    public class EffectsController :MonoBehaviour
    {
        [SerializeField]
        private GameObject ballPrefab;

        [SerializeField]
        private GameObject explosionPrefab;

        [SerializeField]
        private GameObject player;

        [SerializeField]
        private GameManager gameManager;

        [SerializeField]
        private BoxCollider2D ballArea;

        private Func<Vector3, GameObject>[] effects;
        void Start()
        {
            effects = new Func<Vector3, GameObject>[] { CreateBall, Explode};
        }
        public void DoRandomEffect(Vector3 pos)
        {
            effects[UnityEngine.Random.Range(0, effects.Length)](pos);
        }

        public GameObject CreateBall(Vector3 pos)
        {
            var ball = Instantiate(ballPrefab);
            var ballController = ball.GetComponent<BallController>();

            ball.transform.position = pos;
            ballController.playerSet = player;
            ball.GetComponent<CollisionController>().ballAreaSet = ballArea;
            ballController.gameManagerSet = gameManager;

            return ball;
        }

        private GameObject Explode(Vector3 pos)
        {
            var explosion = Instantiate(explosionPrefab);
            explosion.transform.position = pos;
            AudioManager.Instance.PlaySound("Explosion");

            StartCoroutine("DisableExplosionTrigger", explosion.GetComponent<CircleCollider2D>());
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.startLifetime.constantMax);

            return explosion;
        }

        IEnumerator DisableExplosionTrigger(CircleCollider2D explosionCollider)
        {
            yield return new WaitForSeconds(0.3f);
            if (explosionCollider != null)
                explosionCollider.enabled = false;
        }
    }
}
