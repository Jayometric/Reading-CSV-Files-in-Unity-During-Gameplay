using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class CheckSelectedFileExists : MonoBehaviour
{
    public FileSelector fs;
    public Button startButton;
    public GameObject errorBox;
    public TextMeshProUGUI fileErrorText;
    public NumberOfRoundsSlider numberOfRoundsSlider;
    
    // Update is called once per frame
    void Update()
    {
        CheckIfFileIsValid();
    }

    // Check if the file is valid
    public void CheckIfFileIsValid()
    {
        if (fs.selectedFilePath != "")
        {
            if (File.Exists(fs.selectedFilePath))
            {
                // Check if the file has enough contents for a game (has 2 phrases)
                if (fs.GetNumberOfPhrasesInSelectedFile() < 2)
                {
                    //Debug.Log("File does not contain at least 2 phrases. Game cannot be played.");
                    if (fs.GetNumberOfPhrasesInSelectedFile() >= 0)
                    {
                        errorBox.SetActive(true);
                        fileErrorText.text = "Error: File does not contain at least 2 phrases. There must be at least 2 phrases, 1 for the normal round, and 1 for the final round.";
                        numberOfRoundsSlider.SetMaxRoundValue(1);
                        numberOfRoundsSlider.SetValue(1);
                    }
                    
                    if(fs.GetNumberOfPhrasesInSelectedFile() < 0)
                    {
                        errorBox.SetActive(true);
                        fileErrorText.text = "Error: File is currently open. Close the file.";

                    }
                    startButton.interactable = false;
                }
                // Otherwise the game can be played (has more than 2 phrases)
                else
                {
                    startButton.interactable = true;

                    // If the error is still active, turn it off
                    if (errorBox.activeInHierarchy)
                    {
                        errorBox.SetActive(false);
                        fileErrorText.text = "";
                        numberOfRoundsSlider.SetMaxRoundValue();
                    }
                }
            }
            // File does not exist
            else
            {
                startButton.interactable = false;
                errorBox.SetActive(true);
                fileErrorText.text = "Error: File not found. Was it deleted / removed?";
            }
        }
        else
        {
            // No file selected
            startButton.interactable = false;
        }
    }
}
