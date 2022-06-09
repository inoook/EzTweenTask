using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// インスペクターでEzTweenの設定を行う
/// </summary>
[AddComponentMenu("AnimationHelper/TweenAnimBehaviour")]
public class TweenAnimBehaviour : MonoBehaviour
{
    /// <summary>
    /// POSITION はLocalPositionを指定
    /// 
    /// POSITION_RELATIVE はLocalPositionを今の位置からどれくらい動かすかの指定
    /// POSITION_RELATIVE はtempsもtweensもレイアウトでの初期位置からどれくらい動かすかを指定
    /// 
    /// Gizomoでは POSITION, POSITION_RELATIVE のときに
    /// tempsで指定した位置を赤
    /// tweensで指定した位置を青
    /// で表示している。
    /// 
    /// TODO: アニメ開始位置へ戻るtween実装
    /// </summary>

    [System.Serializable]
    public class TweenGroup
    {
        [Header("上から順番に再生")]
        public TweenObj[] chains = default;
        [Header("同時に再生開始")]
        public TweenObj[] parallels = default;

        // アニメ開始位置の保存
        public void CacheFromCondition()
        {
            foreach (var t in chains)
            {
                t.CacheFromCondition();
            }
            foreach (var t in parallels)
            {
                t.CacheFromCondition();
            }
        }
        /// <summary>
        /// 初期位置の保存
        /// </summary>
        public void Cache()
        {
            foreach (var t in chains)
            {
                t.Cache();
            }
            foreach (var t in parallels)
            {
                t.Cache();
            }
        }

        public void Revert()
        {
            foreach (var t in chains)
            {
                t.Revert();
            }
            foreach (var t in parallels)
            {
                t.Revert();
            }
        }

        public void DrawGizmos()
        {
            foreach (var t in chains)
            {
                t.DrawGizmos();
            }
            foreach (var t in parallels)
            {
                t.DrawGizmos();
            }
        }

        public async Task ActForward(bool isForward, CancellationTokenSource cancellationTokenSource)
        {
            Task chainsTask = DoChains(isForward, cancellationTokenSource);
            Task parallelsTask = DoParallels(isForward, cancellationTokenSource);

            await Task.WhenAll(chainsTask, parallelsTask);
            
            Debug.LogWarning($"TweenGroup.ActForward.Complete");
        }

        async Task DoChains(bool isForward, CancellationTokenSource cancellationTokenSource)
        {
            for(int i = 0; i < chains.Length; i++)
            {
                int index = isForward ? i : (chains.Length - 1 - i);
                var t = chains[index];
                if (t.enable)
                {
                    cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    await t.Act(isForward, cancellationTokenSource);
                }
            }
        }

        async Task DoParallels(bool isForward, CancellationTokenSource cancellationTokenSource)
        {
            List<Task> taskList = new List<Task>();
            for (int i = 0; i < parallels.Length; i++)
            {
                int index = isForward ? i : (parallels.Length - 1 - i);
                var t = parallels[index];
                if (t.enable)
                {
                    taskList.Add(t.Act(isForward, cancellationTokenSource));
                }
            }
            await Task.WhenAll(taskList);
        }
    }

    // ----------
    [Header("起動時に再生するか？")]
    [SerializeField] bool playOnStart = true;
    [Header("非表示時にアニメ再生開始状態へ移行するか？")]
    [SerializeField] bool restoreOnDisable = true;

    [Header("アニメーション再生開始状態定義")]
    [SerializeField] TemporaryCondition[] temps = default;

    [Header("アニメーション（再生中はグリーンで表示）")]
    [SerializeField] TweenGroup group = default;

    // -----
    [Header("Children : 上のアニメ全て再生後、再生開始する")]
    [SerializeField] TweenAnimChildren children = null;

    /// <summary>
    /// 初期位置へ移動
    /// </summary>
    void SetTemporary()
    {
        foreach(var t in temps)
        {
            t.Act();
        }
    }

    private bool isInit = false;

    public void Init()
    {
        if (!isInit)
        {
            // 初期レイアウトを保存
            group.Cache();

            // temps で指定したアニメ開始状態にする
            SetTemporary();
            // アニメ開始状態を保存
            group.CacheFromCondition();

            //
            children.Init();
            //
            isInit = true;
        }
    }

    // Start is called before the first frame update
    // 1st
    void Awake()
    {
        Init();
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
        Cancel();

        if (restoreOnDisable)
        {
            RestoreStartCondition();
        }
    }

    [HideInInspector]
    bool isPlaying = false;
    public bool IsPlaying => isPlaying;

    public async Task PlayForward(bool isForward, CancellationTokenSource cancellationTokenSource)
    {
        if (!Application.isPlaying) { return; }

        isPlaying = true;

        await group.ActForward(isForward, cancellationTokenSource);

        if (children != null)
        {
            await children.PlayChildren(isForward, cancellationTokenSource);
        }

        isPlaying = false;
        Debug.LogWarning($"TweenAnimBehaviour.PlayForward.Complete: [{this.gameObject.name}]");
    }


    CancellationTokenSource cancellationTokenSource = null;

    [ContextMenu("Play")]
    public async Task Play()
    {
        Cancel();

        cancellationTokenSource = new CancellationTokenSource();
        await PlayForward(true, cancellationTokenSource);
    }
    [ContextMenu("PlayReverse")]
    public async Task PlayReverse()
    {
        Cancel();

        cancellationTokenSource = new CancellationTokenSource();
        await PlayForward(false, cancellationTokenSource);
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

        foreach (var t in temps)
        {
            t.Restore();
        }

        // -----
        children?.RestoreStartCondition();
    }

    [ContextMenu("Revert")]
    public void Revert()
    {
        if (!Application.isPlaying) { return; }

        Cancel();

        group.Revert();

        // -----
        children?.Revert();
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying) { return; }

        if(temps == null) { return; }

        //
        foreach (var t in temps)
        {
            t.DrawGizmos();
        }
        group.DrawGizmos();
    }

}
