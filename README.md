# Item-Stat-System
This is a system that includes basic functionalities of items, inventories, stats, status effects, 
use effects/actions, and logging. A lot of the architecture revolves around scriptable objects.
Much of the used data can be created through right clicking -> Create -> <some scriptable object>.
There's also a demo scene along with some scriptable objects showing off how everything works.
References can be checked in UI elements and the SaveableEntity gameObject.

Note: This system uses the Scriptable Object Architecture(SO Architecture) asset 
from Everland Games. Import it before using this.

Features
- Stats
- Items, Inventory
- Status Effects
- Logging
- Use Effects

The stats, items/inventory, and status effects use the GameEvent class from the SO Architecture asset. These will
be things like the CharacterHealthDataGameEvent SO (named "Player1HealthChanged") being referenced by the player's
CharacterStats component and the player's UI. In OnEnable, the UI might call AddListener() and update its elements
when the InventoryManager calls Raise(). For implementation details, check the scripts 
PlayerUIManager and CharacterStats(in the CurrentHealth setter and TakeDamage). To create events after importing
SO Architecture, go to ContextMenu -> Game Events -> Custom -> <EventType>

Stats
-------------------
- Components:
    - CharacterStats - attached to anything that uses stats.
- Stats mainly consist of BaseStatValues, StatDataCollection, and StatData.
    - StatData - ScriptableObject
        - Technically more like stat type, such as Strength or Defense, etc. It was named data in case
            there's any reason to add more data inside the class.
    - StatDataCollection - ScriptableObject
        - A collection with all the stats any entity should have.
        - Used by BaseStatValues for initialization, so ideally create this before any BaseStatValues.
    - BaseStatValues - ScriptableObject
        - The base or initial values for all of an entity's stats.
        - Consists of StatData and their values.
        - Currently implemented using 2 lists because dictionaries aren't serialized in the inspector,
            so make sure lists align together correctly (ex. index 0 of StatData list should align with 
            index 0 of the float list and so on).

Items/Inventory
-------------------
- Components: 
    - InventoryManager - attached to anything that will be affected by status effects.
- Uses an Inventory scriptable object to hold items.
- InventoryManager just references an inventory SO.
- Note: may not really be so useful anymore as an SO since there is the SO Architecture GameEvent, plus
    the implementation is probably better off in the InventoryManager.

Status Effects
-------------------
- Components: 
    - StatusEffectManager - attached to anything that will be affected by status effects.
- Created through ContextMenu -> Stats -> Status Effect
    - Related: ContextMenu -> Use Action -> Status Effects -> <StatusEffectUse>
- Status effects can be applied using the StatusEffectUse SO.
- There are apply, update, and remove uses which get called at specific times.

Logging
-------------------
- Created through the context menu under "Logging".
- Takes a color and prefix such as System, Environment, Player, etc. and will append to the beginning of logs.
- Used to simplify displaying of logs for certain groups of objects and can be turned off to 
    improve performance in builds (although log statements can be manually deleted to speed things up more,
    it's just time consuming to find everything).

Use Effects
-------------------
- This consists entirely of scriptable objects.
- Can be used by things such as Status Effects, Items, anything that references the Use base class.
- Ex. Damage, ApplyStatusEffect, Heal, etc. These types will search for a certain component and
    pretty much do whatever its name says. In the context menu, it's listed under "Use Action".
