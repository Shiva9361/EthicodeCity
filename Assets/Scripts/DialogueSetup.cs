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
            name = "Jhon",
            image = johnImage,
            dialogueLines = new string[] {
                "Welcome, Mayor! I am the Architect of this city.",
                "You are the new Mayor of this city. and must rebuild it after unforeseen circumstances caused its collapse.",
                "The previous mayor has been arrested, and it’s now up to you to restore the city and uncover the reasons for its downfall.",
                "Here’s what you’ve got to work with: The government has allotted you ₹10,000, and you have 10 minutes to rebuild the city.",
                "Each building’s cost and construction time are displayed above. Plan your resources wisely, Mayor!",
                "Factories are backbone of the city’s growth. They generate AI Tokens that can reduce building time, but be careful—AI can lead to unexpected consequences.",
                "Mayor, as you build, you'll uncover ethics on the right top corner. Collect them to earn rewards and unlock more buildings!",

                // bank scene
                "Building a bank? ",
                " Here’s a choice: Using AI, you can complete it in just 1 second instead of the usual 100 seconds.",
                "Tempting, isn’t it? But be cautious—AI can lead to issues like security breaches.",
                "Warning! The bank you built using AI has been robbed due to a code breach. Always remember: Faster isn’t always better.",

                //earthquake scene
                "Mayor, an earthquake is predicted in nearby areas.",
                "Ensure your buildings are strong enough to withstand it. AI-built structures may collapse if not tested properly.",
                "Disaster has struck! ",
                "Look at the damage—AI-built structures have collapsed because they weren’t properly tested or quality-assured.",
                //happiness meter
                "Great progress, Mayor! Keep an eye on the Citizen Happiness Meter. The more buildings you construct, the happier they’ll be.",
                //documentation index
                "Don’t forget the Documentation Index. Building at least 5 offices ensures the city’s systems are well-documented and future-proof.",
                "Factories are running at 50% efficiency due to lack of proper code comments. Play the Debugging Challenge mini-game to improve efficiency.",
                "Well done, Mayor! A city built with strong values and responsibility will stand the test of time. Let’s make this city the best it can be!",
            }
        };


    }
}
