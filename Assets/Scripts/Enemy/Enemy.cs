using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int _killPoints;

    private Player _player;
    private PointsHandler _pointsHandler;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _pointsHandler = FindObjectOfType<PointsHandler>();
    }

    public void AddPoints()
    {
        //add score
        _player?.AddScore(_killPoints);

        //show points
        _pointsHandler?.ChangeSprite(_killPoints, transform.position);
    }

}
