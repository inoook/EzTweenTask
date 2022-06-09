using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ez;
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
    public enum TYPE
    {
        SCALE, SCALE_RELATIVE, POSITION, POSITION_RELATIVE, ANGLE, COLOR, ALPHA, __DELAY
    }

    public enum AnimType
    {
        NORMAL, 
        REVERT, // レイアウト初期位置へ戻す
        RESTORE // アニメーション開始位置へ
    }

    public static void DrawGizmo(GameObject target, Vector3 to)
    {
        // uGUIのとき描画サイズ調整
        float scaleAmp = 0.25f;

        float scale = 1;
        if (target.GetComponent<MaskableGraphic>() != null || target.GetComponent<CanvasGroup>() != null)
        {
            scale = 50;
        }
        Transform _trans = target.transform;
        Vector3 worldTo;
        if (_trans.parent != null)
        {
            worldTo = _trans.parent.TransformPoint(to);
        }
        else
        {
            worldTo = to;
        }
        Gizmos.DrawSphere(worldTo, scale * scaleAmp);
    }

    // ----------
    [System.Serializable]
    public class TweenObj
    {
        public bool enable = true;
        public float time = 0;
        public float delay = 0;
        public EzEaseType ezEaseType = EzEaseType.Linear;
        public AnimationCurve animationCurve = null;

        public GameObject target = default;
        public TYPE type = TYPE.POSITION;
        public AnimType animType = AnimType.NORMAL;

        public Vector3 to_Vec3 = default;
        public Color to_Color = default;
        public float to_V = default;

        public bool isPlaying = false;

        //public TweenGroup childGroup = default;

        // easeType, animationCurve を切替しやすくする
        // 使用するtweenライブラリを変更するときはここを変更する
        static async Task TweenAct(EzEaseType easeType, AnimationCurve animationCurve, Vector3 from, Vector3 to, float time, Action<Vector3> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            if(from == to) { return; }

            if (easeType != EzEaseType.AnimationCurve)
            {
                await EzTween.TweenAct(easeType, from, to, time, updateAction, completeAction, cancellationToken);
            }
            else
            {
                await EzTween.TweenAct(animationCurve, from, to, time, updateAction, completeAction, cancellationToken);
            }
        }
        static async Task TweenAct(EzEaseType easeType, AnimationCurve animationCurve, Vector2 from, Vector2 to, float time, Action<Vector2> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            if (from == to) { return; }

            if (easeType != EzEaseType.AnimationCurve)
            {
                await EzTween.TweenAct(easeType, from, to, time, updateAction, completeAction, cancellationToken);
            }
            else
            {
                await EzTween.TweenAct(animationCurve, from, to, time, updateAction, completeAction, cancellationToken);
            }
        }
        static async Task TweenAct(EzEaseType easeType, AnimationCurve animationCurve, Color from, Color to, float time, Action<Color> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            if (from == to) { return; }

            if (easeType != EzEaseType.AnimationCurve)
            {
                await EzTween.TweenAct(easeType, from, to, time, updateAction, completeAction, cancellationToken);
            }
            else
            {
                await EzTween.TweenAct(animationCurve, from, to, time, updateAction, completeAction, cancellationToken);
            }
        }
        static async Task TweenAct(EzEaseType easeType, AnimationCurve animationCurve, float from, float to, float time, Action<float> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
        {
            if (from == to) { return; }

            if (easeType != EzEaseType.AnimationCurve)
            {
                await EzTween.TweenAct(easeType, from, to, time, updateAction, completeAction, cancellationToken);
            }
            else
            {
                await EzTween.TweenAct(animationCurve, from, to, time, updateAction, completeAction, cancellationToken);
            }
        }

        /// <summary>
        /// アニメ実行
        /// </summary>
        /// <param name="animType"></param>
        /// <param name="cancellationTokenSource"></param>
        /// <returns></returns>
        public async Task Act(AnimType animType,  CancellationTokenSource cancellationTokenSource)
        {
            if (!Application.isPlaying) { return; }

            isPlaying = true;

            if (type == TYPE.__DELAY)
            {
                if (delay > 0)
                {
                    await EzTween.DelaySec(delay, cancellationTokenSource.Token);
                }
            }
            else
            {
                // Delay以外
                // tween
                // それぞれのtypeで使用するtweenを選択
                if (animType == AnimType.NORMAL)
                {
                    if (type == TYPE.POSITION)
                    {
                        await TweenPositionAct(ezEaseType, animationCurve, to_Vec3, time, cancellationTokenSource);
                    }
                    else if (type == TYPE.POSITION_RELATIVE)
                    {
                        Vector3 _to = defaultVec3 + to_Vec3;
                        await TweenPositionAct(ezEaseType, animationCurve, _to, time, cancellationTokenSource);
                    }
                    //
                    else if (type == TYPE.SCALE)
                    {
                        await TweenScaleAct(ezEaseType, animationCurve, to_Vec3, time, cancellationTokenSource);
                    }
                    else if (type == TYPE.SCALE_RELATIVE)
                    {
                        Vector3 _to = defaultVec3 + to_Vec3;
                        await TweenScaleAct(ezEaseType, animationCurve, _to, time, cancellationTokenSource);
                    }
                    //
                    else if (type == TYPE.COLOR)
                    {
                        await TweenColorAct(ezEaseType, animationCurve, to_Color, time, cancellationTokenSource);
                    }
                    //
                    else if (type == TYPE.ALPHA)
                    {
                        await TweenAlphaAct(ezEaseType, animationCurve, to_V, time, cancellationTokenSource);
                    }
                }
                else if (animType == AnimType.REVERT)
                {
                    // REVERT
                    if (type == TYPE.POSITION || type == TYPE.POSITION_RELATIVE)
                    {
                        Vector3 to_Vec3 = defaultVec3;
                        await TweenPositionAct(ezEaseType, animationCurve, to_Vec3, time, cancellationTokenSource);
                    }
                    else if (type == TYPE.SCALE || type == TYPE.SCALE_RELATIVE)
                    {
                        Vector3 to_Vec3 = defaultVec3;
                        await TweenScaleAct(ezEaseType, animationCurve, to_Vec3, time, cancellationTokenSource);
                    }
                    else if (type == TYPE.COLOR)
                    {
                        Color to_Color = defaultColor;
                        await TweenColorAct(ezEaseType, animationCurve, to_Color, time, cancellationTokenSource);
                    }
                    else if (type == TYPE.ALPHA)
                    {
                        float to_V = defaultFloat;
                        await TweenAlphaAct(ezEaseType, animationCurve, to_V, time, cancellationTokenSource);
                    }
                }
                else
                {
                    // RESTORE
                    if (type == TYPE.POSITION || type == TYPE.POSITION_RELATIVE)
                    {
                        Vector3 to_Vec3 = fromVec3;
                        await TweenPositionAct(ezEaseType, animationCurve, to_Vec3, time, cancellationTokenSource);
                    }
                    else if (type == TYPE.SCALE || type == TYPE.SCALE_RELATIVE)
                    {
                        Vector3 to_Vec3 = fromVec3;
                        await TweenScaleAct(ezEaseType, animationCurve, to_Vec3, time, cancellationTokenSource);
                    }
                    else if (type == TYPE.COLOR)
                    {
                        Color to_Color = fromColor;
                        await TweenColorAct(ezEaseType, animationCurve, to_Color, time, cancellationTokenSource);
                    }
                    else if (type == TYPE.ALPHA)
                    {
                        float to_V = fromFloat;
                        await TweenAlphaAct(ezEaseType, animationCurve, to_V, time, cancellationTokenSource);
                    }
                }
            }
            //
            isPlaying = false;

            // 子グループ再生
            //await childGroup.ActForward(true, cancellationTokenSource);
        }

        // -----
        // アニメ開始時の値
        private bool isCacheFromValue = false;
        private Vector3 fromVec3;
        private Color fromColor;
        private float fromFloat;

        // tweenを使用するためのショートカット
        async Task TweenPositionAct(EzEaseType ezEaseType, AnimationCurve animationCurve, Vector3 to_Vec3, float time, CancellationTokenSource cancellationTokenSource)
        {
            Transform _trans = target.transform;
            if (!isCacheFromValue)
            {
                fromVec3 = _trans.localPosition;
                isCacheFromValue = true;
            }
            await TweenObj.TweenAct(ezEaseType, animationCurve, _trans.localPosition, to_Vec3, time, (Vector3 v) =>
            {
                _trans.localPosition = v;
            }, null, cancellationTokenSource.Token);
        }

        async Task TweenScaleAct(EzEaseType ezEaseType, AnimationCurve animationCurve, Vector3 to_Vec3, float time, CancellationTokenSource cancellationTokenSource)
        {
            Transform _trans = target.transform;
            if (!isCacheFromValue)
            {
                fromVec3 = _trans.localScale;
                isCacheFromValue = true;
            }
            await TweenObj.TweenAct(ezEaseType, animationCurve, _trans.localScale, to_Vec3, time, (Vector3 v) =>
            {
                _trans.localScale = v;
            }, null, cancellationTokenSource.Token);
         }

        async Task TweenColorAct(EzEaseType ezEaseType, AnimationCurve animationCurve, Color to_Color, float time, CancellationTokenSource cancellationTokenSource)
        {
            Renderer _target = target.GetComponent<Renderer>();
            Color tempColor = Color.black;
            if (_target != null)
            {
                Color from = _target.material.color;
                tempColor = from;
                await TweenObj.TweenAct(ezEaseType, animationCurve, from, to_Color, time, (Color v) =>
                {
                    _target.material.color = v;
                }, null, cancellationTokenSource.Token);
            }
            else
            {
                MaskableGraphic _target_uGUI = target.GetComponent<MaskableGraphic>();
                if (_target_uGUI != null)
                {
                    Color from = _target_uGUI.color;
                    tempColor = from;
                    await TweenObj.TweenAct(ezEaseType, animationCurve, from, to_Color, time, (Color v) =>
                    {
                        _target_uGUI.color = v;
                    }, null, cancellationTokenSource.Token);
                }
                else
                {
                    _NoRendererError();
                }
            }

            if (!isCacheFromValue)
            {
                fromColor = tempColor;
                isCacheFromValue = true;
            }
        }

        async Task TweenAlphaAct(EzEaseType ezEaseType, AnimationCurve animationCurve, float to_V, float time, CancellationTokenSource cancellationTokenSource)
        {
            Renderer _target = target.GetComponent<Renderer>();
            float tempV = 0;
            if (_target != null)
            {
                Color _color = _target.material.color;
                float from = _color.a;
                tempV = from;
                await TweenObj.TweenAct(ezEaseType, animationCurve, from, to_V, time, (float v) =>
                {
                    _color.a = v;
                    _target.material.color = _color;
                }, null, cancellationTokenSource.Token);
            }
            else
            {
                MaskableGraphic _target_uGUI = target.GetComponent<MaskableGraphic>();
                if (_target_uGUI != null)
                {
                    Color _color = _target_uGUI.color;
                    float from = _color.a;
                    tempV = from;
                    await TweenObj.TweenAct(ezEaseType, animationCurve, from, to_V, time, (float v) =>
                    {
                        _color.a = v;
                        _target_uGUI.color = _color;
                    }, null, cancellationTokenSource.Token);
                }
                else
                {
                    CanvasGroup _target_canvas = target.GetComponent<CanvasGroup>();
                    if (_target_canvas != null)
                    {
                        float from = _target_canvas.alpha;
                        tempV = from;
                        await TweenObj.TweenAct(ezEaseType, animationCurve, from, to_V, time, (float v) =>
                        {
                            _target_canvas.alpha = v;
                        }, null, cancellationTokenSource.Token);
                    }
                    else
                    {
                        _NoRendererError();
                    }
                }
            }
            //
            if (!isCacheFromValue)
            {
                isCacheFromValue = true;
                fromFloat = tempV;
            }
        }

        void _NoRendererError()
        {
            Debug.LogWarning("描画できるrenderがありません");
        }

        /// <summary>
        /// レイアウトの初期情報
        /// </summary>
        private Vector3 defaultVec3;
        private Color defaultColor;
        private float defaultFloat;

        public void Cache()
        {
            Cache(out  defaultVec3, out  defaultColor, out  defaultFloat);
        }
        public void CacheFromCondition()
        {
            Cache(out fromVec3, out fromColor, out fromFloat);
        }

        public void Cache(out Vector3 defaultVec3, out Color defaultColor, out float defaultFloat)
        {
            defaultVec3 = Vector3.zero;
            defaultColor = Color.black;
            defaultFloat = 0;
            if (!enable) { return;  }

            if (type == TYPE.POSITION || type == TYPE.POSITION_RELATIVE)
            {
                Transform _trans = target.transform;
                defaultVec3 = _trans.localPosition;
            }
            else if (type == TYPE.SCALE || type == TYPE.SCALE_RELATIVE)
            {
                Transform _trans = target.transform;
                defaultVec3 = _trans.localScale;
            }
            else if (type == TYPE.COLOR)
            {
                Renderer _target = target.GetComponent<Renderer>();
                if (_target != null)
                {
                    defaultColor = _target.material.color;
                }
                else
                {
                    MaskableGraphic _target_uGUI = target.GetComponent<MaskableGraphic>();
                    if (_target_uGUI != null)
                    {
                        defaultColor = _target_uGUI.color;
                    }
                    else
                    {
                        _NoRendererError();
                    }
                }
            }
            else if (type == TYPE.ALPHA)
            {
                Renderer _target = target.GetComponent<Renderer>();
                if (_target != null)
                {
                    defaultFloat = _target.material.color.a;
                }
                else
                {
                    MaskableGraphic _target_uGUI = target.GetComponent<MaskableGraphic>();
                    if (_target_uGUI != null)
                    {
                        defaultFloat = _target_uGUI.color.a;
                    }
                    else
                    {
                        CanvasGroup _target_canvas = target.GetComponent<CanvasGroup>();
                        if (_target_canvas != null)
                        {
                            defaultFloat = _target_canvas.alpha;
                        }
                        else
                        {
                            _NoRendererError();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// レイアウトの初期状態（Editorで再生する前の状態）へ戻す
        /// </summary>
        public void Revert()
        {
            if (type == TYPE.POSITION || type == TYPE.POSITION_RELATIVE)
            {
                Transform _trans = target.transform;
                _trans.localPosition = defaultVec3;
            }
            else if (type == TYPE.SCALE)
            {
                Transform _trans = target.transform;
                _trans.localScale = defaultVec3;
            }
            else if (type == TYPE.COLOR)
            {
                Renderer _target = target.GetComponent<Renderer>();
                if (_target != null)
                {
                    _target.material.color = defaultColor;
                }
                else
                {
                    MaskableGraphic _target_uGUI = target.GetComponent<MaskableGraphic>();
                    if (_target_uGUI != null)
                    {
                        _target_uGUI.color = defaultColor;
                    }
                    else
                    {
                        _NoRendererError();
                    }
                }
            }
            else if (type == TYPE.ALPHA)
            {
                Renderer _target = target.GetComponent<Renderer>();
                if (_target != null)
                {
                    Color _color = _target.material.color;
                    _color.a = defaultFloat;
                    _target.material.color = _color;
                }
                else
                {
                    MaskableGraphic _target_uGUI = target.GetComponent<MaskableGraphic>();
                    if (_target_uGUI != null)
                    {
                        Color _color = _target_uGUI.color;
                        _color.a = defaultFloat;
                        _target_uGUI.color = _color;
                    }
                    else
                    {
                        CanvasGroup _target_canvas = target.GetComponent<CanvasGroup>();
                        if (_target_canvas != null)
                        {
                            _target_canvas.alpha = defaultFloat;
                        }
                        else
                        {
                            _NoRendererError();
                        }
                    }
                }
            }
        }

        public void DrawGizmos()
        {
            if (!enable) { return; }

            if(target == null) { return; }

            Gizmos.color = Color.red;
            if (type == TYPE.POSITION)
            {
                DrawGizmo(target, to_Vec3);
            }
            if (type == TYPE.POSITION_RELATIVE)
            {
                Transform _trans = target.transform;
                DrawGizmo(target, _trans.localPosition + to_Vec3);
            }
            else if (type == TYPE.SCALE)
            {
                Transform _trans = target.transform;
            }
            else if (type == TYPE.SCALE_RELATIVE)
            {
                Transform _trans = target.transform;
            }
            else if (type == TYPE.COLOR)
            {

            }
            else if (type == TYPE.ALPHA)
            {

            }
        }

    }

    // ----------
    [System.Serializable]
    public class TweenGroup
    {
        //[SerializeField] TweenGroup tween = null;

        [Header("上から順番に再生")]
        public TweenObj[] chains = default;
        [Header("同時に再生開始")]
        public TweenObj[] parallels = default;

        CancellationTokenSource cancellationTokenSource = null;

        public void Cancel()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = null;
        }

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

        //
        public async Task ActForward(bool isForward = false)
        {
            cancellationTokenSource = new CancellationTokenSource();

            Task chainsTask = DoChains(isForward, cancellationTokenSource);
            Task parallelsTask = DoParallels(isForward, cancellationTokenSource);

            await Task.WhenAll(chainsTask, parallelsTask);

            Debug.LogWarning($"Complete");
        }
        public async Task ActForward(bool isForward, CancellationTokenSource cancellationTokenSource)
        {
            Task chainsTask = DoChains(isForward, cancellationTokenSource);
            Task parallelsTask = DoParallels(isForward, cancellationTokenSource);

            await Task.WhenAll(chainsTask, parallelsTask);

            Debug.LogWarning($"Complete");
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
                    AnimType animType = t.animType;
                    if (!isForward)
                    {
                        animType = AnimType.RESTORE;
                    }
                    await t.Act(animType, cancellationTokenSource);
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
                    AnimType animType = t.animType;
                    if (!isForward)
                    {
                        animType = AnimType.RESTORE;
                    }
                    taskList.Add(t.Act(animType, cancellationTokenSource));
                }
            }
            await Task.WhenAll(taskList);
        }
    }

    // ----------
    // 開始状態 temps で指定した状態
    [System.Serializable]
    public class TemporaryCondition
    {
        public enum TYPE
        {
            SCALE, SCALE_RELATIVE, POSITION, POSITION_RELATIVE, ANGLE, COLOR, ALPHA
        }
        public bool enable = true;
        public GameObject target = default;
        public TYPE type = TYPE.POSITION;

        public Vector3 to_Vec3 = default;
        public Color to_Color = default;
        public float to_V = default;

        private Vector3 defaultVec3;
        private Color defaultColor;
        private float defaultFloat;

        public void Act()
        {
            if (!enable) { return; }

            if (type == TYPE.POSITION)
            {
                Transform _trans = target.transform;
                _trans.localPosition = to_Vec3;

                defaultVec3 = _trans.localPosition;
            }
            else if (type == TYPE.POSITION_RELATIVE)
            {
                Transform _trans = target.transform;
                _trans.localPosition += to_Vec3;

                defaultVec3 = _trans.localPosition;
            }
            else if(type == TYPE.SCALE)
            {
                Transform _trans = target.transform;
                _trans.localScale = to_Vec3;

                defaultVec3 = _trans.localScale;
            }
            else if (type == TYPE.SCALE_RELATIVE)
            {
                Transform _trans = target.transform;
                _trans.localScale += to_Vec3;

                defaultVec3 = _trans.localScale;
            }
            else if (type == TYPE.COLOR)
            {
                Renderer _target = target.GetComponent<Renderer>();
                if (_target != null)
                {
                    _target.material.color = to_Color;
                }
                else
                {
                    MaskableGraphic _target_uGUI = target.GetComponent<MaskableGraphic>();
                    if (_target_uGUI != null)
                    {
                        _target_uGUI.color = to_Color;
                    }
                    else
                    {
                        _NoRendererError();
                    }
                }
                defaultColor = to_Color;
            }
            else if (type == TYPE.ALPHA)
            {
                Renderer _target = target.GetComponent<Renderer>();
                if (_target != null)
                {
                    Color _color = _target.material.color;
                    _color.a = to_V;
                    _target.material.color = _color;
                }
                else
                {
                    MaskableGraphic _target_uGUI = target.GetComponent<MaskableGraphic>();
                    if (_target_uGUI != null)
                    {
                        Color _color = _target_uGUI.color;
                        _color.a = to_V;
                        _target_uGUI.color = _color;
                    }
                    else
                    {
                        CanvasGroup _target_canvas = target.GetComponent<CanvasGroup>();
                        if (_target_canvas != null)
                        {
                            _target_canvas.alpha = to_V;
                        }
                        else
                        {
                            _NoRendererError();
                        }
                    }
                }
                defaultFloat = to_V;
            }
        }

        public void Restore()
        {
            if (type == TYPE.POSITION || type == TYPE.POSITION_RELATIVE)
            {
                Transform _trans = target.transform;
                _trans.localPosition = defaultVec3;
            }
            else if (type == TYPE.SCALE || type == TYPE.SCALE_RELATIVE)
            {
                Transform _trans = target.transform;
                _trans.localScale = defaultVec3;
            }
            else if (type == TYPE.COLOR)
            {
                Color to_Color = defaultColor;

                Renderer _target = target.GetComponent<Renderer>();
                if (_target != null)
                {
                    _target.material.color = to_Color;
                }
                else
                {
                    MaskableGraphic _target_uGUI = target.GetComponent<MaskableGraphic>();
                    if (_target_uGUI != null)
                    {
                        _target_uGUI.color = to_Color;
                    }
                    else
                    {
                        _NoRendererError();
                    }
                }
            }
            else if (type == TYPE.ALPHA)
            {
                float to_V = defaultFloat;

                Renderer _target = target.GetComponent<Renderer>();
                if (_target != null)
                {
                    Color _color = _target.material.color;
                    _color.a = to_V;
                    _target.material.color = _color;
                }
                else
                {
                    MaskableGraphic _target_uGUI = target.GetComponent<MaskableGraphic>();
                    if (_target_uGUI != null)
                    {
                        Color _color = _target_uGUI.color;
                        _color.a = to_V;
                        _target_uGUI.color = _color;
                    }
                    else
                    {
                        CanvasGroup _target_canvas = target.GetComponent<CanvasGroup>();
                        if (_target_canvas != null)
                        {
                            _target_canvas.alpha = to_V;
                        }
                        else
                        {
                            _NoRendererError();
                        }
                    }
                }
            }
        }


        void _NoRendererError()
        {
            Debug.LogWarning("描画できるrendererがありません");
        }

        public void DrawGizmos()
        {
            if(target == null) { return; }

            Gizmos.color = Color.blue;
            if (type == TYPE.POSITION)
            {
                Transform _trans = target.transform;
                DrawGizmo(target, to_Vec3);
            }
            if (type == TYPE.POSITION_RELATIVE)
            {
                Transform _trans = target.transform;
                DrawGizmo(target, _trans.localPosition + to_Vec3);
            }
            else if (type == TYPE.SCALE)
            {
                Transform _trans = target.transform;

            }
            else if (type == TYPE.SCALE_RELATIVE)
            {
                Transform _trans = target.transform;
            }
            else if (type == TYPE.COLOR)
            {

            }
            else if (type == TYPE.ALPHA)
            {

            }
        }
    }

    // ----------
    [Header("起動時に再生するか？")]
    [SerializeField] bool playOnEnable = true;
    [Header("起動時にアニメ再生開始状態へ戻すか？")]
    [SerializeField] bool restoreOnEnable = true;

    [Header("アニメーション再生開始状態定義")]
    [SerializeField] TemporaryCondition[] temps = default;

    [Header("アニメーション（再生中はグリーンで表示）")]
    [SerializeField] TweenGroup group = default;

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

    // Start is called before the first frame update
    // 1st
    void Awake()
    {
        // 初期レイアウトを保存
        group.Cache();

        // temps で指定したアニメ開始状態にする
        SetTemporary();
        // アニメ開始状態を保存
        group.CacheFromCondition();
    }

    // 2nd
    private void OnEnable()
    {
        if (restoreOnEnable)
        {
            RestoreStartCondition();
        }

        if (playOnEnable)
        {
            _ = Play();
        }
    }

    private void OnDisable()
    {
        Cancel();
    }


    public async Task PlayForward(bool isForward = false)
    {
        if (!Application.isPlaying) { return; }

        Cancel();

        await group.ActForward(isForward);
    }

    [ContextMenu("Play")]
    public async Task Play()
    {
        await PlayForward(true);
    }
    [ContextMenu("PlayReverse")]
    public async Task PlayReverse()
    {
        await PlayForward(false);
    }

    public void Cancel()
    {
        group.Cancel();
    }

    /// <summary>
    /// tempsで設定した初期状態へ戻す
    /// </summary>
    [ContextMenu("Restore")]
    public void RestoreStartCondition()
    {
        if (!Application.isPlaying) { return; }

        group.Cancel();

        foreach (var t in temps)
        {
            t.Restore();
        }
    }

    [ContextMenu("Revert")]
    public void Revert()
    {
        if (!Application.isPlaying) { return; }

        group.Revert();
    }

    private void OnDrawGizmosSelected()
    //private void OnDrawGizmos()
    {
        if (Application.isPlaying) { return; }

        if(temps == null) { return; }

        foreach (var t in temps)
        {
            t.DrawGizmos();
        }
        group.DrawGizmos();
    }

}
