// log

//* 2025/05/05
//* misc
	//// switch to rider
//* card
	// todo: discarding a card as cost messes up card order in the grave
	// todo: madness dmg boost not taking effect
		//// todo: adding non persistent listeners in buff effect is weird
		// todo: reset madness correctly
		// todo: don't add [madness] to name everytime
	//// boost next card dmg
		//// reset
	//// corpse explosion
		//// change unityEvent to pass in dynamic parameter pos
		//// create corpses no exp
			////todo test
			//// change score sprite when is dummy
		// more explosion
			// buff for explosion
		//// chance to spawn more corpses yes exp when killing an enemy
	//// draw bullet
		//// when drawn self, shoot bullet
		//// draw last used card from grave
		// more add ammo
		// more draw
	// discard madness
		//// todo: test discard function
		// more cards to benefit from being discarded
			//// discarded to heal
			// discarded to explode
			//// discarded to draw
			//// discarded to add ammo
			// discarded to add madness
		// more madness
		// more card to discard
			// discard curse to madness
	// pandora heal
		// implement compulsive add curse
		// more curse card
		// more cards that benefit from curses
			// burn all curses in hand, add dmg
			// each curses in hand, add dmg times
		// curse to explode
		// curse to draw
		// curse to add ammo
	// copy last used card
		// debug
	//? self burn poison
		// curse to self burn?
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
      // UI
      // 计算经验值
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