using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesNet6Framework.dwgimageexport;

public class DwgSnapshotsHelper
{
    public static string Snapshot(string path)
    {
        var ext = Path.GetExtension(path);
        var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        var r = new BinaryReader(fs);
        r.BaseStream.Seek(0xD, SeekOrigin.Begin);
        var imgPos = r.ReadInt32();
        r.BaseStream.Seek(imgPos, SeekOrigin.Begin);
        var imgBSentinel = new byte[]
            {
                    0x1F, 0x25, 0x6D, 0x7, 0xD4, 0x36, 0x28, 0x28, 0x9D, 0x57, 0xCA, 0x3F, 0x9D, 0x44, 0x10, 0x2B
            };
        var imgCSentinel = r.ReadBytes(16);

        if (imgBSentinel.ToString() == imgCSentinel.ToString())
        {
            #region Resim okuma işlemleri

            var imgSize = r.ReadUInt32();
            var imgPresent = r.ReadByte();
            var imgHeaderStart = 0;
            var imgHeaderSize = 0;
            var imgBmpStart = 0;
            var imgBmpSize = 0;
            var bmpDataPresent = false;

            var imgWmfStart = 0;
            var imgWmfSize = 0;
            var wmfDataPresent = false;

            for (var i = 0; i < imgPresent; i++)
            {
                var imgcode = r.ReadByte();
                switch (imgcode)
                {
                    case 1:
                        imgHeaderStart = r.ReadInt32();
                        imgHeaderSize = r.ReadInt32();
                        break;
                    case 2:
                        imgBmpStart = r.ReadInt32();
                        imgBmpSize = r.ReadInt32();
                        bmpDataPresent = true;
                        break;
                    case 3:
                        imgWmfStart = r.ReadInt32();
                        imgWmfSize = r.ReadInt32();
                        wmfDataPresent = true;
                        break;
                }
            }

            #endregion

            if (bmpDataPresent)
            {
                #region Okunan resim içeriğini alma

                r.BaseStream.Seek(imgBmpStart, SeekOrigin.Begin);
                var tempPixelData = new byte[imgBmpSize + 14];

                tempPixelData[0] = 0x42;
                tempPixelData[1] = 0x4D;

                tempPixelData[10] = 0x36;
                tempPixelData[11] = 0x4;

                var tempBuffData = new byte[imgBmpSize];
                tempBuffData = r.ReadBytes(imgBmpSize);
                tempBuffData.CopyTo(tempPixelData, 14);

                #endregion

                var memStream = new MemoryStream(tempPixelData);
                var bmp = new Bitmap(memStream);

                //todo resmi bmp olarak aldın işlemleri devam ettir
                var toSave = GetFullPathWithoutExtension(path) + ".bmp";
                bmp.Save(toSave);
                return toSave;
            }
            if (wmfDataPresent)
            {
                //imgwmfsize oku. wmf resmi okumak için.
            }

            var imgESentinel = new byte[]
                {
                        0xE0, 0xDA, 0x92, 0xF8, 0x2B, 0xC9, 0xD7, 0xD7, 0x62, 0xA8, 0x35, 0xC0, 0x62, 0xBB, 0xEF, 0xD4
                };
            imgCSentinel = r.ReadBytes(16);
            if (imgESentinel.ToString() == imgCSentinel.ToString())
            {
                //todo bu kısım resmin bozulup bozulmadığını gösterir.
            }
        }
        return "";
    }

    public static string SnapshotAs(string dwgPath, string bmpPath)
    {
        var ext = Path.GetExtension(dwgPath);
        var fs = new FileStream(dwgPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        var r = new BinaryReader(fs);
        r.BaseStream.Seek(0xD, SeekOrigin.Begin);
        var imgPos = r.ReadInt32();
        r.BaseStream.Seek(imgPos, SeekOrigin.Begin);
        var imgBSentinel = new byte[]
            {
                    0x1F, 0x25, 0x6D, 0x7, 0xD4, 0x36, 0x28, 0x28, 0x9D, 0x57, 0xCA, 0x3F, 0x9D, 0x44, 0x10, 0x2B
            };
        var imgCSentinel = r.ReadBytes(16);

        if (imgBSentinel.ToString() == imgCSentinel.ToString())
        {
            #region Resim okuma işlemleri

            var imgSize = r.ReadUInt32();
            var imgPresent = r.ReadByte();
            var imgHeaderStart = 0;
            var imgHeaderSize = 0;
            var imgBmpStart = 0;
            var imgBmpSize = 0;
            var bmpDataPresent = false;

            var imgWmfStart = 0;
            var imgWmfSize = 0;
            var wmfDataPresent = false;

            for (var i = 0; i < imgPresent; i++)
            {
                var imgcode = r.ReadByte();
                switch (imgcode)
                {
                    case 1:
                        imgHeaderStart = r.ReadInt32();
                        imgHeaderSize = r.ReadInt32();
                        break;
                    case 2:
                        imgBmpStart = r.ReadInt32();
                        imgBmpSize = r.ReadInt32();
                        bmpDataPresent = true;
                        break;
                    case 3:
                        imgWmfStart = r.ReadInt32();
                        imgWmfSize = r.ReadInt32();
                        wmfDataPresent = true;
                        break;
                }
            }

            #endregion

            if (bmpDataPresent)
            {
                #region Okunan resim içeriğini alma

                r.BaseStream.Seek(imgBmpStart, SeekOrigin.Begin);
                var tempPixelData = new byte[imgBmpSize + 14];

                tempPixelData[0] = 0x42;
                tempPixelData[1] = 0x4D;

                tempPixelData[10] = 0x36;
                tempPixelData[11] = 0x4;

                var tempBuffData = new byte[imgBmpSize];
                tempBuffData = r.ReadBytes(imgBmpSize);
                tempBuffData.CopyTo(tempPixelData, 14);

                #endregion

                var memStream = new MemoryStream(tempPixelData);
                var bmp = new Bitmap(memStream);

                //todo resmi bmp olarak aldın işlemleri devam ettir
                var toSave = GetFullPathWithoutExtension(bmpPath) + ".bmp";
                bmp.Save(toSave);
                return toSave;
            }
            if (wmfDataPresent)
            {
                //imgwmfsize oku. wmf resmi okumak için.
            }

            var imgESentinel = new byte[]
                {
                        0xE0, 0xDA, 0x92, 0xF8, 0x2B, 0xC9, 0xD7, 0xD7, 0x62, 0xA8, 0x35, 0xC0, 0x62, 0xBB, 0xEF, 0xD4
                };
            imgCSentinel = r.ReadBytes(16);
            if (imgESentinel.ToString() == imgCSentinel.ToString())
            {
                //todo bu kısım resmin bozulup bozulmadığını gösterir.
            }
        }
        return "";
    }

    private static string GetFullPathWithoutExtension(string path)
    {
        if (string.IsNullOrEmpty(path))
            return "";
        var dirName = Path.GetDirectoryName(path) ?? "";
        var fileName = Path.GetFileNameWithoutExtension(path);
        return Path.Combine(dirName, fileName);
    }
}

