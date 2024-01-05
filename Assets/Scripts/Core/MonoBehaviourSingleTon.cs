using System;
using UnityEngine;

namespace Core
{
  public abstract class MonoBehaviourSingleTon<T> : MonoBehaviour where T : MonoBehaviourSingleTon<T>
  {
    protected static T _instance;
    public static T Instance {
      get => _instance;
    }
    
    protected virtual void Awake()
    {
      if (_instance is null)
      {
        _instance = (T)this;
      }
      else if (Instance != this)
      {
        Debug.LogWarning($"[{name}] There cannot be more than one component. " +
                         $"The component({typeof(T).Name}) was automatically deleted.");
        Destroy(this);
      }
    }

  }
}
