# HMT Game 1
This repository contains the code for the first human-machine teaming game developed as part of the STRONG TACT project.

# Running the Game
The game currently requires that three separate client instances connect to each other in order to play the game.

The easiest way to run the game locally is to:
1. Open the project in the Unity Editor
2. Compile the game to your local platform (or use the WebGL build target) See [Unity's Documentation](https://docs.unity3d.com/Manual/PublishingBuilds.html) for how to build a project.
3. Run the compiled build twice, and run a third instance from within the Unity Editor.
4. Use one instance to create a room and the other two to connect.
5. In this forked repo, I generate the Unity Level from a JSON file created by a PCG tool based on Wave Function Collapse algorithm (https://github.com/eliekozah/PCGToolForDiceAdventureGame). Level is generated using myLevel.JSON file in /Assets/Resources
