---
youtubeId: CBQGTAU8eYM
---

Welcome to Project Brimstone 

# About the Game

Project Brimstone is a single-player 2D roguelike game. Players assume the role of an
alchemist fighting their way through the circles of Hell as described in Dante's Inferno. Through crafting, players
can create weapons, potions, and other useful items to aid them in fighting off
the hordes of enemies they will encounter on their quest through the procedurally-generated
inferno. A full description can be found [here](https://github.com/SCCapstone/RogueGames/wiki/Project-Description).
If you want to play the game check out our github repo [here](https://github.com/SCCapstone/RogueGames) and then 
follow the install instructions listed below or in the readme on the github.

### Temporary Video

{% include youtubePlayer.html id=page.youtubeId %}

# About the Creators
## Logan Corley
###   Email: lcorley@email.sc.edu
## Alex Johnson
###   Email: jvj1@email.sc.edu
## Lukas F. McClamrock
###   Email: lukasm@email.sc.edu
## Mark H. McCoy
###   Email: mhmccoy@email.sc.edu, Linkedin: https://www.linkedin.com/in/mark-mccoy-94217a1b7/
>  Hi I'm Mark! I'm a senior graduating from the University of South Carolina with a BSE in Computer Science and a minor 
>  in history. I was President of the Gamecock Swim Club and a fighter and Secretary of Gamecock Boxing. Currently looking for
>  work for after graduation.
 
## James Thompson
###   Email: jameset@email.sc.edu, 


# How to Play

## External Requirements

To work on and build Project Brimstone you will need the Unity Game Engine version 2020.1.4f1.
[Unity Hub](https://unity3d.com/get-unity/download) is recommended for managing Unity projects and engine versions.

Project Brimstone is being developed on Windows 10 and is currently the only supported platform.

## Running

Project Brimstone can be run directly within the Unity editor for development and debug testing.
A build directory with the game executable is not currently included in the repo.

## Deployment

Project Brimstone can be built by creating a build in the Unity editor for Windows 10. Following
the wizard will create a build directory with the executable and game data bundled for deployment.

## Testing

Two testing files currently exist for Project Brimstone: PlayerUnitTests.cs and PlayerBehaviourTests.cs. These are 
found in the Tests directory.

## Testing Technology

PlayerUnitTests.cs
- TestTakeDamage: tests the Player function TakeDamage to check if new health value is equal to the previous health 
minus the damage taken. Passes if new health value is correct (i.e. the Player's health after taking damage is set to 
the correct value. Fails if there is a discrepancy between values.

PlayerBehaviourTests.cs
- TestCornerCollision: tests wall collision when the Player is moved to the corner of the starting room. Passes if 
collision works properly (i.e. if the wall stops the Player's movement). Fails if it allows the Player to move through it.

## Running Tests

To run the tests, boot up Project Brimstone in the Unity Game Engine, move your cursor to the upper taskbar, and click 
Window -> General -> Test Runner. When the window appears, click "Play Mode" and enter the Tests folder in the Project 
browser. If done correctly, the tests should appear in the Test Runner window. Click "Run All" to run all of the tests. 
Green checkmarks will appear to indicate passed tests, while red X's will appear to indicate failed tests.

