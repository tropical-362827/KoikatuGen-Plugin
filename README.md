# KoikatuGen-Plugin
A Koikatu Plugin to Generate Koikatu Character by Using Deep Learning Model / 深層学習でコイカツキャラを生成するコイカツ用プラグイン

## About this program / このプログラムは?
このプログラムは, [KoikatuGen](https://github.com/tropical-362827/KoikatuGen)で学習したVariational Autoencoderの重みファイルを使って, ゲーム内から直接キャラを生成できるようにするコイカツMODです.

![](https://i.imgur.com/HVbzMaW.png)

## Requirements / 必要要件
KK_Pluginをベースに作っているので, それと同じライブラリが必要となります. つまり,
- BepInEx v5.3
- BepisPlugins
- IllusionModdingAPI
が必要です.

## Acknowledgements / 謝辞 🙇
このプログラムはDeathWeasel1337さんの[KK_Plugins](https://github.com/DeathWeasel1337/KK_Plugins), 特に[RandomCharacterGenerator.KK](https://github.com/DeathWeasel1337/KK_Plugins/tree/master/src/RandomCharacterGenerator.KK)をベースに作られています.

さらに, 乱数生成部分に[C++マニアックさんのメルセンヌ・ツイスタの実装](http://stlalv.la.coocan.jp/MersenneTwister.html)を使用しています.

この場で感謝申し上げます.🙇
