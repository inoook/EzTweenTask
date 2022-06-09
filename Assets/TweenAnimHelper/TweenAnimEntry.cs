using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TweenAnimEntry : MonoBehaviour
{
    [Header("起動時に再生するか？")]
    [SerializeField] bool playOnStart = true;
    [Header("非表示時にアニメ再生開始状態へ移行するか？")]
    [SerializeField] bool restoreOnDisable = true;

    [SerializeField] TweenAnimChildren children = null;

    CancellationTokenSource cancellationTokenSource = null;

    private void Awake()
    {
        children.Init();
    }

    private void Start()
    {
        if (playOnStart)
        {
            _ = Play();
        }
    }

    private void OnDisable()
    {
        if (restoreOnDisable)
        {
            RestoreStartCondition();
        }
    }


    [ContextMenu("Play")]
    public async Task Play()
    {
        Cancel();

        cancellationTokenSource = new CancellationTokenSource();
        await children.PlayChildren(true, cancellationTokenSource);

        Debug.LogWarning($"TweenAnimEntry.PlayForward.Complete: [{this.gameObject.name}]");
    }
    [ContextMenu("PlayReverse")]
    public async Task PlayReverse()
    {
        Cancel();

        cancellationTokenSource = new CancellationTokenSource();
        await children.PlayChildren(false, cancellationTokenSource);

        Debug.LogWarning($"TweenAnimEntry.PlayReverse.Complete: [{this.gameObject.name}]");
    }

    public void Cancel()
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource = null;
    }

    /// <summary>
    /// tempsで設定した初期状態へ戻す
    /// </summary>
    [ContextMenu("Restore")]
    public void RestoreStartCondition()
    {
        if (!Application.isPlaying) { return; }

        Cancel();

        children.RestoreStartCondition();
    }

    [ContextMenu("Revert")]
    public void Revert()
    {
        if (!Application.isPlaying) { return; }

        Cancel();

        children.Revert();
    }
}
