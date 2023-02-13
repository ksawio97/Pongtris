using UnityEngine;
using System;
using System.Runtime.CompilerServices;

namespace Assets.Scripts
{
    public class EffectsController :MonoBehaviour
    {
        [SerializeField]
        private GameObject ballPrefab;

        [SerializeField]
        private GameObject player;

        private Action<Vector3>[] effects;
        void Start()
        {
            effects = new Action<Vector3>[]{CreateBall, Explode};
        }
        public void DoRandomEffect(Vector3 pos)
        {
            effects[UnityEngine.Random.Range(0, effects.Length)](pos);
        }

        private void CreateBall(Vector3 pos)
        {
            var ball = Instantiate(ballPrefab);
            ball.transform.position = pos;
            ball.GetComponent<BallController>().playerSet = player;
        }

        private void Explode(Vector3 pos)
        {
            void CreateExposionObject()
            {
                var explosion = new GameObject();

                explosion.name = "Explosion";
                explosion.tag = "Explosion";
                explosion.transform.position = pos;

                explosion.AddComponent<CircleCollider2D>().radius = 2;
                explosion.GetComponent<CircleCollider2D>().isTrigger = true;
                explosion.AddComponent<Rigidbody2D>().gravityScale = 0;

                Destroy(explosion, 0.5f);
            }
            CreateExposionObject();
        }
    }
}
