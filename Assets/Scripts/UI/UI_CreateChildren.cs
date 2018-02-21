using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CreateChildren : MonoBehaviour
{
    public GameObject _prefab;
    public int _count;
    public List<GameObject> _childrens = new List<GameObject>();

    [ContextMenu("Create")]
    public void Create()
    {
        for (int i = 0; i < _count; i++)
        {
            var child = Instantiate(_prefab, transform);
            _childrens.Add(child);
        }
    }

    [ContextMenu("Destroy")]
    public void DestroyChildren()
    {
        _childrens.ForEach(x => Destroy(x));
    }
}
