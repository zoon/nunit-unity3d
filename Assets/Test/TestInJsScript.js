import NUnit.Framework;

var runTests : boolean;

function Start ()
{
    if (runTests)
        NUnitLiteUnityRunner.RunTests();
}

@TestFixture
class SomeTestsInJsScripts
 {
     @Test
     function SomeTest()
     {
         Assert.That(42, Is.GreaterThan(40) & Is.LessThan(50));
     }


}


