using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Core.Shaders
{
  [System.Serializable, VolumeComponentMenu("Blur")]
  public class Blur : VolumeComponent, IPostProcessComponent
  {
    [Tooltip("Standard deviation (spread) of the blur. Grid size is approx. 3x larger.")]
    public FloatParameter strength = new MinFloatParameter(0f,0f);

    public bool IsActive()
    {
      return (strength.value > 0.0f) && active;
    }

    public bool IsTileCompatible()
    {
      return false;
    }
  }

  public class BlurRenderPass : ScriptableRenderPass
  {
    private Material material;
    private Blur _blur;

    private RenderTargetIdentifier source;
    private RenderTargetHandle blurTex;
    private int blurTexID;

    public bool Setup(ScriptableRenderer renderer)
    {
      source = renderer.cameraColorTarget;
      _blur = VolumeManager.instance.stack.GetComponent<Blur>();
      renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;

      if (_blur != null && _blur.IsActive())
      {
        material = new Material(Shader.Find("PostProcessing/Blur"));
        return true;
      }

      return false;
    }

    public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
    {
      if (_blur == null || !_blur.IsActive())
      {
        return;
      }

      blurTexID = Shader.PropertyToID("_BlurTex");
      blurTex = new RenderTargetHandle();
      blurTex.id = blurTexID;
      cmd.GetTemporaryRT(blurTex.id, cameraTextureDescriptor);

      base.Configure(cmd, cameraTextureDescriptor);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
      if (_blur == null || !_blur.IsActive())
      {
        return;
      }

      CommandBuffer cmd = CommandBufferPool.Get("Blur Post Process");

      // Set Blur effect properties.
      int gridSize = Mathf.CeilToInt(_blur.strength.value * 3.0f);

      if (gridSize % 2 == 0)
      {
        gridSize++;
      }

      material.SetInteger("_GridSize", gridSize);
      material.SetFloat("_Spread", _blur.strength.value);

      // Execute effect using effect material with two passes.
      cmd.Blit(source, blurTex.id, material, 0);
      cmd.Blit(blurTex.id, source, material, 1);

      context.ExecuteCommandBuffer(cmd);
      cmd.Clear();
      CommandBufferPool.Release(cmd);
    }

    public override void FrameCleanup(CommandBuffer cmd)
    {
      cmd.ReleaseTemporaryRT(blurTexID);
      base.FrameCleanup(cmd);
    }
  }
}
