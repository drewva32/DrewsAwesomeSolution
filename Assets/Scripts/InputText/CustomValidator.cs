using UnityEngine;

/// <summary>
/// Create your validator class and inherit TMPro.TMP_InputValidator 
/// Note that this is a ScriptableObject, so you'll have to create an instance of it via the Assets -> Create -> Input Field Validator 
/// </summary>
[CreateAssetMenu(fileName = "Input Field Validator", menuName = "Input Field Validator")]
public class CustomValidator : TMPro.TMP_InputValidator
{
    /// <summary>
    /// Override Validate method to implement your own validation
    /// </summary>
    /// <param name="text">This is a reference pointer to the actual text in the input field; changes made to this text argument will also result in changes made to text shown in the input field</param>
    /// <param name="pos">This is a reference pointer to the input field's text insertion index position (your blinking caret cursor); changing this value will also change the index of the input field's insertion position</param>
    /// <param name="ch">This is the character being typed into the input field</param>
    /// <returns>Return the character you'd allow into </returns>
    public override char Validate(ref string text, ref int pos, char ch)
    {
        Debug.Log("tried to validate");
        if ( ch == 44 || ch == 45 || ch == 32 ||System.Char.IsDigit(ch))
        {
            // if ((int)ch == 45 && pos != 0 && text.Substring(pos - 1) == "-" )
            //     return '\0';
            text = text.Insert(pos,ch.ToString());
            pos++;
            return ch;
        }

        if (ch == '\n')
        {
            text = text.Insert(pos,ch.ToString());
            pos += ch;
            return ch;
        }

        return '\0';
    }
}