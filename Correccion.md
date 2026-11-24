# Corrección, Trabajo Práctico N°08: Platformer 2D

- **Alumno/a:** Mironoff
- **Entrega:** `TP08_C1_Mironoff_Hunters`
- **Materia:** Programación con Motores de Videojuegos I
- **Nota TP08:** 10

> Hiciste 15 commits después del cierre (2026-11-09 18:00). El commit al cierre es `c776e28` y el último es `ae4b296` (2026-11-10 20:46), con 239 archivos cambiados (51 scripts, incluido el README). Corregí la última versión del repo; no descuenta puntos.


## 1. Checklist de requisitos

### General

| #  | Requisito | Estado | Evidencia |
|----|-----------|--------|-----------|
| G1 | Repositorio `TP08_C1_Apellido_Titulo` | ✓ | La carpeta y el remoto `origin` de `.git/config` se llaman `TP08_C1_Mironoff_Hunters` |
| G2 | Proyecto de Unity con el mismo nombre | ✓ | `ProjectSettings/ProjectSettings.asset:16` `productName: TP08_C1_Mironoff_Hunters` |
| G3 | Al menos 2 escenas: MainMenu y Gameplay | ✓ | `Assets/Scenes/MainMenu.unity`, `Town.unity` y `Forest.unity` en Build Settings |
| V1 | Publicado en Itch.io y presentable | ✓ | Está publicado en itch.io (revisado el 2026-11-16) |
| V2 | Itch.io jugable en web y descargable para Windows | ✓ | Carga la build web y tiene descarga para Windows |

### Jugador

| #  | Requisito | Estado | Evidencia |
|----|-----------|--------|-----------|
| J1 | Se mueve a derecha e izquierda | ✓ | `Assets/Scripts/Player/PlayerBrain/Controllers/Locomotion/Components/PlayerMovement.cs:50`, llamado desde `PlayerBrain.FixedUpdate` |
| J2 | Salta | ✓ | `Assets/Scripts/Player/PlayerBrain/Controllers/Locomotion/Components/PlayerJump.cs:41-57`, con coyote time y corte de salto |
| J3 | Puede dañar enemigos | ✓ | `PlayerAttackArea` implementa `IDamageProvider` y `EnemyBrain.OnTriggerEnter2D` (`Assets/Scripts/Enemy/Shared/EnemyBrain.cs:76-89`) aplica el daño |
| J4 | Barra de vida | ✓ | `Assets/Scripts/Player/UI/UIHealth.cs` (puntos de vida por grupo), conectado en `PlayerUIManager.cs:36` |
| J5 | Puede curarse | ✓ | `HealEffect` (`HealingBalmItem`) y `PlayerHealthController.RestoreFullHealth` al usar un `Checkpoint` |
| J6 | Todas sus acciones animadas y con sonido | ✓ | `PlayerController.controller` con Idle, Moving, Jumping, Attacking y Dead en `PlayerBrain.HandleAnimations`. Hay sonidos de salto, aterrizaje, ataque, daño, pasos (`PlayerFootsteps`), monedas e ítems en `PlayerAudioConfig.asset` |

### Enemigos

| #  | Requisito | Estado | Evidencia |
|----|-----------|--------|-----------|
| E1 | Dañan al jugador | ✓ | `EnemyAttackAreaMarker` en `Zombie.prefab`, `NormalPlant.prefab`, `FastPlant.prefab`, `Boss.prefab` y en las balas; `PlayerBrain.cs:440-447` |
| E2 | Poseen barra de vida | ✓ | `Assets/Scripts/Enemy/UI/UIEnemyHealth.cs`, `Assets/Scripts/Enemy/UI/UIBossHealth.cs` |
| E3 | Feedback visual al morir (partículas + animación) | Parcial | Tenés animación de muerte (`ZombieDeath.anim`, `NormalPlantDeath.anim`, estado `Dead` en `ZombieBrain.cs:48`), pero ningún prefab de enemigo tiene `ParticleSystem` y ningún script dispara partículas al morir |
| E4 | Todas sus acciones animadas y con sonido | Parcial | Ataque, disparo, enterrarse y daño suenan (`ZombieAudioConfig.asset`, `PlantAudioConfig.asset`), pero `AudioCategory` no tiene sonido de muerte y ni `ZombieBrain` ni `PlantBrain` reproducen audio al morir |

