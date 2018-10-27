# YoloSharp
A .NET wrapper for OpenCV Yolo (darknet)

## 動作環境
- Windows 10 (64bit)
- .NET Framework 4.5.2 以降
- [Microsoft Visual C++ 2017 再頒布可能パッケージ](https://visualstudio.microsoft.com/ja/downloads/?q=#other-ja)
  - ダウンロードボタンを押した後、vc_redist.x64.exe にチェックを入れて 次へ をクリック

## ビルド環境
- Visual Studio 2017

## サンプル
- [YoloSharp Sample Application](https://1drv.ms/f/s!AtVeMj_gKPtbpoUW41zX4dyXA32q2g) (2018/1/28更新)

画像ファイルを Drag & Drop すると物体を判別し、結果を result フォルダに保存します。

![YoloSharp Detection](https://user-images.githubusercontent.com/179872/34451961-7eae720c-ed78-11e7-96bf-baa5d0a3f835.png)

なお、OpenCV 版の Yolo は、現在 CPU版のみ対応しています。

## ビルド方法
1．opencv-3.4.1-vc14_vc15.exe を[ダウンロード](https://opencv.org/opencv-3-4-1.html)して、c:\opencv341 フォルダに展開します
![Download Path](https://user-images.githubusercontent.com/179872/47597072-640b1500-d9c6-11e8-96b5-003fe12cdb24.png)

２．Visual Studio 2017 で src/YoloSharp.sln を開きビルドします。プラットフォームは x64 を設定してください。1. で c:\opencv341 以外のフォルダに展開した場合は、YoloSharp のプロパティを開き、構成プロパティ > VC++ディレクトリ 以下の インクルードディレクトリ、参照ディレクトリ、ライブラリディレクトリ にそれぞれのパスを追加してください。

![VC++ directory](https://user-images.githubusercontent.com/179872/47597201-80f41800-d9c7-11e8-91ea-a4bf869496b4.png)

また、リンカー > 入力 > 追加の依存ファイルに opencv_world341.lib を追加 (Releaseビルドの場合。Debugビルドの場合は、opencv_world341d.lib) してください。

![Linker directory](https://user-images.githubusercontent.com/179872/47597536-7edf8880-d9ca-11e8-976c-73535cd941c4.png)

.NET Framework で新たにビルドする場合は、プロパティ > ビルド > プラットフォームターゲット を、Any CPU ではなく x64 にしてください。

また、
```C:\opencv341\build\x64\vc15\bin``` から、opencv_world341.dll (または opencv_world341d.dll) をコピーし、model フォルダに、*.cfg, *.weight, *.names ファイルを置いてください。なお、上記のサンプルにもこれらのファイルが含まれています。

実行には [Microsoft Visual C++ 2017 再頒布可能パッケージ](https://visualstudio.microsoft.com/ja/downloads/?q=#other-ja) の x64版 (vc_redist.x64.exe) が必要です。Visual Studio 2017 が入っている環境ではなくても動作します。

