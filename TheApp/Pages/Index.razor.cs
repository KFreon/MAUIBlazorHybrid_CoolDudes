using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Database;
using Microsoft.AspNetCore.Components;
using TheApp.Data;

namespace TheApp.Pages
{
    public partial class Index
    {
        [Inject] IDataService dataService { get; set; }

        public ObservableCollection<CoolDude> CoolDudes { get; set; } = new();
        public ObservableCollection<CoolDude> UncoolDudes { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await dataService.Initialise();
            await RefreshDudes();
        }

        private async Task RefreshDudes()
        {
            CoolDudes.Clear();
            UncoolDudes.Clear();
            foreach(var dude in await dataService.GetDudes())
            {
                if (dude.AreTheyACoolDude)
                    CoolDudes.Add(dude);
                else
                    UncoolDudes.Add(dude);
            }

        }

        public class NameDetails
        {
            [Required]
            public string Name { get; set; }
            
            [Required]
            public bool IsCool { get; set; }
        }

        public NameDetails Deets { get; set; } = new();

        public async Task AddACoolDude(NameDetails newDude)
        {
            await dataService.AddADude(newDude.Name, newDude.IsCool);
            await RefreshDudes();
        }

        public async Task DeleteADude(CoolDude dude)
        {
            await dataService.DeleteADude(dude);
            await RefreshDudes();
        }
    }
}