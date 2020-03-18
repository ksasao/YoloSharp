# YoloSharp
A .NET wrapper for OpenCV Yolo v2/v3 (darknet)

## 動作環境
- Windows 10 (64bit)
- .NET Framework 4.5.2 以降
- [Microsoft Visual C++ 2017 再頒布可能パッケージ](https://visualstudio.microsoft.com/ja/downloads/?q=#other-ja)
  - ダウンロードボタンを押した後、vc_redist.x64.exe にチェックを入れて 次へ をクリック

## ビルド環境
- Visual Studio 2017

## サンプル
- [YoloSharp Sample Application](https://github.com/ksasao/YoloSharp/releases/download/v1.0/YoloSharpSample_vs2015_opencv343.zip) (2018/1/28更新)

画像ファイルを Drag & Drop すると物体を判別し、結果を result フォルダに保存します。

![YoloSharp Detection](https://user-images.githubusercontent.com/179872/34451961-7eae720c-ed78-11e7-96bf-baa5d0a3f835.png)

OpenCL (FP16を含む) / OpenVINO などをサポートしており、NVIDIA, AMD, Intel GPU などを利用することが可能です。CUDA はサポートしていません。

## ビルド方法
1．opencv-3.4.3-vc14_vc15.exe を[ダウンロード](https://github.com/opencv/opencv/releases/tag/3.4.3)して、c:\opencv343 フォルダに展開します
![Download Path](https://user-images.githubusercontent.com/179872/47597072-640b1500-d9c6-11e8-96b5-003fe12cdb24.png)

２．Visual Studio 2017 で src/YoloSharp.sln を開きビルドします。プラットフォームは x64 を設定してください。1. で c:\opencv343 以外のフォルダに展開した場合は、YoloSharp のプロパティを開き、構成プロパティ > VC++ディレクトリ 以下の インクルードディレクトリ、参照ディレクトリ、ライブラリディレクトリ にそれぞれのパスを追加してください。

![VC++ directory](https://user-images.githubusercontent.com/179872/47599334-e4da0900-d9e6-11e8-8523-05b1b5910ebc.png)

また、リンカー > 入力 > 追加の依存ファイルに opencv_world343.lib を追加 (Releaseビルドの場合。Debugビルドの場合は、opencv_world343d.lib) してください。

![Linker directory](https://user-images.githubusercontent.com/179872/47599328-b8be8800-d9e6-11e8-8da5-1b805265a3f7.png)

.NET Framework で新たにビルドする場合は、プロパティ > ビルド > プラットフォームターゲット を、Any CPU ではなく x64 にしてください。

また、
```C:\opencv343\build\x64\vc15\bin``` から、opencv_world343.dll (または opencv_world343d.dll) をコピーし、model フォルダに、*.cfg, *.weight, *.names ファイルを置いてください。なお、上記のサンプルにもこれらのファイルが含まれています。

実行には [Microsoft Visual C++ 2017 再頒布可能パッケージ](https://visualstudio.microsoft.com/ja/downloads/?q=#other-ja) の x64版 (vc_redist.x64.exe) が必要です。Visual Studio 2017 が入っている環境ではなくても動作します。

