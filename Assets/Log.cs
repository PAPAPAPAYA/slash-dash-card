// log

//* 2025/05/05
//* Doc
	// move cards first, then invoke effect
	// card movement logics are in CardManagerNew, CardUIManager will update according to CardManagerNew's lists
	// currently many events pass in enemy object, including counter effect, might fuck me later
	// does on kill effect counts score kill?
//* misc
	// document
	//// switch to rider
//* card
	//* passive effects
		// when drawn, add lingering effect
		// when selected, add lingering effect; when not in hand or grave, remove lingering effect
		//// when used, add lingering effect
		// when in hand, add lingering effect, when in grave, remove lingering effect
	//* counter effects
		//// counter
			//// when enemy hit, counter++
			//// when enemy killed, counter++
	//* on enemy killed
		//// when used, add lingering effect: on enemy killed effects become on enemy hit
		//// when hit x enemies, spawn a fake score
		//// high dmg, enemy will have 1 hp left
		//// when enemy killed, might spawn more scores
		//// when enemy killed, explosion
		//// when enemy killed, +madness
		//// when x enemies killed, hp+
		//// when enemy killed, next card add effect: on hit, explosion
		// add lingering effect: when poison kills: spawn 1 fake score
	//* explosion
		// on hit: apply debuff: when dies, explosion
		// when in hand, add lingering effect: each time player is hit, explosion
		// add lingering effect: explosion become poison
		// add lingering effect: bullets explode
		// discard 1, on hit: explosion
		// consume 2 bullets, on hit: explosion
		// self burn, on hit: explosion
	// * poison
		// poison: need to transfer from old structure, resolve when need to reload
		// on hit: apply poison
		// gain curse, on hit: apply poison
		// when drawn, add lingering effect: when dmg is dealt: apply poison
		// on hit: double poison stack
		// apply lingering effect: when dmg is dealt: apply that many poison
		// add lingering effect: bullets apply poison
		// on hit: apply poison based on madness stack
		// add lingering effect: poison resolves quicker
		// on hit: deal dmg x target poison stack
		// self burn, on hit: deal dmg based on target poison stack
	//* madness
		// madness: when dealing dmg, +dmg
		// when a bullet is consumed: + madness
		// if discarded: +madness
		// +madness
		// add lingering effect: when hurt: +madness
		// +madness, add debuff: - madness
		// when kill: + madness
		// discard all curses: add madness
		// gain curse (self burn), when played 5 times: heal based on madness stack
		// deal dmg x madness stack
	//* multi-hit / aoe
		// deal dmg x 2
		// discard next card: deal dmg x 4
		// buff next card: +range
		// buff next card: hit count + 1
		// deal dmg x curse count in grave
		// deal dmg x (max hp - current hp)
		// add lingering effect: when hit: deal dmg
	//* high dmg
		// buff next card: +dmg
		// deal dmg = current hp
		// deal dmg; when full health: deal more dmg
		// deal dmg = curse count in grave
		// + bullet based on last dmg dealt
	//* hp
		// when drawn: + max hp
		// when discarded 3 times: heal
		// when activated 5 times: heal
		// when overheal: +bullet
	// * self burn
		// self burn: + bullet
		// (4 - lost hp) bullets: high dmg
	//* bullet
		// when consume, shoot out bullet; mana
		// if player has bullet, more dmg
		// + bullet based on [bullet] in grave
		// + bullet based on poisoned enemy count
		// discard next, + bullet; + bullet based on discarded bullet cost
		// + bullet
		// when drawn: when discard, + bullet
		// when discarded: + bullet
		// add lingering effect: when draw, shoot out bullets based on current bullet stack
		// add lingering effect: when +bullet, +bullet
		// if bullet stack > 0: deal more dmg
		// consume all bullet stack: deal dmg x bullet stack consumed
	//* discard
		// discard next: deal dmg
		// discard next: draw card from grave
	//* utility
		// draw a [madness] card from grave
		// repeat last card
		// draw a [bullet] card from grave
	// need to add reset funcs to all cards
	//// todo: discarding a card as cost messes up card order in the grave
		//// try pay cost event only check if the cost can be satisfied, pay cost in activation event
		//// not buffing
		//// order still wrong, hand index!!
	//// todo: madness dmg boost not taking effect
		//// todo: adding non persistent listeners in buff effect is weird
		//// todo: reset madness correctly
		//// todo: not applying madness dmg boost
		//// todo: reset cards when they are drawn
		//// todo: don't add [madness] to name everytime
			//// reset name when drawn
		//// madness effect component is destroyed when resetting, and is only added when card doesn't have it already
	//// boost next card dmg
		//// reset
	//* corpse explosion
		//// change unityEvent to pass in dynamic parameter pos
		//// create corpses no exp
			//// todo test
			//// change score sprite when is dummy
		// more explosion
			// buff for explosion
		//// chance to spawn more corpses yes exp when killing an enemy
	//* draw bullet
		//// when drawn self, shoot bullet
		//// draw last used card from grave
		// more add ammo
		// more draw
		// boost ammo dmg
	//* discard madness
		//// todo: test discard function
		// more cards to benefit from being discarded
			//// discarded to heal
			// discarded to explode
			//// discarded to draw
			//// todo:  discarded to draw [ammo] WIP
				//// when adding cards to hand, should we draw them and reorder the grave or copy them from grave to hand? since if we only draw them straight from grave, the order is messed up
					//// drawing and reorder later introduces more depth, but would it be too complicated?
					//// yes it would, don't care about grave order, just draw them in hand order when reloading
				//// todo: null ref error is thrown
			//// discarded to add ammo
			//// discarded to add madness
		// more madness
		// more card to discard
			//// todo need testing: discard all curse to madness
	//* pandora heal
		//// todo: implement compulsive add curse
			//// time point: when added to hand in upgrade menu
			//// fix invoking "when selected" time point  skipping confirm button func
		// more curse card
			//// self burn
			//// discard
		// more cards that benefit from curses
			// burn all curses in hand, add dmg
			// each curses in hand, add dmg times
		// todo test curse to explode
		// todo test curse to draw
		// todo test curse to add ammo
	// copy last used card
		// debug
	//* self burn poison
		// give poison dot
		// poisoned death spawn bullets
		// poisoned enemy spawn more corpse
		// poisoned explosion
		//// curse to self burn
	//// add a rubbish card
		//// curse
			//? need to be drawn two times
			//// burn this card when used
			//// self burn
	//// heal
	//// death explosion: need to distinguish source of damage
	//// revamp ammo: everytime the player draws, shoot out bullets based on ammo count
	//// use object pooling for bullets
	//// copy last used card
	//// madness cut: each time it's used, +1 dmg
	//// add ammo, when there is any ammo left, shoot out bullet when card drawn (ammo amount can be set in card)
	//// discard next card, add 3 ammo
	//// self burn, explosion when entering grave
	//// reset madness //invokes event in lingeringEffectManager, but timing is off
