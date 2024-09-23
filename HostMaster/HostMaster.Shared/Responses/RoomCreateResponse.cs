using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMaster.Shared.Responses;

public class RoomCreateResponse
{
    public int id { get; set; }
    public string? roomNumber { get; set; }
    public decimal price { get; set; }
    public bool isAvailable { get; set; }
    public int accommodationId { get; set; }
    public object? accommodation { get; set; }
    public int roomTypeId { get; set; }
    public object? roomType { get; set; }
    public object? reservations { get; set; }
    public object? roomInventoryItems { get; set; }
    public object? photos { get; set; }
}