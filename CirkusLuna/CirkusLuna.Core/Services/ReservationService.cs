using CirkusLuna.Core.Exceptions;
using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Models;

namespace CirkusLuna.Core.Services
{
    /// <summary>
    /// Service for reservation-related business logic and validation
    /// Enforces business rules: future performances only, capacity limits, VIP limits
    /// </summary>
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepo;
        private readonly IForestillingRepository _forestillingRepo;
        private readonly IKundeRepository _kundeRepo;
        
        public ReservationService(
            IReservationRepository reservationRepo,
            IForestillingRepository forestillingRepo,
            IKundeRepository kundeRepo)
        {
            _reservationRepo = reservationRepo;
            _forestillingRepo = forestillingRepo;
            _kundeRepo = kundeRepo;
        }
        
        /// <summary>
        /// Creates a reservation with full business rule validation
        /// Throws exceptions if business rules are violated
        /// </summary>
        public Reservation CreateReservation(
            int kundeId, 
            int forestillingId, 
            int antalBilletter, 
            Billettype billettype)
        {
            // Get forestilling
            var forestilling = _forestillingRepo.GetById(forestillingId);
            if (forestilling == null)
                throw new ArgumentException("Forestilling ikke fundet");
            
            // Business rule: Only future performances
            if (forestilling.Dato < DateTime.Now)
                throw new PastPerformanceException(
                    "Kan ikke reservere billetter til tidligere forestillinger");
            
            // Business rule: Check capacity based on ticket type
            if (billettype == Billettype.VIP)
            {
                // Check VIP capacity (max 10 per performance)
                if (forestilling.LedigeVIPPladser < antalBilletter)
                    throw new VIPCapacityExceededException(
                        $"Kun {forestilling.LedigeVIPPladser} VIP-pladser tilbage");
            }
            else
            {
                // Check total capacity (max 150 per performance)
                if (forestilling.LedigePladser < antalBilletter)
                    throw new ReservationFullException(
                        $"Kun {forestilling.LedigePladser} pladser tilbage");
            }
            
            // Get kunde
            var kunde = _kundeRepo.GetById(kundeId);
            if (kunde == null)
                throw new ArgumentException("Kunde ikke fundet");
            
            // Create reservation
            var reservation = new Reservation
            {
                KundeId = kundeId,
                Kunde = kunde,
                ForestillingId = forestillingId,
                Forestilling = forestilling,
                AntalBilletter = antalBilletter,
                Billettype = billettype,
                ReservationsDato = DateTime.Now
            };
            
            _reservationRepo.Add(reservation);
            
            // Add reservation to forestilling's list for capacity calculations
            forestilling.Reservationer.Add(reservation);
            
            return reservation;
        }
        
        /// <summary>
        /// Checks if a reservation can be made without throwing exceptions
        /// </summary>
        public bool CanReserve(int forestillingId, int antalBilletter, Billettype billettype)
        {
            var forestilling = _forestillingRepo.GetById(forestillingId);
            if (forestilling == null) return false;
            if (forestilling.Dato < DateTime.Now) return false;
            
            if (billettype == Billettype.VIP)
                return forestilling.LedigeVIPPladser >= antalBilletter;
            else
                return forestilling.LedigePladser >= antalBilletter;
        }
        
        public List<Reservation> GetReservationsByKunde(int kundeId)
        {
            return _reservationRepo.GetByKundeId(kundeId);
        }
        
        /// <summary>
        /// Calculate total price for a reservation
        /// Uses BillettypePris helper
        /// </summary>
        public decimal CalculateTotalPrice(int antalBilletter, Billettype billettype)
        {
            return BillettypePris.GetPris(billettype) * antalBilletter;
        }
    }
}
