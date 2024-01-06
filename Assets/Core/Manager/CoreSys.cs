using System;
using Core.Controller;
using UnityEngine;

namespace Core.Manager
{
  public class CoreSys : MonoBehaviourSingleTon<CoreSys>
  {
    public new static CoreSys Instance {
      get {
        return _instance == null ? _instance = Load() : _instance;
      }
    }

    public CameraController cam;

    public TransitionController transition;

    public CameraController CreateCamera()
    {
      if (cam != null)
      {
        Debug.LogWarning("Core Camera already exists.");
        return cam;
      }
      
      var prefab = Resources.Load<CameraController>("Core/Camera");
      var obj = Instantiate(prefab);
      cam = obj;

      return obj;
    }
    
    public static CoreSys Load(bool log = false)
    {
      if (FindObjectOfType<CoreSys>() is null)
      {
        var prefab = Resources.Load<CoreSys>("Core/CoreSys");
        var obj = Instantiate(prefab);
        obj.name = "CoreSys";
        return obj;
      }
      else if(log)
        Debug.LogWarning("CoreSys already exists.");
      
      return _instance;
    }
    
  }
}
