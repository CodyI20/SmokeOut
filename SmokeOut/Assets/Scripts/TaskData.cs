using UnityEngine;

[CreateAssetMenu(fileName ="New Task", menuName = "Task System/Task Data")]
public class TaskData : ScriptableObject
{
    public string taskName;
    public string taskDescription;
    public bool isComplete;
}
