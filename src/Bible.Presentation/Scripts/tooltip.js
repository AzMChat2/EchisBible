
var balloon = new Balloon;

function popupStrongs(mouseEvent, strongsId)
{
	if (strongsId != 0)
	{
		var strongsText = window.external.GetStrongsPopupText(strongsId);
		balloon.showTooltip(mouseEvent, strongsText, 0, 250);
	}
}


