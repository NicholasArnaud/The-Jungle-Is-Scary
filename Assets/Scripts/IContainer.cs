using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContainer
{
    void AddToInventory(Item gem);
    void RemoveFromInventory(Item gem);
}
