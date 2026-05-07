# Solitaire Developer Case Study

## Overview

This project is a Unity prototype created for the Solitaire Developer Case Study.

The goal was to implement a minimal Solitaire-style interaction system with:

- card movement between stacks,
- drag-and-drop interaction,
- undo functionality,
- clean and modular architecture.

The project intentionally avoids implementing full Solitaire rules or gameplay generation in order to stay focused on the requested feature scope.

---

# Implemented Features

## Gameplay Prototype

- 2D card movement between multiple stacks
- Drag-and-drop interaction using Unity EventSystem + Physics2D raycasts
- Undo support
- Multi-step undo-ready architecture
- Visual card snapping to stack positions

---

# Architecture

The project is split into isolated responsibilities to keep systems modular and reusable.

## Bootstrap Layer

### `GameBootstrap`

Unity-specific entry point.

Responsibilities:
- provide scene references,
- initialize composition root,
- stay minimal and framework-oriented.

---

## Composition Layer

### `GameCompositionRoot`

Composition root responsible for wiring dependencies.

Responsibilities:
- create runtime services,
- bind controllers/views,
- subscribe systems together,
- manage lifecycle/disposal.

---

## Gameplay Layer

### `GameRoundController`

Core gameplay controller.

Responsibilities:
- react to drag events,
- validate and execute moves,
- create turn records,
- process undo operations,
- emit gameplay events.

### `TurnHistoryService`

Stores turn history.

Current implementation allows configurable undo depth while still supporting the assignment requirement of undoing at least one previous move.

### `TurnInfo`

Immutable turn data object describing:
- moved card,
- source stack,
- destination stack.

---

## View Layer

### `GameFieldView`

Responsible only for visual updates.

Responsibilities:
- update card positions,
- synchronize draggable state after moves/undo,
- handle visual snapping.

---

## Drag & Drop Package

The drag-and-drop system was intentionally implemented as an isolated reusable module.

Features:
- event-driven API,
- Unity EventSystem integration,
- Physics2D raycast support,
- reusable interfaces for draggable items and docks,
- no Solitaire-specific dependencies.

The gameplay layer reacts to drag events instead of embedding game logic directly into drag handlers.

---

# Technical Decisions

## Why full Solitaire was not implemented

The assignment focuses on undo functionality and architecture quality rather than full gameplay implementation.

To keep the scope realistic for the time limit, the prototype only implements:

- stack-based card movement,
- drag-and-drop,
- undo flow.

This allowed more attention to be spent on:

- code structure,
- separation of responsibilities,
- reusable systems,
- stability.

---

# Edge Cases Considered

## Undo after additional moves

Tested scenario:

1. Move card A → B
2. Move card B → C
3. Undo
4. Move card again
5. Undo again

This revealed a synchronization issue where draggable interaction state could become outdated after undo.

The issue was fixed by synchronizing both:

- visual card position,
- draggable current dock state.

This ensures interaction state remains valid after rollback operations.

---

# AI Assistance

AI tools were intentionally used during development as allowed by the assignment.

## AI-assisted areas

- architecture validation,
- responsibility separation review,
- drag-and-drop API design discussions,
- edge case analysis,
- documentation generation,
- code review and refactoring feedback.

## Examples of prompts used

- "Review this architecture and highlight bottlenecks"
- "Design an isolated reusable drag-and-drop system for Unity"
- "Review this code according to SOLID principles"
- "Analyze undo-flow edge cases"

AI-generated suggestions were reviewed and adapted manually during implementation.

---

# Possible Future Improvements

With more time, the following improvements could be added:

- proper Solitaire gameplay rules,
- animated card transitions,
- redo system,
- automated tests,
- dependency injection framework,
- card stacking visuals,
- mobile touch polish,
- data-driven game setup,
- configurable move validation.

---

# Tech Stack

- Unity
- C#
- Unity EventSystem
- Physics2D

