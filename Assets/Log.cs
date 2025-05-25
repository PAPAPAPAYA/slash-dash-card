// log

//* 2025/05/05
//* misc
	//// switch to rider
//* card
	// use object pooling for bullets
	// copy last used card
	//// madness cut: each time it's used, +1 dmg
	//// add ammo, when there is any ammo left, shoot out bullet when card drawn (ammo amount can be set in card)
	//// discard next card, add 3 ammo
	//// self burn, explosion when entering grave//todo: object pool
	//// reset madness // todo: invokes event in lingeringEffectManager, but timing is off
//* system
	// get card
	//// enlarge current card
	//// show card
	// arrange card
	//// debug grave order
//* structure
	//// use card to set ability variables//todo: when it comes to it, change the corresponding ability func
	//// use card to store effect and timing? //todo: card function ready to be tested, start with slashing state and work backwards
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