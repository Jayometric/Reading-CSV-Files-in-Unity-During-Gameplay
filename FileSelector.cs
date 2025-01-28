using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System;
using TMPro;

public class FileSelector : MonoBehaviour
{

    public TMP_Dropdown fileDropdown;
    public List<string> csvFiles = new List<string>();
    public string fileName;
    public string selectedFilePath;
    private string applicationDataPath;
    private string customFilesPath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set the data paths for the application and the custom folder
        applicationDataPath = Application.persistentDataPath;
        customFilesPath = applicationDataPath + "/Custom/";

        // Load all of the CSV files and set the dropdown options
        LoadAllCSVFiles();
    }

    // Get the CSV file names and store them in a List<string>
    public void GetAllCSVFiles()
    {
        // Clear the current list as this code is re-used
        csvFiles.Clear();

        // Attempt to find all of the CSV files
        try
        {
            // Ensure the directory exists, if not, create it. 
            if (!Directory.Exists(customFilesPath))
            {
                Directory.CreateDirectory(customFilesPath);
            }

            // Add an initial value to the list, unnecessary but this is just a user instruction
            csvFiles.Add("Select a custom game");

            // Create a list of all of the files ending with .csv in this directory
            string[] files = Directory.GetFiles(customFilesPath, "*.csv");

            // Go through each file and add their file's name to the csvFiles (NOT THEIR FILE PATH), these will be the dropdown values
            foreach (string file in files)
            {
                csvFiles.Add(Path.GetFileName(file));
            }

            // Check if any files were found
            if (files.Length == 0)
            {
                // If there are no files, disable the dropdown
                fileDropdown.interactable = false;
            }
        }

        // Catch ezceptions for access denied
        catch (UnassignedReferenceException)
        {
            Debug.LogError("Access denied to some directories.");
        }
        // Catch other errors and debug their message
        catch (Exception e)
        {
            Debug.LogError($"An error occurred while accessing files: {e.Message}");
        }
    }

    // Take the csvFiles list and add these as options to the dropdown
    private void SetDropdownOptions()
    {
        // Clear options, as this code is reused. 
        fileDropdown.ClearOptions();
        // Add the list of CSV files top the dropdown options
        fileDropdown.AddOptions(csvFiles);

        // Check that the filedropdown has more than 1 value (the user instruction should always exist)
        // If it did not have values added, make it not able to be interacted with
        // Note: If you're not using the user instruction line, change this to 0 or match your scenario.
        if (fileDropdown.options.Count > 1)
        {
            fileDropdown.interactable = true;
        }
    }

    // Function used for On Value Changed of the dropdown
    public void OnCsvSelected()
    {
        // Make sure the default first value to tell them what to do is not selected
        if (fileDropdown.value != 0)
        {
            // Get the name of the file from the option selected
            fileName = fileDropdown.options[fileDropdown.value].text;
            // Make the file file path for the file
            selectedFilePath = customFilesPath + fileName;
        }
        // If they select the first value, remove the selectedFilePath and fileName. This is an instruction not a file.
        else
        {
            fileName = "";
            selectedFilePath = "";
        }
    }

    // Used to open the folder directory specified
    public void OpenCustomFilesPath()
    {
        Application.OpenURL(customFilesPath);
    }

    public void LoadAllCSVFiles()
    {
        // Remove current selection
        fileName = "";
        selectedFilePath = "";
        GetAllCSVFiles();
        SetDropdownOptions();
    }

    public void SetSelectedFileName()
    {
        PlayerPrefs.SetString("Custom File Path", selectedFilePath);
    }

    public int GetNumberOfPhrasesInSelectedFile()
    {
        try
        {
            return File.ReadAllLines(selectedFilePath).Length;
        }
        catch (Exception e)
        {
            Debug.Log("Exception on file read: " + e.Message);
        }
        return -1;
    }
}
