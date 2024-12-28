using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [Header("속도 세팅")]
    [Tooltip("배경 스크롤링 속도를 설정합니다.")]
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
