<img width="960" height="400" alt="iIGhUP" src="https://github.com/user-attachments/assets/1d7d303c-8360-4484-9d9e-fa89a562b313" />



# Exodus--Second-semester-project
Exodus is a tactical escort shooter where you protect a convoy of civilians from an invisible demonic threat. Switch between normal vision — fast but blind — and a slowed demonic state that reveals enemies and arms you for the fight. Stay sharp. Stay close. Keep them alive.

Exodus was developed as a student project at the [S4G School for Games](https://www.school4games.net/) between June and August 2025.

Play it on [Itch.io!](https://s4g.itch.io/exodus)

## Responsibilities

In this project I focused primarily on gameplay programming. This also included the [NPCs](https://github.com/AnubisDev161/Exodus--Second-semester-project/blob/main/Scripts/NPC/ConvoyNPC.cs) and the [enemy AI](https://github.com/AnubisDev161/Exodus--Second-semester-project/blob/main/Scripts/Enemy/Enemy.cs), an area in which I already had some experience from my [Goap RTS project.](https://github.com/AnubisDev161/GOAP-AI-RTS-Project)
The most challenging and also most important part for me in this project was the [player.](https://github.com/AnubisDev161/Exodus--Second-semester-project/blob/main/Scripts/Player/Player.cs)
I was also co producer of the project and partly responsible for sound design and sound implementation.
And as always [UI](https://github.com/AnubisDev161/Exodus--Second-semester-project/tree/main/Scripts/UI) and soume backend logic.

## About the project

The game was developed using the Unity game engine. Version 6000.0.51f1.
The programming language was C#.

## My Workflow

### The problem

Changing a game's core Design in the middle of its production may sound a bit crazy, but at least for us, it was the best decision we ever made in the project.
Thus it was still a big challenge, the gameplay needed to change dramatically and our NPCs and Enemies needed an entire behavior rework.


### The Solution 
This was only possible because all of our characters in the game, NPCs, Enemies and the player itself, are all based on state machines that all work in the same way.
With this state machine setup it was rather easy to add new states and scrapp those that weren't needed anymore. The pattern was so effectvie that we succesfully changed the entire game goal
within a single week.

### How it works

The state machine pattern is based on a simple idea, separating code in several parts - so called states - that all have their own task.
Additionally in this project, I focused strictly on separating the "how is something done?" from the "what is supposed to happen?".
On the pictures below you can see a single state script that's only managing what method is being triggered and when. 

<img width="871" height="776" alt="image" src="https://github.com/user-attachments/assets/84383006-dc54-46b6-9a12-3a0a1a444426" />

The actual behavior is within the player, for example "UpdateMove". The state script only calls the UpdateMove method or not. And therfore it is deciding, whether the player can move or not.

<img width="1076" height="598" alt="image" src="https://github.com/user-attachments/assets/58734a72-69ca-432d-b6a7-03cf320e9689" />

This approach on state machines to strictly separate what and when to execute logic from what the actual logic is doing, really gave us the possibility to quickly change the entire enemy and NPC behavior



