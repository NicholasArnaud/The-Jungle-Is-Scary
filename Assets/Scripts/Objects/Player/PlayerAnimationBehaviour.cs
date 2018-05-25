using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add this to respond to an animation event for the player
/// </summary>
[DisallowMultipleComponent]
public class PlayerAnimationBehaviour : MonoBehaviour, IPlayerDiedHandler
{
    public UnityEngine.Events.UnityEvent DeathResponse;
    public UnityEngine.Events.UnityEvent DamagedResponse;
     
    public void OnPlayerDied(Object[] args)
    {
        DeathResponse.Invoke();
    }

    public void OnPlayerDamaged(Object[] args)
    {
        DamagedResponse.Invoke();
    }
}
