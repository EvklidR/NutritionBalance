namespace UserProfileService.Application.DTOs
{
    public class CreateMealDTO
    {
        public string Name { get; set; }
        public List<CreateEatenFoodDTO> Foods { get; set; } = new List<CreateEatenFoodDTO>();
    }
}
