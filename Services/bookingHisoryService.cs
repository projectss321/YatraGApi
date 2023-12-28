using Microsoft.AspNetCore.Mvc;
using yatracub.Models;
using yatracub.Repository.Interface;
using yatracub.Services.Interface;

namespace yatracub.Services
{
    public class bookingHisoryService : IbookingHistoryservice
    {
        private readonly IbookingHistoryrepo _bookinghistoryrepo;
        public bookingHisoryService(IbookingHistoryrepo bookinghistoryrepo)
        {
            _bookinghistoryrepo = bookinghistoryrepo;
        }
        public string saveUpdateBookings(bookinghistory booking)
        {
            return _bookinghistoryrepo.saveUpdateBookings(booking);
        }
    }
}
