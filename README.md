# Due to some personal reasons, Jvedio temporary maintenance, the latest version of the [4.6.0.0](https://github.com/hitchao/Jvedio/releases/tag/Jvedio4.6.0.0),Or download  [other version](https://github.com/hitchao/Jvedio/releases)





[中文](README_CHS.md) [English](README.md) [日本語](README_JP.md)



<h1 align="center">Jvedio</h1>





<h3 align="center">Local Video Management</h3>




---








&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Jvedio is a local video management software that supports scanning local videos and importing the software to establish a video library,
Extract the unique identification code of the video, automatically classify the video,
Add tags to manage videos, use artificial intelligence to identify actors, support translation information,
Capture video pictures based on FFmpeg, smooth and beautiful application software on Window desktop


WebSite：[Jvedio](https://hitchao.github.io/JvedioWebPage/) | Download：[Latest Version](https://github.com/hitchao/Jvedio/releases)





[![Click to view the software](https://s3.ax1x.com/2021/03/06/6u8UJA.png)](https://s3.ax1x.com/2021/03/06/6u8UJA.png)
[![Click to view the software](https://s3.ax1x.com/2021/03/06/6u8oeU.png)](https://s3.ax1x.com/2021/03/06/6u8oeU.png)

---

## 1 Background
Jvedio is a video management software that integrates local video recognition and scanning, picture display, screening, and scraping. At present, the similar management software on GitHub includes: [jellyfin](https://github.com/jellyfin/jellyfin), [Emby](https://github.com/MediaBrowser/Emby), and the existing software on the market includes : Extreme Shadow Pie, KODI, PLEX, etc.

|Software|Main Writing Language|Support Platform|
|--|--|--|
|Jvedio|C#|win7,win8,win10|
|jellyfin|C#|All platforms|
|Emby|C#|All platforms|
|Jiyingpai|?|Window|
|KODI|?|-|
|PLEX|?|-|


### 1.1 The original intention of the development

With more and more videos stored on the computer, the desire for a good video management software becomes stronger. After using emby and PLEX, I found some **problems**:

- Startup method: start a server first, and then use WEB to manage the software
- Information storage method: Put pictures and metadata information together in the same directory of the movie, which leads to very troublesome information transfer
- Interface: The WEB writing interface is efficient and simple, but I really hope that the margins, shadows, zoom behavior, maximize and minimize buttons of the form can be fully customized
- Function: Data filtering is not rich enough
- Feelings: lack of a video management software developed by the Chinese

Therefore, I developed Jvedio. In order to deepen my understanding of Jvedio and video management, many functions and modules are written from scratch, including interface, function, crawler, database management, etc.


### 1.2 About the code

I haven't studied C# systematically, but I have heard the name of C#, it can do anything, and it is very convenient and simple, so the code I wrote may look crappy, but it does not matter, I know I will continue to grow , Jvedio will also attract more people to come in and continue to optimize and improve, and I can see a bright future.

There are not many obscure things in Jvedio's code. I try to implement a function in the simplest way.


## 2 Installation

Jvedio currently does not have an installation package. The software is released and stored as compressed files. After the updated version, if there is no special instructions, you can overwrite the source files after decompression.



## 3 How To Use？

### 3.1 For Developer
Please install and open Visual Studio (this is 2019), select **Clone Repository**, and fill in the repository location

`https://github.com/hitchao/Jvedio.git`

or use git

`git clone https://github.com/hitchao/Jvedio.git`


### 3.2 Window User

Please See：[Jvedio Introduction](https://github.com/hitchao/Jvedio/wiki)


## 4 Related items


|||
|--|--|
|Jvedio official webpage|[JvedioWebPage](https://github.com/hitchao/JvedioWebPage)|
|Chrome (360 speed browser) plug-in|[Jvedio-Chrome-Extensions](https://github.com/hitchao/Jvedio-Chrome-Extensions)|
|Jvedio upgraded server source|[jvedioupdate](https://github.com/hitchao/jvedioupdate)|
|Gif control modified in|[WpfAnimatedGif](https://github.com/hitchao/WpfAnimatedGif)|

## 5 Ways to Participate in Contribution

- No sponsorship is required, you are welcome to submit [Pull Request](https://github.com/hitchao/Jvedio/pulls)
- Welcome to promote Jvedio and let more people use it


## 6 Function list




### 6.1 Support Service
- Specific website search by themselves

|Service name| |
|--|--|
|豆瓣|√|
|Emby|√|



### 6.2 Interface: supports three skins (black, white, blue)

[![Click to view](https://s3.ax1x.com/2021/03/06/6uGSeO.png)](https://s3.ax1x.com/2021/03/06/6uGSeO.png)

[![Click to view](https://s3.ax1x.com/2021/03/06/6uGPFH.png)](https://s3.ax1x.com/2021/03/06/6uGPFH.png)


### 6.3 Language: Support Chinese, English, Japanese

[![Click to view](https://s3.ax1x.com/2021/03/06/6uJaKP.png)](https://s3.ax1x.com/2021/03/06/6uJaKP.png)

[![Click to view](https://s3.ax1x.com/2021/03/06/6uJfbT.png)](https://s3.ax1x.com/2021/03/06/6uJfbT.png)


### 6.4 Multi-Video Library Management


[![Click to view](https://s3.ax1x.com/2021/03/06/6uJLKx.png)](https://s3.ax1x.com/2021/03/06/6uJLKx.png)


### 6.5 Other functions

- Picture display mode: thumbnail, poster image, preview image, GIF (may be the only video management software that supports GIF)

[![Click to view](https://s3.ax1x.com/2021/03/06/6uYFMt.png)](https://s3.ax1x.com/2021/03/06/6uYFMt.png)

- Abundant filtering functions: with picture/no picture, HD/Chinese/streaming, only display and playable, only segmented video, video type selection

[![Click to view](https://s3.ax1x.com/2021/03/06/6uYlMq.png)](https://s3.ax1x.com/2021/03/06/6uYlMq.png)


- Rich right-click function

[![Click to view](https://s3.ax1x.com/2021/03/06/6uY3LV.png)](https://s3.ax1x.com/2021/03/06/6uY3LV.png)


- Quick navigation function

[![Click to view](https://s3.ax1x.com/2021/03/06/6uYJdU.png)](https://s3.ax1x.com/2021/03/06/6uYJdU.png)

- Intelligent classification

[![Click to view](https://s3.ax1x.com/2021/03/06/6uYLWj.png)](https://s3.ax1x.com/2021/03/06/6uYLWj.png)


- Highly customizable settings



[![Click to view](https://s3.ax1x.com/2021/03/06/6utx9H.png)](https://s3.ax1x.com/2021/03/06/6utx9H.png)


- Rich tool library


[![Click to view](https://s3.ax1x.com/2021/03/06/6ut3pd.png)](https://s3.ax1x.com/2021/03/06/6ut3pd.png)


- Batch processing function



[![Click to view](https://s3.ax1x.com/2021/03/06/6utJXt.png)](https://s3.ax1x.com/2021/03/06/6utJXt.png)


- Movie library management function



[![Click to view](https://s3.ax1x.com/2021/03/06/6utscn.png)](https://s3.ax1x.com/2021/03/06/6utscn.png)

- Upgrade


[![Click to view](https://s3.ax1x.com/2021/03/06/6ut0hQ.png)](https://s3.ax1x.com/2021/03/06/6ut0hQ.png)

















## 7 Thanks

**Thanks to the following netizens for their contributions in the development of Jvedio 4.0**, I hope that with your support, `Jvedio` will develop better and better!


板块|网友
:--:|:--:
UI|青萍之末, Engine, Erdon, Erik
调试|Sheldon, SHAWN, dddsG, EEE, Jion 等人
赞助支持|小猪培根 等众多网友


