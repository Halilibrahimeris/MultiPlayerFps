using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RefreshParent : MonoBehaviour
{
    [HideInInspector]public bool isActive;
    private Vector3 MovePoint;
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        isActive = true;
        MovePoint = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        transform.DOMove(MovePoint, 2f).SetLoops(-1, LoopType.Yoyo);
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void SetActiveChild()
    {
        gameObject.SetActive(true);
        isActive = true;
    }
}
