using Backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.UnitTests
{
    public static class SeedData
    {

        //ini ToDoListTask 1
        public static readonly ToDoTask ToDoListTask1 = new ToDoTask
        {
            Id = 1,
            Title = "hdURdnRKmNBkhtJCZqOMOgkczOfXSUrMCrSJxKVqrkoZolospkqaxLzAtrSPItsSVphqazEPVXUerLnNaniidROqrSQFsFhxrWDz",
            Description = "aEZqUIhEEpVLJoDaqyIjzXPqXmVHoIcVtCCMozXJiBnQMmihzYNyJGgDojThnaPvMzRGlrmqbzAxjzdkEnHPvdMnWzXEgpWIlrDN",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            Completed = 50,
            IsDeleted = false,
        };

        //ini ToDoListTask 2
        public static readonly ToDoTask ToDoListTask2 = new ToDoTask
        {
            Id = 2,
            Title = "yLaKxaQAuNcIvOIPbgBuckNtgJklAfscGGOIvKSdZGizkNVBiaRNCJrNxgdopfzPUzCpzzTRNXVVBOISnjCaNfawOAKPgeouHTek",
            Description = "AzeoVMjXLAGcZCpZGaypEkjjaOikXHHhUUMfvIsobrVdhShRpLEFDtBQQuNHdzzEsZHFpKdxhJNNPGBIgRuMCCdaSwOBvlLcrRkI",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            IsDeleted = false,
            Completed = 50
        };

        //ini ToDoListTask 3
        public static readonly ToDoTask ToDoListTask3 = new ToDoTask
        {
            Id = 3,
            Title = "upviATOTkIRGfQJVlxDcLYyXbUasdehTprvMuRKZApBeKdRrYhFjjPYVrXGgtVpFbxRoVxaUmdbVXTNxDiUwYxkaTZUXKegfAvDN",
            Description = "kxFnCyYKJcLAthdNqVwcCKHxvVbQzlaMupnzfnQawNBCVfHOZAWHZxEapIrhXIKYBzGDteiDEVgpJFcQNMqzOTmwkNKWviDoCcAV",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            IsDeleted = false,
            Completed = 50
        };

        //ini ToDoListTask 4
        public static readonly ToDoTask ToDoListTask4 = new ToDoTask
        {
            Id = 4,
            Title = "VZNEhYrbJjPDDchhXDXyCFCwUgHnTolwlnPLiBLnDpaVLxLdTwPkijjQWRjBREIUyTRPNWzVrkuMfgkXUdUWZBOCDyoRSJWzhFXs",
            Description = "usxoXlpgMgnklvwEPLOhiDhOufZJsrZOXaeIQNgMykFcjPwKanQjAOOrKjurMgoUHZMLQnnCfCjxxEffiPjEbpNILnyhToASwkVJ",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            IsDeleted = false,
            Completed = 50
        };

        //ini ToDoListTask 5
        public static readonly ToDoTask ToDoListTask5 = new ToDoTask
        {
            Id = 5,
            Title = "BDzRaMqMHzUBSwKgLYXbkLqUbGHJWHeazwhTRyFAfhKUITPQMUKJHXepMepTEgpzVtBVPqONSXvCBWMHqrxCxVSbSrHnImvzsKyO",
            Description = "xqkccopShcNimCAaaDYjbEgbwKqXxqwqcwzeizpCRUaJiQGIyorqclesKIfRySUrgWGpCWsaNVSGeWsfEiYJyFNqSHTZTYEzAicM",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            IsDeleted = false,
            Completed = 50
        };

        //ToDoListTaskForNew 
        public static readonly ToDoTask ToDoListTaskForNew = new ToDoTask
        {
            Id = 6,
            Title = "XJOnBNBGEfSaMpImWAcljxtZDXcncYdNTQygBiaMjSkFkjgnwNfQdRkaPpkekOegCEGwrclnZQBRNnpUrprOsMjCfYQMywcVoDDf",
            Description = "rqNPTMcAPmvXPnpVDQSMZpdogcKjJrItXmBVWnMLnLiJgxYPNKYSdHCCNZjqCtqXeCAdmUyQEHZNGTHiDuVzSpPPjyIxDVJuVKfe",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            IsDeleted = false,
            Completed = 50
        };

        //ToDoListTaskForDelete 
        public static readonly ToDoTask ToDoListTaskForDelete = new ToDoTask
        {
            Id = 6,
            Title = "kfcvkKGFsXKyYeSXYDioeXuiekeWkMEaNpeQtiMsCluhCeEhJcmyVtKNktQAgkVfTjkizCepqpmscNgAfwMHaFFTfQfzRMMRQONS",
            Description = "wHfGRlWMQUjXNFNeQDiWeYecVcNAOGJOFIdxZNNfInBrdtDJzodSzlUYdDlpXWyJKUSFbPEDpDNrXmzbZEAadrPUoFUWbYHmorgI",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            IsDeleted = false,
            Completed = 50
        };
    }
}
