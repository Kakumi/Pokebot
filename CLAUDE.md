# CLAUDE.md

## Project Overview
This repository is **Pokebot**, a **BizHawk external tool** written in **C# (.NET Framework 4.8)** for automating Pokémon games (primarily Gen 2 and Gen 3).

This is a **memory-driven emulator integration project**, not a standard .NET application.

Claude must optimize for:
- correctness over elegance,
- emulator safety over abstraction,
- minimal, targeted changes.

---

## How the Project Works
Pokebot operates by:
1. Running inside BizHawk as an external tool.
2. Detecting the loaded ROM via hash.
3. Creating a `GameVersion` object via `VersionFactory`.
4. Reading live game state through `IGameMemory`.
5. Executing bot logic every emulator frame.
6. Sending controller inputs via `ActionRunner`.

The main loop lives in:
- `src/Pokebot.cs`

---

## Architecture Overview

### Core Flow
- `Pokebot.cs` → orchestrates everything
- `VersionFactory` → selects game + memory + runner
- `BotFactory` → instantiates bots
- `GameVersion` → bundles runtime dependencies

### Key Layers

#### Memory Layer
- `IGameMemory`, `Gen2Memory`, `Gen3Memory`
- Reads emulator memory and exposes:
  - game state
  - Pokémon data
  - RNG / tasks / IDs

#### Action Layer
- `ActionRunners/*`
- Translates bot decisions into button inputs

#### Bots
- Located in `Factories/Bots`
- Implement `IBot`
- Drive behavior using memory + runner

#### UI Layer
- `Panels/*`
- WinForms-based BizHawk tool UI

#### Watchers
- `PokemonWatcher`
- Syncs emulator state → UI

---

## Critical Design Rules

### Separation of Concerns (STRICT)
- Memory parsing → `Models/Memory`
- Inputs/actions → `ActionRunners`
- Logic → Bots
- UI → Panels
- Wiring → `Pokebot.cs`

### ROM Support
- Always hash-based
- Never assume compatibility by name alone

## Symbols
In this project, **symbols** are named memory definitions imported from Pokémon decompilation projects. They are effectively a mapping from a symbolic name to:
- a memory address,
- a size,
- and a RAM/ROM memory group or region.

A symbol lets the code refer to game data by meaning instead of hardcoded raw offsets. For example, instead of scattering literal addresses throughout the code, Pokebot can look up a named symbol and read the correct memory location for the current game/revision.

For Pokebot, symbols are part of the runtime contract between:
- the decompilation project data,
- the loaded ROM/hash,
- and the memory reader implementation.

### How to think about symbols in this repo
- Symbols are **authoritative memory metadata**, not arbitrary constants.
- They come from external decompilation projects for the target game.
- They may differ by game, revision, language, or project branch.
- If a symbol changes upstream, Pokebot memory reads may break even if the C# code still compiles.
- A wrong symbol is usually worse than duplicated code.

### How Claude should use symbols
- Prefer symbol-based memory access over introducing new hardcoded addresses.
- Treat symbol names as version-specific and generation-specific.
- Do not assume a symbol from one game or decomp project exists in another.
- Do not rename, “simplify,” or consolidate symbol usage unless the mapping is proven equivalent.
- When adding support for a new ROM/revision, verify that the required symbols exist and match the expected memory layout.

### Important caution
Symbols are not just “addresses.” They also encode **what memory region is being read** and **how much data belongs to that symbol**. Code that ignores symbol size or memory group can read the wrong data even if the address looks correct.

### Example source of symbols
A typical source is a Pokémon decompilation project such as `pret/pokeruby`, where named symbols are defined for game structures and RAM locations. These symbol definitions are then used by tools like Pokebot to read emulator memory in a structured way. :contentReference[oaicite:0]{index=0}

### Emulator Constraints
- Frame timing matters
- Determinism matters
- Save-state safety matters

---

## Project-Specific Guidelines

### Memory Code
- Accuracy > abstraction
- Do NOT "clean up" offsets blindly
- Keep Gen 2 and Gen 3 logic separate

### Action Runners
- Deterministic inputs only
- No unnecessary input spam
- Keep per-game differences here (not in bots)

### Bots
- Use:
  - `GameVersion.Memory`
  - `GameVersion.Runner`
- Do NOT embed memory offsets or input logic
- Preserve event flow:
  - `PokemonEncountered`
  - `PokemonFound`
  - `StateChanged`

### Pokebot.cs
- Central orchestrator → high risk file
- Do not add game-specific branching here unless unavoidable

---

## General Engineering Principles

- Make the **smallest safe change**
- Prefer clarity over cleverness
- Follow existing patterns before creating new ones
- Avoid unnecessary abstractions
- Do not introduce new dependencies lightly

---

## C# Guidelines

- Use modern C# when it improves clarity (not for style points)
- Prefer explicit naming
- Keep methods focused
- Use `async/await` correctly (avoid blocking)
- Handle nullability intentionally
- Avoid hidden side effects
- Always use curly brackets even for return inside a condition

---

## Error Handling

- Never swallow exceptions silently
- Use meaningful error messages
- Prefer structured logging
- Exceptions = exceptional cases only

---

## Testing & Validation

There are no strong unit tests → **manual validation required in BizHawk**

Always verify:
- ROM detection works
- Bot runs correctly
- UI updates correctly
- No crashes in update loop
- Save-state behavior is safe

---

## Performance

- Correctness first
- Optimize only if needed
- Avoid unnecessary allocations in hot paths

---

## Refactoring Rules

- Only refactor when it supports the task
- No large unrelated rewrites
- Call out bigger issues instead of silently fixing everything

---

## Typical Tasks

### Adding a Bot
1. Implement bot in `Factories/Bots`
2. Register in `BotFactory`
3. Use Memory + Runner (no shortcuts)
4. Test in BizHawk

### Fixing a Bug
1. Find root cause
2. Apply minimal fix
3. Verify manually
4. Consider edge cases

### Adding Game Support
1. Add ROM hash
2. Verify symbols
3. Validate memory parsing
4. Test bots individually

---

## What Not To Do

- Do NOT over-abstract
- Do NOT mix memory + UI logic
- Do NOT hardcode game support
- Do NOT rewrite working systems unnecessarily
- Do NOT introduce async behavior in frame loop
- Do NOT break save-state logic

---

## When Requirements Are Unclear
- Make the safest assumption
- Follow existing patterns
- Explicitly state assumptions