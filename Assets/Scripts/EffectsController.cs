using UnityEngine;
using System;

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
            Debug.Log("Explosion!");
        }
    }
}
