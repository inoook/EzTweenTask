using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Ez;

public class EzTweenChainSample : MonoBehaviour
{
    [SerializeField] EzEaseType ezEaseType = EzEaseType.Linear;
    [SerializeField] Transform targetTrans = null;
    [SerializeField] Renderer targetRenderer = null;

    int count = 0;
    CancellationTokenSource cancellationTokenSource = null;
    void Cancel()
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource = null;

        cancellationTokenSource = new CancellationTokenSource();
    }

    // 一つのタスクが終わって次のタスクへ
    async Task ActTask_Chain()
    {
        Cancel();

        count++;
        count %= 2;

        var token = cancellationTokenSource.Token;

        Color colorTo = (count == 0) ? Color.red : Color.green;
        await EzTween.TweenRendererColor(targetRenderer, ezEaseType, colorTo, 2, token, () => { Debug.Log("color.complete"); });
        //await ColorTask();

        Debug.LogWarning("Delay.Start");
        await EzTween.DelaySec(3, token);
        Debug.LogWarning("Delay.End");

        Vector3 posTo = (count == 0) ? new Vector3(0, 5, 0) : new Vector3(5, 0, 0);
        await EzTween.TweenLocalPosition(targetTrans, ezEaseType, posTo, 2, token, () => { Debug.Log("pos.complete"); });

        Debug.Log("ActTask_Chain.Complete");
    }
    async Task ColorTask()
    {
        Color colorTo = (count == 0) ? Color.red : Color.green;
        await EzTween.TweenRendererColor(targetRenderer, ezEaseType, colorTo, 2, cancellationTokenSource.Token, () => { Debug.Log("color.complete"); });
    }

    public void DrawGUI()
    {
        GUILayout.Label(this.GetType().Name);
        if (GUILayout.Button("ActTask_Chain"))
        {
            _ = ActTask_Chain();
        }
        if (GUILayout.Button("CancelChain"))
        {
            Cancel();
        }
    }
}
