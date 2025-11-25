# EcoConvenienceStation MiniGame

Interactive Textile-Recycling Kiosk -  [MiniGame Prototype](https://thecdm.sharepoint.com/:v:/s/projects/EXnuqbGH8MRNnu_DQzdT3y4BwE-nNw68WkZMOvwzJVWUPA?e=T2cvcC)



## Overview

The Fabric Catching Minigame is a fast-paced, gesture-controlled experience designed as part of our Eco Convenience Station. The game serves as the first touchpoint for users interacting with our textile-recycling kiosk, aiming to increase awareness, educate on materials, and encourage proper recycling behavior.

Players use hand-tracking to catch all falling textile “materials” (good or bad), reinforcing recycling concepts through quick reflexes, visual feedback, and short, repeatable interactions.

### Key Features

👐 **Hand-Tracking Interaction**: Real-time gesture tracking controls the basket without physical controllers.


📦 **Dynamic Material Spawning**:
Adjustable spawn rate, probabilistic material type, and random positions.

🟢 **Material Info System**:
Each successful run may display a material card with color-coded eco-status.

⏱️ **Countdown & Timer System**:
3-second countdown → 30-second gameplay session.

🔊 **Audio Feedback**:
Success, failure, and session-end sounds improve clarity and engagement.

🎉 **Confetti Animation**:
Triggered upon game completion or high performance.

### Tech Stack

- Engine: **Unity 2022.3.6.2f2** (6000 compatible)

- UI: TextMeshPro, AudioWide(Font), [Figma References](https://www.figma.com/proto/kSSljKCSMHSv0y1LvvpNqn/Project1C?page-id=0%3A1&node-id=336-82&p=f&viewport=1133%2C-1012%2C0.11&t=9KTdcWrJedZNogcT-1&scaling=contain&content-scaling=fixed&starting-point-node-id=336%3A61)

- Input: Hand-Tracking Package ([Mediapipe](https://github.com/TesseraktZero/UnityHandTrackingWithMediapipe))

- Art: Simple icon-based materials(see Assets and Prefab folders)

- Hardware (PoC): Laptop + webcam(Logitech Brio 4k) + touchscreen monitor

### Content

- Asstes/Fonts: Custom Fonts that align with the Figma UI design.
- Asstes/Prefab: Materials prefab for spawning.
- Asstes/Sound: Royalty-free audio cue from [PIXABAY](https://pixabay.com/sound-effects/search/freesound/).
- Asstes/Sprites: Sprites for material prefabs.
- Asstes/Script: For mirroring the web camera.
- Assets/MediaPipeUnity/Samples/Scenes/HandLandmark Detection: The actual Scene and script usage.


## Deployment & Usage

1. Upon cloning this repo, open with Unity 2022.3.6.2f2. Or directly import [this package](https://thecdm.sharepoint.com/:u:/s/projects/EcXtn6W86EpMk2dflCm-vNwBvfTaS8znNEzfJk7ACBpE2Q?e=3JWOcI) to a new Universal 3D Unity Project.
2. To make changes, navitage to Assets -> MediaPipeUnity -> Samples -> Scenes -> HandLandmark Detection.
3. Enter scene "HandCatchGame".
4. Hit play.

## License

For educational and prototyping purposes only. Not for commercial use.
