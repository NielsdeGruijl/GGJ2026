- Can inventory masks be simplified further?
	- One prefab with dynamic component addition?

https://www.youtube.com/watch?v=cc5l66FwpQ4


- Enemy wave spawning
	- more delay between enemies spawning
	- avoid collisions on spawn

- Improve object pooling
	- Enable storing objects of types to avoid Get Component calls


EntityStatModifier
- enum StatType
- enum StatModificationType
- float changeValue
- 

 PlayerData class
 - Stores EntityDataSO (baseData)
	 - BaseData
	 - List EntityStatModifier LevelUp data
 - DataModifiers
	 - Flat change
	 - Mult change
- void ApplyStatChange(EntityStatModifier)

- Rework playermaskdata and DebuffData
	- Use EntityData instead?




===== done =====

- Debuff System:
	- EntityDebuffManager
	- BaseDebuff C# class
		- Debuff
	
	- DebuffSO
		- DOT
		- Flat
		- Mult
		- Custom
	- DebuffSO return new BaseDebuff