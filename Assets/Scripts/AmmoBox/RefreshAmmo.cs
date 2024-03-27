using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RefreshAmmo : MonoBehaviour
{
    public bool isActive;
    private Vector3 MovePoint;
    [SerializeField]private float rotationSpeed;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.CompareTag("Player"))
        {
            var camera = other.GetComponentInChildren<Camera>();
            var _player = camera.GetComponentInChildren<Weapon>();
            _player.MagazineSize += 120;
            gameObject.SetActive(false);
            isActive = false;
        }
    }
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
}
