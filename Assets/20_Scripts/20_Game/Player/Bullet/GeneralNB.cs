using UnityEngine;

public class GeneralNB : MonoBehaviour
{
    [SerializeField]
    private float shotSPD = 1; // 弾の速度

    private void Update ()
    {
        Vector3 tmp = this.transform.position;
        tmp.y += shotSPD * Time.deltaTime;
        this.transform.position = tmp;

        if(this.transform.position.y >= 10)
        {
            Destroy(gameObject);
        }
	}
}
