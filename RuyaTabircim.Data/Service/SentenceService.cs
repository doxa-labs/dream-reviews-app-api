using System;
using System.Linq;
using MongoDB.Driver;
using RuyaTabircim.Data.Model;
using System.Collections.Generic;
using RuyaTabircim.Data.Service.Interface;
using RuyaTabircim.Data.Repository.Interface;

namespace RuyaTabircim.Data.Service
{
    public class SentenceService : ISentenceService
    {
        ISentenceRepository Repository { get; }
        public SentenceService(ISentenceRepository repository)
        {
            Repository = repository;
        }

        public SentenceSummary GetRandom()
        {
            Random r = new Random();
            List<Sentence> sens = Repository.Find(s => s.IsOk == true).ToList();
            Sentence sen = sens[r.Next(sens.Count)];

            SentenceSummary ss = new SentenceSummary();
            ss.en = sen.En;
            ss.tr = sen.Tr;
            ss.id = sen.Id;

            return ss;
        }

        public SentenceSummary GetForEdit()
        {
            string id = "5a973919cb543a26b09c6463";
            Sentence sen = Repository.First(s => s.Id == id);

            Sentence senNext = Repository.First(s => s.No == sen.No);
            SentenceSummary ss = new SentenceSummary();
            ss.en = senNext.En;
            ss.tr = senNext.Tr;
            ss.id = senNext.Id;

            // update begin
            var filter = Builders<Sentence>.Filter.Where(s => s.Id == id);
            var update = Builders<Sentence>.Update.Set(s => s.No, sen.No + 1);
            Repository.Update(filter, update);
            // update end
            
            return ss;
        }

        public bool Insert(List<Sentence> values)
        {
            Repository.Insert(values);
            return true;
        }

        public bool Edit(SentenceSummary value)
        {
            var filter = Builders<Sentence>.Filter.Where(d => d.Id == value.id);
            var update = Builders<Sentence>.Update
                                               .Set(d => d.Tr, value.tr)
                                               .Set(d => d.En, value.en)
                                               .Set(d => d.IsOk, true);
            return Repository.Update(filter, update);
        }

        public Stats GetStat()
        {
            string id = "5a973919cb543a26b09c6463";
            Sentence sen = Repository.First(s => s.Id == id);

            return new Stats { Done = sen.No, Left = 86376 - sen.No, Total = 86376 };
        }
    }
}