//* system
	// need to show madness & ammo
	// tag
		// need to modify DrawAmmoCard() implementation
	//// todo: don't care about grave's order, when reloading, draw cards in hand order
	////!: when entering upgrade menu and moving all cards from grave to hand, the order is messed up
	// different rarity and different chance
	//// show slash dmg
	//// todo last card in grave doesn't need to be dashed
	//// todo refs of magnet and cardholder become missing when putting a card to option magnet then putting it back
	//// todo when putting card to option magnet, card ui manager's cardholders_hand doesn't update
	//// todo when confirming new cards, first card is used
	////todo WIP need to instantiate card along with cardholder, or copy last card is bugged coz when one card is altered, all cards with the same name are altered
	//// check for cost, if cost not satisfied, no effect
	//// object pool enemy
		//// object pool score
		////scores have more than 1 hp, and need to check if i-frame is reset -- turns out i was releasing the scores incorrectly
	//// todo when player dash into an enemy, player get stuck inside an enemy
		//// todo stop player when player hit an enemy without invincibility
	//// get card // UI is such a fucking mess
		//// clicking confirm button will trigger ammo effect: because cards are drawn
		////todo new cards automatically sorted to last in hand
		////todo can't shift right when there's space
			////todo actually the card being dragged was snapped to a new magnet before shifting right
		////todo when there is no space, shifting is bugged
		////todo placing card holder to the empty space will cause auto shift right to ignore the newly placed card
			//// current implementation will prevent said problem, but need to optimize call frequency of AutoShiftRight
		//// disable card dragging when in game state
		//// show card options
			//// need to disable player character control
				//// card usage disabled
				////todo can also disable player input ui
		//// activate new card magnets when a new card is in hand zone
		//// auto shift card holders to right
		//// stop listening for mouse over if already dragging a card
		//// todo: add a button to confirm order and cards in hand
			//// todo: show and hide confirm button
		//// todo: allocate actual cards to card options
		//// todo: because the card options are enlarged and shrinks when dragged, when dragging outside the original size, it flickers between enlarged and shrunk
		//// todo:  optimize to shift left or shift right
		//// todo: shift card holders on the left side's myCard if a new card is in between two existing card holders / card magnets
			//// need to add newly obtained card to hand, or else auto shift right will bug out
				//// even added newly obtained card's cardholder to cardholder_hand, auto shift right is still bugging out, maybe need to refresh the order of the list?
			//// shifting when there's no space in hand
				//// shift left done, need to implement shift right
			//// when no space in hand, auto shift right is weird
			//// remove card holder from hand when it's not in hand
			//// shifting cards double booking
			//// no shifting
			//// or
			//// auto shift right, shift all cards on left/right when inserting a new card
		//// make hand and grave magnets and assign them
		//// make cards moveable with mouse drag
		//// cards in hand appear bigger when in grave
		//// snap cards to position //when getting card being dragged position through card ui manager, the position doesn't update
	//// enlarge current card
	//// show card
	//// arrange card
	//// debug grave order
//* structure
	//// use card to set ability variables//todo: when it comes to it, change the corresponding ability func
	//// use card to store effect and timing? //card function ready to be tested, start with slashing state and work backwards
	//// send card to grave
	//// invoke OnToGrave event
	//// ammo
	//// support multiple abilities in one card

//* 2025/01/01
//* spawner优化
      // 跟场景不跟人
      // 根据spawner允许的敌人数，拉远镜头，扩充场景
      // spawn敌人在围墙内
//* upgrade
      //// UI
      //// 计算经验值
      // abilities
	    // 增加伤害
	    // 增加击中时的stun
	    //// 增加碰撞体数量
	    //// 被击中时范围击飞
	    //// 被击中时aoe
	    //// 击中时小刀
		  //// 小刀homing
		  //// 小刀击中aoe
		  //// 小刀有生命
	    //// 击杀aoe
	    //// 刹车时aoe
	    // 尸体弹跳碰撞造成伤害
	    //// 尝试当无法击杀时，玩家会马上停下，击飞敌人
	    	//// 将这个东西做成能力
		//// 算了，即使一次过让玩家能推开多个敌人，假如玩家马上停下则拉不开距离，假如玩家反弹则不可预知玩家会去到哪
//* 敌人
	//// 解决敌人重叠问题的同时，让玩家穿过敌人的同时，触发伤害和受伤