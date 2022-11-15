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
        public string PastParticiple { get; private set; }
        public string PastSimple { get; private set; }

        public IrregularVerb(string baseForm, string pastParticiple, string pastSimple)
        {
            BaseForm = baseForm;
            PastParticiple = pastParticiple;
            PastSimple = pastSimple;
        }
    }
}