### Pickables

| #  | Requisito | Estado | Evidencia |
|----|-----------|--------|-----------|
| K1 | Heal: el jugador se cura | Parcial | Lo tenés como ítem de tienda (`HealingBalmItem`, `HealEffect.cs`), no como pickable. `PickableType.cs` solo tiene `Coin` |
| K2 | Damage: aumenta el daño | Parcial | `Embers` y `PosionPowder` (`DamageEffect.cs`) cambian el `DamageType` y `PlayerAttackArea.cs:27` multiplica por 1.2. Es un ítem de tienda, no un pickable |
| K3 | Invulnerability por x segundos | Parcial | `WardingCharm` con `InvulnerabilityEffect.cs` y la duración en `InvulnerabilityEffectDataSo`. Es un ítem de tienda, no un pickable |
| K4 | Reward: monedas para el shop | ✓ | `Pickable.cs`, `Coin.prefab`, `PlayerPickablesHandler.cs:19-21`, y se usan en `StoreManager.HandlePurchaseRequest` |

### Sonido

| #  | Requisito | Estado | Evidencia |
|----|-----------|--------|-----------|
| S1 | AudioMixer con Master, Music, FX y UI | ✓ | `Assets/Audio/AudioMixer.mixer` con los grupos Master, Background (música), SFX y UI |
| S2 | Al menos 3 grupos modificables con sliders en Settings | Parcial | Tenés solo 2 controles (`MusicVolumeButtons.asset` con `VolumeBackground` y `SFXVolumeButtons.asset` con `VolumeSFX` + `VolumeUI`), y son botones +/- con una imagen de relleno, no `Slider`. Master no se puede modificar |
| S3 | Toda acción del juego se refleja en sonido | Parcial | Te falta el sonido de muerte de los enemigos y el de lanzar `HollyWater` (`PlayerBrain.HandleItemUsed` no tiene `case ItemType.HollyWater`) |

### Datos

| #  | Requisito | Estado | Evidencia |
|----|-----------|--------|-----------|
| D1 | Configuraciones del juego en ScriptableObject | ✓ | Más de 60 clases `*DataSo` con sus `.asset` en `Assets/Data/` (jugador, enemigos, audio, pool, tienda, checkpoints). Hay excepciones puntuales que te marqué en el código (`PlayerAttackArea.cs:27`, `ZombieBrain.cs:143`) |
| D2 | Datos de partida guardados en PlayerPrefs | ✗ | No hay ninguna referencia a `PlayerPrefs` en `Assets/Scripts/`. `GameSessionManager` guarda todo en memoria y se pierde al cerrar el juego |

### Extras

| #  | Extra | Estado | Evidencia |
|----|-------|--------|-----------|
| X1 | Doble salto | ✓ | `PlayerJump.cs:15,23,56` (`_doubleJumpUsed`) |
| X2 | Triple salto por power up | ✗ | No hay power up de salto |
| X3 | Tienda con monedas | ✓ | `Assets/Scripts/Store/StoreManager.cs`, `StoreItem.cs`, `UIStore.cs` |
| X4 | Integración PlayFab | ✗ | No hay referencias a PlayFab |
| X5 | Multijugador local | ✓ | `GameModeController.cs` y `PlayerInputs.cs` con binding groups por jugador y pantalla dividida. Ojo: según el `README.md` el P2 usa J/I/L, no las flechas que pide el PDF |

## 2. Cumplimiento de Pautas de Cursada

| ID | Pauta | Estado | Evidencia |
|----|-------|--------|-----------|
| P1 | Unity 6000.3.21f1 | ✓ | `ProjectSettings/ProjectVersion.txt` dice `6000.3.21f1` |
| P2 | Repositorio público | ✓ | El repositorio es público (revisado el 2026-11-16) |
| P3 | Nombre del repo | ✓ | `TP08_C1_Mironoff_Hunters` |
| P4 | Código en inglés | ✓ | Clases, métodos, campos, enums y escenas en inglés en los scripts que leí. Hay errores de tipeo (`PosionPowder`, `HollyWater`, `RaiseHealhChanged`), pero nada en español |
| P5 | Un repo por TP, sin branches | ✓ | `git branch -a` muestra solo `main`; 87 commits desde el 2026-10-22, sin carpetas de otro TP |

