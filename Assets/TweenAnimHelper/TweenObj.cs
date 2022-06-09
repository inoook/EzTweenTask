using Ez;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// ----------
[System.Serializable]
public class TweenObj
{
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

    public enum TYPE
    {
        SCALE, SCALE_RELATIVE, POSITION, POSITION_RELATIVE, ANGLE, COLOR, ALPHA, __DELAY
    }

    public enum AnimType
    {
        NORMAL,
        REVERT, // レイアウト初期位置へ戻す
        __RESTORE // アニメーション開始位置へ
    }

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

    // 子階層としてchildGroupを追加したが煩雑になりすぎて使いにくそうなので削除した。有効にするにはTweenObjDrawer.csも変更する必要あり。
    //public bool isPlayingChildGroup = false;
    //public TweenObj[] childGroup = default;

    // easeType, animationCurve を切替しやすくする
    // 使用するtweenライブラリを変更するときはここを変更する
    static async Task TweenAct(EzEaseType easeType, AnimationCurve animationCurve, Vector3 from, Vector3 to, float time, Action<Vector3> updateAction, Action completeAction = null, CancellationToken cancellationToken = default)
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
    /// <param name="isForward"></param>
    /// <param name="cancellationTokenSource"></param>
    /// <returns></returns>
    public async Task Act(bool isForward, CancellationTokenSource cancellationTokenSource)
    {
        if (!Application.isPlaying) { return; }
        if (target == null) { return; }

        // 子の再生
        //if (!isForward)
        //{
        //    await PlayChildGroup(isForward, cancellationTokenSource);
        //}

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

            AnimType _animType = animType;
            if (!isForward)
            {
                _animType = AnimType.__RESTORE;
            }

            // tween
            // それぞれのtypeで使用するtweenを選択
            if (_animType == AnimType.NORMAL)
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
            else if (_animType == AnimType.REVERT)
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
        //
        isPlaying = false;

        // -----
        // 子グループ再生
        //if (isForward)
        //{
        //    await PlayChildGroup(isForward, cancellationTokenSource);
        //}
    }

    //private async Task PlayChildGroup(bool isForward, CancellationTokenSource cancellationTokenSource)
    //{
    //    isPlayingChildGroup = true;
    //    for (int i = 0; i < childGroup.Length; i++)
    //    {
    //        int index = isForward ? i : (childGroup.Length - 1 - i);
    //        var t = childGroup[index];
    //        if (t.enable)
    //        {
    //            cancellationTokenSource.Token.ThrowIfCancellationRequested();
    //            await t.Act(isForward, cancellationTokenSource);
    //        }
    //    }
    //    isPlayingChildGroup = false;
    //}


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
        Cache(out defaultVec3, out defaultColor, out defaultFloat);

        //foreach (var c in childGroup)
        //{
        //    c.Cache();
        //}
    }
    public void CacheFromCondition()
    {
        Cache(out fromVec3, out fromColor, out fromFloat);

        //foreach (var c in childGroup)
        //{
        //    c.CacheFromCondition();
        //}
    }

    public void Cache(out Vector3 defaultVec3, out Color defaultColor, out float defaultFloat)
    {
        defaultVec3 = Vector3.zero;
        defaultColor = Color.black;
        defaultFloat = 0;
        if (!enable) { return; }

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

        if (target == null) { return; }

        Gizmos.color = Color.red;
        if (type == TYPE.POSITION)
        {
            TweenObj.DrawGizmo(target, to_Vec3);
        }
        if (type == TYPE.POSITION_RELATIVE)
        {
            Transform _trans = target.transform;
            TweenObj.DrawGizmo(target, _trans.localPosition + to_Vec3);
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