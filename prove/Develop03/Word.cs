using System.IO.Pipelines;

public class Word
{
    private string _text;       //this attribute will be one word from the scripture
    private bool _isHidden;      //this attribute will keep track if the word is hidden

    public Word(string text)    //constructor
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()          //method to change word to hidden
    {
        _isHidden = true;
    }

    public bool isHidden()      //method to check if word is hidden
    {
        return _isHidden;
    }

    public string GetWordText()
    {
        string result = _text;

        if (_isHidden)          //if word is hidden, show the characters as _
        {
            int charCount = result.Length;
            char myChar = '_';
            result = new string(myChar,charCount);         
        }

        return result;          //return result as converted text or original text
        
    }
}