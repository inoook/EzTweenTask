using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ez;
using System.Threading.Tasks;

public class TweenAnimBehaviourTest : MonoBehaviour
{
    [SerializeField] TweenAnimCombiner tweenAnimCombiner = null;

    [SerializeField] Rect drawRect = new Rect(10,10,200,200);

    private void OnGUI()
    {
        GUILayout.BeginArea(drawRect);
        GUILayout.Label($"count: {EzTween.TweenCount}");
        
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


    async Task PlayForwardCombiner(bool isForward = true)
    {
        // 順に再生
        await tweenAnimCombiner.PlayForward(isForward);
        Debug.LogWarning("PlayCombiner_Complete");
    }
}
