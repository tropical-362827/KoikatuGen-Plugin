# KoikatuGen-Plugin
A Koikatu Plugin to Generate Koikatu Character by Using Deep Learning Model / 深層学習でコイカツキャラを生成するコイカツ用プラグイン

## このプログラムは? / About this program
このプログラムは, コイカツ公式アップローダーのデータを使い[KoikatuGen](https://github.com/tropical-362827/KoikatuGen)で学習した機械学習モデルの重みファイルを使って, ゲーム内から直接キャラを生成できるようにするコイカツMODです. (学習済みパラメータをセットで配っているのでそのまま使えます)

![](https://i.imgur.com/GEC068x.png)

## 必要なMOD / Requirements
[KK_Plugins](https://github.com/DeathWeasel1337/KK_Plugins)をベースに作っているので, それと同じMODが必要となります. つまり,
- BepInEx v5.3
- BepisPlugins (の中のExtensibleSaveFormat)
- IllusionModdingAPI

が必要です. HFPatch環境であればおそらくそのまま動作すると思います.

## インストール / Installation
[リリースページ](https://github.com/tropical-362827/KoikatuGen-Plugin/releases)から`KoikatuGen-Plugin.zip`をダウンロードし解凍した後, コイカツフォルダ内の `BepInEx` に上書きコピーしてください.

## How to use / 使い方
MODを入れた後キャラメイクを起動すると, 体型をいじるメニューに`キャラ生成`という欄が追加されます. その中の`キャラクターを生成する`を押します.

### 学習Epochについて
Epochは機械学習の分野で学習回数を表す単位です. このEpoch数が多ければ大きいほど学習データに近いキャラが生成される一方で, 個性的なキャラクターが生成できなくなります.
個性的なキャラが欲しければ少ないEpoch, 安定感が欲しければ高いEpochのパラメータを使ってください.

## [KoikatuGen](https://github.com/tropical-362827/KoikatuGen)で学習したファイルを使う / Using parameters trained by [KoikatuGen](https://github.com/tropical-362827/KoikatuGen)
(書いている途中)

## 謝辞 / Acknowledgements 🙇
このプログラムはDeathWeasel1337さんの[KK_Plugins](https://github.com/DeathWeasel1337/KK_Plugins), 特に[RandomCharacterGenerator.KK](https://github.com/DeathWeasel1337/KK_Plugins/tree/master/src/RandomCharacterGenerator.KK)をベースに作られています.

さらに, 乱数生成部分に[C++マニアックさんのメルセンヌ・ツイスタの実装](http://stlalv.la.coocan.jp/MersenneTwister.html)を使用しています.

この場で感謝申し上げます.🙇
