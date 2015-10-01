using UnityEngine;
using UnityEditor;
using System.Collections;

public class SmartClone : ScriptableWizard
{
    public int numberOfCopies = 1;
    public Vector3 cloneTranslation;
    public Vector3 cloneRotation;
    public bool incrementalRotation = false;
    public Vector3 cloneScale = new Vector3(1, 1, 1);
    public bool incrementalScale = false;
    public bool uniqueCloneNames = true;
    public bool AddToParent = true;
    public bool addOriginalToParent = false;
    public string parentName = "New Parent";


    [MenuItem("GameObject/Cloning/Smart clone")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Smart Clone", typeof(SmartClone),
            "Clone", "Reset");

        //If you don't want to use the secondary button simply leave it out:
        //ScriptableWizard.DisplayWizard("Create Light", typeof(WizardCreateLight));
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        if (!Selection.activeGameObject)
            isValid = false;
        else
            isValid = true;
    }

    void OnWizardCreate()
    {
        bool newParent = false;

        GameObject parentObject = GameObject.Find(parentName);

        if (!parentObject)
        {
            parentObject = new GameObject(parentName);
            newParent = true;
        }

        if (addOriginalToParent)
        {
            Selection.activeGameObject.transform.parent = parentObject.transform;
        }

        GameObject currentSelection = Selection.activeGameObject;

        for (int i = 0; i < numberOfCopies; i++)
        {
            GameObject clone = Instantiate(currentSelection) as GameObject;

            if (uniqueCloneNames)
                clone.name = currentSelection.name + i;
            else
                clone.name = currentSelection.name;

            if (newParent && !addOriginalToParent)
            {
                clone.transform.Translate(cloneTranslation * i);

                if (incrementalRotation)
                    clone.transform.Rotate(cloneRotation * i);
                else
                    clone.transform.Rotate(cloneRotation);

                if (cloneScale != Vector3.one && cloneScale != Vector3.zero)
                {
                    if (incrementalScale)
                        clone.transform.localScale = (currentSelection.transform.localScale + cloneScale) * i;
                    else
                        clone.transform.localScale = (currentSelection.transform.localScale + cloneScale);
                }
                else
                    clone.transform.localScale = currentSelection.transform.localScale;
            }
            else
            {
                clone.transform.Translate(cloneTranslation * (i + 1));

                if (incrementalRotation)
                    clone.transform.Rotate(cloneRotation * (i + 1));
                else
                    clone.transform.Rotate(cloneRotation);

                if (cloneScale != Vector3.one && cloneScale != Vector3.zero)
                {
                    if (incrementalScale)
                        clone.transform.localScale = (currentSelection.transform.localScale + cloneScale) * (i + 1);
                    else
                        clone.transform.localScale = (currentSelection.transform.localScale + cloneScale);
                }
                else
                    clone.transform.localScale = currentSelection.transform.localScale;

            }

            if (AddToParent)
                clone.transform.parent = parentObject.transform;

        }
    }

    void OnWizardOtherButton()
    {
        numberOfCopies = 1;
        cloneTranslation = new Vector3(0, 0, 0);
        cloneRotation = new Vector3(0, 0, 0);
        cloneScale = new Vector3(1, 1, 1);
        uniqueCloneNames = true;
        AddToParent = true;
        addOriginalToParent = false;
        parentName = "New Parent";
    }

}