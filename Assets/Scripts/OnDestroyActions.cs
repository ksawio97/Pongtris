using System;
using UnityEngine;

public class OnDestroyActions : MonoBehaviour
{
    private Action<GameObject> _Dispatcher;

    public Action<GameObject> DispatcherSet { set { _Dispatcher = value; } }

    private Action<Vector3> _DestroyEffect;

    public Action<Vector3> DestroyEffectSet { set { _DestroyEffect = value; } }

    void OnDestroy()
    {
        if (_Dispatcher != null)
            _Dispatcher(gameObject);
        if (_DestroyEffect != null)
            _DestroyEffect(transform.position);
    }
}
