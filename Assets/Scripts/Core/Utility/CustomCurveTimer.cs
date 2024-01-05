using System;
using System.Collections;
using UnityEngine;

namespace Core.Utility
{
  [Serializable]
  public class CustomCurveTimer
  {
    public delegate void CustomCurveTimerEventListener(CustomCurveTimer sender);

    /// <summary>
    /// Call when timer ends
    /// </summary>
    public event CustomCurveTimerEventListener onEnd;

    /// <summary>
    /// Call when timer starts
    /// </summary>
    public event CustomCurveTimerEventListener onStart;

    /// <summary>
    /// Call when before timer starts
    /// </summary>
    public event CustomCurveTimerEventListener onBeforeStart;

    /// <summary>
    /// Call when before timer ends
    /// </summary>
    public event CustomCurveTimerEventListener onBeforeEnd;

    public event CustomCurveTimerEventListener onForceStop;

    /// <summary>
    /// Call when timer activating
    /// </summary>
    public event CustomCurveTimerEventListener onTick;

    [SerializeField]
    private bool m_isUnscaled;

    [SerializeField]
    private float m_elapsedTime;

    [SerializeField]
    [Min(0.1f)]
    private float m_duration;

    [SerializeField]
    [Range(0f, 1f)]
    private float m_value;
    
    [SerializeField]
    private bool m_isPlaying;

    [SerializeField]
    private AnimationCurve m_curve = AnimationCurve.Linear(0,0,1,1);

    /// <summary>
    /// Current time of timer
    /// </summary>
    public float elapsedTime {
      get => m_elapsedTime;
      private set => m_elapsedTime = value;
    }

    /// <summary>
    /// End value of timer
    /// </summary>
    public float duration {
      get => m_duration;
      set => m_duration = value;
    }

    /// <summary>
    /// Is working with unscaled delta time
    /// </summary>
    public bool isUnscaled {
      get => m_isUnscaled;
      set => m_isUnscaled = value;
    }

    public float value {
      get => m_value;
      private set => m_value = value;
    }

    public bool isPlaying {
      get => m_isPlaying;
      private set => m_isPlaying = value;
    }

    public AnimationCurve curve {
      get => m_curve;
      set => m_curve = value;
    }

    private Coroutiner coroutiner;

    public CustomCurveTimer(float duration = 1)
    {
      coroutiner = new Coroutiner(Routine);
      this.duration = duration;
    }

    public CustomCurveTimer(float duration, CustomCurveTimerEventListener onEndCallback) : this(duration)
    {
      onEnd += onEndCallback;
    }

    private IEnumerator Routine()
    {
      onStart?.Invoke(this);
      while (true) {
        isPlaying = true;
        elapsedTime += isUnscaled ? Time.unscaledDeltaTime : Time.deltaTime;

        var t = elapsedTime / duration;

        value = curve.Evaluate(elapsedTime);

        onTick?.Invoke(this);
        if (elapsedTime >= duration) {
          onBeforeEnd?.Invoke(this);
          onEnd?.Invoke(this);
          isPlaying = false;
          yield break;
        }

        yield return new WaitForEndOfFrame();
      }
    }

    /// <summary>
    /// Start this timer
    /// </summary>
    /// <param name="startValue">starting value</param>
    public void Start(float startValue = 0f)
    {
      elapsedTime = startValue;
      Resume();
    }

    /// <summary>
    /// Resume this timer
    /// </summary>
    public void Resume()
    {
      onBeforeStart?.Invoke(this);
      coroutiner.Start();
    }

    /// <summary>
    /// Stop this timer
    /// </summary>
    public void Stop()
    {
      coroutiner.Stop();
      m_isPlaying = false;
      onForceStop?.Invoke(this);
    }
  }
}
