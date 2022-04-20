using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Ez;

public class EzTweenTest : MonoBehaviour
{
    [SerializeField] EzEaseType ezEaseType = EzEaseType.Linear;
    [SerializeField] Transform targetTrans = null;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            EzTween.CancelAll();
        }
    }

    CancellationTokenSource scaleCancellationTokenSource = null;
    async Task Act_ToScale(float time = 1)
    {
        scaleCancellationTokenSource?.Cancel();
        scaleCancellationTokenSource = new CancellationTokenSource();

        Vector3 to = Vector3.one * Random.Range(1f, 5f);
        Debug.LogWarning($"to: {to.x}");
        await EzTween.TweenAct(ezEaseType, targetTrans.localScale, to, time, (Vector3 v) => {
            targetTrans.localScale = v;
        }, null, scaleCancellationTokenSource.Token);

        scaleCancellationTokenSource = null;
        Debug.Log("Complete_Act_ToScale");
    }

    async Task Act_RandomPosition(float time = 1)
    {
        Debug.LogWarning("RandomPos");
        float to = Random.Range(-5f, 5f);
        float t = Time.time;
        await EzTween.TweenAct(ezEaseType, targetTrans.localPosition.x, to, time, (v) => {
            targetTrans.localPosition = new Vector3(v, 0, 0);
        });
        Debug.Log(Time.time - t);
        Debug.Log("Complete_Act_RandomPosition");
    }

    async Task Act_RandomScale(float time = 1) {
        Debug.Log("Start_Act_RandomScale");

        float to = Random.Range(1f, 5f);
        await EzTween.TweenAct(ezEaseType, targetTrans.localScale.x, to, time, (v) => {
            targetTrans.localScale = Vector3.one * v;
        });
        Debug.Log("Complete_Act_RandomScale");
    }


    async Task Act_ToScale__(float time = 1) {
        Vector3 to = Vector3.one * Random.Range(1f, 5f);
        await EzTween.TweenAct(ezEaseType, targetTrans.localScale, to, time, (v) => {
            targetTrans.localScale = v;
        });
        Debug.Log("Complete_Act_ToScale");
    }

    async Task Act_RandomRot(float time = 1) {
        Debug.Log("Start_Act_RandomRot");

        Quaternion to = Random.rotationUniform;
        await EzTween.TweenAct(ezEaseType, targetTrans.rotation, to, time, (v) => {
            targetTrans.rotation = v;
        });
        Debug.Log("Complete_Act_RandomRot");
    }

    [SerializeField] AnimationCurve animationCurve = null;
    async Task Act_RandomScaleAnimCurve(float time = 1) {
        Vector3 to = Vector3.one * Random.Range(1f, 5f);
        await EzTween.TweenAct(animationCurve, targetTrans.localScale, to, time, (v) => {
            targetTrans.localScale = v;
        });
        Debug.Log("Complete_Act_Act_RandomScaleAnimCurve");
    }

    async Task Act_RandomColor(float time = 1) {
        Debug.Log("Start_Act_RandomColor");

        Color to = Random.ColorHSV();
        Renderer _renderer = targetTrans.GetComponent<Renderer>();
        await EzTween.TweenAct(ezEaseType, _renderer.material.color, to, time, (Color v) => {
            _renderer.material.color = v;
        });
        Debug.Log("Complete_Act_RandomColor");
    }

    async Task Act_Chain()
    {
        await Act_RandomRot(1);
        await Act_RandomScale(2);
        Debug.Log("Complete");
    }

    async Task ActPara()
    {
        await Task.WhenAll(Act_RandomRot(1), Act_RandomScale(2), Act_RandomColor(1.5f));
        Debug.Log("Complete");
    }

    //void Act_Chain2() {
    //    float to = Random.Range(-5f, 5f);
    //    EzTween.TweenAct(this, ezEaseType, targetTrans.localPosition.x, to, 1, (v) => {
    //        targetTrans.localPosition = new Vector3(v, 0, 0);
    //    }, () => {
    //        Debug.Log("Complete_Act_Chain_pos");
    //        float to2 = Random.Range(1f, 5f);
    //        EzTween.TweenAct(targetTrans, ezEaseType, targetTrans.localScale.x, to2, 1, (v) => {
    //            targetTrans.localScale = Vector3.one * v;
    //        }, () => {
    //            Debug.Log("Complete_Act_Chain_scale");
    //            //
    //            EzTween.TweenAct(this, ezEaseType, targetTrans.localPosition.x, Random.Range(-5f, 5f), 1, (v) => {
    //                targetTrans.localPosition = new Vector3(v, 0, 0);
    //            }, () => {
    //                Debug.Log("Complete_Act_Chain_pos");
    //            });
    //        });
    //    });
    //}

    //IEnumerator Act_Chain3() {
    //    Vector3 to1 = Random.insideUnitSphere * Random.Range(0, 5f);
    //    yield return EzTween.TweenLocalPosition(targetTrans, EzEaseType.Linear, to1, 1);

    //    yield return new WaitForSeconds(1.0f); // delay

    //    Vector3 to2 = Vector3.one * Random.Range(1f, 5f);
    //    yield return EzTween.TweenScale(targetTrans, EzEaseType.Linear, to2, 1);

    //    Debug.Log("complete");
    //}

    Tween tween = null;
    async Task Act_TweenRun()
    {
        tween?.Cancel();

        if (tween == null)
        {
            tween = new Tween();
        }
        float to = Random.Range(-5f, 5f);
        await tween.StartTween(ezEaseType, targetTrans.localPosition.x, to, 1, (v) =>
        {
            targetTrans.localPosition = new Vector3(v, 0, 0);
        });
        Debug.Log("Complete_Act_RandomPosition");
    }

    [SerializeField] Rect drawRect = new Rect(10,10,200,200);
    private void OnGUI() {
        GUILayout.BeginArea(drawRect);
        if (GUILayout.Button("Act_ToScale"))
        {
            _ = Act_ToScale();
        }
        if (GUILayout.Button("Act_RandomPosition")) {
            _ = Act_RandomPosition();
        }
        if (GUILayout.Button("Act_RandomScale")) {
            _ = Act_RandomScale();
        }
        if (GUILayout.Button("Act_RandomRot")) {
            _ = Act_RandomRot();
        }
        //if (GUILayout.Button("Act_ToScale")) {
        //    Act_ToScale();
        //}
        if (GUILayout.Button("Act_RandomScaleAnimCurve")) {
            _ = Act_RandomScaleAnimCurve();
        }
        if (GUILayout.Button("Act_RandomColor")) {
            _ = Act_RandomColor();
        }
        if (GUILayout.Button("parallel: Act_RandomPosition / Act_RandomScale")) {
            _ = Act_RandomPosition();
            _ = Act_RandomScale();
            _ = Act_RandomColor();
        }

        if (GUILayout.Button("chain: Act_Chain"))
        {
            _ = Act_Chain();
        }
        if (GUILayout.Button("ActPara"))
        {
            _ = ActPara();
        }
        //if (GUILayout.Button("chain: Act_Chain2")) {
        //    Act_Chain2();
        //}
        //if (GUILayout.Button("chain: Act_Chain3")) {
        //    StartCoroutine(Act_Chain3());
        //}

        GUILayout.Space(20);
        if (GUILayout.Button("Act_TweenRun"))
        {
            _ = Act_TweenRun();
        }
        if (GUILayout.Button("Cancel_Act_Tween"))
        {
            tween?.Cancel();
        }

        GUILayout.Space(20);

        if (GUILayout.Button("CancelAll"))
        {
            EzTween.CancelAll();
        }

        GUILayout.EndArea();
    }
}
