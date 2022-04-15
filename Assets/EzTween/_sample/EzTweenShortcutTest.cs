using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ez;

public class EzTweenShortcutTest : MonoBehaviour
{

    [SerializeField] EzEaseType ezEaseType = EzEaseType.Linear;
    [SerializeField] Transform targetTransA = null;
    [SerializeField] Transform targetTransB = null;


    void Act_RandomPosition() {
        float timeA = Random.Range(0.5f, 2f);
        Vector3 toA = Random.insideUnitSphere * Random.Range(0, 5f);
        _ = EzTween.TweenLocalPosition(targetTransA, ezEaseType, toA, timeA, () => {
            Debug.Log("Complete_Act_RandomPositionA");
        });

        float timeB = Random.Range(0.5f, 2f);
        Vector3 toB = Random.insideUnitSphere * Random.Range(0, 5f);
        _ = EzTween.TweenLocalPosition(targetTransB, ezEaseType, toB, timeB, () => {
            Debug.Log("Complete_Act_RandomPositionB");
        });
    }

    void Act_RandomColor() {
        float timeA = 1;
        Color toA = Random.ColorHSV();
        Renderer rendererA = targetTransA.GetComponent<Renderer>();
        _ = EzTween.TweenRendererColor(rendererA, ezEaseType, toA, timeA, () => {
            Debug.Log("Complete_Act_RandomColorA");
        });

        float timeB = 2;
        Color toB = Random.ColorHSV();
        Renderer rendererB = targetTransB.GetComponent<Renderer>();
        _ = EzTween.TweenRendererColor(rendererB, ezEaseType, toB, timeB, () => {
            Debug.Log("Complete_Act_RandomColorB");
        });
    }

    void Cancel() {

    }
    void CancelAll() {
        EzTween.CancelAll();
    }

    [SerializeField] Rect drawRect = new Rect(10, 10, 200, 200);
    private void OnGUI() {
        GUILayout.BeginArea(drawRect);
        if (GUILayout.Button("Act_RandomPosition")) {
            Act_RandomPosition();
        }
        if (GUILayout.Button("Act_RandomColor")) {
            Act_RandomColor();
        }
        if (GUILayout.Button("Cancel")) {
            Cancel();
        }
        if (GUILayout.Button("CancelAll")) {
            CancelAll();
        }
        GUILayout.EndArea();
    }
}
