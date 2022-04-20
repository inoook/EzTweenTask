using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Ez
{
    public enum EzEaseType
    {
        Linear,
        SineIn, SineOut, SineInOut,
        QuadIn, QuadOut, QuadInOut,
        CubicIn, CubicOut, CubicInOut,
        QuartIn, QuartOut, QuartInOut,
        ExpIn, ExpOut, ExpInOut,
        CircIn, CircOut, CircInOut,
        ElasticIn, ElasticOut, ElasticInOut,
        BackIn, BackOut, BackInOut,
        BounceIn, BounceOut, BounceInOut,
        AnimationCurve
    }

    public class Tween
    {
        // # Example
        // Tween 内で cancellationTokenSrc を生成するので、
        // Cancelするときは tween?.Cancel(); を使用する。

        //EzTween.Tween tween = null;
        //async Task Act_TweenRun()
        //{
        //    tween?.Cancel();
        //    if (tween == null)
        //    {
        //        tween = new EzTween.Tween();
        //    }
        //    float to = Random.Range(-5f, 5f);
        //    await tween.Start(ezEaseType, targetTrans.localPosition.x, to, 1, (v) =>
        //    {
        //        targetTrans.localPosition = new Vector3(v, 0, 0);
        //    });
        //    Debug.Log("Complete_Act_RandomPosition");
        //}

        CancellationTokenSource cancellationTokenSrc = null;

        public Tween() { }

        public CancellationTokenSource GetCancellationTokenSource(CancellationToken cancellationToken)
        {
            if (cancellationTokenSrc == null)
            {
                cancellationTokenSrc = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            }
            else
            {
                if (cancellationTokenSrc.IsCancellationRequested)
                {
                    cancellationTokenSrc = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                }
            }
            return cancellationTokenSrc;
        }

        // 旧バージョンの関数名は Run にしていたが StartTween へ関数名変更
        public async Task StartTween(EzEaseType easeType, float from, float to, float time, Action<float> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            cancellationTokenSrc = GetCancellationTokenSource(cancellationToken);
            await EzTween.TweenAct(easeType, from, to, time, updateAction, completeAction, cancellationTokenSrc.Token);
            //if (cancellationTokenSrc.IsCancellationRequested)
            //{
            //    cancellationTokenSrc = null;
            //}
        }
        public async Task StartTween(EzEaseType easeType, Vector3 from, Vector3 to, float time, Action<Vector3> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            cancellationTokenSrc = GetCancellationTokenSource(cancellationToken);
            await EzTween.TweenAct(easeType, from, to, time, updateAction, completeAction, cancellationTokenSrc.Token);
            //if (cancellationTokenSrc.IsCancellationRequested)
            //{
            //    cancellationTokenSrc = null;
            //}
        }
        public async Task StartTween(EzEaseType easeType, Vector2 from, Vector2 to, float time, Action<Vector2> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            cancellationTokenSrc = GetCancellationTokenSource(cancellationToken);
            await EzTween.TweenAct(easeType, from, to, time, updateAction, completeAction, cancellationTokenSrc.Token);
            //if (cancellationTokenSrc.IsCancellationRequested)
            //{
            //    cancellationTokenSrc = null;
            //}
        }
        public async Task StartTween(EzEaseType easeType, Color from, Color to, float time, Action<Color> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            cancellationTokenSrc = GetCancellationTokenSource(cancellationToken);
            await EzTween.TweenAct(easeType, from, to, time, updateAction, completeAction, cancellationTokenSrc.Token);
            //if (cancellationTokenSrc.IsCancellationRequested)
            //{
            //    cancellationTokenSrc = null;
            //}
        }
        public async Task StartTween(EzEaseType easeType, Quaternion from, Quaternion to, float time, Action<Quaternion> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            cancellationTokenSrc = GetCancellationTokenSource(cancellationToken);
            await EzTween.TweenAct(easeType, from, to, time, updateAction, completeAction, cancellationTokenSrc.Token);
            //if (cancellationTokenSrc.IsCancellationRequested)
            //{
            //    cancellationTokenSrc = null;
            //}
        }

        public void Cancel()
        {
            cancellationTokenSrc?.Cancel();
            cancellationTokenSrc?.Dispose();

            cancellationTokenSrc = null;
        }

        // 使いやすくするためのショートカット
        public async Task StartTweenPosition(Transform transform, EzEaseType easeType, Vector3 to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            Vector3 from = transform.position;
            await StartTween(easeType, from, to, time, (v) =>
            {
                transform.position = v;
            }, completeAction, cancellationToken);
        }
        public async Task StartTweenLocalPosition(Transform transform, EzEaseType easeType, Vector3 to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            Vector3 from = transform.localPosition;
            await StartTween(easeType, from, to, time, (v) =>
            {
                transform.localPosition = v;
            }, completeAction, cancellationToken);
        }
        public async Task StartTweenEulerAngles(Transform transform, EzEaseType easeType, Vector3 to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            Vector3 from = transform.position;
            await StartTween(easeType, from, to, time, (v) =>
            {
                transform.eulerAngles = v;
            }, completeAction, cancellationToken);
        }
        public async Task StartTweenLocalEulerAngles(Transform transform, EzEaseType easeType, Vector3 to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            Vector3 from = transform.position;
            await StartTween(easeType, from, to, time, (v) =>
            {
                transform.localEulerAngles = v;
            }, completeAction, cancellationToken);
        }
        public async Task StartTweenRotation(Transform transform, EzEaseType easeType, Quaternion to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            Quaternion from = transform.rotation;
            await StartTween(easeType, from, to, time, (v) =>
            {
                transform.rotation = v;
            }, completeAction, cancellationToken);
        }
        public async Task StartTweenLocalRotation(Transform transform, EzEaseType easeType, Quaternion to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            Quaternion from = transform.localRotation;
            await StartTween(easeType, from, to, time, (v) =>
            {
                transform.localRotation = v;
            }, completeAction, cancellationToken);
        }
        public async Task StartTweenScale(Transform transform, EzEaseType easeType, Vector3 to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            Vector3 from = transform.localScale;
            await StartTween(easeType, from, to, time, (v) =>
            {
                transform.localScale = v;
            }, completeAction, cancellationToken);
        }
        // renderer
        public async Task StartTweenRendererColor(Renderer renderer, EzEaseType easeType, Color to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            Color from = renderer.material.color;
            await StartTween(easeType, from, to, time, (v) =>
            {
                renderer.material.color = v;
            }, completeAction, cancellationToken);
        }
        // material
        public async Task StartTweenMaterial(Material material, string paramName, EzEaseType easeType, float to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            float from = material.GetFloat(paramName);
            await StartTween(easeType, from, to, time, (v) =>
            {
                material.SetFloat(paramName, v);
            }, completeAction, cancellationToken);
        }
        public async Task StartTweenMaterial(Material material, string paramName, EzEaseType easeType, Color to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            Color from = material.GetColor(paramName);
            await StartTween(easeType, from, to, time, (v) =>
            {
                material.SetColor(paramName, v);
            }, completeAction, cancellationToken);
        }
        // ui
        public async Task StartTweenUiColor(MaskableGraphic uiElement, EzEaseType easeType, Color to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            Color from = uiElement.color;
            await StartTween(easeType, from, to, time, (v) =>
            {
                uiElement.color = v;
            }, completeAction, cancellationToken);
        }
        public async Task StartTweenCanvasGroupAlpha(CanvasGroup canvasGroup, EzEaseType easeType, float to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            float from = canvasGroup.alpha;
            await StartTween(easeType, from, to, time, (v) =>
            {
                canvasGroup.alpha = v;
            }, completeAction, cancellationToken);
        }
    }

    /// <summary>
    /// 終了時に全てのtweenのCancelを行う
    /// </summary>
    public class EzTweenBehav : MonoBehaviour
    {
        static EzTweenBehav instance;
        static GameObject go;
        static public EzTweenBehav Instance
        {
            get {
                if (!Application.isPlaying)
                {
                    return null;
                }
                 if (instance == null)
                {
                    go = new GameObject();
                    instance = go.AddComponent<EzTweenBehav>();
                    go.name = "EzTween";
                    DontDestroyOnLoad(go);
                }
                return instance;
            }
        }

        CancellationTokenSource destoryCancellationTokenSource = null; // App終了時にCancel実行。全てのtweenにこのtokenを設定している。

        public void Init()
        {
            if(destoryCancellationTokenSource == null)
            {
                destoryCancellationTokenSource = new CancellationTokenSource();
            }
        }

        public CancellationToken DestoryCancellationToken => destoryCancellationTokenSource.Token;

        public void CancelAllTween()
        {
            destoryCancellationTokenSource?.Cancel();
            destoryCancellationTokenSource?.Dispose();

            destoryCancellationTokenSource = new CancellationTokenSource();
        }

        void Update()
        {
            this.gameObject.name = "EzTween (" + EzTween.TweenCount + ")";
        }

        void OnApplicationQuit()
        {
            EzTween.CancelAll();

            destoryCancellationTokenSource?.Cancel();
            destoryCancellationTokenSource?.Dispose();
            destoryCancellationTokenSource = null;
        }
    }

    public class EzTween
    {
        #region tween
        static List<CancellationTokenSource> tweenList = new List<CancellationTokenSource>();
        public static int TweenCount => tweenList.Count;

        /// <summary>
        /// 実行中※のtweenのキャンセル。Chainなどで未実行のtweenはキャンセルされない。
        /// </summary>
        public static void CancelAll()
        {
            EzTweenBehav.Instance.CancelAllTween();

            // ---
            //Debug.LogWarning($"CancelAll: {tweenList.Count}");
            // Note: foreach を使うと、DelayにTokenを渡しているとエラーが発生するときがある
            // for であればエラーは起きない
            // Delay にToken 渡さないでforeachが安全そう？

            //foreach (var m in tweenList)
            //{
            //    m.Cancel();
            //}

            //Debug.LogWarning($"[CancelAll] tweenList.Count: {tweenList.Count}");
            //for (int i = tweenList.Count-1; i >= 0; i--)
            //{
            //    var m = tweenList[i];
            //    m.Cancel();
            //}
            //tweenList.Clear();
        }

        // ----------
        // easeType tween
        // それぞれの型でのショートカット
        // EzEaseType / float
        public static async Task TweenAct(EzEaseType easeType, float from, float to, float time, Action<float> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            await TweenBaseAct(from, to, time, updateAction, completeAction, easeType, null, cancellationToken);
        }
        // EzEaseType / Vecotr3
        public static async Task TweenAct(EzEaseType easeType, Vector3 from, Vector3 to, float time, Action<Vector3> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Vector3 p = Vector3.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, easeType, null, cancellationToken);
        }
        // EzEaseType / Vector2
        public static async Task TweenAct(EzEaseType easeType, Vector2 from, Vector2 to, float time, Action<Vector2> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Vector2 p = Vector2.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, easeType, null, cancellationToken);
        }
        // EzEaseType / Color
        public static async Task TweenAct(EzEaseType easeType, Color from, Color to, float time, Action<Color> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Color p = Color.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, easeType, null, cancellationToken);
        }
        // EzEaseType / Color32
        public static async Task TweenAct(EzEaseType easeType, Color32 from, Color32 to, float time, Action<Color32> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Color32 p = Color32.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, easeType, null, cancellationToken);
        }
        // EzEaseType / Quaternion
        public static async Task TweenAct(EzEaseType easeType, Quaternion from, Quaternion to, float time, Action<Quaternion> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Quaternion p = Quaternion.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, easeType, null, cancellationToken);
        }
        //
        // AnimationCurve tween
        // Tween をアニメーションカーブで定義
        // float
        public static async Task TweenAct(AnimationCurve animationCurve, float from, float to, float time, Action<float> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            await TweenBaseAct(from, to, time, updateAction, completeAction, EzEaseType.AnimationCurve, animationCurve, cancellationToken);
        }
        // AnimationCurve / Vector3
        public static async Task TweenAct(AnimationCurve animationCurve, Vector3 from, Vector3 to, float time, Action<Vector3> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Vector3 p = Vector3.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, EzEaseType.AnimationCurve, animationCurve, cancellationToken);
        }
        //  AnimationCurve / Vector2
        public static async Task TweenAct(AnimationCurve animationCurve, Vector2 from, Vector2 to, float time, Action<Vector2> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Vector2 p = Vector2.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, EzEaseType.AnimationCurve, animationCurve, cancellationToken);
        }
        //  AnimationCurve / Color
        public static async Task TweenAct(AnimationCurve animationCurve, Color from, Color to, float time, Action<Color> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Color p = Color.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, EzEaseType.AnimationCurve, animationCurve, cancellationToken);
        }
        //  AnimationCurve / Color32
        public static async Task TweenAct(AnimationCurve animationCurve, Color32 from, Color32 to, float time, Action<Color32> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Color32 p = Color32.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, EzEaseType.AnimationCurve, animationCurve, cancellationToken);
        }
        //  AnimationCurve / Quaternion
        public static async Task TweenAct(AnimationCurve animationCurve, Quaternion from, Quaternion to, float time, Action<Quaternion> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Quaternion p = Quaternion.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, EzEaseType.AnimationCurve, animationCurve, cancellationToken);
        }
        #endregion

        static async Task TweenBaseAct(float from, float to, float time, Action<float> updateAction, Action completeAction = null, EzEaseType easeType = EzEaseType.Linear, AnimationCurve curve = null, CancellationToken token = default)
        {
            await TweenAsync(from, to, time, updateAction, completeAction, easeType, curve, token);
        }

        public static Func<float, float> GetEaseAction(EzEaseType type)
        {
            switch (type)
            {
                case EzEaseType.SineIn: return (v) => Easing.SineIn(v);
                case EzEaseType.SineOut: return (v) => Easing.SineOut(v);
                case EzEaseType.SineInOut: return (v) => Easing.SineInOut(v);
                case EzEaseType.QuadIn: return (v) => Easing.QuadIn(v);
                case EzEaseType.QuadOut: return (v) => Easing.QuadOut(v);
                case EzEaseType.QuadInOut: return (v) => Easing.QuadInOut(v);
                case EzEaseType.CubicIn: return (v) => Easing.CubicIn(v);
                case EzEaseType.CubicOut: return (v) => Easing.CubicOut(v);
                case EzEaseType.CubicInOut: return (v) => Easing.CubicInOut(v);
                case EzEaseType.QuartIn: return (v) => Easing.QuartIn(v);
                case EzEaseType.QuartOut: return (v) => Easing.QuartOut(v);
                case EzEaseType.QuartInOut: return (v) => Easing.QuartInOut(v);
                case EzEaseType.ExpIn: return (v) => Easing.ExpIn(v);
                case EzEaseType.ExpOut: return (v) => Easing.ExpOut(v);
                case EzEaseType.ExpInOut: return (v) => Easing.ExpInOut(v);
                case EzEaseType.CircIn: return (v) => Easing.CircIn(v);
                case EzEaseType.CircOut: return (v) => Easing.CircOut(v);
                case EzEaseType.CircInOut: return (v) => Easing.CircInOut(v);
                case EzEaseType.ElasticIn: return (v) => Easing.ElasticIn(v);
                case EzEaseType.ElasticOut: return (v) => Easing.ElasticOut(v);
                case EzEaseType.ElasticInOut: return (v) => Easing.ElasticInOut(v);
                case EzEaseType.BackIn: return (v) => Easing.BackIn(v);
                case EzEaseType.BackOut: return (v) => Easing.BackOut(v);
                case EzEaseType.BackInOut: return (v) => Easing.BackInOut(v);
                case EzEaseType.BounceIn: return (v) => Easing.BounceIn(v);
                case EzEaseType.BounceOut: return (v) => Easing.BounceOut(v);
                case EzEaseType.BounceInOut: return (v) => Easing.BounceInOut(v);
                case EzEaseType.Linear: return (v) => Easing.Linear(v);
                default: return (v) => Easing.Linear(v);
            }
        }

        static void AddTween(CancellationTokenSource cancellationTokenSource)
        {
            if (!tweenList.Contains(cancellationTokenSource))
            {
                tweenList.Add(cancellationTokenSource);
            }
        }
        static void RemoveTween(CancellationTokenSource cancellationTokenSource)
        {
            if (tweenList.Contains(cancellationTokenSource))
            {
                tweenList.Remove(cancellationTokenSource);
            }
        }

        #region tween
        /// <summary>
        /// Tween の基幹コード
        /// CancellationToken を使用し、キャンセルをコントロールする場合はこれを使う
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="time"></param>
        /// <param name="updateAction"></param>
        /// <param name="completeAction"></param>
        /// <param name="easeType"></param>
        /// <param name="curve"></param>
        /// <param name="cancellToken"></param>
        /// <returns></returns>
        static async Task TweenAsync(float from, float to, float time, Action<float> updateAction, Action completeAction = null, EzEaseType easeType = EzEaseType.Linear, AnimationCurve curve = null, CancellationToken cancellToken = default)
        {
            if (!Application.isPlaying) { return; }

            EzTweenBehav.Instance.Init();
            CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellToken, EzTweenBehav.Instance.DestoryCancellationToken);

            AddTween(tokenSource);

            var _token = tokenSource.Token;
            //var _token = cancellToken;

            try
            {
                float t = 0;
                float d = to - from;
                float startTime = Time.time;

                if (d != 0)
                {
                    Func<float, float> easeAct = GetEaseAction(easeType);
                    //while (Application.isPlaying &&  t < time)
                    while (t < time)
                        {
                        //t += Time.deltaTime;
                        t = Time.time - startTime;
                        float per = t / time;
                        if (per >= 1) {
                            updateAction(to);
                            completeAction?.Invoke();
                            break;
                        } // Complete

                        per = Mathf.Clamp01(per);

                        if (curve != null)
                        {
                            per = curve.Evaluate(per);
                        }
                        else
                        {
                            per = easeAct(per);
                        }

                        //await Task.Delay((int)(Time.deltaTime*1000));
                        await Task.Delay(33, _token);
                        //await Task.Delay(33, cancellToken);  // tokenを渡すと EzTween.CancelAll() で foreachをしようしているとエラーが発生する時がある。forだとエラーが起きない？

                        _token.ThrowIfCancellationRequested();

                        updateAction(from + d * per);
                    }
                }
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenAsync cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            RemoveTween(tokenSource);
        }

        public static async Task DelaySec(float sec, CancellationToken cancellToken = default)
        {
            if (!Application.isPlaying) { return; }

            EzTweenBehav.Instance.Init();
            CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellToken, EzTweenBehav.Instance.DestoryCancellationToken);

            AddTween(tokenSource);

            try
            {
                var _token = tokenSource.Token;
                //var _token = cancellToken;
                _token.ThrowIfCancellationRequested();
                await Task.Delay(Mathf.CeilToInt(1000*sec), _token);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[Delay.cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            RemoveTween(tokenSource);
        }
        #endregion


        #region shortcut
        // ----------
        // よく使うtweenを使いやすくするため。各パラメーターのTween
        // transform
        public static async Task TweenPosition(Transform transform, EzEaseType easeType, Vector3 to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try
            {
                Vector3 from = transform.position;
                await TweenAct(easeType, from, to, time, (v) =>
                {
                    transform.position = v;
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenPosition cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public static async Task TweenLocalPosition(Transform transform, EzEaseType easeType, Vector3 to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try
            {
                Vector3 from = transform.localPosition;
                await TweenAct(easeType, from, to, time, (v) =>
                {
                    transform.localPosition = v;
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenLocalPosition cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public static async Task TweenRotation(Transform transform, EzEaseType easeType, Quaternion to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try
            {
                Quaternion from = transform.rotation;
                await TweenAct(easeType, from, to, time, (v) =>
                {
                    transform.rotation = v;
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenRotation cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public static async Task TweenLocalRotation(Transform transform, EzEaseType easeType, Quaternion to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try {
                Quaternion from = transform.localRotation;
                await TweenAct(easeType, from, to, time, (v) =>
                {
                    transform.localRotation = v;
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenLocalRotation cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public static async Task TweenScale(Transform transform, EzEaseType easeType, Vector3 to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try
            {
                Vector3 from = transform.localScale;
                await TweenAct(easeType, from, to, time, (v) =>
                {
                    transform.localScale = v;
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenScale cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        // renderer
        public static async Task TweenRendererColor(Renderer renderer, EzEaseType easeType, Color to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try
            {
                Color from = renderer.material.color;
                await TweenAct(easeType, from, to, time, (v) =>
                {
                    renderer.material.color = v;
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenRendererColor cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        // material
        public static async Task TweenMaterial(Material material, string floatParamName, EzEaseType easeType, float to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try {
                float from = material.GetFloat(floatParamName);
                await TweenAct(easeType, from, to, time, (v) =>
                {
                    material.SetFloat(floatParamName, v);
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenMaterial cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        // ui
        public static async Task TweenUiColor(MaskableGraphic uiElement, EzEaseType easeType, Color to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try {
                Color from = uiElement.color;
                await TweenAct(easeType, from, to, time, (v) =>
                {
                    uiElement.color = v;
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenUiColor cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public static async Task TweenCanvasGroupAlpha(CanvasGroup canvasGroup, EzEaseType easeType, float to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try {
                float from = canvasGroup.alpha;
                await TweenAct(easeType, from, to, time, (v) =>
                {
                    canvasGroup.alpha = v;
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenCanvasGroupAlpha cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        // ----------
        // AnimationCurve
        public static async Task TweenPosition(Transform transform, AnimationCurve animationCurve, Vector3 to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try
            {
                Vector3 from = transform.position;
                await TweenAct(animationCurve, from, to, time, (v) =>
                {
                    transform.position = v;
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenPosition cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public static async Task TweenLocalPosition(Transform transform, AnimationCurve animationCurve, Vector3 to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try
            {
                Vector3 from = transform.localPosition;
                await TweenAct(animationCurve, from, to, time, (v) =>
                {
                    transform.localPosition = v;
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenLocalPosition cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public static async Task TweenRotation(Transform transform, AnimationCurve animationCurve, Quaternion to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try
            {
                Quaternion from = transform.rotation;
                await TweenAct(animationCurve, from, to, time, (v) =>
                {
                    transform.rotation = v;
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenRotation cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public static async Task TweenLocalRotation(Transform transform, AnimationCurve animationCurve, Quaternion to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try
            {
                Quaternion from = transform.localRotation;
                await TweenAct(animationCurve, from, to, time, (v) =>
                {
                    transform.localRotation = v;
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenLocalRotation cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public static async Task TweenScale(Transform transform, AnimationCurve animationCurve, Vector3 to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try
            {
                Vector3 from = transform.localScale;
                await TweenAct(animationCurve, from, to, time, (v) =>
                {
                    transform.localScale = v;
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenScale cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        // renderer
        public static async Task TweenRendererColor(Renderer renderer, AnimationCurve animationCurve, Color to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try
            {
                Color from = renderer.material.color;
                await TweenAct(animationCurve, from, to, time, (v) =>
                {
                    renderer.material.color = v;
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenRendererColor cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        // material
        public static async Task TweenMaterial(Material material, string floatParamName, AnimationCurve animationCurve, float to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try
            {
                float from = material.GetFloat(floatParamName);
                await TweenAct(animationCurve, from, to, time, (v) =>
                {
                    material.SetFloat(floatParamName, v);
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenMaterial cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        // ui
        public static async Task TweenUiColor(MaskableGraphic uiElement, AnimationCurve animationCurve, Color to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try
            {
                Color from = uiElement.color;
                await TweenAct(animationCurve, from, to, time, (v) =>
                {
                    uiElement.color = v;
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenUiColor cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public static async Task TweenCanvasGroupAlpha(CanvasGroup canvasGroup, AnimationCurve animationCurve, float to, float time, CancellationToken cancellationToken = default, Action completeAction = null)
        {
            try
            {
                float from = canvasGroup.alpha;
                await TweenAct(animationCurve, from, to, time, (v) =>
                {
                    canvasGroup.alpha = v;
                }, completeAction, cancellationToken);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log($"[TweenCanvasGroupAlpha cancel]: " + e);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        #endregion

    }
}
