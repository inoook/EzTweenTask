using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Ez;

/// <summary>
/// Tween tween = new Tween(); を使用したサンプル
/// tween?.Cancel(); でキャンセルできるのが利点
/// </summary>
public class EzTweenUseTweenSample : MonoBehaviour
{
    [SerializeField] EzEaseType ezEaseType = EzEaseType.Linear;
    [SerializeField] Transform targetTrans = null;
    [SerializeField] Renderer targetRenderer = null;
    [SerializeField] MaskableGraphic uGUI = null;
    [SerializeField] CanvasGroup canvasGroup = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            EzTween.CancelAll();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = null;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            positionTween?.Cancel();
        }
    }

    // ----------
    Tween positionTween = new Tween();
    int count = 0;
    async Task ActTask_StartTweenLocalPosition(float time = 3)
    {
        Debug.LogWarning("ActTask_StartTweenLocalPosition");
        positionTween?.Cancel();

        count++;
        count %= 2;
        Vector3 to = (count == 0) ? new Vector3(0,5,0) : new Vector3(5,0,0);
        await positionTween.StartTweenLocalPosition(targetTrans, ezEaseType, to, time);
        Debug.LogWarning("Position.Complete"); // awaitを使用しているためtween終了時に実行される。positionTween.Cancelしても実行される。
    }

    CancellationTokenSource cancellationTokenSource = null;
    async Task ActTask_StartTweenLocalPositionManualCancel(float time = 2)
    {
        Debug.LogWarning("ActTask_StartTweenLocalPositionManualCancel");
        cancellationTokenSource?.Cancel();
        cancellationTokenSource = null;

        if (cancellationTokenSource == null)
        {
            cancellationTokenSource = new CancellationTokenSource();
        }

        count++;
        count %= 2;
        Vector3 to = (count == 0) ? new Vector3(0, 5, 0) : new Vector3(5, 0, 0);
        await positionTween.StartTweenLocalPosition(targetTrans, ezEaseType, to, time, cancellationTokenSource.Token);
        Debug.LogWarning("PositionManualCancel.Complete"); // awaitを使用しているためtween終了時に実行される。positionTween.Cancelしても実行される。
    }

    void Act_StartTweenLocalPosition(float time = 2)
    {
        positionTween?.Cancel();
        Vector3 to = Vector3.one * Random.Range(1f, 5f);
        _ = positionTween.StartTweenLocalPosition(targetTrans, ezEaseType, to, time, default, () => {
            Debug.LogWarning("complete");
        });
        Debug.LogWarning("Act_StartTweenLocalPosition.Complete");// StartTweenLocalPosition実行時に実行される
    }

    // ----------
    Tween rotationlTween = new Tween();
    async Task ActTask_StartTweenRotation(float time = 2)
    {
        rotationlTween?.Cancel();
        Quaternion to = Quaternion.AngleAxis(90, Vector3.up) * targetTrans.rotation;// 実行時の姿勢からY軸90度回転
        await rotationlTween.StartTweenRotation(targetTrans, ezEaseType, to, time, default, () => {
            Debug.LogWarning("complete");
        });
        Debug.LogWarning("Rotation.Complete");
    }

    // ----------
    Tween scaleTween = new Tween();
    async Task ActTask_StartTweenScale(float time = 2)
    {
        scaleTween?.Cancel();
        Vector3 to = Vector3.one * Random.Range(1f, 5f);
        await scaleTween.StartTweenScale(targetTrans, ezEaseType, to, time, default, () => {
            Debug.LogWarning("complete");
        });
        Debug.LogWarning("Scale.Complete");
    }

    // ----------
    Tween colorTween = new Tween();
    int count_color = 0;
    async Task ActTask_StartTweenRendererColor(float time = 2)
    {
        colorTween?.Cancel();
        //Color to = Random.ColorHSV();
        count_color++;
        count_color %= 2;
        Color to = (count_color == 0) ? Color.red : Color.green;
        await colorTween.StartTweenRendererColor(targetRenderer, ezEaseType, to, time);
        Debug.LogWarning("Color.Complete");
    }

    // ----------
    Tween materialTween = new Tween();
    async Task ActTask_StartTweenMaterial(float time = 2)
    {
        materialTween?.Cancel();
        Color to = Random.ColorHSV();
        Material mat = targetRenderer.material;
        await materialTween.StartTweenMaterial(mat, "_Color", ezEaseType, to, time, default, () => {
            Debug.LogWarning("complete");
        });
        Debug.LogWarning("Material.Complete");
    }
    // ----------
    Tween uGuiTween = new Tween();
    async Task ActTask_StartTweenUiColor(float time = 2)
    {
        uGuiTween?.Cancel();
        Color to = Random.ColorHSV();
        await uGuiTween.StartTweenUiColor(uGUI, ezEaseType, to, time, default, () => {
            Debug.LogWarning("complete");
        });
        Debug.LogWarning("UiColor.Complete");
    }
    // ----------
    Tween canvasGroupTween = new Tween();
    async Task ActTask_StartTweenCanvasGroup(float time = 2)
    {
        canvasGroupTween?.Cancel();
        float to = canvasGroup.alpha > 0.5f  ? 0 : 1 ;
        await canvasGroupTween.StartTweenCanvasGroupAlpha(canvasGroup, ezEaseType, to, time, default, () => {
            Debug.LogWarning("complete");
        });
        Debug.LogWarning("CanvasGroup.Complete");
    }

  
    public void DrawGUI()
    {
        GUILayout.Label(this.GetType().Name);
        GUILayout.Label("tween を使用したサンプル");
        if (GUILayout.Button("ActTask_StartTweenLocalPosition"))
        {
            _ = ActTask_StartTweenLocalPosition();
        }
        if (GUILayout.Button("ActTask_StartTweenLocalPositionManualCancel"))
        {
            _ = ActTask_StartTweenLocalPositionManualCancel();
        }

        if (GUILayout.Button("Act_StartTweenLocalPosition"))
        {
            Act_StartTweenLocalPosition();
        }
        if (GUILayout.Button("positionTween.Cancel"))
        {
            positionTween?.Cancel();
        }

        GUILayout.Space(20);
        if (GUILayout.Button("ActTask_StartTweenRotation"))
        {
            _ = ActTask_StartTweenRotation();
        }
        GUILayout.Space(20);
        if (GUILayout.Button("ActTask_StartTweenScale"))
        {
            _ = ActTask_StartTweenScale();
        }

        GUILayout.Space(20);
        if (GUILayout.Button("ActTask_StartTweenRendererColor"))
        {
            _ = ActTask_StartTweenRendererColor();
        }
        if (GUILayout.Button("ActTask_StartTweenMaterial"))
        {
            _ = ActTask_StartTweenMaterial();
        }
        GUILayout.Space(20);
        if (GUILayout.Button("ActTask_StartTweenUiColor"))
        {
            _ = ActTask_StartTweenUiColor();
        }
        if (GUILayout.Button("ActTask_StartTweenCanvasGroup"))
        {
            _ = ActTask_StartTweenCanvasGroup();
        }

    }
}
