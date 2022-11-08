using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishBattle.DAL.Entities
{
    public class IrregularVerb
    {
        public int Id { get; private set; }
        public string BaseForm { get; private set; }
        public string PastPrinciple { get; private set; }
        public string Preterit { get; private set; }

        public IrregularVerb(string baseForm, string pastPrinciple, string preterit)
        {
            BaseForm = baseForm;
            PastPrinciple = pastPrinciple;
            Preterit = preterit;
        }
    }
}
