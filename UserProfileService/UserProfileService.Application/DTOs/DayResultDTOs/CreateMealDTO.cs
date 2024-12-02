namespace UserProfileService.Application.DTOs
{
    public class CreateMealDTO
    {
        public int DayResultId { get; set; }
        public string Name { get; set; }
        public List<CreateOrUpdateEatenFoodDTO> Foods { get; set; } = new List<CreateOrUpdateEatenFoodDTO>();
    }
}
