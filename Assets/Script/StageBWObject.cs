using UnityEngine;

public class StageBWObject : MonoBehaviour
{
    BoxCollider2D _box2d;

    private void Start()
    {
        _box2d = GetComponent<BoxCollider2D>();
    }

    public void ColliderChange()
    {
        if (_box2d.isTrigger)
        {
            _box2d.isTrigger = false;
        }
        else
        {
            _box2d.isTrigger = true;
        }
    }
}
