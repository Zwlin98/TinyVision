using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralImageProcess
{
    interface IMood
    {
        IImageMatrix ChangeBrightAndContrast(IImageMatrix src, int alpha, int beta);
        IImageMatrix CutImage(IImageMatrix src, int x, int y,int width,int height);
        IImageMatrix FilterColor(IImageMatrix src, int alpha, int beta);
        IImageMatrix Hist(IImageMatrix src, int alpha, int beta);
        IImageMatrix HlsColorAdjust(IImageMatrix src, int alpha, int beta);
        IImageMatrix ImageSize(IImageMatrix src, int alpha, int beta);
        IImageMatrix ReverseColor(IImageMatrix src, int alpha, int beta);
        IImageMatrix RgbColorAdjust(IImageMatrix src, int alpha, int beta);
        IImageMatrix TakeColor(IImageMatrix src, int alpha, int beta);
        IImageMatrix ToGrey(IImageMatrix src, int alpha, int beta);
    }
}
