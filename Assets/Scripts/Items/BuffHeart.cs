using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHeart : VCNVMonoBehaviour
{
    [SerializeField] protected int buffHp = 1;
    public int BuffHp=>buffHp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Xóa obj");
            //Destroy(transform.parent.gameObject);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
