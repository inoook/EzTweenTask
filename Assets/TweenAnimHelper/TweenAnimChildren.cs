using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class TweenAnimChildren
{
    //[Header("Children 上のアニメ全て再生後、再生開始する")]
    [SerializeField] TweenAnimBehaviour[] chains = null;
    [SerializeField] TweenAnimBehaviour[] parallels = null;

    [SerializeField] bool isPlayingChains = false;
    [SerializeField] bool isPlayingParallels = false;

    public bool IsPlayingChains => isPlayingChains;
    public bool IsPlayingParallels => isPlayingParallels;

    public async Task PlayChildren(bool isForward, CancellationTokenSource cancellationTokenSource)
    {
        Task chainsTask = PlayChildrenChains(isForward, cancellationTokenSource);
        Task parallelsTask = PlayChildrenParallels(isForward, cancellationTokenSource);

        await Task.WhenAll(chainsTask, parallelsTask);
    }
    public async Task PlayChildrenChains(bool isForward, CancellationTokenSource cancellationTokenSource)
    {
        isPlayingChains = true;
        for (int i = 0; i < chains.Length; i++)
        {
            int index = isForward ? i : (chains.Length - 1 - i);
            var t = chains[index];
            cancellationTokenSource.Token.ThrowIfCancellationRequested();
            await t.PlayForward(isForward, cancellationTokenSource);
        }

        isPlayingChains = false;
    }
    public async Task PlayChildrenParallels(bool isForward, CancellationTokenSource cancellationTokenSource)
    {
        isPlayingParallels = true;
        List<Task> taskList = new List<Task>();
        for (int i = 0; i < parallels.Length; i++)
        {
            int index = isForward ? i : (parallels.Length - 1 - i);
            var t = parallels[index];
            taskList.Add(t.PlayForward(isForward, cancellationTokenSource));
        }
        await Task.WhenAll(taskList);

        isPlayingParallels = false;
    }

    public void Init()
    {
        foreach (var c in chains)
        {
            c.Init();
        }
        foreach (var c in parallels)
        {
            c.Init();
        }
    }

    public void Revert()
    {
        foreach (var c in chains)
        {
            c.Revert();
        }
        foreach (var c in parallels)
        {
            c.Revert();
        }
    }

    public void RestoreStartCondition()
    {
        foreach (var c in chains)
        {
            c.RestoreStartCondition();
        }
        foreach (var c in parallels)
        {
            c.RestoreStartCondition();
        }
    }
}
