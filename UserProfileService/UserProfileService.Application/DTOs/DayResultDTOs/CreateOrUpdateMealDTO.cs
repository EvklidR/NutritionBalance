namespace UserProfileService.Application.DTOs
{
    public class CreateOrUpdateMealDTO
    {
        public string Name { get; set; }
        public List<CreateOrUpdateEatenFoodDTO> Foods { get; set; } = new List<CreateOrUpdateEatenFoodDTO>();
    }
}
