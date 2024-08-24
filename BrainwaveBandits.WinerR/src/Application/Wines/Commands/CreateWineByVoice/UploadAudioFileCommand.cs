﻿using System.Net.Http.Headers;
using BrainwaveBandits.WinerR.Application.Common.Models;

public class UploadAudioFileCommand : IRequest<List<string>>
{
    public AudioFileDto File { get; }

    public UploadAudioFileCommand(AudioFileDto file)
    {
        File = file;
    }
}
