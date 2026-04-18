![Unity](https://img.shields.io/badge/Unity-6000.2.6f2-black?logo=unity)
![Status](https://img.shields.io/badge/Status-Completed-brightgreen)
![Platform](https://img.shields.io/badge/Platform-PC-blue)
![Architecture](https://img.shields.io/badge/Architecture-FSM%20%2B%20Event--Driven-orange)

# Unity Unified Interaction System

## 📌 Overview

This project is a 3D interactive scene developed in Unity (**6000.2.6f2**) as a test assignment.
It demonstrates a modular interaction system, inventory handling, dialogue system, and additional gameplay mechanics.

---

## ▶ How to Run

1. Open project in Unity Hub (version 6000.2.6f2)
2. Load the game scene:

Assets/_ProjectFiles/Scenes/GameScene.unity

3. Press Play

Alternatively, a build can be run from the provided executable (if included).

---

## 🎮 Controls

* **WASD** — Move
* **Mouse** — Look around
* **E (Press)** — Interact
* **E (Hold)** — Continuous interaction (e.g. valve)
* **Mouse Hold (LMB)** — Rotate inspected item

---

## ⚙️ Core Features

### 🔹 Interaction System

* Unified interaction via a single key (**E**)
* Supports:

  * Press
  * Hold
  * Release
* Context-sensitive UI hint:

  ```
  E — [Action]
  ```

---

### 🔹 Item System

* Player can hold **only one item at a time**
* Items can be:

  * Inspected (with smooth transition)
  * Rotated with mouse
  * Picked up / placed back into slots

#### Implemented Items:

* **Key** — used to open chest
* **Note** — unfolds with animation during inspection
* **Custom Item** — used in quest system

---

### 🔹 Inspection System

* Smooth item transition to inspect position
* Mouse-based rotation
* UI description display
* Dedicated inspection state (FSM-based)

---

### 🔹 NPC & Dialogue System

* Supports:

  * **Linear dialogue**
  * **Branching dialogue with choices**

#### Quest Dialogue:

* NPC requests a **random item** (excluding key and note)
* Quest UI appears in top-right corner
* Completion updates UI with a checkmark

---

### 🔹 Quest System

* Random item selection via `QuestManager`
* Event-driven architecture
* Item validation on delivery
* Integrated with dialogue system

---

### 🔹 Chest System

* Opens **only if player holds a key**
* Key is consumed upon use
* Incorrect interaction triggers feedback animation

---

### 🔹 Valve Mechanism

* Requires **holding E**
* Rotates valve and opens door proportionally
* Returns to initial state smoothly when released

---

### 🔹 Custom Interaction (Extra Feature)

* Interactive **button**
* Event-driven system (`OnButtonPressed`)
* Triggers external systems (e.g. fireworks)

---

## 🏗️ Architecture

Project follows **feature-based structure**:

```
_ProjectFiles/
    Player/
    Interaction/
    Dialogue/
    Quest/
    UI/
    Items/
```

### Key Principles:

* Separation of concerns
* Event-driven communication
* Finite State Machine (Player states)
* ScriptableObjects for configuration
* Minimal coupling between systems

---

## 🧩 Notable Systems

* **PlayerContext** — central access point for player components
* **IInteractable** — unified interaction interface
* **FSM (State Machine)** — controls player states
* **Bootstrap** — binds systems together
* **ScriptableObjects** — configurable parameters

---

## 🎯 Additional Polish

* Smooth item transitions
* Animated note (book-like unfolding)
* UI feedback for interactions
* Firework effect as bonus interaction

---

## 🧠 Design Decisions

- Interaction system is built around a single interface (`IInteractable`) to ensure scalability and consistency.
- Player logic is handled via a finite state machine to clearly separate behaviors (movement, inspection, dialogue).
- Systems communicate using events to reduce coupling between gameplay features.
- ScriptableObjects are used for configuration to allow easy tuning without modifying code.

---

## 📦 Unity Version

```
Unity 6000.2.6f2 (project was developed and tested using this version)
```

---

## ⏱ Development Time

~2-3 days

---

## 📝 Notes

The project focuses on clean architecture, modular systems, and extensibility rather than visual fidelity.
