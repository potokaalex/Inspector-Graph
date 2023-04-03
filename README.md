![titleBanner](https://user-images.githubusercontent.com/98838657/229546267-bbbf65cd-2c0b-4e33-b7c3-f2c7cb267a79.png)

## Introduction
Inspector Graph is a tool for drawing a mathematical function as a property in the inspector.<br>
This tool is open source.

## Requirements
 - Unity 2021.2 and up.

## User manual
To use this in your project, you have to download the repository and place it anywhere in the "Assets" folder.<br>
Then, in any Monobehaviour script, connect the "InspectorGraph" namespace and inherit from "IFunction".<br>
Don't forget to define the "GetFunctionValue" method in which you should specify your mathematical function.<br>
You may not initialize it, but it must be public or marked with the "[SerializeField]" attribute.<br>
The place where the graph is displayed depends on the location of the "Graph" field.<br>
