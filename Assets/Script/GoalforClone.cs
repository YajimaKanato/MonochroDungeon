using UnityEngine;

public class GoalforClone : MonoBehaviour
{
    [Header("Target")]
    [SerializeField]
    Transform _clone;

    static bool _isGoal = false;
    public static bool IsGoal {  get { return _isGoal; } }

    // Update is called once per frame
    void Update()
    {
        if (_clone != null)
        {
            if (Vector3.Distance(transform.position, _clone.position) < 0.05f)
            {
                _isGoal = true;
            }
        }
    }
}
