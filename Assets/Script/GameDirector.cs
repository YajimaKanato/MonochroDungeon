using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [Header("StageClear")]
    [SerializeField]
    Text _text;

    [Header("StageNumber")]
    [SerializeField]
    int num;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (GoalforPlayer.IsGoal && GoalforClone.IsGoal)
        {
            _text.GetComponent<Text>().text = "Stage" + num + " Clear!";
        }
    }
}
