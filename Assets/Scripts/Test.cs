using System;
using System.Collections;
using Core.Controller;
using Core.Manager;
using Core.Sprite;
using UnityEngine;

public class Test : MonoBehaviour
{
  private SpriteFlasher _spriteFlasher;

  private void Awake()
  {
    _spriteFlasher = GetComponent<SpriteFlasher>();
  }

  private void Start()
  {
    _spriteFlasher.Flash();
    
    CoreSys.Instance.CreateCamera();
  }

  IEnumerator TestRoutine()
  {
    yield return new WaitForSeconds(1f);

    var defInt = CoreSys.Instance.cam.postProcessing.bloom.intensity.value;
    CoreSys.Instance.cam.SetBloomIntensity(50f, 7f);
    
    yield return new WaitForSeconds(0.5f);
    CoreSys.Instance.transition.SetColor(Color.white);
    CoreSys.Instance.transition.Play(TransitionController.Type.FadeOut, 1f);
    
    yield return new WaitForSeconds(3f);
    
    CoreSys.Instance.transition.Play(TransitionController.Type.FadeIn, 1f);
    CoreSys.Instance.cam.SetBloomIntensity(defInt, 7f);
  }

  public void TestMessage()
  {
    StartCoroutine(TestRoutine());
    Debug.Log("TExt");
  }
}
