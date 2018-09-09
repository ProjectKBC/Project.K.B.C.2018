using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralNB : MonoBehaviour {
    
    public float shotSPD = 1; // 弾の速度

    private void Update ()
    {
        Vector3 tmp = this.transform.position;
        tmp.z += shotSPD;
        this.transform.position = tmp;

        if(this.transform.position.z >= 10)
        {
            Destroy(gameObject);
        }
	}
}
