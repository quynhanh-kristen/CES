using Microsoft.AspNetCore.Components;
using RoutePlanning.Application.Locations.Queries.SelectableLocationList;

namespace RoutePlanning.Client.Web.Shared;

public sealed partial class LocationSelector
{
    [Parameter]
    public string? Label { get; set; }

    [Parameter]
    public IEnumerable<SelectableLocation>? Locations { get; set; }

    [Parameter]
    public SelectableLocation? Selected { get; set; }

    [Parameter]
    public EventCallback<SelectableLocation?> SelectedChanged { get; set; }

    private async Task OnSelectedChanged(ChangeEventArgs e)
    {
        var selectedId = Guid.Parse((string?)e.Value ?? "");
        Selected = Locations?.Single(l => l.LocationId == selectedId);
        await SelectedChanged.InvokeAsync(Selected);
    }
}
