// ReSharper disable All
namespace Glitch9
{
    /// <summary>
    /// Enum for file extensions
    /// </summary>
    public enum FileExt
    {
        Unset,

        // Audio
        mp3, // audio default
        wav,
        ogg,
        flac,

        // Image
        png, // image default
        jpg,
        jpeg,
        //gif, // not supported
        bmp,

        // Video
        mp4, // video default
        avi,
        mov,
    }
}