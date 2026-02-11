
06/02/2026

- Implemented basic debuff system
- Implemented bouncing and sticky goop mask
- Created setup for randomized tile generation
- Optimized coroutines
- Added animation to items from chests

07/02/2026

- Significantly improved performance
	- Added sprite atlas
	- Improved coroutines
	- Improved damage popups
- Reworked debuff system to be more dynamic
	- Lingering areas now apply a DOT debuff instead of dealing damage directly

08/02/2026

- Implemented new chest types
- Implemented new masks
	- Luck mask
		- new sprite
	- Magnet mask
		- new sprite
	- Fire rate mask
	- Increase damage mask
- Balance changes
	- chests now increase in price upon opening a chest

09/02/2026

- Implemented damage flash shader

10/02/2026

- Refactored player/enemy data into unified EntityData system
- Refactored masks to use EntityData for player stat interactions
- Implemented knockback system
- Improved healbeam and Titanium (formerly DamagingAura) mask logic