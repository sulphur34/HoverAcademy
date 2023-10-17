using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField] protected GameObject _container;
    [SerializeField] private int _capacity;

    protected System.Random _random;
    private List<GameObject> _pool;

    public void Reset()
    {
        foreach (var item in _pool)
        {
            item.SetActive(false);
        }
    }

    protected void Initialize(Vehicle[] prefabs)
    {
        _pool = new List<GameObject>();
        _random = new System.Random();

        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = GetRandomContainer(prefabs);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }

    protected void Initialize(Vehicle prefab)
    {
        Initialize(new Vehicle[] { prefab });
    }    

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(gameObject => gameObject.activeSelf == false);
        return result != null;
    }

    protected abstract GameObject GetRandomContainer(Vehicle[] prefabs);
}
