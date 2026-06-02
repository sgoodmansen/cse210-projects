using System.IO.Pipelines;

public class Scripture
{
    private Reference _reference;                      //attribute to store the scripture reference object called _reference
    private List<Word> _wordsList = new List<Word>();   //create new list of Word objects

    public Scripture(Reference reference, string text)  //Constructor
    {
        _reference = reference;
        string[] words = text.Split(' ');               //split by whitespace between words, store in words array
        foreach (string word in words)
        {
            Word newWord = new Word(word);             //convert string text to Word object
            _wordsList.Add(newWord);                   //add Word object to list
        }
        
    }

    public void HideRandomWords(int numberToHide)
    {
        Random random = new Random();
        
        int hideCount = 0;
        int totalVisible = 0;

        if (numberToHide > 3)
        {
            numberToHide = 3;
        }

        if (numberToHide < 1)
        {
            numberToHide = 1;
        }

        foreach (Word word in _wordsList)
        {
            if (!word.isHidden())                   //checks if value is false 
            {
                totalVisible ++;                    //if word is visible, add to count
            }
        }

        if (totalVisible < numberToHide)
        {
            numberToHide = totalVisible;
        }

        while (hideCount < numberToHide)
        {
            int index = random.Next(_wordsList.Count);

            if (!_wordsList[index].isHidden())          //if word is not hidden
            {
                _wordsList[index].Hide();               //change to hidden
                hideCount ++;                           //increase the count of hidden items
            }
            
        }
    }

    public bool AllHidden()
    {
        foreach (Word word in _wordsList)           //check is word is hidden
        {
            if(!word.isHidden())
            {
                return false;                       //if any word is hidden, return false
            }
        }

        return true;                                //if no words hidden, return true
    }

    public string DisplayScripture()
    {
        string result = _reference.BuildReference() + " ";              //retrieve the scripture reference
        
        foreach (Word word in _wordsList)                               //retrieve the next word in the list
        {
            result += word.GetWordText() + " ";                         //send to Word.GetWordText() to get rendered version of word and add rendered word to the result string
        }
        
        return result;
    }
}