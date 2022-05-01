using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PUSL2020.Application.Services;
using PUSL2020.Domain.Enums;

namespace PUSL2020.Web.Areas.BackOffice.Pages
{
    public class SummaryModel : PageModel
    {
        private readonly IAccidentService _accidentService;

        public SummaryModel(IAccidentService accidentService)
        {
            _accidentService = accidentService;
        }

        public dynamic[] CarAccidents { get; set; }
        
        public string[] Years { get; set; }
        public IEnumerable<SeriesModel> Series { get; set; }

        public record SeriesModel(string Name, double[] Values);
        
        public async Task<IActionResult> OnGetAsync()
        {
            await LoadCarAccidentsAsync();

            var accidents = (await _accidentService.GetAccidentsPercentageGroupByVehicleClassAndYearAsync())
                .ToList();

            
            Years = accidents.GroupBy(test => test.Year).Select(grp => grp.Key.ToString()).ToArray();

            Series = accidents.GroupBy(x => x.VehicleClass).Select(a =>
            {
                return new SeriesModel(Enum.GetName(a.Key)!, a.Select(x => Math.Round(x.Percentage, 2)).ToArray());
            });

            return Page();
        }

        private async Task LoadCarAccidentsAsync()
        {
            var accidents =
                await _accidentService.GetAccidentsPercentageByVehicleClassGroupByCauseAsync(VehicleClass.B);
            CarAccidents = accidents
                .Select(kv => new { category = GetAccidentCauseName(kv.Key), value = Math.Round(kv.Value, 2) })
                .ToArray();
        }

        private string GetAccidentCauseName(AccidentCause cause) => Enum.GetName(cause)!;
    }
}