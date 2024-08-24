using BrainwaveBandits.WinerR.Application.Common.Interfaces;
using BrainwaveBandits.WinerR.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace BrainwaveBandits.WinerR.Application.Recipes.Queries;

public class GetRecipeFromDishNameQueryHandler : IRequestHandler<GetRecipeFromDishNameQuery, Recipe>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IOpenAIService _openAiService;
    private readonly IMemoryCache _cache;

    public GetRecipeFromDishNameQueryHandler(IApplicationDbContext context, IMapper mapper,
        IOpenAIService openAiService, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _openAiService = openAiService;
        _cache = cache;
    }

    public async Task<Recipe> Handle(GetRecipeFromDishNameQuery request, CancellationToken cancellationToken)
    {
        string cacheKey = $"Recipe_{request.DishName}";
        
        if (_cache.TryGetValue(cacheKey, out Recipe? cachedRecipe))
        {
            return cachedRecipe!;
        }
        
        Recipe recipe = await _openAiService.GetRecipeFromDishNameAsync(request.DishName);


        _cache.Set(cacheKey, recipe);

        return recipe;
    }
}
