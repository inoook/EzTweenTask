# EzTweenTask
Simple tween for Unity using async/await

```cs
float time = Random.Range(0.5f, 2f);
Vector3 to = Random.insideUnitSphere * Random.Range(0, 5f);
await EzTween.TweenLocalPosition(targetTrans, EzEaseType.Linear, to, time);
```
```cs
Color to = Random.ColorHSV();
await EzTween.TweenRendererColor(renderer, EzEaseType.Linear, to, time);
```

## Cancel
```cs
// cancel
cancellationTokenSource?.Cancel();
cancellationTokenSource = null;

// Play
cancellationTokenSource = new CancellationTokenSource();

Vector3 to = Vector3.one * Random.Range(1f, 5f);
await EzTween.TweenScale(targetTrans, animationCurve, to, time, cancellationTokenSource.Token);
```

## Chain
```cs
async Task Chain()
{
 var token = cancellationTokenSource.Token;

 Color colorTo = Random.ColorHSV();
 await EzTween.TweenRendererColor(targetRenderer, ezEaseType, colorTo, 2, token, () => { Debug.Log("color.complete"); });

 await EzTween.DelaySec(3, token);

 Vector3 posTo = Vector3.one * Random.Range(1f, 5f);
 await EzTween.TweenLocalPosition(targetTrans, ezEaseType, posTo, 2, token, () => { Debug.Log("pos.complete"); });
 
 Debug.Log("Chain.Complete");
}
```

## Parallel
```cs
async Parallel()
{
 var token = cancellationTokenSource.Token;

 Color colorTo = Random.ColorHSV();
 Task task1 = EzTween.TweenRendererColor(targetRenderer, ezEaseType, colorTo, 2, token, () => { Debug.Log("color.complete"); });

 Vector3 posTo = Vector3.one * Random.Range(1f, 5f);
 Task task2 = EzTween.TweenLocalPosition(targetTrans, ezEaseType, posTo, 3, token, () => { Debug.Log("pos.complete"); });

 await Task.WhenAll(task1, task2); // 全て終了

 Debug.Log("Parallel.Complete");
}
```


## EzTween
https://github.com/inoook/EzTween
