using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class OnDestroyActions : MonoBehaviour
{
    [SerializeField]
    private GameObject boxDestroyedParticlesPrefab;

    private Action<GameObject> _Dispatcher;

    public Action<GameObject> DispatcherSet { set { _Dispatcher = value; } }

    private Action<Vector3> _DestroyEffect;

    public Action<Vector3> DestroyEffectSet { set { _DestroyEffect = value; } }

    private Action _PointsAdd;

    public Action PointsAddSet { set { _PointsAdd = value; } }
    private static Action _GameEnd;

    public static Action GameEnd { set { _GameEnd = value; } }

    private bool gettingDestroyed = false;

    void OnNormalBoxDestroy()
    {
        AudioManager.Instance.PlaySound("BoxDestroy");
        var particles = Instantiate(boxDestroyedParticlesPrefab);
        particles.transform.position = transform.position;

        var psMain = particles.GetComponent<ParticleSystem>().main;
        psMain.startColor = GetComponent<SpriteRenderer>().color;

        particles.GetComponent<ParticleSystem>().Play();

        Destroy(particles, particles.GetComponent<ParticleSystem>().main.startLifetime.constantMax);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (!gettingDestroyed && coll.transform.CompareTag("Kill"))
            _GameEnd();
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.transform.CompareTag("Explosion"))
        {
            gettingDestroyed = true;
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.transform.CompareTag("Ball"))
        {
            gettingDestroyed = true;
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.transform.CompareTag("Ball"))
        {
            gettingDestroyed = true;
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        //to prevent doing code when scene is getting closed
        if (!gettingDestroyed)
            return;

        if (_Dispatcher != null)
            _Dispatcher(gameObject);

        if (_DestroyEffect != null)
        {
            AudioManager.Instance.PlaySound("SpecialBoxDestroy");
            _DestroyEffect(transform.position);
        }
        else
            OnNormalBoxDestroy();

        if (_PointsAdd != null)
            _PointsAdd();
    }
}
