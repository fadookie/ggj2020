using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackInterface : MonoBehaviour
{
    public GameObject panel;
    public Text text;
    public Dropdown dropdown;
    public InputField numberfield;

    public bool debug;

    // Start is called before the first frame update
    void Start()
    {

        //example add

        if (debug)
        {
            Dropdown entity = CreateDropDown("entity1", "test2", "anotherone");
            Dropdown action = CreateDropDown("open", "close");
            InputField entry = CreateField();


            ClearHackerInterface();
            HideHackingInterface();
            SetupConditional("if @ then @ ", new List<Dropdown>(new Dropdown[] { action, entity }), new List<InputField>(new InputField[] { entry }));
            ShowHackingInterface(0, 0);
        }

    }



    // Update is called once per frame
    void Update()
    {

        if (debug && Input.GetKeyDown(KeyCode.Space))
        {
            if (panel.activeSelf)
                HideHackingInterface();
            else
                ShowHackingInterface(0, 0);
        }

    }

    /// <summary>
    /// craft a conditional string of text and dropdowns and numberfields and add them to the pending popup
    ///  this will take in a format string in the form of " text @ then change x to #"
    /// where @ will be replaced by the next dropdowns
    ///and # will be replaced by number fields
    /// </summary>
    /// <param name="conditional"></param>
    /// <param name="dropdowns"></param>
    /// <param name="numfields"></param>
    public void SetupConditional(string conditional, List<Dropdown> dropdowns, List<InputField> numfields)
    {

        //string to display if there is no object provided but is being asked for
        string formatString = "no[{0}{1}]";

        int found_start = 0;
        int found_end = 0;
        int dropdownIndex = 0;
        int numfieldIndex = 0;

        for (int i = 0; i < conditional.Length; i++)
        {
            char c = conditional[i];
            found_end = i;
            if (c == '@' || c == '#') 
            {
                //if the char is a special character escape the text and add it to the hacking
                //interface
                string textChunk = conditional.Substring(found_start, found_end - found_start);
                Text textChunkUI = Instantiate<Text>(text);
                AddChildToParent(panel.transform, textChunkUI.transform);
                textChunkUI.text = textChunk;
                found_start = i + 1;

                switch (c)
                {
                    case '@':


                        if (dropdowns.Count > dropdownIndex)
                        {
                            Dropdown dropdownToUse = dropdowns[dropdownIndex];
                            AddChildToParent(panel.transform, dropdownToUse.transform);
                            dropdownIndex++;
                        }
                        else
                        {
                            Text errorText = Instantiate<Text>(text);
                            errorText.text = string.Format(formatString, "drop", dropdownIndex);
                            AddChildToParent(panel.transform, errorText.transform);
                        }

                        break;
                    case '#':

                        if (numfields.Count > numfieldIndex)
                        {
                            InputField numfieldToUse = numfields[numfieldIndex];
                            AddChildToParent(panel.transform, numfieldToUse.transform);
                            numfieldIndex++;
                        }
                        else
                        {
                            Text errorText = Instantiate<Text>(text);
                            errorText.text = string.Format(formatString, "field", numfieldIndex);
                            AddChildToParent(panel.transform, errorText.transform);
                        }
                        break;

                    default: break;
                }
            }


        }

        //add the last bit of text to the hacking interface if any
        string lastChunk = conditional.Substring(found_start, Mathf.Max(0, found_end - found_start));
        Text lastTestChunk = Instantiate<Text>(text);
        AddChildToParent(panel.transform, lastTestChunk.transform);
        lastTestChunk.text = lastChunk;


    }

    /// <summary>
    ///  remove all children from the hacking interface essentially wiping it clean
    /// </summary>
    public void ClearHackerInterface()
    {
        Transform panelTransform = panel.transform;
        foreach (Transform child in panelTransform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    ///add a child to a parent using the game object transforms
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="child"></param>
    private void AddChildToParent(Transform parent, Transform child)
    {
        child.SetParent(parent);
    }

    public InputField CreateField(float defaultValue = 0)
    {
        InputField numfieldtoreturn = Instantiate<InputField>(numberfield);

        numfieldtoreturn.text = "" + defaultValue;

        return numfieldtoreturn;
    }

    /// <summary>
    /// Create a dropdown with the given arguments
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public Dropdown CreateDropDown(params string[] args)
    {

        Dropdown drop = Instantiate<Dropdown>(dropdown);
        drop.ClearOptions();
        drop.AddOptions(new List<string>(args));
        return drop;
    }

    //-------------code for popup

     /// <summary>
     /// show the hacking interface panel and place it at the given position
     /// </summary>
     /// <param name="x"></param>
     /// <param name="y"></param>
    public void ShowHackingInterface(float x, float y)
    {

        panel.SetActive(true);
        panel.transform.position.Set(x, y, panel.transform.position.z);

    }

    /// <summary>
    /// Hide the hacking interface lol
    /// </summary>
    public void HideHackingInterface()
    {
        panel.SetActive(false);
    }
}
