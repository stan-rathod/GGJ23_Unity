using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data_Dialogues", menuName = "ScriptableObjects/NarrativeDialogue", order = 1)]
public class NarrativeDialogue : ScriptableObject
{
    public List<string> dialogues = new List<string>();
}
