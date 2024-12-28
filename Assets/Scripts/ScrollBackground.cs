using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [Header("�ӵ� ����")]
    [Tooltip("��� ��ũ�Ѹ� �ӵ��� �����մϴ�.")]
    public float speed;

    MeshRenderer mr;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        mr.material.mainTextureOffset += new Vector2(GameManager.instance.CalculateSpeed(speed) * Time.deltaTime, 0);
    }
}
