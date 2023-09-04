using System.Collections;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
	public TextMeshProUGUI textComponent;
	public string[] lines;
	public float textSpeed;

	public int index;
	[SerializeField] GameObject dialogueBox;
	[SerializeField] Button continueButton;

	void Start()
	{
		StartCoroutine(WaitForExplanation());
	}
	void StartExplanation()
	{
		//index = 0;
		StartCoroutine(TypeExplanation());
	}
	// continue button to continue dialogue
	public void ContinueExplanation()
	{
		if (index < lines.Length - 1)
		{
			index++;
			textComponent.text = string.Empty;
			StartCoroutine(TypeExplanation());
		}
		else
		{
			dialogueBox.SetActive(false);
		}
	}

	// skip button to skip entire dialogue and load level scene

	private IEnumerator TypeExplanation()
	{
		continueButton.interactable = false;
		foreach (char c in lines[index].ToCharArray())
		{
			textComponent.text += c;
			yield return new WaitForSeconds(textSpeed);
		}
		continueButton.interactable = true;
	}

	private IEnumerator WaitForExplanation()
	{
		continueButton.interactable = false;
		yield return new WaitForSeconds(.2f);
		dialogueBox.SetActive(true);
		textComponent.text = string.Empty;
		index = 0;
		yield return new WaitForSeconds(.5f);
		StartExplanation();
	}
}