## 3. Estructura de carpetas

| Carpeta esperada | Encontrada | Observación |
|------------------|------------|-------------|
| `Art/Animations/` | ✓ | |
| `Art/Audio/` | ✗ | El audio está en `Assets/Audio/` (mixer, `Music/`, `SFX/`) |
| `Art/Fonts/` | ✓ | |
| `Art/Materials/` | ✗ | `Assets/Art/Shaders/CRT.mat` es un material y está dentro de `Shaders/` |
| `Art/Shaders/` | ✓ | |
| `Art/Sprites/` | ✓ | |
| `Data/` | ✓ | Organizada por sistema |
| `Prefabs/` | ✓ | |
| `Scenes/` | ✓ | |
| `Scripts/` | ✓ | La organizaste por sistema (`Player/`, `Enemy/`, `Core/`, `UI/`...) en vez de `Data/Game/Ui`, con los `*DataSo.cs` en subcarpetas `Data/` de cada sistema. No se penaliza |

Carpetas extra: `Assets/Physics/Materials/` (materiales físicos), `Assets/Art/SpriteAtlas/`, `Assets/Art/TilePalletes/`, `Assets/TMPEffects/`, `Assets/Settings/` (URP). No se penalizan.

Archivos mal ubicados:
- `Assets/Audio/` tendría que estar en `Assets/Art/Audio/`.
- `Assets/Art/Shaders/CRT.mat` tendría que estar en `Assets/Art/Materials/`.

Ruido de repo: `Packages/com.unity.render-pipelines.universal/` es el paquete de URP completo embebido en el repositorio (534 archivos `.cs`). No lo corregí, pero si no lo modificaste te conviene volver a usarlo desde el Package Manager. El `.gitignore` de Unity está y `Library/` no está versionado.

## 4. Qué le falta
- Guardar los datos de partida en `PlayerPrefs` (monedas, checkpoints alcanzados, jefe derrotado, volúmenes). Lo pide la consigna.
- Pickables de Heal, Damage e Invulnerability en el mundo (por ejemplo, como drop de enemigos con `PickableSpawner`), además de la versión de tienda.
- Partículas y sonido cuando mueren los enemigos.
- Al menos 3 controles de volumen independientes en Settings, idealmente `Slider`, incluyendo Master.
- Corregir los bugs de efectos superpuestos (`InvulnerabilityEffect`, `DamageEffect`) y el de consumir la curación con la vida llena.

## 5. Hallazgos de código

### Error (0)

### Bug (4)
- `Assets/Scripts/Player/PlayerBrain/PlayerBrain.cs:449` Con la invulnerabilidad activa no se resta vida, pero suena `DamageSFX` y se aplica el knockback.
- `Assets/Scripts/Player/PlayerBrain/Controllers/EffectsController/Effects/HealEffect.cs:21` Si usás `HealingBalmItem` con la vida completa, se consume el ítem sin curar.
- `Assets/Scripts/Player/PlayerBrain/Controllers/EffectsController/Effects/InvulnerabilityEffect.cs:49` Con dos `WardingCharm` seguidos, la primera corrutina apaga la invulnerabilidad de la segunda.
- `Assets/Scripts/Player/PlayerBrain/Controllers/EffectsController/Effects/DamageEffect.cs:49` Con dos efectos de daño seguidos, la primera corrutina vuelve el daño a `Normal`.

