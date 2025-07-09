
# Unity Story-Driven Dialogue System

A flexible, YAML-powered dialogue system for Unity games with story and time-based progression.  
Designed for sandbox-style games (like *My Summer Car*) that have NPCs and dialogue evolving over in-game days.

---

## 🌟 Features

- 🗂️ YAML-based dialogue definitions
- 🗓️ Dialogue scenes organized by in-game day
- 🎲 Random variant support for varied responses
- 🧠 Enum-based scene types (e.g., `Greeting`, `StoreTalk`, `Quest`)
- 🎮 Easy integration with Unity triggers and NPCs
- 📚 Scalable and readable structure

---

## 📁 Directory Structure

```

DialogueSystem-DayBased/
├── Assets/
│   └── Scripts/
│       ├── DialogueManager.cs
│       ├── DialogueTypes.cs
│       └── DialogueTrigger.cs
├── Dialogue/
│   └── dialogue.yaml
├── README.md
└── LICENSE

````

---

## ✍️ YAML Structure

```yaml
days:
  - day: 1
    scenes:
      - id: store_greeting
        type: Greeting
        variants:
          - lines:
              - speaker: Shopkeeper
                text: "Welcome to the store!"
          - lines:
              - speaker: Shopkeeper
                text: "Need anything today?"
  - day: 2
    scenes:
      - id: quest_hint
        type: QuestStart
        variants:
          - lines:
              - speaker: Grandpa
                text: "I left something near the lake..."
````

---

## 🔧 How to Use

1. Install [YamlDotNet](https://github.com/aaubry/YamlDotNet) via NuGet or Unity package.
2. Place your `dialogue.yaml` in a `Resources` or `StreamingAssets` folder.
3. Use `DialogueManager.Instance.StartScene()` or `StartRandomScene()` to trigger dialogue.
4. Customize `DialogueType` enum to match your game's structure.

---

## 📜 License

This project is licensed under the MIT License.

