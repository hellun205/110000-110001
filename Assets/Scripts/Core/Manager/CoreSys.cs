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
    
    public static CoreSys Load(bool log = false)
    {
      if (FindObjectOfType<CoreSys>() is null)
      {
        var obj = Instantiate(Resources.Load<CoreSys>("Core/CoreSys"));
        obj.name = "CoreSys";
        return obj;
      }
      else if(log)
        Debug.LogWarning("CoreSys already exists.");
      
      return _instance;
    }
  }
}
