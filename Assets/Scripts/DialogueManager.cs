using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Image questionImage;           // UI Image to display the question
    public TMP_InputField inputField;     // Input field for user answers
    public Button submitButton;           // Submit button
    public Sprite[] questionImages;       // Array of question images

    private int currentQuestionIndex = 0;

    void Start()
    {
        DisplayQuestion();
        submitButton.onClick.AddListener(HandleSubmit);
    }

    void DisplayQuestion()
    {
        if (currentQuestionIndex < questionImages.Length)
        {
            questionImage.sprite = questionImages[currentQuestionIndex]; // Update the image
            questionImage.enabled = true;  // Make sure the image is visible
            inputField.text = "";          // Clear the input field
        }
        else
        {
            EndQuiz(); // End the quiz if there are no more questions
        }
    }

    void HandleSubmit()
    {
        string userAnswer = inputField.text;

        // Validate user input
        if (!string.IsNullOrWhiteSpace(userAnswer))
        {
            Debug.Log($"Answer for Question {currentQuestionIndex + 1}: {userAnswer}");

            // Move to the next question
            currentQuestionIndex++;
            DisplayQuestion();
        }
        else
        {
            Debug.Log("Please enter an answer before submitting.");
        }
    }

    void EndQuiz()
    {
        Debug.Log("Quiz Completed!");
        questionImage.gameObject.SetActive(false);  // Hide the image
        inputField.gameObject.SetActive(false);     // Hide the input field
        submitButton.gameObject.SetActive(false);   // Hide the submit button
    }
}
