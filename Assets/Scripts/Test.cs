using System;
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
  }
}