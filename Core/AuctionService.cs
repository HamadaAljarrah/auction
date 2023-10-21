﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using DistLab2.Core.Interfaces;
using DistLab2.Persistence;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NuGet.Packaging.Signing;
using AutoMapper;
using DistLab2.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DistLab2.Core
{
    public class AuctionService : IAuctionService
    {
        private readonly IReposetory<AuctionDb> _auctionReposetory;
        private readonly IReposetory<BidDb> _bidReposetory;

        private readonly IMapper  _mapper;
        public AuctionService(IReposetory<AuctionDb> auctinReposetory, IReposetory<BidDb> bidReposetory ,IMapper mapper) {
            _auctionReposetory = auctinReposetory;
            _bidReposetory = bidReposetory;
            _mapper = mapper;
        }

        public void CreateAuction(Auction auction)
        {        
            AuctionDb auctionDb = _mapper.Map<AuctionDb>(auction);
            _auctionReposetory.Add(auctionDb);

        }

        public IEnumerable<Auction> GetAll()
        {
            IEnumerable<AuctionDb> auctionDBs = _auctionReposetory.GetAll();

            IEnumerable<Auction> auctions = _mapper.Map<IEnumerable<Auction>>(auctionDBs);
            Console.WriteLine("Printing the list of auctions:");

            foreach (var auction in auctions)
            {
                Console.WriteLine($"Auction Id: {auction.Id}, Name: {auction.Name}, Description: {auction.Description}, " +
                                   $"Starting Price: {auction.StartingPrice}, Created Date: {auction.CreatedDate}, " +
                                   $"End Date: {auction.EndDate}, User Id: {auction.UserId},");
                if (auction.Bids != null)
                {
                    Console.WriteLine("Bids found in service:");
                    foreach (var bid in auction.Bids)
                    {
                        Console.WriteLine("Bid from Service: " + bid.Id);
                    }
                }
                else
                {
                    Console.WriteLine("No bids found for this auction.");
                }
            }

            return auctions;
        }

        public void Remove(Auction auction)
        {
            throw new NotImplementedException();
        }
        //public Auction GetById(int id)
        //{
        //    AuctionDb auctionDb = _auctionReposetory.GetById(id).Include(a => a.Bids)
        //        .FirstOrDefault(a => a.Id == id);

        //    if (auctionDb != null)
        //    {
        //        return _mapper.Map<Auction>(auctionDb);
        //    }

        //    return null; // Return null or handle the case when the auction is not found.
        //}


        public Auction GetById(int id)
        {
            AuctionDb auctionDb = _auctionReposetory.GetById(id);
            Auction auction = _mapper.Map<Auction>(auctionDb);
            return auction;
        }

        //public Auction GetById(int id)
        //{
        //    AuctionDb auctionDb = _auctionReposetory.GetAll()
        //        .Include(a => a.Bids) // Include bids in the query
        //        .SingleOrDefault(a => a.Id == id);

        //    if (auctionDb == null)
        //    {
        //        return null;
        //    }

        //    Auction auction = _mapper.Map<Auction>(auctionDb);
        //    Console.WriteLine("in service 2: name " + auction.Name);
        //    return auction;
        //}
        public void PlaceBid(int auctionId, int bidAmount)
        {
            AuctionDb auction = _auctionReposetory.GetById(auctionId);

            if (auction == null)
            {
       
                return;
            }
            decimal highestBidAmount = auction.Bids.Max(b => (int?)b.Amount) ?? 0;
            if (bidAmount <= highestBidAmount)
            {
                return;
            }

            BidDb newBid = new BidDb //todo läg dynamic id
            {
                Amount = bidAmount,
                AuctionId= auctionId,
                UserId = "Marcus.Okodugha247@gmail.com",
                CreatedTime = DateTime.Now
            };
            Console.WriteLine("Amount s " + newBid.Amount);
            Console.WriteLine("UserId s " + newBid.UserId);
            Console.WriteLine("a id  " + auctionId);

            Console.WriteLine("Amount s " + newBid.CreatedTime);


            _bidReposetory.Add(newBid);


            // Save the changes to the database
            _bidReposetory.Update(newBid);
        }

    }
}