### Warning (11)
- `Assets/Scripts/Player/PlayerBrain/PlayerBrain.cs:139` `StopMovement()` modifica el Rigidbody2D desde `Update` en cada frame mientras el jugador está muerto.
- `Assets/Scripts/Player/PlayerBrain/Controllers/Combat/PlayerAttackArea.cs:27` El multiplicador `1.2f` está hardcodeado fuera de un ScriptableObject.
- `Assets/Scripts/Enemy/Zombie/ZombieBrain.cs:143` La probabilidad de sonido es por frame, así que depende de los FPS, y usa números mágicos.
- `Assets/Scripts/Enemy/Shared/Components/Combat/RangeAttack.cs:26` `GetComponent` en cada disparo.
- `Assets/Scripts/Persistence/GameSessionManager.cs:20` Siete campos de checkpoint y un switch de siete casos.
- `Assets/Scripts/Checkpoints/FastTravel.cs:56` `Update` reasigna `interactable` a siete botones en cada frame.
- `Assets/Scripts/GameManager/GameManager.cs:9` Campos `public static` mutables.
- `Assets/Scripts/UI/UIMenuManager.cs:66` String mágico `"Gamepad"`.
- `Assets/Scripts/ObjectPooling/ObjectPool.cs:76` Devuelve `null` con el pool agotado y los que lo llaman no lo chequean.
- `Assets/Scripts/Player/UI/UIHealth.cs:26` Accedés a `objs[i]` sin validar el tamaño de la lista.
- `Assets/Scripts/Throwables/Throwable.cs:26` Explota con cualquier trigger, sin filtro.

### Suggestion (16)
- `Assets/Scripts/Player/PlayerBrain/Controllers/Combat/PlayerAttackArea.cs:32` El daño se recalcula en cada `Update`.
- `Assets/Scripts/Enemy/Zombie/ZombieBrain.cs:26` La variable local `_rb` tiene guion bajo.
- `Assets/Scripts/Checkpoints/Checkpoint.cs:102` Switch duplicado del de `GameSessionManager`.
- `Assets/Scripts/UI/UIMenuManager.cs:77` `RemoveAllListeners()` en vez de `RemoveListener`.
- `Assets/Scripts/ObjectPooling/ObjectPool.cs:117` `DisableBossBullets()` es idéntico a `DisableBullets()`.
- `Assets/Scripts/Throwables/Throwable.cs:11` Un `[SerializeField]` que después pisás en `Awake`.
- `Assets/Scripts/Player/PlayerBrain/Controllers/Inventory/InventorySlot.cs:27` Quedó un comentario de duda.
- `Assets/Scripts/Enemy/Shared/PickableSpawner.cs:27` La local `RandomChance` está en PascalCase.
- `Assets/Scripts/Enemy/Shared/PickableSpawner.cs:44` Quedaron notas de planificación.
- `Assets/Scripts/Player/PlayerBrain/Controllers/Health/PlayerHealthController.cs:88` La vida al reaparecer está hardcodeada en 1.
- `Assets/Scripts/Player/PlayerBrain/Controllers/Health/PlayerHealthController.cs:95` Typo en `RaiseHealhChanged`.
- `Assets/Scripts/Core/Inventory/ItemType.cs:9` Typos en `PosionPowder` y `HollyWater`.
- `Assets/Scripts/Store/StoreManager.cs:53` Lógica de compra duplicada por jugador.
- `Assets/Scripts/Audio/AudioManager.cs:23` Cálculo dentro del loop y factor 30 en vez de 20.
- `Assets/Scripts/Core/GameMode/GameModeController.cs:7` Quedó el flag de prueba `_testCoop`.
- `Assets/Scripts/Pause/PauseManager.cs:25` Código comentado.

## 6. Recomendaciones

1. Guardá monedas, checkpoints alcanzados, `GameManager.BossKilled` y volúmenes en `PlayerPrefs`, con las claves como `const` (D2).
2. Agregá pickables de Heal, Damage e Invulnerability a `PickableType` y al `PickableSpawner` de los enemigos, reutilizando `HealEffect`, `DamageEffect` e `InvulnerabilityEffect` (K1 a K3).
3. Agregá un `ParticleSystem` y un `AudioCategory` de muerte en `EnemyBrain`, que se disparen cuando `IsAlive` pasa a false (E3, E4, S3).
4. Pasá a 3 o 4 controles de volumen independientes (Master, Music, SFX, UI), con `Slider` (S2).
5. Guardá la `Coroutine` de cada efecto en `PlayerEffectsController` y frenala antes de arrancar otra del mismo tipo (dos bugs).
6. En `UseCurrentSlotItem`, validá que el ítem se pueda aplicar antes de descontarlo.
7. Reemplazá los siete campos y switches de checkpoints (`GameSessionManager`, `Checkpoint`, `FastTravel`) por colecciones.
8. Mové `Assets/Audio/` a `Assets/Art/Audio/` y `CRT.mat` a `Assets/Art/Materials/`.
