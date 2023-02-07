using UnityEngine;
using System;

namespace Assets.Scripts
{
    public static class EffectsController
    {

        private static Action[] effects = new Action[]
        {
        CreateBall,
        Explode
        };

        public static void DoRandomEffect()
        {
            effects[UnityEngine.Random.Range(0, effects.Length)]();
        }

        private static void CreateBall()
        {
            Debug.Log("New Ball!");
        }

        private static void Explode()
        {
            Debug.Log("Explosion!");
        }
    }
}
