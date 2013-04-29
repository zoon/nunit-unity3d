NUnitLite 0.7 Test Runner for Unity3D 4.x
======================================

Overview
--------

This is a test runner for [NUnitLite][1], that redirects test results
to [Unity3D][2] console.

Works under full AOT condition (IOS etc.). 

After compilation of C# files Unity gives you two assemblies:

- Assembly-CSharp-firstpass.dll for 'Plugins' and '[Pro] Standard Assets'
- Assembly-CSharp.dll for other scripts


Then, if you want to have tests in both places - you should call
UnityTestRunner.RunTests() from both places. One call per assembly
is enough, but you can call it as many times as you want - all
calls after first are ignored.

All of the above is correct for UnityScript (*.js) scripts too.

You can use 'MonoBahavior' classes for tests, but Unity gives you one
harmless warning per class. Using special Test classes would be a
better idea.

Copyright (C) 2013 by Andrew Zhilin <a href="mailto:andrew_zhilin@yahoo.com">andrew_zhilin@yahoo.com</a>

[1]:http://nunitlite.org
[2]:http://unity3d.com
