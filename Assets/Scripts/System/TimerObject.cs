using System.Collections;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Name: TimerObject
/// Description:
/// Usage:
/// </summary>
[CreateAssetMenu(menuName = "TimerObject")]
public class TimerObject : ScriptableObject
{
    public float seconds = 1;
    public void Execute(MonoBehaviour monoBehaviour, UnityAction callback)
    {
        monoBehaviour.StartCoroutine(Routine(callback));
    }

    private IEnumerator Routine(UnityAction callback)
    {
        yield return new WaitForSeconds(seconds);
        callback.Invoke();
    }
}
