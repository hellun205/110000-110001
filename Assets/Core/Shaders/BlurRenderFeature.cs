using UnityEngine.Rendering.Universal;

namespace Core.Shaders
{
  public class BlurRendererFeature : ScriptableRendererFeature
  {
    BlurRenderPass blurRenderPass;

    public override void Create()
    {
      blurRenderPass = new BlurRenderPass();
      name = "Blur";
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
      renderer.EnqueuePass(blurRenderPass);
    }

    public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
    {
      base.SetupRenderPasses(renderer, in renderingData);
      blurRenderPass.Setup(renderer);
    }
  }
}
