// This file is an example of how to make a function to read in a CSV file and go through each line.
// This file will not work on it's own. This is just an example.
    private void LoadCustomPhrases(string filePath)
    {
        // Read in the entire file
        string[] allLines = File.ReadAllLines(filePath);
        Debug.Log("All lines read from: " + filePath);
        // Creating phrase list
        foreach (string s in allLines)
        {
            // Create a new phrase
            Phrase p = new Phrase();
            // Split the line with comma as the delimiter.
            string[] splitLine = s.Split(",");
            // [0] = Category. [1] = Phrase
            p.category = splitLine[0];
            p.phrase = splitLine[1]; 

            phrases.Add(p);
        }

        Debug.Log("Created all phrases. Total phrases: " + phrases.Count);
    }
