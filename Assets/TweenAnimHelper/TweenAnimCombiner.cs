using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 複数を合成しTaskとして再生
/// </summary>
[AddComponentMenu("AnimationHelper/TweenAnimCombiners")]
public class TweenAnimCombiner : MonoBehaviour
{
    [SerializeField] TweenAnimBehaviour[] tweenAnimBehaviours = null;

    [ContextMenu("Play")]
    public async Task Play()
    {
        if (!Application.isPlaying) { return; }

        foreach(var tweenAnim in tweenAnimBehaviours)
        {
            await tweenAnim.Play();
        }
        Debug.LogWarning("TweenAnimCombiner.Play_Complete");
    }

    public void Cancel()
    {
        foreach (var tweenAnim in tweenAnimBehaviours)
        {
            tweenAnim.Cancel();
        }
    }

    /// <summary>
    /// 初期状態へ戻す
    /// </summary>
    [ContextMenu("Restore")]
    public void RestoreStartCondition()
    {
        if (!Application.isPlaying) { return; }

        foreach (var tweenAnim in tweenAnimBehaviours)
        {
            tweenAnim.RestoreStartCondition();
        }
    }

    [ContextMenu("Revert")]
    public void Revert()
    {
        if (!Application.isPlaying) { return; }

        foreach (var tweenAnim in tweenAnimBehaviours)
        {
            tweenAnim.Revert();
        }
    }
}
