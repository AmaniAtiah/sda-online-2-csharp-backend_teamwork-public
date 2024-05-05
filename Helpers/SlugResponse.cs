namespace Backend.Helpers
{
  public class SlugResponse
  {
    public static string GenerateSlug(string name)
    {
      return name.ToLower().Replace(" ", "-");
    }
  }
}