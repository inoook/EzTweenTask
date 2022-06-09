using Ez;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TweenAnimBehaviourMethodTest : MonoBehaviour
{
    [SerializeField] TweenAnimBehaviour tweenAnimBehaviour = null;

    [SerializeField] Rect drawRect = new Rect(10, 10, 200, 200);

    private void OnGUI()
    {
        GUILayout.BeginArea(drawRect);
        GUILayout.Label($"count: {EzTween.TweenCount}");

        if (GUILayout.Button("Play"))
        {
            _ = Play();
        }
        if (GUILayout.Button("PlayReverse"))
        {
            _ = tweenAnimBehaviour.PlayReverse();
        }
        if (GUILayout.Button("Cancel"))
        {
            tweenAnimBehaviour.Cancel();
        }
        if (GUILayout.Button("Restore (アニメ開始位置へ)"))
        {
            // アニメ開始位置へ
            tweenAnimBehaviour.RestoreStartCondition();
        }
        if (GUILayout.Button("Revert (レイアウト初期位置へ)"))
        {
            // レイアウト初期位置へ
            tweenAnimBehaviour.Revert();
        }
        GUILayout.EndArea();
    }

    async Task Play()
    {
        await tweenAnimBehaviour.Play();
        Debug.LogWarning("TweenAnimBehaviour.Complete");
    }

}
