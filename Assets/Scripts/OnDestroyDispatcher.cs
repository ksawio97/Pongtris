using System;
using UnityEngine;

public class OnDestroyDispatcher : MonoBehaviour
{
    private Action<GameObject> _Dispatcher;

    public Action<GameObject> Dispatcher { set { _Dispatcher = value; } }
    void OnDestroy()
    {
        if (_Dispatcher != null)
            _Dispatcher(gameObject);
    }
}
