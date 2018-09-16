using UnityEngine;

public class GeneralNB : MonoBehaviour
{
    [SerializeField]
    private float shotSpeed = 60;
    private Vector3 shotStartPos;
    [SerializeField]
    private float shotRange = 70;

    private void Start()
    {
        shotStartPos = this.transform.position;
    }

    private void Update()
    {
        this.transform.position += Vector3.up * shotSpeed * Time.deltaTime;

        if (shotRange < Mathf.Abs(shotStartPos.y - this.transform.position.y))
        {
            Destroy(gameObject);
        }
    }
}
