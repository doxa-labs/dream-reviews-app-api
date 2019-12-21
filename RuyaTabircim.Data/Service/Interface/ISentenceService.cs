using RuyaTabircim.Data.Model;
using System.Collections.Generic;

namespace RuyaTabircim.Data.Service.Interface
{
    public interface ISentenceService
    {
        SentenceSummary GetRandom();
        bool Insert(List<Sentence> values);
        bool Edit(SentenceSummary value);
        SentenceSummary GetForEdit();
        Stats GetStat();
    }
}
