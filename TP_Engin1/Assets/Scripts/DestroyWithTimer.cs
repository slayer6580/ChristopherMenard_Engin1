using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithTimer : MonoBehaviour
{
    [SerializeField]
    private int m_timeBeforeDestroy;
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine("SelfDestruct");
    }

    private IEnumerator SelfDestruct() 
    {
        yield return new WaitForSeconds(m_timeBeforeDestroy);
        Destroy(gameObject);
    }

}
