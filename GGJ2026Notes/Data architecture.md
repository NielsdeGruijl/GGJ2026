- Can inventory masks be simplified further?
	- One prefab with dynamic component addition?

https://www.youtube.com/watch?v=cc5l66FwpQ4


- Enemy wave spawning
	- more delay between enemies spawning
	- avoid collisions on spawn

- Improve object pooling
	- Enable storing objects of types to avoid Get Component calls



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