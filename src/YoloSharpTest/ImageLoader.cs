using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloSharpTest
{
    public class ImageLoader
    {
        enum ExifOrientation : ushort
        {
            TopLeft = 1,
            TopRight = 2,
            BottomRight = 3,
            BottomLeft = 4,
            LeftTop = 5,
            RightTop = 6,
            RightBottom = 7,
            LeftBottom = 8
        }
        /// <summary>
        /// 画像のExif情報に基づき回転を行って32bit ARGB 画像としてファイルを読み込む。
        /// アルゴリズムは <a href="http://blog.shibayan.jp/entry/20140428/1398688687">http://blog.shibayan.jp/entry/20140428/1398688687</a>参照 
        /// </summary>
        /// <param name="filename">読み込む対象のBitmap</param>
        /// <returns>回転情報を反映したBitmap</returns>
        public static Bitmap Load(string filename)
        {
            Bitmap result;
            using (Bitmap bitmap = new Bitmap(filename))
            {
                // 0x0112 = Orientation を保持するタグ ID
                var property = bitmap.PropertyItems.FirstOrDefault(p => p.Id == 0x0112);

                if (property != null)
                {
                    var rotation = RotateFlipType.RotateNoneFlipNone;

                    var orientation = (ExifOrientation)BitConverter.ToUInt16(property.Value, 0);

                    // Exif 情報に従って画像を回転させる
                    switch (orientation)
                    {
                        case ExifOrientation.TopLeft:
                            break;
                        case ExifOrientation.TopRight:
                            rotation = RotateFlipType.RotateNoneFlipX;
                            break;
                        case ExifOrientation.BottomRight:
                            rotation = RotateFlipType.Rotate180FlipNone;
                            break;
                        case ExifOrientation.BottomLeft:
                            rotation = RotateFlipType.RotateNoneFlipY;
                            break;
                        case ExifOrientation.LeftTop:
                            rotation = RotateFlipType.Rotate270FlipY;
                            break;
                        case ExifOrientation.RightTop:
                            rotation = RotateFlipType.Rotate90FlipNone;
                            break;
                        case ExifOrientation.RightBottom:
                            rotation = RotateFlipType.Rotate90FlipY;
                            break;
                        case ExifOrientation.LeftBottom:
                            rotation = RotateFlipType.Rotate270FlipNone;
                            break;
                    }

                    bitmap.RotateFlip(rotation);

                    property.Value = BitConverter.GetBytes((ushort)ExifOrientation.TopLeft);
                    bitmap.SetPropertyItem(property);
                }
                // Covert to 32bit ARGB 
                result = new Bitmap(bitmap.Width, bitmap.Height);
                using(Graphics g = Graphics.FromImage(result))
                {
                    g.DrawImage(bitmap,
                        new Rectangle(0, 0, result.Width, result.Height),
                        new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                        GraphicsUnit.Pixel);
                }
            }
            return result;
        }

        /// <summary>
        /// 画像が特定のアスペクト比となるように周囲に余白(黒背景)を追加します
        /// </summary>
        /// <param name="bmp">操作対象となるビットマップ</param>
        /// <param name="aspectRatio">アスペクト比(width/height)</param>
        /// <returns>余白を追加したビットマップ</returns>
        public static Bitmap AddBorder(Bitmap bmp, float aspectRatio)
        {
            if(aspectRatio <= 0f)
            {
                return new Bitmap(bmp);
            }
            float a = 1f * bmp.Width / bmp.Height;
            if(a < aspectRatio) // 元画像が指定したアスペクト比より縦長なので左右に余白を追加
            {
                Bitmap output = new Bitmap((int)(bmp.Height * aspectRatio), bmp.Height);
                using(Graphics g = Graphics.FromImage(output))
                {
                    g.Clear(Color.Black);

                    int s = (output.Width - bmp.Width) / 2;
                    Rectangle dest = new Rectangle(s, 0, bmp.Width, bmp.Height);
                    Rectangle src = new Rectangle(0, 0, bmp.Width, bmp.Height);
                    g.DrawImage(bmp, dest, src, GraphicsUnit.Pixel);
                }
                return output;
            }
            else // 元画像が指定したアスペクト比より横長なので上下に余白を追加
            {
                Bitmap output = new Bitmap(bmp.Width, (int)(bmp.Width / aspectRatio));
                using (Graphics g = Graphics.FromImage(output))
                {
                    g.Clear(Color.Black);

                    int s = (output.Height - bmp.Height) / 2;
                    Rectangle dest = new Rectangle(0, s, bmp.Width, bmp.Height);
                    Rectangle src = new Rectangle(0, 0, bmp.Width, bmp.Height);
                    g.DrawImage(bmp, dest, src, GraphicsUnit.Pixel);
                }
                return output;
            }
        }
    }
}
