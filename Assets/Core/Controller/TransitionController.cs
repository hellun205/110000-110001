using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Controller
{
  public class TransitionController : MonoBehaviour
  {
    private Image _image;
    private Animator _anim;

    public enum Type
    {
      In,
      Out,
      FadeIn,
      FadeOut
    }

    private void Awake()
    {
      _image = GetComponent<Image>();
      _anim = GetComponent<Animator>();
    }

    public void Play(Type type, float duration = 1f)
    {
      _anim.SetFloat("speed", 1 / duration);
      _anim.Play(type.ToString());
    }

    public void SetColor(Color color)
    {
      _image.color = color;
    }
  }
}
