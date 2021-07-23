using UnityEngine;

public class ExplosionDestroyer : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
