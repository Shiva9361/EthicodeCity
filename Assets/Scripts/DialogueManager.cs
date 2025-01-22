using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;
    public Image specialImage; // Image to display for specific dialogue

    public float textSpeed;
    public Character[] characters;  // Array of 4 characters (Alex, Elena, Sam, John)

    private int currentLineIndex = 0;
    private int currentCharacterIndex = 0;

    void Start()
    {
        StartDialogue();
        specialImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (dialogueText.text == characters[currentCharacterIndex].dialogueLines[currentLineIndex])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = characters[currentCharacterIndex].dialogueLines[currentLineIndex];
            }
        }
    }

    void StartDialogue()
    {
        currentLineIndex = 0;
        currentCharacterIndex = 0;
        DisplayCharacterDialogue();
    }

    void DisplayCharacterDialogue()
    {
        nameText.text = characters[currentCharacterIndex].name;
        characterImage.sprite = characters[currentCharacterIndex].image;
        dialogueText.text = string.Empty;

        CheckSpecialDialogue();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in characters[currentCharacterIndex].dialogueLines[currentLineIndex].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (currentLineIndex < characters[currentCharacterIndex].dialogueLines.Length - 1)
        {
            currentLineIndex++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            currentCharacterIndex++;
            if (currentCharacterIndex < characters.Length)
            {
                currentLineIndex = 0;
                DisplayCharacterDialogue();
            }
            else
            {
                gameObject.SetActive(false);  // End the dialogue
            }
        }
    }
    void CheckSpecialDialogue()
    {
        // Check if the current line is the special dialogue
        string currentDialogue = characters[currentCharacterIndex].dialogueLines[currentLineIndex];
        if (currentDialogue == "You are the new Mayor of this city. and must rebuild it after unforeseen circumstances caused its collapse.")
        {
            Debug.Log("Special image activated!");
            specialImage.gameObject.SetActive(true); // Show the special image
        }
        else
        {
            Debug.Log("Special image deactivated!");
            specialImage.gameObject.SetActive(false); // Hide the special image if it's not the special line
        }
    }
}
