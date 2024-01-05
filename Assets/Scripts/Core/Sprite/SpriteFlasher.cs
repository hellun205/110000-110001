using System;
using Core.Utility;
using DG.Tweening;
using UnityEngine;

namespace Core.Sprite
{
  [DisallowMultipleComponent]
  public class SpriteFlasher : MonoBehaviour
  {
    [ColorUsage(true, true)]
    public Color color = Color.white;

    [Range(0f, 1f)]
    public float amount = 1f;

    [Min(0f)]
    public float duration = 0f;

    public AnimationCurve speedCurve;

    private SpriteRenderer[] _spriteRenderers;
    private Material[] _materials;
    
    private CustomCurveTimer _timer;
    
    private void Awake()
    {
      _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
      
      Init();
    }

    private void Reset()
    {
      foreach (var sr in GetComponentsInChildren<SpriteRenderer>())
      {
        sr.material = Resources.Load<Material>("Shaders/FlashMaterial");
      }
    }

    private void InitTimer()
    {
      _timer = new CustomCurveTimer();
      _timer.onTick += TimerOnTick;
      _timer.onEnd += TimerOnEnd;

      _timer.duration = duration;
      _timer.curve = speedCurve;
    }
    private void TimerOnEnd(CustomCurveTimer sender)
    {
      _timer = null;
    }

    private void TimerOnTick(CustomCurveTimer sender)
    {
      // SetFlashAmount(Mathf.Lerp(1f, speedCurve.Evaluate(sender.elapsedTime), sender.value));
      Debug.Log(sender.value);
    }

    private void Init()
    {
      _materials = new Material[_spriteRenderers.Length];

      for (var i = 0; i < _spriteRenderers.Length; i++)
      {
        _materials[i] = _spriteRenderers[i].material;
      }
    }

    private void SetFlashColor()
    {
      foreach (Material material in _materials)
      {
        material.SetColor("_FlashColor", color);
      }
    }

    private void SetFlashAmount(float amount)
    {
      foreach (Material material in _materials)
      {
        material.SetFloat("_FlashAmount", amount);
      }
    }

    public void Flash()
    {
      SetFlashColor();
      InitTimer();
      _timer.Start();
    }

  }
}
