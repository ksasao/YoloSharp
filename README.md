# YoloSharp
A .NET wrapper for OpenCV Yolo (darknet)

## 動作環境
- Windows 10 (64bit)
- .NET Framework 4.5.2 以降
- [Microsoft Visual C++ 2015 再頒布可能パッケージ Update 3](https://www.microsoft.com/ja-jp/download/details.aspx?id=53840)
  - ダウンロードボタンを押した後、vc_redist.x64.exe にチェックを入れて 次へ をクリック

## サンプル
- [YoloSharp Sample Application](https://1drv.ms/f/s!AtVeMj_gKPtbpoUW41zX4dyXA32q2g) (2018/1/28更新)

画像ファイルを Drag & Drop すると物体を判別し、結果を result フォルダに保存します。

![YoloSharp Detection](https://user-images.githubusercontent.com/179872/34451961-7eae720c-ed78-11e7-96bf-baa5d0a3f835.png)

なお、OpenCV 版の Yolo は、現在 CPU版のみ対応しています。

## ビルド方法
1．opencv-3.3.1-vc14.exe を[ダウンロード](https://github.com/opencv/opencv/releases/tag/3.3.1)して、c:\opencv331 フォルダに展開します
![Download Path](https://user-images.githubusercontent.com/179872/34452126-a98376aa-ed7b-11e7-99ca-5a92502dff1b.png)

２．Visual Studio 2015 で src/YoloSharp.sln を開きビルドします。1. で c:\opencv331 以外のフォルダに展開した場合、または、OpenCV 3.4 を利用する場合には、YoloSharp のプロパティを開き、構成プロパティ > VC++ディレクトリ 以下の インクルードディレクトリ、参照ディレクトリ、ライブラリディレクトリ にそれぞれのパスを追加してください。

![VC++ directory](https://user-images.githubusercontent.com/179872/34452166-affe4734-ed7c-11e7-8b53-1d0bbf4f1fad.png)

また、リンカー > 入力 > 追加の依存ファイルに opencv_world331.lib を追加 (Releaseビルドの場合。Debugビルドの場合は、opencv_world331d.lib) してください。

![Linker directory](https://user-images.githubusercontent.com/179872/34452204-9ba7ab44-ed7d-11e7-9c7b-60ff8c5c889c.png)

.NET Framework で新たにビルドする場合は、プロパティ > ビルド > プラットフォームターゲット を、Any CPU ではなく x64 にしてください。

また、
```C:\opencv331\build\x64\vc14\bin``` から、opencv_world331.dll (または opencv_world331d.dll) をコピーし、model フォルダに、*.cfg, *.weight, *.names ファイルを置いてください。なお、上記のサンプルにもこれらのファイルが含まれています。

実行には [Microsoft Visual C++ 2015 再頒布可能パッケージ Update 3](https://www.microsoft.com/ja-jp/download/details.aspx?id=53840) の x64版 (vc_redist.x64.exe) が必要です。Visual Studio 2015 が入っている環境ではなくても動作します。
  - concrt140.dll, msvcp140.dll, vcruntime140.dll を利用しています
