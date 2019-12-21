using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using MongoDB.Driver;
using RuyaTabircim.Data.Model;
using RuyaTabircim.Data.Repository.Interface;
using RuyaTabircim.Data.Service.Interface;

namespace RuyaTabircim.Data.Service
{
    public class DreamService : IDreamService
    {
        IDreamRepository Repository { get; }
        ISpiritRepository SpiritRepository { get; }
        public DreamService(IDreamRepository repository, ISpiritRepository spiritRepository)
        {
            Repository = repository;
            SpiritRepository = spiritRepository;
        }

        public IEnumerable<Dream> GetBySpiritId(string id)
        {
            return Repository.Collection.Find(d => d.SpiritId == id).Sort(Builders<Dream>.Sort.Descending(d => d.CreatedOn)).ToEnumerable();
        }

        public Dream Get(string id)
        {
            Dream dream = Repository.First(s => s.Id == id);
            if (dream != null && dream.IsWatched == false)
            {
                dream.Reply = "You should watch Ads first.";
            }

            return dream;
        }

        public bool Send(RequestDream value)
        {
            Dream d = new Dream();
            d.IsReplied = false;
            d.Text = value.Text;
            d.SpiritId = value.Id;
            d.Reply = "";

            Repository.Insert(d);
            return true;
        }

        public bool Reply(RequestDream value)
        {
            Dream dd = Get(value.Id);
            SendNotification(dd.SpiritId);

            var filter = Builders<Dream>.Filter.Where(d => d.Id == value.Id);
            var update = Builders<Dream>.Update
                                               .Set(d => d.Reply, value.Text)
                                               .Set(d => d.IsReplied, true);
            return Repository.Update(filter, update);
        }

        public IEnumerable<DreamUser> GetForReply()
        {
         //   return Repository.Collection.Find(d => d.IsReplied == false).Sort(Builders<Dream>.Sort.Descending(d => d.CreatedOn)).ToEnumerable();

            List<DreamUser> list = new List<DreamUser>();
            List<Dream> dreams = Repository.Find(d => d.IsReplied == false).ToList();

            foreach (Dream drm in dreams)
            {
                DreamUser dr = new DreamUser();
                dr.CreatedOn = drm.CreatedOn;
                dr.Id = drm.Id;
                dr.IsReplied = drm.IsReplied;
                dr.IsWatched = drm.IsWatched;
                dr.ModifiedOn = drm.ModifiedOn;
                dr.Reply = drm.Reply;
                dr.SpiritId = drm.SpiritId;
                dr.Text = drm.Text;
                dr.Username = SpiritRepository.Find(s => s.Id == drm.SpiritId).FirstOrDefault().Username;
                list.Add(dr);
            }

            list.Reverse();
            return list;
        }

        public bool Watch(string id, string spiritId)
        {
            var dream = Repository.First(d => d.Id == id && d.SpiritId == spiritId);
            if (dream != null)
            {
                var filter = Builders<Dream>.Filter.Where(d => d.Id == id);
                var update = Builders<Dream>.Update.Set(d => d.IsWatched, true);
                return Repository.Update(filter, update);
            }
            else
            {
                return false;
            }
        }

        private static readonly HttpClient client = new HttpClient();
        public void SendNotification(string id)
        {
            Spirit s = SpiritRepository.Get(id);
            try
            {
                var values = new Dictionary<string, string> {
                                    { "to", s.DeviceToken },
                                    { "title", "Rüya Tabircim" },
                                    { "body", "Rüyanız yorumlandı :)" }};

                var content = new FormUrlEncodedContent(values);
                var response = client.PostAsync("https://exp.host/--/api/v2/push/send", content);
                //var responseString = response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public IEnumerable<DreamUser> GetAll()
        {
            List<DreamUser> list = new List<DreamUser>();
            List<Dream> dreams = Repository.FindAll().ToList();

            foreach (Dream drm in dreams)
            {
                DreamUser dr = new DreamUser();
                dr.CreatedOn = drm.CreatedOn;
                dr.Id = drm.Id;
                dr.IsReplied = drm.IsReplied;
                dr.IsWatched = drm.IsWatched;
                dr.ModifiedOn = drm.ModifiedOn;
                dr.Reply = drm.Reply;
                dr.SpiritId = drm.SpiritId;
                dr.Text = drm.Text;
                dr.Username = SpiritRepository.Find(s => s.Id == drm.SpiritId).FirstOrDefault().Username;
                list.Add(dr);
            }

            list.Reverse();
            return list;
        }
    }
}
