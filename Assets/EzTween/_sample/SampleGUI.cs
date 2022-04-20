using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleGUI : MonoBehaviour
{
    [SerializeField] EzTweenBasicSample basicSample = null;
    [SerializeField] EzTweenUseTweenSample useTweenSample = null;
    [SerializeField] EzTweenParallelSample parallelSample = null;
    [SerializeField] EzTweenChainSample chainSample = null;

    [SerializeField] Rect drawRect = new Rect(10, 10, 200, 200);
    private void OnGUI()
    {
        GUILayout.BeginArea(drawRect);
        GUILayout.Label($"TweenCount: {Ez.EzTween.TweenCount}");
        basicSample.DrawGUI();
        GUILayout.Space(20);
        useTweenSample.DrawGUI();
        GUILayout.Space(20);
        parallelSample.DrawGUI();
        GUILayout.Space(20);
        chainSample.DrawGUI();
        GUILayout.EndArea();
    }
}
