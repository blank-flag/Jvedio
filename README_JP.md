# 一部個人jvedio原因で、しばらく守護のため、現在の最新版[4.6.0.0](https://github.com/hitchao/Jvedio/releases/tag/Jvedio4.6.0.0)、やダウンロード[他版](https://github.com/hitchao/Jvedio/releases)



[中文](README_CHS.md) [English](README.md) [日本語](README_JP.md)


<h1 align="center">Jvedio</h1>





<h3 align="center">ローカルビデオ管理</h3>




---






&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Jvedioは、ローカルビデオのスキャンとソフトウェアのインポートをサポートして、ビデオライブラリを確立するローカルビデオ管理ソフトウェアです。
ビデオの一意の識別コードを抽出し、ビデオを自動的に分類し、
タグを追加して動画を管理し、人工知能を使用して俳優を特定し、翻訳情報をサポートし、
ウィンドウデスクトップ上のFFmpeg、スムーズで美しいアプリケーションソフトウェアに基づいてビデオ写真をキャプチャします


公式ウェブサイト：[Jvedio](https://hitchao.github.io/JvedioWebPage/) | ダウンロードリンク：[最新バージョン](https://github.com/hitchao/Jvedio/releases)






[![クリック・ビュー・ソフトウェア](https://s3.ax1x.com/2021/03/06/6u8UJA.png)](https://s3.ax1x.com/2021/03/06/6u8UJA.png)
[![クリック・ビュー・ソフトウェア](https://s3.ax1x.com/2021/03/06/6u8oeU.png)](https://s3.ax1x.com/2021/03/06/6u8oeU.png)

---

## 1 背景
Jvedioは、ローカルビデオの認識とスキャン、画像表示、スクリーニング、およびスクレイピングを統合するビデオ管理ソフトウェアです。 現在、GitHubの同様の管理ソフトウェアには、[jellyfin](https://github.com/jellyfin/jellyfin)、[Emby](https://github.com/MediaBrowser/Emby)、および上の既存のソフトウェアが含まれています。市場には、Extreme Shadow Pie、KODI、PLEXなどが含まれます。

|ソフトウェア|主なライティング言語|サポートプラットフォーム|
|-|-|-|
| Jvedio | C＃| win7、win8、win10 |
| jellyfin | C＃|すべてのプラットフォーム|
| Emby | C＃|すべてのプラットフォーム|
| Jiyingpai |？|ウィンドウ|
| KODI |？|-|
| PLEX |？|-|


### 1.1 開発の本来の意図

コンピュータに保存される映画が増えるにつれ、優れたビデオ管理ソフトウェアへの欲求が強くなります。embyとPLEXを使用した後、いくつかの**問題**が見つかりました。

- 起動方法：最初にサーバーを起動し、次にWEBを使用してソフトウェアを管理します
- 情報の保存方法：画像とメタデータ情報をムービーの同じディレクトリにまとめると、非常に面倒な情報転送につながります
- インターフェース：WEB書き込みインターフェースは効率的でシンプルですが、フォームの余白、影、ズーム動作、最大化および最小化ボタンを完全にカスタマイズできることを本当に望んでいます。
- 機能：データフィルタリングが十分に豊富ではありません
- 感情：中国人によって開発されたビデオ管理ソフトウェアの欠如

そこで、Jvedioを開発しました。Jvedioと動画管理についての理解を深めるために、インターフェース、関数、クローラー、データベース管理など、多くの関数やモジュールをゼロから作成しています。


### 1.2 コードについて

私はC＃を体系的に勉強していませんが、C＃の名前を聞いたことがあります。それは何でもでき、非常に便利でシンプルなので、私が書いたコードはくだらないように見えるかもしれませんが、問題ではありません。続行することはわかっています。成長するために、Jvedioはまた、より多くの人々を引き付け、最適化と改善を続け、明るい未来を見ることができます。

Jvedioのコードには、あいまいなものは多くありません。私は、最も簡単な方法で関数を実装しようとしています。

## 2 インストール

Jvedioには現在インストールパッケージがありません。ソフトウェアはリリースされ、圧縮ファイルに保存されます。更新されたバージョンの後、特別な指示がない場合は、解凍後にソースファイルを上書きできます。



## 3 使い方？

### 3.1 開発者
Visual Studioをインストールして開き(これは2019年です）、**Clone Repository** を選択して、リポジトリの場所を入力してください

`https://github.com/hitchao/Jvedio.git`

または使用する git

`git clone https://github.com/hitchao/Jvedio.git`


### 3.2 Window ユーザー

チュートリアルを見る：[Jvedioの説明](https://github.com/hitchao/Jvedio/wiki)


## 4関連項目


|||
|-|-|
| Jvedio公式ウェブページ| [JvedioWebPage](https://github.com/hitchao/JvedioWebPage)|
| Chrome(360スピードブラウザ)プラグイン| [Jvedio-Chrome-Extensions](https://github.com/hitchao/Jvedio-Chrome-Extensions)|
| Jvedioアップグレードサーバーソース| [jvedioupdate](https://github.com/hitchao/jvedioupdate)|
| Gifコン​​トロールが変更されました| [WpfAnimatedGif](https://github.com/hitchao/WpfAnimatedGif)|


## 5 貢献に参加するつの方法

- スポンサーシップは必要ありません。[Pull Request](https://github.com/hitchao/Jvedio/pulls)を送信してください。
- Jvedioを宣伝し、より多くの人に使用してもらうことを歓迎します



## 6 機能リスト

### 6.1 サポートされているサーバーソース
- 具体的なurlは自分で検索する

|サーバー名| |
|--|--|
|豆瓣|√|
|Emby|√|




### 6.2 インターフェース：3つのスキン（黒、白、青）をサポート

[![クリックして表示](https://s3.ax1x.com/2021/03/06/6uGSeO.png)](https://s3.ax1x.com/2021/03/06/6uGSeO.png)

[![クリックして表示](https://s3.ax1x.com/2021/03/06/6uGPFH.png)](https://s3.ax1x.com/2021/03/06/6uGPFH.png)


### 6.3 言語：中国語、英語、日本語をサポート

[![クリックして表示](https://s3.ax1x.com/2021/03/06/6uJaKP.png)](https://s3.ax1x.com/2021/03/06/6uJaKP.png)

[![クリックして表示](https://s3.ax1x.com/2021/03/06/6uJfbT.png)](https://s3.ax1x.com/2021/03/06/6uJfbT.png)


### 6.4 マルチビデオライブラリ管理


[![クリックして表示](https://s3.ax1x.com/2021/03/06/6uJLKx.png)](https://s3.ax1x.com/2021/03/06/6uJLKx.png)


### 6.5 その他の機能

- 画像表示モード：サムネイル、ポスター画像、プレビュー画像、GIF(GIFをサポートする唯一のビデオ管理ソフトウェアである可能性があります)

[![クリックして表示](https://s3.ax1x.com/2021/03/06/6uYFMt.png)](https://s3.ax1x.com/2021/03/06/6uYFMt.png)

-豊富なフィルタリング機能：画像付き/画像なし、HD /中国語/ストリーミング、表示と再生のみ、セグメント化されたビデオのみ、ビデオタイプの選択

[![クリックして表示](https://s3.ax1x.com/2021/03/06/6uYlMq.png)](https://s3.ax1x.com/2021/03/06/6uYlMq.png)


- 豊富な右クリック機能

[![クリックして表示](https://s3.ax1x.com/2021/03/06/6uY3LV.png)](https://s3.ax1x.com/2021/03/06/6uY3LV.png)


- クイックナビゲーション機能

[![クリックして表示](https://s3.ax1x.com/2021/03/06/6uYJdU.png)](https://s3.ax1x.com/2021/03/06/6uYJdU.png)

- インテリジェントな分類

[![クリックして表示](https://s3.ax1x.com/2021/03/06/6uYLWj.png)](https://s3.ax1x.com/2021/03/06/6uYLWj.png)


- 高度にカスタマイズ可能な設定



[![クリックして表示](https://s3.ax1x.com/2021/03/06/6utx9H.png)](https://s3.ax1x.com/2021/03/06/6utx9H.png)


- 豊富なツールライブラリ


[![クリックして表示](https://s3.ax1x.com/2021/03/06/6ut3pd.png)](https://s3.ax1x.com/2021/03/06/6ut3pd.png)


- バッチ処理機能



[![クリックして表示](https://s3.ax1x.com/2021/03/06/6utJXt.png)](https://s3.ax1x.com/2021/03/06/6utJXt.png)


- ムービーライブラリ管理機能



[![クリックして表示](https://s3.ax1x.com/2021/03/06/6utscn.png)](https://s3.ax1x.com/2021/03/06/6utscn.png)

- アップグレード


[![クリックして表示](https://s3.ax1x.com/2021/03/06/6ut0hQ.png)](https://s3.ax1x.com/2021/03/06/6ut0hQ.png)













## 7 ありがとう


**Jvedio 4.0の開発に貢献してくれた以下のネチズンに感謝します**。皆様のご支援により、 `Jvedio`がどんどん良くなっていくことを願っています！

板块|网友
:--:|:--:
UI|青萍之末, Engine, Erdon, Erik
技术支持|[JavGO](https://github.com/javgo-2020/JavGo), [JavSDT](https://github.com/junerain123/javsdt)
调试|Sheldon, SHAWN, dddsG, EEE, Jion 等人
赞助支持|小猪培根 等众多网友
