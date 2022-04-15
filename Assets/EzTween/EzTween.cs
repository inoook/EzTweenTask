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

    //public static class Extension
    //{
    //    public static float Lerp(this float me, float to, float t) {
    //        return Mathf.Lerp(me, to, t);
    //    }
    //    public static Color Lerp(this Color me, Color to, float t) {
    //        return Color.Lerp(me, to, t);
    //    }
    //    public static Vector3 Lerp(this Vector3 me, Vector3 to, float t) {
    //        return Vector3.Lerp(me, to, t);
    //    }
    //    public static Vector2 Lerp(this Vector2 me, Vector2 to, float t) {
    //        return Vector2.Lerp(me, to, t);
    //    }
    //    public static Quaternion Lerp(this Quaternion me, Quaternion to, float t) {
    //        return Quaternion.Lerp(me, to, t);
    //    }
    //}

    //public interface ITweenValue<T>
    //{
    //    T Lerp(T to, float v);
    //}


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


        // 旧バージョンでは Run にしていたが StartTween へ関数名変更
        public async Task StartTween(EzEaseType easeType, float from, float to, float time, Action<float> updateAction, Action completeAction = null)
        {
            cancellationTokenSrc = new CancellationTokenSource();
            await EzTween.TweenAct(easeType, from, to, time, updateAction, completeAction, cancellationTokenSrc);
            cancellationTokenSrc = null;
        }
        public async Task StartTween(EzEaseType easeType, Vector3 from, Vector3 to, float time, Action<Vector3> updateAction, Action completeAction = null)
        {
            cancellationTokenSrc = new CancellationTokenSource();
            await EzTween.TweenAct(easeType, from, to, time, updateAction, completeAction, cancellationTokenSrc);
            cancellationTokenSrc = null;
        }
        public async Task StartTween(EzEaseType easeType, Vector2 from, Vector2 to, float time, System.Action<Vector2> updateAction, System.Action completeAction = null)
        {
            cancellationTokenSrc = new CancellationTokenSource();
            await EzTween.TweenAct(easeType, from, to, time, updateAction, completeAction, cancellationTokenSrc);
            cancellationTokenSrc = null;
        }
        public async Task StartTween(EzEaseType easeType, Color from, Color to, float time, System.Action<Color> updateAction, System.Action completeAction = null)
        {
            cancellationTokenSrc = new CancellationTokenSource();
            await EzTween.TweenAct(easeType, from, to, time, updateAction, completeAction, cancellationTokenSrc);
            cancellationTokenSrc = null;
        }
        public async Task StartTween(EzEaseType easeType, Quaternion from, Quaternion to, float time, System.Action<Quaternion> updateAction, System.Action completeAction = null)
        {
            cancellationTokenSrc = new CancellationTokenSource();
            await EzTween.TweenAct(easeType, from, to, time, updateAction, completeAction, cancellationTokenSrc);
            cancellationTokenSrc = null;
        }

        public void Cancel()
        {
            cancellationTokenSrc?.Cancel();

            // remove
            if (cancellationTokenSrc != null)
            {
                EzTween.RemoveTween(cancellationTokenSrc);
            }
        }

        // 使いやすくするためのショートカット
        public async Task StartTweenPosition(Transform transform, EzEaseType easeType, Vector3 to, float time, System.Action completeAction = null)
        {
            Vector3 from = transform.position;
            await StartTween(easeType, from, to, time, (v) =>
            {
                transform.position = v;
            }, completeAction);
        }
        public async Task StartTweenLocalPosition(Transform transform, EzEaseType easeType, Vector3 to, float time, System.Action completeAction = null)
        {
            Vector3 from = transform.localPosition;
            await StartTween(easeType, from, to, time, (v) =>
            {
                transform.localPosition = v;
            }, completeAction);
        }
        public async Task StartTweenRotation(Transform transform, EzEaseType easeType, Quaternion to, float time, System.Action completeAction = null)
        {
            Quaternion from = transform.rotation;
            await StartTween(easeType, from, to, time, (v) =>
            {
                transform.rotation = v;
            }, completeAction);
        }
        public async Task StartTweenLocalRotation(Transform transform, EzEaseType easeType, Quaternion to, float time, System.Action completeAction = null)
        {
            Quaternion from = transform.localRotation;
            await StartTween(easeType, from, to, time, (v) =>
            {
                transform.localRotation = v;
            }, completeAction);
        }
        public async Task StartTweenScale(Transform transform, EzEaseType easeType, Vector3 to, float time, System.Action completeAction = null)
        {
            Vector3 from = transform.localScale;
            await StartTween(easeType, from, to, time, (v) =>
            {
                transform.localScale = v;
            }, completeAction);
        }
        // renderer
        public async Task StartTweenRendererColor(Renderer renderer, EzEaseType easeType, Color to, float time, System.Action completeAction = null)
        {
            Color from = renderer.material.color;
            await StartTween(easeType, from, to, time, (v) =>
            {
                renderer.material.color = v;
            }, completeAction);
        }
        // material
        public async Task StartTweenMaterial(Material material, string floatParamName, EzEaseType easeType, float to, float time, System.Action completeAction = null)
        {
            float from = material.GetFloat(floatParamName);
            await StartTween(easeType, from, to, time, (v) =>
            {
                material.SetFloat(floatParamName, v);
            }, completeAction);
        }
        // ui
        public async Task StartTweenUiColor(MaskableGraphic uiElement, EzEaseType easeType, Color to, float time, System.Action completeAction = null)
        {
            Color from = uiElement.color;
            await StartTween(easeType, from, to, time, (v) =>
            {
                uiElement.color = v;
            }, completeAction);
        }
        public async Task StartTweenCanvasGroupAlpha(CanvasGroup canvasGroup, EzEaseType easeType, float to, float time, System.Action completeAction = null)
        {
            float from = canvasGroup.alpha;
            await EzTween.TweenAct(easeType, from, to, time, (v) =>
            {
                canvasGroup.alpha = v;
            }, completeAction);
        }

    }

    public class EzTweenBehav : MonoBehaviour
    {
        static EzTweenBehav instance;
        static GameObject go;
        static public EzTweenBehav Instance
        {
            get {
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

        public void Init()
        {
            
        }

        public void UpdateCount(int count)
        {
            go.name = $"EzTween: ({count})";
        }
        void OnDestroy()
        {
            EzTween.CancelAll();
        }
    }

    public class EzTween
    {
        #region tween
        static List<CancellationTokenSource> tweenList = new List<CancellationTokenSource>();
        public static int TweenCount => tweenList.Count;

        public static void CancelAll()
        {
            //Debug.LogWarning($"CancelAll: {tweenList.Count}");
            // Note: foreach を使うと、DelayにTokenを渡しているとエラーが発生するときがある
            // for であればエラーは起きない
            // Delay にToken 渡さないでforeachが安全そう

            //foreach (var m in tweenList)
            //{
            //    m.Cancel();
            //}
            for (int i = 0; i < tweenList.Count; i++)
            {
                var m = tweenList[i];
                m.Cancel();
            }
            tweenList.Clear();
            //Debug.LogWarning("CancenAll END");
        }

        // easeType tween
        // それぞれの型でのショートカット
        // EzEaseType / float
        public static async Task TweenAct(EzEaseType easeType, float from, float to, float time, System.Action<float> updateAction, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            await TweenBaseAct(from, to, time, updateAction, completeAction, easeType, null, cancellationTokenSource);
        }
        // EzEaseType / Vecotr3
        public static async Task TweenAct(EzEaseType easeType, Vector3 from, Vector3 to, float time, System.Action<Vector3> updateAction, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Vector3 p = Vector3.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, easeType, null, cancellationTokenSource);
        }
        // EzEaseType / Vector2
        public static async Task TweenAct(EzEaseType easeType, Vector2 from, Vector2 to, float time, System.Action<Vector2> updateAction, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Vector2 p = Vector2.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, easeType, null, cancellationTokenSource);
        }
        // EzEaseType / Color
        public static async Task TweenAct(EzEaseType easeType, Color from, Color to, float time, System.Action<Color> updateAction, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Color p = Color.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, easeType, null, cancellationTokenSource);
        }
        // EzEaseType / Color32
        public static async Task TweenAct(EzEaseType easeType, Color32 from, Color32 to, float time, System.Action<Color32> updateAction, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Color32 p = Color32.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, easeType, null, cancellationTokenSource);
        }
        // EzEaseType / Quaternion
        public static async Task TweenAct(EzEaseType easeType, Quaternion from, Quaternion to, float time, System.Action<Quaternion> updateAction, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Quaternion p = Quaternion.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, easeType, null, cancellationTokenSource);
        }
        //
        // AnimationCurve tween
        // Tween をアニメーションカーブで定義
        // float
        public static async Task TweenAct(AnimationCurve animationCurve, float from, float to, float time, System.Action<float> updateAction, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            await TweenBaseAct(from, to, time, updateAction, completeAction, EzEaseType.AnimationCurve, animationCurve, cancellationTokenSource);
        }
        // AnimationCurve / Vector3
        public static async Task TweenAct(AnimationCurve animationCurve, Vector3 from, Vector3 to, float time, System.Action<Vector3> updateAction, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Vector3 p = Vector3.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, EzEaseType.AnimationCurve, animationCurve, cancellationTokenSource);
        }
        //  AnimationCurve / Vector2
        public static async Task TweenAct(AnimationCurve animationCurve, Vector2 from, Vector2 to, float time, System.Action<Vector2> updateAction, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Vector2 p = Vector2.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, EzEaseType.AnimationCurve, animationCurve, cancellationTokenSource);
        }
        //  AnimationCurve / Color
        public static async Task TweenAct(AnimationCurve animationCurve, Color from, Color to, float time, System.Action<Color> updateAction, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Color p = Color.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, EzEaseType.AnimationCurve, animationCurve, cancellationTokenSource);
        }
        //  AnimationCurve / Color32
        public static async Task TweenAct(AnimationCurve animationCurve, Color32 from, Color32 to, float time, System.Action<Color32> updateAction, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Color32 p = Color32.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, EzEaseType.AnimationCurve, animationCurve, cancellationTokenSource);
        }
        //  AnimationCurve / Quaternion
        public static async Task TweenAct(AnimationCurve animationCurve, Quaternion from, Quaternion to, float time, System.Action<Quaternion> updateAction, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            await TweenBaseAct(0, 1, time, (v) =>
            {
                Quaternion p = Quaternion.LerpUnclamped(from, to, v);
                updateAction(p);
            }, completeAction, EzEaseType.AnimationCurve, animationCurve, cancellationTokenSource);
        }
        #endregion

        #region shortcut
        // よく使うtweenを使いやすくするため
        // transform
        public static async Task TweenPosition(Transform transform, EzEaseType easeType, Vector3 to, float time, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            Vector3 from = transform.position;
            await TweenAct(easeType, from, to, time, (v) =>
            {
                transform.position = v;
            }, completeAction, cancellationTokenSource);
        }
        public static async Task TweenLocalPosition(Transform transform, EzEaseType easeType, Vector3 to, float time, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            Vector3 from = transform.localPosition;
            await TweenAct(easeType, from, to, time, (v) =>
            {
                transform.localPosition = v;
            }, completeAction, cancellationTokenSource);
        }
        public static async Task TweenRotation(Transform transform, EzEaseType easeType, Quaternion to, float time, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            Quaternion from = transform.rotation;
            await TweenAct(easeType, from, to, time, (v) =>
            {
                transform.rotation = v;
            }, completeAction, cancellationTokenSource);
        }
        public static async Task TweenLocalRotation(Transform transform, EzEaseType easeType, Quaternion to, float time, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            Quaternion from = transform.localRotation;
            await TweenAct(easeType, from, to, time, (v) =>
            {
                transform.localRotation = v;
            }, completeAction, cancellationTokenSource);
        }
        public static async Task TweenScale(Transform transform, EzEaseType easeType, Vector3 to, float time, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            Vector3 from = transform.localScale;
            await TweenAct(easeType, from, to, time, (v) =>
            {
                transform.localScale = v;
            }, completeAction, cancellationTokenSource);
        }
        // renderer
        public static async Task TweenRendererColor(Renderer renderer, EzEaseType easeType, Color to, float time, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            Color from = renderer.material.color;
            await TweenAct(easeType, from, to, time, (v) =>
            {
                renderer.material.color = v;
            }, completeAction, cancellationTokenSource);
        }
        // material
        public static async Task TweenMaterial(Material material, string floatParamName, EzEaseType easeType, float to, float time, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            float from = material.GetFloat(floatParamName);
            await TweenAct(easeType, from, to, time, (v) =>
            {
                material.SetFloat(floatParamName, v);
            }, completeAction, cancellationTokenSource);
        }
        // ui
        public static async Task TweenUiColor(MaskableGraphic uiElement, EzEaseType easeType, Color to, float time, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            Color from = uiElement.color;
            await TweenAct(easeType, from, to, time, (v) =>
            {
                uiElement.color = v;
            }, completeAction, cancellationTokenSource);
        }
        public static async Task TweenCanvasGroupAlpha(CanvasGroup canvasGroup, EzEaseType easeType, float to, float time, System.Action completeAction = null, CancellationTokenSource cancellationTokenSource = null)
        {
            float from = canvasGroup.alpha;
            await TweenAct(easeType, from, to, time, (v) =>
            {
                canvasGroup.alpha = v;
            }, completeAction, cancellationTokenSource);
        }
        #endregion


        static async Task TweenBaseAct(float from, float to, float time, System.Action<float> updateAction, System.Action completeAction = null, EzEaseType easeType = EzEaseType.Linear, AnimationCurve curve = null, CancellationTokenSource tokenSource = null)
        {
            if (tokenSource == null)
            {
                tokenSource = new CancellationTokenSource();
                Debug.LogWarning("create: CancellationTokenSource");
            }
            AddTween(tokenSource);

            //try
            //{
                await TweenAsync(tokenSource.Token, from, to, time, updateAction, completeAction, easeType, curve);
            //}
            //catch (System.OperationCanceledException e)
            //{
            //    Debug.Log($"TweenBaseAct cancel: " + e);
            //}
            //catch (System.Exception e)
            //{
            //    Debug.LogError(e);
            //}

            RemoveTween(tokenSource);

            tokenSource = null;
        }

        public static void AddTween(CancellationTokenSource cancellationTokenSource)
        {
            tweenList.Add(cancellationTokenSource);
            EzTweenBehav.Instance.UpdateCount(tweenList.Count);

        }
        public static void RemoveTween(CancellationTokenSource cancellationTokenSource)
        {
            if (tweenList.Contains(cancellationTokenSource))
            {
                tweenList.Remove(cancellationTokenSource);
                EzTweenBehav.Instance.UpdateCount(tweenList.Count);
            }
        }

        public static System.Func<float, float> GetEaseAction(EzEaseType type)
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

        #region tween

        static async Task TweenAsync(CancellationToken cancellToken, float from, float to, float time, System.Action<float> updateAction, System.Action completeAction = null, EzEaseType easeType = EzEaseType.Linear, AnimationCurve curve = null)
        {
            EzTweenBehav.Instance.Init();

            try
            {
                float t = 0;
                float d = to - from;
                float startTime = Time.time;

                if (d != 0)
                {
                    System.Func<float, float> easeAct = GetEaseAction(easeType);
                    while (t < time)
                    {
                        //t += Time.deltaTime;
                        t = Time.time - startTime;
                        float per = t / time;
                        if (per >= 1) { break; } // Complete

                        per = Mathf.Clamp01(per);

                        if (curve != null)
                        {
                            per = curve.Evaluate(per);
                        }
                        else
                        {
                            per = easeAct(per);
                        }

                        cancellToken.ThrowIfCancellationRequested();

                        updateAction(from + d * per);

                        //await Task.Delay((int)(Time.deltaTime*1000));
                        await Task.Delay(33);
                        //await Task.Delay(33, cancellToken);  // tokenを渡すと EzTween.CancelAll() で foreachをしようしているとエラーが発生する時がある。forだとエラーが起きない？
                    }
                }

                //Debug.Log("[Tween] Complete");
                updateAction(to);
                completeAction?.Invoke();
            }
            catch (System.OperationCanceledException e)
            {
                Debug.Log($"TweenAsync cancel: " + e);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
            cancellToken.ThrowIfCancellationRequested();
        }
        #endregion
    }
}
