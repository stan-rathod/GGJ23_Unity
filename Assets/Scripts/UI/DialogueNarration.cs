using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DialogueNarration : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private NarrativeDialogue dialogueData;
    private IEnumerator coroutine;

    private void Start()
    {
        dialogueText.text = "";
        coroutine = DialogueSequence();
        StartCoroutine(coroutine);
    }

    IEnumerator DialogueSequence()
	{
		// Start after one second delay (to ignore Unity hiccups when activating Play mode in Editor)
		yield return new WaitForSeconds(1);

        for (int i = 0; i < dialogueData.dialogues.Count; i++)
        {
            dialogueText.DOFade(1, 0.1f);
            dialogueText.text = dialogueData.dialogues[i];
            yield return new WaitForSeconds(2.0f);
            dialogueText.DOFade(0, 0.1f);
            yield return new WaitForSeconds(0.2f);
        }
	}

    public void Skip()
    {
        StopCoroutine(coroutine);
        dialogueText.text = "Done";
    }
}
