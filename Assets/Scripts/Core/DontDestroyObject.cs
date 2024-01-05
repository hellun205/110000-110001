using System;
using UnityEngine;

namespace Core
{
  public class DontDestroyObject : MonoBehaviour
  {
    [SerializeField]
    private bool registerOnAwake = true;

    [SerializeField]
    private bool removeOnRegister = true;

    private void Awake()
    {
      if (registerOnAwake)
        Register();
    }

    public void Register()
    {
      DontDestroyOnLoad(gameObject);
      if (removeOnRegister)
        Destroy(this);
    }
  }
}
