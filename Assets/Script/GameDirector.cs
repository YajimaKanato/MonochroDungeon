using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [Header("GroupInCanvas")]
    [SerializeField]
    GameObject _canvas;

    [Header("PauseGroup")]
    [SerializeField]
    GameObject _pause;

    static bool _isPausing = false;
    public static bool IsPausing { get { return _isPausing; } }

    private void Start()
    {
        _isPausing = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (GoalforPlayer.IsGoal && GoalforClone.IsGoal)
        {
            ObjectActive(_canvas);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _pause.activeSelf == false)
        {
            _isPausing = true;
            ObjectActive(_pause);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _pause.activeSelf == true)
        {
            _isPausing = false;
            ObjectInactive(_pause);
        }
    }

    void ObjectActive(GameObject obj)
    {
        obj.SetActive(true);
    }

    void ObjectInactive(GameObject obj)
    {
        obj.SetActive(false);
    }
}
