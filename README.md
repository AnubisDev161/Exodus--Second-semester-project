<img width="960" height="400" alt="iIGhUP" src="https://github.com/user-attachments/assets/1d7d303c-8360-4484-9d9e-fa89a562b313" />



# EXODUS--SECOND-SEMESTER-PROJECT
Exodus is a tactical escort shooter where you protect a convoy of civilians from an invisible demonic threat. Switch between normal vision — fast but blind — and a slowed demonic state that reveals enemies and arms you for the fight. Stay sharp. Stay close. Keep them alive.

Exodus was developed as a student project at the [S4G School for Games](https://www.school4games.net/) between June and August 2025.

Play it on [Itch.io!](https://s4g.itch.io/exodus)

## Responsibilities 
### Main Focus
- Gameplay & AI Programming, - implemented AI behavior ([NPCs](https://github.com/AnubisDev161/Exodus--Second-semester-project/blob/main/Scripts/NPC/ConvoyNPC.cs), [enemy AI](https://github.com/AnubisDev161/Exodus--Second-semester-project/blob/main/Scripts/Enemy/Enemy.cs)) and [Player](https://github.com/AnubisDev161/Exodus--Second-semester-project/blob/main/Scripts/Player/Player.cs) movement.
- Co-Producing, 
  
### Additional Tasks
- UI & Backend logic, [UI](https://github.com/AnubisDev161/Exodus--Second-semester-project/tree/main/Scripts/UI)
- Sound Design, managed comunication between departments


## Project Details

The game was developed using the Unity game engine. Version 6000.0.51f1.
The programming language was C#.

# EXAMPLE OF MY WORK 🔧

### The Problem :x:
Due to some Design issues we needed to change the game's entire Design in the middle of its production. This was a big challenge, the gameplay needed to change dramatically and our NPCs and Enemies needed an entire behavior rework.

### The Solution :heavy_check_mark:
This was only possible because all of our characters in the game, NPCs, Enemies and the player itself, are all based on state machines that all work in the same way.
With this [state machine](https://github.com/AnubisDev161/Exodus--Second-semester-project/blob/main/Scripts/Player/Player_State_Machine/PlayerStateMachine.cs) setup it was rather easy to add new states and scrapp those that weren't needed anymore.

### How it works

The state machine pattern is based on a simple idea, separating code in several parts - so called states - that all have their own task.
Additionally in this project, I focused strictly on separating the "how is something done?" from the "what is supposed to happen?".
On the pictures below you can see a single [state script](https://github.com/AnubisDev161/Exodus--Second-semester-project/blob/main/Scripts/Player/Player_State_Machine/Concrete_States/PlayerWalkState.cs) that's only managing what method is being triggered and when. 

<img width="871" height="776" alt="image" src="https://github.com/user-attachments/assets/84383006-dc54-46b6-9a12-3a0a1a444426" />

The actual behavior is within the player, for example "UpdateMove". The state script only calls the UpdateMove method or not. And therfore it is deciding, whether the player can move or not in this particular state.

<img width="1076" height="598" alt="image" src="https://github.com/user-attachments/assets/58734a72-69ca-432d-b6a7-03cf320e9689" />

### Conclusion
This state machine approach, which strictly separates the execution of logic from the logic itself, gave us the ability to quickly change the behavior of enemies and NPCs.
