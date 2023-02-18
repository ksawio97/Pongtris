using System;
using UnityEngine;

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

    void OnNormalBoxDestroy()
    {
        var particles = Instantiate(boxDestroyedParticlesPrefab);
        particles.transform.position = transform.position;

        var psMain = particles.GetComponent<ParticleSystem>().main;
        psMain.startColor = GetComponent<SpriteRenderer>().color;

        particles.GetComponent<ParticleSystem>().Play();

        Destroy(particles, particles.GetComponent<ParticleSystem>().main.startLifetime.constantMax);
    }

    void OnDestroy()
    {
        if (!GetComponent<Box>().destroyedFromGameObject)
            return;

        if (_Dispatcher != null)
            _Dispatcher(gameObject);

        if (_DestroyEffect != null)
            _DestroyEffect(transform.position);
        else
            OnNormalBoxDestroy();

        if (_PointsAdd != null)
            _PointsAdd();
    }
}
