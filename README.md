![Unity](https://img.shields.io/badge/Unity-6000.2.6f2-black?logo=unity)
![Status](https://img.shields.io/badge/Status-Completed-brightgreen)
![Platform](https://img.shields.io/badge/Platform-PC-blue)
![Architecture](https://img.shields.io/badge/Architecture-FSM%20%2B%20Event--Driven-orange)

# Unity Unified Interaction System

## 🎬 Demo
![Video](./README_Assets/gameplay.gif)

---

## 📌 Overview

This project was created as a test assignment to explore building a modular interaction system in Unity.
The goal was not just to implement features, but to structure them in a way that keeps systems decoupled and easy to extend.

---

## ⚙️ Core Features
- Unified interaction system (press / hold / release)
- Item inspection and inventory handling
- Dialogue system (linear and branching)
- Quest system with item validation
- Interactive world objects (valve, chest, button)

---

## 🧠 Architecture Overview
The project is structured around several independent systems:
- **Player layer** — handles input and state (FSM)
- **Gameplay systems** — Dialogue, Quest, Interaction
- **UI layer** — reacts to gameplay events
Systems do not directly depend on each other.  
Communication is handled through events to reduce coupling.

---

## 🧠 Design Decisions
- **IInteractable interface**  
  Provides a single entry point for all interactions, making it easy to extend the system.
- **Finite State Machine (FSM)**  
  Used to clearly separate player behaviors (movement, dialogue, inspection).
- **Event-driven communication**  
  Dialogue, Quest and UI systems communicate through events instead of direct references.
- **ScriptableObjects for configuration**  
  Used to keep data editable without modifying code.

---

## ⚖️ Trade-offs
- A full dependency injection setup was intentionally avoided to keep the project simple.
- `PlayerContext` acts as a central access point, which simplifies integration but increases coupling.
- Some systems are still tied to MonoBehaviour and could be further decoupled.

---

## ⚠️ Limitations
- No save/load system
- Limited separation between runtime logic and Unity components
- Project scale is small, so some architectural decisions are simplified

---

## ▶ How to Run
1. Open the project in Unity Hub (6000.2.6f2)
2. Load the scene:
`Assets/_ProjectFiles/Scenes/GameScene.unity`
3. Press Play

---

## 🎮 Controls
- **WASD** — Move  
- **Mouse** — Look  
- **E (Press)** — Interact  
- **E (Hold)** — Continuous interaction  
- **Mouse Hold (LMB)** — Rotate inspected item  

---

## 🏗️ Project Structure
```
_ProjectFiles/
    Player/
    Interaction/
    Dialogue/
    Quest/
    UI/
    Items/
```
---

## ⏱ Development Time
~2–3 days

---

## 📝 Notes
The focus of this project is system design and interaction architecture rather than visual polish.