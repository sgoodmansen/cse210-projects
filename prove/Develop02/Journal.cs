public class Journal
{
    public List<Entry> entries = new List<Entry>();

    public void AddEntry(Entry newEntry)
    {
        entries.Add(newEntry);
    }

    public void DisplayEntries()
    {
        foreach (Entry entry in entries)
        {
            Console.WriteLine();
            Console.WriteLine($"Date: {entry.entryDate} - Prompt: {entry.prompt}");
            Console.WriteLine(entry.response);
            Console.WriteLine("-----------------------------------------------------------------------------------");
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in entries)
            {
                outputFile.WriteLine($"{entry.entryDate}|{entry.prompt}|{entry.response}");
            }
        }  
    }

    public void LoadFromFile(string filename)
    {
        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            string[] parts = line.Split("|");

            Entry entry = new Entry();
            entry.entryDate = parts[0];
            entry.prompt = parts[1];
            entry.response = parts[2];

            entries.Add(entry);

        }
    }

    public int GetCount()
    {
        return entries.Count;
    }
}