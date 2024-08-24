namespace BrainwaveBandits.WinerR.Application.Common.Models;
public class AudioFileDto
{
    public string FileName { get; set; } = null!;
    public byte[] FileContent { get; set; } = null!;
    public string ContentType { get; set; } = null!;
}

