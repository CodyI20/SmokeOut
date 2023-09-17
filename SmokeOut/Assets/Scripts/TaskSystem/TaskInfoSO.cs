using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskInfoSO", menuName = "ScriptableObjects/TaskInfoSO", order = 1 )]
public class TaskInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]

    public string displayName;

    [Header("Requirements")]
    public int levelRequirement;

    public TaskInfoSO[] taskPrerequisites;

    [Header("Steps")]
    public GameObject[] taskStepPrefabs;

    [Header("Rewards")]
    public int goldReward;
    public int experienceReward;

    //ensure the id is always the name of the Scriptable Object asset
    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty( this );
#endif

    }
}
