#region Usings

using UnityEngine;

#endregion

public class CsharpScriptsTestDriver : MonoBehaviour
{
    #region Editor Fields

    public bool RunTests;

    #endregion



    #region Unity Callbacks

    private void Start()
    {
        if (RunTests)
            NUnitLiteUnityRunner.RunTests();
    }

    #endregion

}
