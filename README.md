# SLIDDES-Unity-Networking-Web
A collection for web networking in Unity.
SLIDDES © 2022

## About
Hello and thank you for using SLIDDES Software.
SLIDDES Unity Networking Web is a collection for web communication in Unity.

## Installation
You can install it as a package for Unity.

For more information on how to install it:
https://docs.unity3d.com/Manual/upm-ui-giturl.html

## Example
using SLIDDES.Networking.Web

string result = "";
yield return StartCoroutine(WebRequest.GetJson("url", x => result = x));

## Other
For more information or contact, go to https://sliddes.com/