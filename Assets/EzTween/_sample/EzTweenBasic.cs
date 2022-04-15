using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Ez;

public class EzTweenBasic : MonoBehaviour
{
    [SerializeField] EzEaseType ezEaseType = EzEaseType.Linear;
    [SerializeField] Transform targetTrans = null;
    [SerializeField] Renderer targetRenderer = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            EzTween.CancelAll();
        }
    }

    // ----------
    Tween scaleTween = new Tween();
    async Task ActTask_StartTweenLocalPosition(float time = 2)
    {
        scaleTween?.Cancel();
        Vector3 to = Vector3.one * Random.Range(1f, 5f);
        await scaleTween.StartTweenLocalPosition(targetTrans, ezEaseType, to, time);
        Debug.LogWarning("ActTask_StartTweenLocalPosition.Complete"); // awaitを使用しているためtween終了時に実行される。scaleTween.Cancelしても実行される。
    }

    void Act_StartTweenLocalPosition(float time = 2)
    {
        scaleTween?.Cancel();
        Vector3 to = Vector3.one * Random.Range(1f, 5f);
        _ = scaleTween.StartTweenLocalPosition(targetTrans, ezEaseType, to, time, () => {
            Debug.LogWarning("complete");
        });
        Debug.LogWarning("Act_StartTweenLocalPosition.Complete");// tween実行時に実行される
    }
    // ----------

    Tween colorTween = new Tween();

    async Task ActTask_StartTweenRendererColor(float time = 2)
    {
        colorTween?.Cancel();
        Color to = Random.ColorHSV();
        await colorTween.StartTweenRendererColor(targetRenderer, ezEaseType, to, time, () => {
            Debug.LogWarning("complete");
        });
        Debug.LogWarning("ActTask_StartTweenRendererColor.Complete");// tween実行時に実行される
    }

    private void OnDestroy()
    {
        //EzTween.CancelAll();
    }

    [SerializeField] Rect drawRect = new Rect(10, 10, 200, 200);
    private void OnGUI()
    {
        GUILayout.BeginArea(drawRect);
        GUILayout.Label("実行方法サンプル");
        if (GUILayout.Button("ActTask_StartTweenLocalPosition"))
        {
            _ = ActTask_StartTweenLocalPosition();
        }
        if (GUILayout.Button("Act_StartTweenLocalPosition"))
        {
            Act_StartTweenLocalPosition();
        }
        if (GUILayout.Button("scaleTween.Cancel"))
        {
            scaleTween?.Cancel();
        }

        GUILayout.Space(20);
        if (GUILayout.Button("ActTask_StartTweenRendererColor"))
        {
            _ = ActTask_StartTweenRendererColor();
        }
        GUILayout.Space(20);

        if (GUILayout.Button("CancelAll"))
        {
            EzTween.CancelAll();
        }

        GUILayout.EndArea();
    }
}
