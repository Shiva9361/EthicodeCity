using UnityEngine;

public class DialogueSetup : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public Sprite Question1;
    public Sprite Question2;

    void Start()
    {
        // Assign question images
        dialogueManager.questionImages = new Sprite[] { Question1, Question2 };
    }
}
