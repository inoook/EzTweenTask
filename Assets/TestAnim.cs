using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �t���[���������Ȃ����m�F�̂��߂̃_�~�[�A�j��
/// </summary>
public class TestAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.LogWarning("Update");
        this.transform.Rotate(new Vector3(0, Time.deltaTime * 20, 0));
    }
}
