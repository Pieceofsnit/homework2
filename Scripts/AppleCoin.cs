using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCoin : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            Destroy(gameObject);
        }
    }
}
