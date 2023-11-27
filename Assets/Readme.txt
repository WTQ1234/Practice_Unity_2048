todo
禅模式 基础UI
没有战斗，只有分数，可以使用撤回
可以自定义长宽大小？
和战斗模式相区分，单独的UIManager或者做出Dialog界面
将基础的UIManager 转换成 Dialog

Manager 本身是希望做解耦


存档——三个模式的存档
各有特色，比如禅模式还需要存积分和长宽

声音的效果 钢琴
雨的manager 暂时屏蔽雨声


主界面上挂的音效

目前的大块功能：
水————跟水的机制挂钩
雨的效果————雨太多了太烦人 留几个接口跟难度挂钩

战斗特效
主界面的UI —— 具体逻辑缺失
禅模式的搭建
故事模式——每关的敌人、难度、故事 LevelData
todo 难度系数考虑加上buff  即每个属性按一定倍率进行叠加和缩放

水的机制和难度的机制对游戏的影响

关卡的配置——敌人有哪些等等
主界面-进入游戏-感谢名单-版权警告
主界面的图可以都搞上边框统一一下
模式：
禅模式

故事模式——剧情
    基础的对话界面
无尽模式——专注战斗（待定）


两边战斗的时候给一点特效 类似一个小导弹的爆炸？ Number里面有一个todo
敌人攻击的动画，后续和特效，震屏等都提出做为Action
敌人释放技能时考虑把棋盘冻结，做一个起雾特效

考虑把UI移动到中间或左边，从而把视觉焦点集中在左边

其他模式里自带设置按钮：
包括横屏和竖屏设置等等

设置界面
考虑多语言
游戏模式中是否有水的影响
表现上是否有水和水声
钢琴曲的选择

致谢界面

声音——钢琴弹奏的实现和基础的音乐，去掉其余音效
—— 播放钢琴曲 或人声 或自动钢琴

敌人死掉和自己死掉的处理——成功、失败、重来基础UI？
大头特效：考虑做类似Mirror的破防，或者用在战败里
胜利：带个墨镜
井字的特效表示激怒效果

6个人的技能
作为伙伴的技能？

// 玩家当前的技能State —— 指道具State，扣血是直接扣的

将撤回提出作为技能

基础的一些UI动画如Tips等
对话，打字机效果以及相关配置
5份技能
想法1:5个敌人，类似mirror
每个人两个技能：
每隔n个回合发动技能 1，普攻，2.增益技能，3.终极技能
分为增益，减益，禁锢格子，大量伤害等

2：5个伙伴，每个伙伴带有技能，技能可以充能 4-8-16-32-64 5个槽位，每次合成只能数字，即给槽位充能，然后发动技能


技能可选？需要成就来解锁？

Buff
连打：当有足够空间，并且存在Combo时，额外生成一个数字



UI水效果-波浪UI
https://assetstore.unity.com/packages/tools/gui/waves-ui-173558
花海？
https://assetstore.unity.com/packages/tools/particles-effects/coral-fur-flutters-effect-186465
2D流光
https://www.bilibili.com/video/BV1CR4y1T7Kn
翻页的文字和钟的UI，考虑做难度和倒计时？
https://assetstore.unity.com/packages/tools/gui/flip-text-clock-163265
照片粒子变成下雨，考虑做战败动画
https://assetstore.unity.com/packages/tools/particles-effects/image-particle-rain-effect-182277
照片被分成格子逐个绘制，考虑做隐藏的boss显露出真容？最后解锁一张cg？
https://assetstore.unity.com/packages/tools/particles-effects/2d-photo-deconstructed-effect-169297


水面效果
https://www.bilibili.com/video/BV12b4y1X7Hj?spm_id_from=333.337.search-card.all.click
https://www.bilibili.com/video/BV1Tq4y1j7Nu?spm_id_from=333.337.search-card.all.click

浮力
https://www.bilibili.com/video/BV1Et411y73k?spm_id_from=333.337.search-card.all.click
https://www.bilibili.com/video/BV1JU4y187DU/?spm_id_from=333.788.recommend_more_video.2

屏幕效果
https://blog.csdn.net/poem_qianmo/article/details/49556461

下雨

考虑屏幕上的水起雾或者附着雨滴

回合音效————考虑音乐可视化————一个进度条，在规定时间内下一回合，才有Combo，以及伤害加成
BattleEffect？


道具-
玉米肠-回血

剧情：
枝江暴雨
打5个人
然后获取5个伙伴
伙伴对应数字，合成对应数字时充能，充能后触发技能
每个人物有5个小技能    和作为伙伴的1个小技能
作为伙伴的和作为敌人的
1 普攻
2 3倍伤害
3 治疗/为自己添加增益
4 为敌人施加减益
5 改变地图（如打乱顺序，锁定指定格子，）

晚 A 1  4  下雨，改变机制，可以控制天气，进而控制水面的高度
贝 B 2  8  击碎一个指定的数字，无法获得怒气，造成伤害
迦 C 3  16  todo待定 召唤骑士，可抵挡100血量伤害，期间造成1.5倍伤害
然 D 4  32  圣嘉然的宽恕：给予一次撤回机会
乃 E 5  64  将两个指定的数字交换位置

雨水机制：每次合成A，都会下雨
雨代表难度
雨水会上涨——水面以下的数字会上浮，以上的数字会落下

敌人：每隔一定时间算一个回合
时间之内进行可以达成combo
combo可以使敌人的回合间隔+1s

在指定的行数进行合成，将会影响机制，比如造成不同类型的伤害，或者1，4行打伤害，2行加怒气，3行是治疗
或者水上是伤害+怒气效果，水下是伤害+治疗效果

细雨  基础模式
暴雨  敌人释放技能变快
季风  棋盘会扩张
龙宫  所有数字会上浮

有怒气和回血的设定么？

最后打羊驼？打鸟巢？

字体：
文悦新青年体  好像是切片常用的

过场动画
考虑下载成视频或动图，作为关卡加载
https://www.bilibili.com/video/BV1qr4y1v7o7?spm_id_from=333.999.0.0
https://www.bilibili.com/video/BV1GS4y1y7eB?spm_id_from=333.337.search-card.all.click



图片来源
技能
https://space.bilibili.com/2979309
主图
https://t.bilibili.com/648084816703520833?tab=2
玉米肠
https://t.bilibili.com/649599973969625104?tab=2

标题1
https://t.bilibili.com/648105578600595459?tab=2
标题2
https://t.bilibili.com/653035715641212966?tab=2
标题3  授权ok
https://www.bilibili.com/video/BV1sY411A77d?spm_id_from=333.999.0.0

好看但是没用到的
https://t.bilibili.com/649751826833342488?tab=2
https://t.bilibili.com/591763501742193099?tab=2
https://t.bilibili.com/652270004576714770?tab=2
https://t.bilibili.com/654582908896411657?tab=2
https://t.bilibili.com/654865981609345047?tab=2
https://t.bilibili.com/645656725668495416?tab=2
https://t.bilibili.com/652601151110250497?tab=2

以后可能用到
能力雷达图
https://blog.csdn.net/linxinfa/article/details/115661299
MMD3D模型
https://www.aplaybox.com/u/554009131