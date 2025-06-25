using UnityEngine;

public class GoalforPlayer : MonoBehaviour
{
    [Header("Target")]
    [SerializeField]
    Transform _player;

    static bool _isGoal = false;
    public static bool IsGoal {  get { return _isGoal; } }

    private void Start()
    {
        _isGoal = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            if(Vector3.Distance(transform.position, _player.position) < 0.05f)
            {
                _isGoal = true;
            }
        }
    }
}
