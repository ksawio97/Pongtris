﻿using UnityEngine;
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
        private GameOverController gameOverController;

        [SerializeField]
        private BoxCollider2D ballArea;

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
            var ballController = ball.GetComponent<BallController>();

            ball.transform.position = pos;
            ballController.playerSet = player;
            ballController.ballAreaSet = ballArea;
            ballController.gameOverControllerSet = gameOverController;
        }

        private void Explode(Vector3 pos)
        {
            var explosion = Instantiate(explosionPrefab);
            explosion.transform.position = pos;

            StartCoroutine("DisableExplosionTrigger", explosion.GetComponent<CircleCollider2D>());
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.startLifetime.constantMax);
        }

        IEnumerator DisableExplosionTrigger(CircleCollider2D explosionCollider)
        {
            yield return new WaitForSeconds(0.3f);
            if (explosionCollider != null)
                explosionCollider.enabled = false;
        }
    }
}
