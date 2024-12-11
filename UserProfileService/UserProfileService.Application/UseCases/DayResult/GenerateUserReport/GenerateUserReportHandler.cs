using MediatR;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.DayResult
{
    public class GenerateUserReportHandler : IRequestHandler<GenerateUserReportQuery, MemoryStream>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateUserReportHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MemoryStream> Handle(GenerateUserReportQuery request, CancellationToken cancellationToken)
        {
            var dayResults = await _unitOfWork.DayResultRepository.GetAllByPeriodAsync(request.profileId, request.startDate, request.endDate);

            var caloriesData = new List<(DateOnly Date, double Calories)>();
            var macrosData = new List<(DateOnly Date, double Proteins, double Fats, double Carbs)>();
            var foodMap = new Dictionary<string, (double Weight, double Calories, double Proteins, double Fats, double Carbs)>();

            QuestPDF.Settings.License = LicenseType.Community;

            foreach (var dayResult in dayResults)
            {
                double totalCalories = 0, totalProteins = 0, totalFats = 0, totalCarbs = 0;

                foreach (var meal in dayResult.Meals)
                {
                    foreach (var food in meal.Foods)
                    {
                        var foodDetails = food.Food;
                        double foodCalories = (foodDetails.Calories * food.Weight) / 100;
                        double foodProteins = (foodDetails.Proteins * food.Weight) / 100;
                        double foodFats = (foodDetails.Fats * food.Weight) / 100;
                        double foodCarbs = (foodDetails.Carbohydrates * food.Weight) / 100;

                        totalCalories += foodCalories;
                        totalProteins += foodProteins;
                        totalFats += foodFats;
                        totalCarbs += foodCarbs;

                        if (foodMap.ContainsKey(foodDetails.Name))
                        {
                            var existing = foodMap[foodDetails.Name];
                            foodMap[foodDetails.Name] = (
                                existing.Weight + food.Weight,
                                existing.Calories + foodCalories,
                                existing.Proteins + foodProteins,
                                existing.Fats + foodFats,
                                existing.Carbs + foodCarbs
                            );
                        }
                        else
                        {
                            foodMap[foodDetails.Name] = (food.Weight, foodCalories, foodProteins, foodFats, foodCarbs);
                        }
                    }
                }

                caloriesData.Add(( dayResult.Date, totalCalories ));
                macrosData.Add((dayResult.Date, totalProteins, totalFats, totalCarbs));
            }

            using var pdfStream = new MemoryStream();
            GeneratePdf(pdfStream, dayResults, caloriesData, macrosData, foodMap);
            pdfStream.Position = 0;
            return pdfStream;
        }

        private void GeneratePdf(MemoryStream stream, IEnumerable<Domain.Entities.DayResult> dayResults,
                                 List<(DateOnly Date, double Calories)> caloriesData,
                                 List<(DateOnly Date, double Proteins, double Fats, double Carbs)> macrosData,
                                 Dictionary<string, (double Weight, double Calories, double Proteins, double Fats, double Carbs)> foodMap)
        {
            var topFoods = foodMap.OrderByDescending(f => f.Value.Weight).Take(15).ToList();

            QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);
                    page.DefaultTextStyle(TextStyle.Default.FontSize(12));

                    page.Header()
                        .Text("User Nutrition Report")
                        .FontSize(20).SemiBold().AlignCenter();

                    page.Content().Column(col =>
                    {
                        col.Item().Text("Calories and Macros Overview").FontSize(16).SemiBold();

                        // Добавление текстовой информации о калориях и макросах по дням
                        foreach (var dayResult in dayResults)
                        {
                            var date = dayResult.Date.ToDateTime(new TimeOnly()).ToString("yyyy-MM-dd");
                            var calories = caloriesData.FirstOrDefault(c => c.Date == dayResult.Date).Calories;
                            var macros = macrosData.FirstOrDefault(m => m.Date == dayResult.Date);

                            col.Item().Text($"Date: {date}");
                            col.Item().Text($"Total Calories: {calories:F2}");
                            col.Item().Text($"Proteins: {macros.Proteins:F2} g");
                            col.Item().Text($"Fats: {macros.Fats:F2} g");
                            col.Item().Text($"Carbohydrates: {macros.Carbs:F2} g");
                            col.Item().Text(""); // Пустая строка для разделения
                        }

                        col.Item().Text("Top 15 Foods").FontSize(16).SemiBold();

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Cell().Text("Food Name");
                            table.Cell().Text("Weight (g)");
                            table.Cell().Text("Calories");
                            table.Cell().Text("Proteins");
                            table.Cell().Text("Fats");
                            table.Cell().Text("Carbs");

                            foreach (var food in topFoods)
                            {
                                table.Cell().Text(food.Key);
                                table.Cell().Text(food.Value.Weight.ToString());
                                table.Cell().Text(food.Value.Calories.ToString("F2"));
                                table.Cell().Text(food.Value.Proteins.ToString("F2"));
                                table.Cell().Text(food.Value.Fats.ToString("F2"));
                                table.Cell().Text(food.Value.Carbs.ToString("F2"));
                            }
                        });
                    });
                });
            }).GeneratePdf(stream);
        }
    }
}