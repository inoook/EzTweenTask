using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ez;
using System.Threading.Tasks;

public class TweenAnimBehaviourTest : MonoBehaviour
{
    [SerializeField] TweenAnimBehaviour tweenAnimBehaviour = null;
    [SerializeField] TweenAnimBehaviour tweenAnimBehaviourSphere = null;

    [SerializeField] TweenAnimCombiner tweenAnimCombiner = null;

    [SerializeField] Rect drawRect = new Rect(10,10,200,200);

    private void OnGUI()
    {
        GUILayout.BeginArea(drawRect);
        GUILayout.Label($"count: {EzTween.TweenCount}");
        if (GUILayout.Button("Play All"))
        {
            //_ = ezTweenBehaviour.Play();
            _ = Play();
        }
        if (GUILayout.Button("Cancel1"))
        {
            tweenAnimBehaviour.Cancel();
        }
        if (GUILayout.Button("Cancel2"))
        {
            tweenAnimBehaviour.Cancel();
        }
        if (GUILayout.Button("Cancel All"))
        {
            tweenAnimBehaviour.Cancel();
            tweenAnimBehaviourSphere.Cancel();
        }
        if (GUILayout.Button("Restore All"))
        {
            // アニメ開始位置へ
            tweenAnimBehaviour.RestoreStartCondition();
            tweenAnimBehaviourSphere.RestoreStartCondition();
        }

        GUILayout.Space(20);

        GUILayout.Label("tweenAnimCombiner");
        if (GUILayout.Button("Play "))
        {
            _ = PlayForwardCombiner(true);
        }
        if (GUILayout.Button("Play Reverse"))
        {
            _ = PlayForwardCombiner(false);
        }
        if (GUILayout.Button("Cancel"))
        {
            tweenAnimCombiner.Cancel();
        }
        if (GUILayout.Button("Restore"))
        {
            // アニメ開始位置へ
            tweenAnimCombiner.RestoreStartCondition();
        }
        GUILayout.EndArea();
    }

    async Task Play()
    {
        // 順に再生
        await tweenAnimBehaviour.Play();
        await tweenAnimBehaviourSphere.Play();
        Debug.LogWarning("Play_Complete");
    }

    async Task PlayForwardCombiner(bool isForward = true)
    {
        // 順に再生
        await tweenAnimCombiner.PlayForward(isForward);
        Debug.LogWarning("PlayCombiner_Complete");
    }
}
