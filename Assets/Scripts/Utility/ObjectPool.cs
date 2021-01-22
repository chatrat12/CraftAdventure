using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    public IEnumerable<T> CheckedOutObjects => _checkedOutObjects;

    private Queue<T> _freeObjects = new Queue<T>();
    private List<T> _checkedOutObjects = new List<T>();
    private Func<T> _createObjectFunction = null;

    public ObjectPool(Func<T> createObjectFunction)
    {
        _createObjectFunction = createObjectFunction;
    }

    public T GetFreeObject()
    {
        var result = _freeObjects.Count > 0 ? _freeObjects.Dequeue() :
                                              _createObjectFunction?.Invoke() ?? null;
        if (result != null)
        {
            result.gameObject.SetActive(true);
            _checkedOutObjects.Add(result);
        }
        return result;
    }

    public void ReturnToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        _freeObjects.Enqueue(objectToReturn);
        _checkedOutObjects.Remove(objectToReturn);
    }
}