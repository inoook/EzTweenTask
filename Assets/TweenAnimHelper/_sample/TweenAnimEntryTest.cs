using Ez;
using System.Threading.Tasks;
using UnityEngine;

public class TweenAnimEntryTest : MonoBehaviour
{
    [SerializeField] TweenAnimEntry tweenAnimEntry = null;
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
            _ = tweenAnimEntry.PlayReverse();
        }
        if (GUILayout.Button("Cancel"))
        {
            tweenAnimEntry.Cancel();
        }
        if (GUILayout.Button("Restore (アニメ開始位置へ)"))
        {
            // アニメ開始位置へ
            tweenAnimEntry.RestoreStartCondition();
        }
        if (GUILayout.Button("Revert (レイアウト初期位置へ)"))
        {
            // レイアウト初期位置へ
            tweenAnimEntry.Revert();
        }
        GUILayout.EndArea();
    }

    async Task Play()
    {
        await tweenAnimEntry.Play();
        Debug.LogWarning("TweenAnimBehaviour.Complete");
    }
}
