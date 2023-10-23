using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    protected System.Random _random;
    private List<GameObject> _pool;
    private int _capacity = 5;

    public int ActiveItemsLeft => _pool.Where(item => item.gameObject.activeSelf).Count();

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

    protected void Initialize(Vehicle[] prefabs, float itemsAmount)
    {
        _capacity = Convert.ToInt32(itemsAmount);
        Initialize(prefabs);
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
