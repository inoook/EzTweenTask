
// ----------
// 開始状態 temps で指定した状態
using UnityEngine;
using UnityEngine.UI;

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
        else if (type == TYPE.SCALE)
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
        if(target == null) { return; }

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
        if (target == null) { return; }

        Gizmos.color = Color.blue;
        if (type == TYPE.POSITION)
        {
            Transform _trans = target.transform;
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