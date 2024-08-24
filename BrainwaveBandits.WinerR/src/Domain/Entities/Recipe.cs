namespace BrainwaveBandits.WinerR.Domain.Entities;
public class Recipe
{
    public string Name { get; set; } = null!;
    public List<Ingredient> Ingredients { get; set; } = null!;
    public Ingredient MainIngredient { get; set; } = null!;
}
