using Core.Shaders;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Core.Controller
{
  public class CameraController : MonoBehaviour
  {
    public Camera cam;

    public Volume volume;

    public Camera uiCam;

    public struct PostProcessing
    {
      public Bloom bloom;
      public Blur blur;
    }

    public PostProcessing postProcessing;

    private void Awake()
    {
      postProcessing = new PostProcessing();

      volume.profile.TryGet<Bloom>(out postProcessing.bloom);
      volume.profile.TryGet<Blur>(out postProcessing.blur);
    }

    public Tweener SetBlurStrength(float strength, float duration)
    {
      return DOTween.To(
        x => postProcessing.blur.strength.SetValue(new FloatParameter(x)),
        postProcessing.blur.strength.value,
        strength,
        duration
      );
    }
    
    public Tweener SetBloomIntensity(float intensity, float duration)
    {
      return DOTween.To(
        x => postProcessing.bloom.intensity.SetValue(new FloatParameter(x)),
        postProcessing.bloom.intensity.value,
        intensity,
        duration
      );
    }
    
    public Tweener SetBloomThreshold(float threshold, float duration)
    {
      return DOTween.To(
        x => postProcessing.bloom.threshold.SetValue(new FloatParameter(x)),
        postProcessing.bloom.threshold.value,
        threshold,
        duration
      );
    }
  }
}
