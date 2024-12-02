namespace UserProfileService.Application.DTOs
{
    public class UpdateMealDTO
    {
        public int Id { get; set; }
        public int DayResultId { get; set; }
        public string Name { get; set; }
        public List<CreateOrUpdateEatenFoodDTO> Foods { get; set; } = new List<CreateOrUpdateEatenFoodDTO>();
    }
}
