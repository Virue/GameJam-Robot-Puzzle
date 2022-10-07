using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

using System.Windows.Media.Imaging;

public class DirectBitmap
{
    public Bitmap Bitmap { get; set; }
    private Int32[] Bits;
    public bool Disposed { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    protected GCHandle BitsHandle { get; private set; }
    public DirectBitmap(int width, int height)
    {
        Width = width;
        Height = height;
        Bits = new Int32[Width * Height];
        BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
        Bitmap = new Bitmap(Width, Height, Width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject() );
    }
    public unsafe void SetPixel(int x, int y, int sizX , int sizY, Color colour)
    {
        int index = x + (y * Width);
        int col = colour.ToArgb();

        Bits[index] = col;
    }

    public unsafe void SetSqr(int x, int y, int sizX, int sizY, Color colour)
    {
        //Bits[x + (y * Width)] = colour.ToArgb();

        for (int i = 0; i < sizX; i++)
        {
            for (int j = 0; j < sizY; j++)
            {

                int indx = x + i + ((y + j) * Width);
                Bits[indx] = colour.ToArgb();
                //fixed (Int32* ptr = Bits)
                    //ptr[(int)(x * sizX + i + ((y + j) * sizY * Width ))] = colour.ToArgb();
            }
        }
    }


    public Color GetPixel(int x, int y)
    {
        int index = x + (y * Width);
        int col = Bits[index];
        Color result = Color.FromArgb(col);
        return result;
    }
    public void Dispose()
    {
        if (Disposed) return;
        Disposed = true;
        Bitmap.Dispose();
        BitsHandle.Free();
    }
}


//del all below when confident

public class Wbitmap
{
    public WriteableBitmap wb { get; set; }
    public System.Drawing.Bitmap Bitmap { get; set; }
    public BitmapImage BitmapImage { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    
    public Wbitmap(int w, int h)
    {
        Height = h;
        Width = w;

        wb = new WriteableBitmap(
                Width,
                Height,
                96,
                96,
                System.Windows.Media.PixelFormats.Bgr32,
                null);
    }
    public void SetPixel(int x, int y, Color colour)
    {

        byte[] ColorData = { colour.B, colour.G, colour.R, colour.A }; // B G R

        System.Windows.Int32Rect rect = new System.Windows.Int32Rect( x, y, 1, 1);

        wb.WritePixels(rect, ColorData, 4, 0);

    }

    public void DrawPixel(int x, int y, Color colour)
    {
        int column = x;
        int row = y;

        // Reserve the back buffer for updates.
        wb.Lock();

        unsafe
        {
            // Get a pointer to the back buffer.
            IntPtr pBackBuffer = wb.BackBuffer;

            // Find the address of the pixel to draw.
            pBackBuffer += row * wb.BackBufferStride;
            pBackBuffer += column * 4;

            // Compute the pixel's color.
            int color_data = 255 << 16; // R
            color_data |= 128 << 8;   // G
            color_data |= 255 << 0;   // B

            // Assign the color data to the pixel.
            *((int*)pBackBuffer) = color_data;
        }

        // Specify the area of the bitmap that changed.
        wb.AddDirtyRect(new System.Windows.Int32Rect(column, row, 1, 1));

        // Release the back buffer and make it available for display.
        wb.Unlock();
    }


}