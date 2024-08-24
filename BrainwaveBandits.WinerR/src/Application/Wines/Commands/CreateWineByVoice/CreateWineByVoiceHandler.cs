using BrainwaveBandits.WinerR.Application.Wines.Commands.CreateWine;
using BrainwaveBandits.WinerR.Domain.Entities;
using BrainwaveBandits.WinerR.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace BrainwaveBandits.WinerR.Application.Wines.Commands.CreateWineByVoice;
public class CreateWineByVoiceHandler : IRequestHandler<CreateWineByVoiceQuery, int>
{
    public async Task<int> Handle(CreateWineByVoiceQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // call whisper returns ID


        // call Create wine command with ID gets wine from imported DB and copies it to wine DB (Inventory)

        // Check if already inside then amount ++ if not add it

        return 1;
    }
}
