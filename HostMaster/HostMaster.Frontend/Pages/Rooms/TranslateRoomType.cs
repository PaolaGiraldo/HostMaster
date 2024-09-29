using HostMaster.Shared.Entities;
using HostMaster.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace HostMaster.Frontend.Pages.Rooms
{
    public class TranslateRoomType
    {
        public string Get(RoomType roomType, IStringLocalizer localizer)
        {
            if (roomType == null) return string.Empty;

            return roomType.TypeName switch
            {
                "SINGLE" => localizer["SingleRoom"],
                "DOUBLE" => localizer["DoubleRoom"],
                "DOUBLE 2 BEDS" => localizer["Double2Beds"],
                _ => roomType.TypeName
            };
        }
    }
}