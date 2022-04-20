using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Ez;
using System.Threading.Tasks;

public class EzTweenBasicSample : MonoBehaviour
{
    [SerializeField] EzEaseType ezEaseType = EzEaseType.Linear;
    [SerializeField] Transform targetTrans = null;

    [SerializeField] AnimationCurve animationCurve = null;

    CancellationTokenSource cancellationTokenSource = null;

    /// <summary>
    /// Updateを定義
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    async Task RandomTweenAct(float time = 1)
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource = null;

        cancellationTokenSource = new CancellationTokenSource();

        Vector3 to = Vector3.one * Random.Range(1f, 5f);
        Debug.LogWarning($"to: {to.x}");
        await EzTween.TweenAct(ezEaseType, targetTrans.localScale, to, time, (Vector3 v) => {
            targetTrans.localScale = v;
        }, null, cancellationTokenSource.Token);

        Debug.Log("RandomScaleTweenAct.Complete");
    }

    /// <summary>
    ///使用頻度の高いプロパティ用のショートカット。targetTrans を指定。updateの中はEzTween内に記述済み
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    async Task RandomTweenScale(float time = 1)
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource = null;

        cancellationTokenSource = new CancellationTokenSource();

        Vector3 to = Vector3.one * Random.Range(1f, 5f);
        Debug.LogWarning($"to: {to.x}");
        await EzTween.TweenScale(targetTrans, ezEaseType, to, time, cancellationTokenSource.Token);

        Debug.Log("RandomScaleTweenScale.Complete");
    }


    async Task RandomTweenScaleAnimationCurve(float time = 1)
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource = null;

        cancellationTokenSource = new CancellationTokenSource();

        Vector3 to = Vector3.one * Random.Range(1f, 5f);
        Debug.LogWarning($"to: {to.x}");
        await EzTween.TweenScale(targetTrans, animationCurve, to, time, cancellationTokenSource.Token);

        Debug.Log("RandomScaleTweenScaleAnimationCurve.Complete");
    }

    async Task Test()
    {
        Renderer renderer = targetTrans.GetComponent<Renderer>();

        Color to = Random.ColorHSV();
        float time = 1;
        await EzTween.TweenRendererColor(renderer, EzEaseType.Linear, to, time);
    }

    public void DrawGUI()
    {
        GUILayout.Label(this.GetType().Name);
        GUILayout.Label("EzTween.Tween を使用した基本的なサンプル");
        if (GUILayout.Button("RandomTweenAct"))
        {
            _ = RandomTweenAct();
        }
        if (GUILayout.Button("RandomTweenScale"))
        {
            _ = RandomTweenScale();
        }
        if (GUILayout.Button("RandomTweenScaleAnimationCurve"))
        {
            _ = RandomTweenScaleAnimationCurve();
        }

        GUILayout.Space(20);
        if (GUILayout.Button("CancelAll"))
        {
            EzTween.CancelAll();
        }
    }
}
