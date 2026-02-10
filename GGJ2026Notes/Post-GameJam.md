[[Masks]]
[[Items]]
[[Data architecture]]
[[VFX]]
[[Assets]]
[[Misc]]
[[Bug report]]
[[Enemies]]
[[UI]]
[[Balancing]]
[[Economy]]
[[World-interactables]]
[[Play tests]]
[[PlayerCharacters]]


- Fix healbeam DOT effect
	- Only apply once per sec, only remove duration after hit
	- If enemy is gone before hit, don't reduce duration and move on to next

- Combat juice:
	- Add enemy knockback on hit
	- Check damage flash shader performance
	- Improve damage popup performance



- Come up with better bogged lingering area solution
	- currently uses exact same prefab - same debuff data
	- can't change data (damage) independently