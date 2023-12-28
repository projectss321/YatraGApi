using Microsoft.AspNetCore.Mvc;
using yatracub.Models;

namespace yatracub.Services.Interface
{
    public interface IbookingHistoryservice
    {
        public string saveUpdateBookings(bookinghistory booking);
    }
}
