using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Ez;

public class EzTweenParallelSample : MonoBehaviour
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

    // タスクを平行して実行
    async Task ActTask_Parallel()
    {
        Cancel(); // 実行中をキャンセル

        count++;
        count %= 2;

        var token = cancellationTokenSource.Token;

        Color colorTo = (count == 0) ? Color.red : Color.green;
        Task task1 = EzTween.TweenRendererColor(targetRenderer, ezEaseType, colorTo, 2, token, () => { Debug.Log("color.complete"); });

        Vector3 posTo = (count == 0) ? new Vector3(0, 5, 0) : new Vector3(5, 0, 0);
        Task task2 = EzTween.TweenLocalPosition(targetTrans, ezEaseType, posTo, 3, token, () => { Debug.Log("pos.complete"); });

        //await Task.WhenAny(task1, task2); // どれかがが終了
        await Task.WhenAll(task1, task2); // 全て終了

        Debug.Log("ActTask_Parallel.Complete");
    }

    public void DrawGUI()
    {
        GUILayout.Label(this.GetType().Name);
        if (GUILayout.Button("ActTask_Parallel"))
        {
            _ = ActTask_Parallel();
        }
        if (GUILayout.Button("CancelParallel"))
        {
            Cancel();
        }
    }
}
