using UnityEngine;

public class DialogueSetup : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public Sprite alexImage;
    public Sprite elenaImage;
    public Sprite samImage;
    public Sprite johnImage;

    void Start()
    {
        // Assign character data
        dialogueManager.characters = new Character[4];

        // Alex
        dialogueManager.characters[0] = new Character()
        {
            name = "Alex",
            image = alexImage,
            dialogueLines = new string[] {
                "Hey, I'm Alex.",
                "It's a beautiful day, isn't it?",
                "Let's go on an adventure!"
            }
        };

        // Elena
        dialogueManager.characters[1] = new Character()
        {
            name = "Elena",
            image = elenaImage,
            dialogueLines = new string[] {
                "Hello, I'm Elena.",
                "I love exploring new places.",
                "Ready to join me?"
            }
        };

        // Sam
        dialogueManager.characters[2] = new Character()
        {
            name = "Sam",
            image = samImage,
            dialogueLines = new string[] {
                "Yo, it's Sam here!",
                "I'm all for some action.",
                "Count me in!"
            }
        };

        // John
        dialogueManager.characters[3] = new Character()
        {
            name = "John",
            image = johnImage,
            dialogueLines = new string[] {
                "Hi, I'm John.",
                "I'm excited to be here.",
                "Let's make this an unforgettable trip!"
            }
        };
    }
}
