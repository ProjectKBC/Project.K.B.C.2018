using UnityEngine;

public class VeronicaNB : MonoBehaviour
{
    public string shooter;

    private void Awake()
    {
        this.shooter = "";
    }

    private void Update()
    {
        switch (this.shooter)
        {
            case "Player1":
                this.transform.position = PlayerManager.GameObjectPl1.transform.position;
                break;

            case "Player2":
                this.transform.position = PlayerManager.GameObjectPl2.transform.position;
                break;

            case "none":
                Destroy(gameObject);
                break;
        }
    }
}