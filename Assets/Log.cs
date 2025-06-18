// log

//* 2025/05/05
//* misc
	//// switch to rider
//* card
	// add a rubbish card //todo: WIP
		// curse
			// need to be drawn two times
			// burn this card when used
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
	//// check for cost, if cost not satisfied, no effect
	//// object pool enemy
		//// object pool score
		////scores have more than 1 hp, and need to check if i-frame is reset -- turns out i was releasing the scores incorrectly
	// get card // UI is such a fucking mess
		// show card options
			// need to disable player character control
		//// activate new card magnets when a new card is in hand zone
		//// auto shift card holders to right
		//// stop listening for mouse over if already dragging a card
		//todo: add a button to confirm order and cards in hand
		//todo: allocate actual cards to card options
		// todo: because the card options are enlarged and shrinks when dragged, when dragging outside the original size, it flickers between enlarged and shrunk
		// todo: shift magnet on the left side's myCard if a new card is in between two existing card holders / card magnets
			// need to add newly obtained card to hand, or else auto shift right will bug out
				// even added newly obtained card's cardholder to cardholder_hand, auto shift right is still bugging out, maybe need to refresh the order of the list?
			// need to optimize to shift left or shift right
		
			//// no shifting
			//// or
			//// auto shift right, shift all cards on left/right when inserting a new card
		//// make hand and grave magnets and assign them
		//// make cards moveable with mouse drag
		//// cards in hand appear bigger when in grave
		//// snap cards to position //when getting card being dragged position through card ui manager, the position doesn't update
	// when player dash into an enemy, player get stuck inside an enemy
	//// enlarge current card
	//// show card
	// arrange card
		// activate card moveability only when CardObtainManager is showing options
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