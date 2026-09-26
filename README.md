# Escape Room Project

## Game Concept Summary
This project is a first-person escape room experience set in a living room . The player explores the room, interacts with objects, completes puzzle tasks, and attempts to unlock the final door before the timer runs out.

The core gameplay loop is built around:
- exploring the environment,
- interacting with key objects,
- solving environmental puzzles,
- tracking progress,
- and escaping before the timer expires.

## Puzzle Tasks
The project includes multiple interactive puzzle elements:

1. Painting Interaction
   - The player clicks an interactable painting to trigger a room interaction or object reveal.

2. Book Sequence Puzzle
   - The player must click the correct books in the correct order.
   - Wrong selections reset the sequence and deduct time.

3. Clock Puzzle
   - The player opens a clock interface and selects the correct time.
   - Success reveals a hidden code or clue.

4. TV Remote Interaction
   - The player interacts with the TV remote as part of the environment puzzle flow.

5. Door Lock Escape
   - The player uses the discovered code to unlock the final door and complete the game.

## Asset Sources
This project uses a mix of Unity-provided content and imported external assets. Examples include:

- Low Poly Living Room Pack
  - https://assetstore.unity.com/packages/3d/props/interior/low-poly-living-room-pack-141071

- TextMeshPro
  - https://docs.unity3d.com/Packages/com.unity.textmeshpro@latest

- Unity Standard Asset / default package content
  - https://unity.com

- Additional character and prop content included in the project package structure

If you are using custom or third-party assets, make sure to credit the original creators and follow all licensing terms for redistribution.

## Controls
### Mouse + Keyboard
- Left Mouse Click: interact with objects and UI buttons
- Move Mouse: aim/look around the scene
- WASD: move the player
- Shift: sprint (if enabled in your movement setup)
- ESC: pause / exit menu if implemented in your project

## Project Notes
- The game uses a timer-based fail condition.
- Puzzle progress updates dynamically in the UI.
- The game ends when the player unlocks the final door or the timer reaches zero.
