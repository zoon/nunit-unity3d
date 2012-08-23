#region Usings

using UnityEngine;


#endregion

public class CsharpScriptsTestDriver : MonoBehaviour
{
    // ReSharper disable UnassignedField.Global
    // ReSharper disable InconsistentNaming
    public bool runTests;
    // ReSharper restore InconsistentNaming
    // ReSharper restore UnassignedField.Global


    // ReSharper disable UnusedMember.Local
    private void Start()
        // ReSharper restore UnusedMember.Local
    {
        if (runTests)
            NUnitLiteUnityRunner.RunTests();
    }
}
